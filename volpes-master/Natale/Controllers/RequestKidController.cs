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
    public class RequestKidController : Controller
    {
        // GET: RequestKid
        public ActionResult Index()
        {
            NataleMongoDB db = new NataleMongoDB();
            var requests_kids = db.GetAllRequestKid();
            RequestsKids model = new RequestsKids();
            model.EntityList = requests_kids.ToList();
            return View(model);
        }

        public ActionResult Details(string id)
        {
            NataleMongoDB db = new NataleMongoDB();
            var request_kid = db.GetRequest(id);
            RequestsKids model = new RequestsKids();
            ViewBag.Id = request_kid.ID;
            ViewBag.KidName = request_kid.KidName;
            ViewBag.Date = request_kid.Date.ToString("dd-MMM-yyyy");
            switch (request_kid.Status.ToString())
            {
                case "0":
                    ViewBag.Status = "In Progress";
                    break;
                case "1":
                    ViewBag.Status = "Available";
                    break;
                case "2":
                    ViewBag.Status = "Done";
                    break;

                default:
                    break;
            }

            model.ToyList = request_kid.ToyKids;
            
            return View(model);
        }

        
        public ActionResult Edit(string id)
        {
            NataleMongoDB db = new NataleMongoDB();
            var request_kid = db.GetRequest(id);
            //utile per estrarre tutti i giochi richiesto dal bambino
            RequestsKids modelToy = new RequestsKids();
            modelToy.ToyList = request_kid.ToyKids;
            Toy toy = new Toy();
            //utile per passare alla view lo stato dell ordine
            RequestKid model = new RequestKid();
            model.Status = request_kid.Status;
            return View(model);
        }

        public ActionResult Save(int status,string id)
        {
            if (string.IsNullOrWhiteSpace(status.ToString()))
            {
                throw new MissingFieldException("name cannot be null");
            }
            bool result;
            NataleMongoDB db = new NataleMongoDB();
            var request_kid = db.GetRequest(id);
            RequestsKids modelToy = new RequestsKids();
            modelToy.ToyList = request_kid.ToyKids;
            Toy toy = new Toy();
            var query = modelToy.ToyList.GroupBy(x => x)
                                .Select(y => new { Element = y.Key, Counter = y.Count() })
                                .ToList();
            foreach (var toyRequest in query)
            {
                toy = db.GetToy(toyRequest.Element.ToyName);
                if (toy.Amount <= toyRequest.Counter)
                {
                    ModelState.AddModelError("", "Order no Avaible");
                    return RedirectToAction("Details", id);
                }
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                RequestKid requestkid = new RequestKid();
                
            }
            
            result = db.UpdateStatus(new RequestKid
            {
                ID = id,
                Status = status
            });
            

            foreach (var toyRequest in modelToy.ToyList)
            {
                
                toy = db.GetToy(toyRequest.ToyName);
                result = db.UpdateAmountToy(toy);
                if (toy.Amount == 0)
                {
                    db.RemoveToy(toy.ID);
                }
            }
            
            return RedirectToAction("Index", new { result = result });
        }
    }
}