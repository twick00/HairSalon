using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {

        [HttpGet("/specialty/{id}")]
        public IActionResult ViewSpecialty(int id)
        {
            Specialty.FindSpecialty(id);
            return View("Details", Specialty.FindSpecialty(id));
        }
        [HttpPost("/specialty/deleteall")]
        public IActionResult DeleteALl()
        {
            Specialty.DeleteAll();
            return RedirectToAction("Index", "Home");
        }
    }
}

    