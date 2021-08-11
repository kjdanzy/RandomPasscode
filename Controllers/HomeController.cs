using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RandomPasscode.Models;


namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            //string newPassword = Security.GeneratePassword(14, 10);
            string newPassCode = "";
            string[] characterSet = new string[]{"ABCDEFGHJKLMNOPQRSTUVWXYZ",
            "abcdefghijkmnopqrstuvwxyz",
            "0123456789"};
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int randSelectLevel = rand.Next(0,3);
                int character = rand.Next(0, characterSet[randSelectLevel].Length);
                newPassCode += characterSet[randSelectLevel][character];
            }

            int? currentCount = HttpContext.Session.GetInt32("Counter");
            if (currentCount != null){
                ViewBag.RandomPasscode = newPassCode;
                ViewBag.Count = currentCount;
            }

            ViewBag.CurrentPage = "Home";
            return View();
        }

        [HttpPost]
        public IActionResult Submit()
        {
            return RedirectToAction("Index");
        }

        public IActionResult CountUp()
        {
            int? countVar = HttpContext.Session.GetInt32("Counter");
            if(countVar == null)
            {
                HttpContext.Session.SetInt32("Counter", 1);
            }
            else{
                countVar++;
                HttpContext.Session.SetInt32("Counter", (int) countVar);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
