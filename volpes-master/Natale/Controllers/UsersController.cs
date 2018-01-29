﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Natale.Classes;
using System.Security.Cryptography;
using NataleMongoDB = Natale.Classes.MongoDB;
using System.Text;

namespace Natale.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Login()
        {
            return View();
        }

        string Encrypt(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] resultHash;
            SHA512 shaM = new SHA512Managed();
            resultHash = shaM.ComputeHash(data);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < resultHash.Length; i++)
            {
                result.Append(resultHash[i].ToString("X2"));
            }
            return result.ToString().ToLower();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            user.Password = Encrypt(user.Password);
            NataleMongoDB db = new NataleMongoDB();
            var account = db.GetUser(user);
            if (account != null)
            {
                Session["Email"] = account.Email.ToString();
                Session["ID"] = account.ID.ToString();
                Session["ScreenName"] = account.ScreenName.ToString();
                Session["IsAdmin"] = account.isAdmin.ToString();
                return RedirectToAction($"../Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password Error");
                
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["ID"] != null)
            {
                Session.Clear();
                return RedirectToAction("Logout");
            }
            return View();
        }

    }
}