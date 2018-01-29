using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Natale.Classes;
using Natale.Models;
using NataleMongoDB = Natale.Classes.MongoDB;

namespace Natale.Controllers
{
    public class ToysController : Controller
    {
        // GET: Toys
        public ActionResult Index()
        {
            NataleMongoDB db = new NataleMongoDB();
            var toys = db.GetAllToys();
            Toys model = new Toys();
            model.EntityList = toys.ToList();
            
            return View(model);
        }
    }
}