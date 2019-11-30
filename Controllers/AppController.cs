using System;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController: Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Detuch Treat";

            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact US";
            //throw new InvalidOperationException("Bad things happend");
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid){
                //send email
                _mailService.SendMessage("vlad@gmail.com", model.Subject, model.Message);
                ViewBag.UserMessage = "Message sent";
                ModelState.Clear();
                Console.WriteLine(model);
            }
            else
            {
                //show errors
            }
            
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
    }
}