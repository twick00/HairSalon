using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {

        [HttpGet("/client/{id}")]
        public IActionResult ViewClient(int id)
        {
            ViewBag.Stylists = Stylist.GetAllStylist();
            
            return View("Details",Client.FindClient(id));
        }

        [HttpPost("/client/{id}/delete")]
        public IActionResult DeleteClient(int id)
        {
            Client foundClient = Client.FindClient(id);
            foundClient.DeleteThis();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/client/editstylist/{id}")]
        public IActionResult EditClientStylist(int id)
        {
            if (string.IsNullOrEmpty(Request.Form["new-stylist"]))
            {
                return View("Index");
            }
            int newStylistId = Int32.Parse(Request.Form["new-stylist"]);
            Client.ChangeThisStylist(id, newStylistId);
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost("/client/deleteall")]
        public IActionResult DeleteAllClients()
        {
            Client.DeleteAll();
            return RedirectToAction("Index", "Home");
        }

    }
}