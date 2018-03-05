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
            ViewBag.Specialty = Specialty.GetAllSpecialties();
            
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
            else if (Request.Form["person"] == "specialty")
            {
                Specialty newSpecialty = new Specialty(Request.Form["name"],0,true);
            }
            else {
                return RedirectToAction("PostNewPerson");
            }
            return RedirectToAction("Index");
        }

    }
}
