﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private WorldContext _context;

        public AppController(IMailService service, WorldContext context)
        {
            _mailService = service;
            _context = context;
        }
        //will contain method that will be called based on pattern of requests coming in
        public IActionResult Index()
        {
            var trips = _context.Trips.OrderBy(t => t.Name).ToList();
            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];


                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration problem.");
                }

                if (_mailService.SendMail(email,
                    email,
                    $"Contact Page from {model.Name} ({model.Email})",
                    model.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Sent.  Thanks!";
                }
            }

            return View();
        }


    }
}
