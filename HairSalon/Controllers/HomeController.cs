using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {

            ViewBag.Clients = Client.GetAllClients();
            ViewBag.Stylists = Stylist.GetAllStylist();
            
            return View();
        }
        [HttpGet("/addnew")]
        public IActionResult NewPerson()
        {

            ViewBag.Stylists = Stylist.GetAllStylist();
            return View();
        }
        [HttpPost("/addnew")]
        public IActionResult PostNewPerson()
        {
            if (Request.Form["person"] == "stylist")
            {
                Stylist newStylist = new Stylist(Request.Form["name"],0,true);
            }
            else if (Request.Form["person"] == "client" && Stylist.GetAllStylist().Count > 0)
            {
                Client newClient = new Client(Request.Form["name"],0,true);
                newClient.NewClientStylistRel(Int32.Parse(Request.Form["stylist"]));
            }
            else {
                return RedirectToAction("PostNewPerson");
            }
            return RedirectToAction("Index");
        }
        [HttpGet("/client/{id}")]
        public IActionResult ViewClient(int id)
        {
            ViewBag.Stylists = Stylist.GetAllStylist();
            
            return View("Details",Client.FindClient(id));
        }
        [HttpGet("/stylist/{id}")]
        public IActionResult ViewStylist(int id)
        {
            
            return View("Details",Stylist.FindStylist(id));
        }
        [HttpPost("/client/editstylist/{id}")]
        public IActionResult EditClientStylist(int id)
        {
            // if (string.IsNullOrEmpty(Request.Form["new-stylist"]))
            // {
            //     return View("Index");
            // }
            int newStylistId = Int32.Parse(Request.Form["new-stylist"]);
            Client.ChangeThisStylist(id, newStylistId);
            return RedirectToAction("Index");
        }
    }
}
