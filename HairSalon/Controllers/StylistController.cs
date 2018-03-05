using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {

        [HttpGet("/stylist/{id}")]
        public IActionResult ViewStylist(int id)
        {
            ViewBag.AllSpecialties = Specialty.GetAllSpecialties();
            return View("Details", Stylist.FindStylist(id));
        }

        [HttpPost("/stylist/{id}/delete")]
        public IActionResult DeleteStylist(int id)
        {
            System.Console.WriteLine("-------------------------------------------------");
            Stylist foundStylist = Stylist.FindStylist(id);
            foundStylist.DeleteThis();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost("/stylist/deleteall")]
        public IActionResult DeleteAllStylists()
        {
            Stylist.DeleteAll();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/stylist/{id}/changename")]
        public IActionResult ChangeStylistName(int id)
        {
            Stylist thisStylist = Stylist.FindStylist(id);
            string result = Request.Form["new-name"];
            thisStylist.ChangeName(result);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost("/stylist/{id}/addspecialty")]
        public IActionResult AddSpecialty(int id)
        {
            Stylist thisStylist = Stylist.FindStylist(id);
            thisStylist.AddSpecialty(Int32.Parse(Request.Form["specialty"]));
            return RedirectToAction("Index", "Home");
        }
    }
}