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
        [HttpGet("/client/{id}")]
        public IActionResult ViewClient(int id)
        {

            
            return View("Details",Client.FindClient(id));
        }
        [HttpGet("/stylist/{id}")]
        public IActionResult ViewStylist(int id)
        {
            
            return View("Details",Stylist.FindStylist(id));
        }

        public IActionResult ViewStylist()
        {


            return View(Stylist.GetAllStylist());
        }
    }
}
