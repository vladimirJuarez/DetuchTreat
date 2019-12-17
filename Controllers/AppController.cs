using System;
using System.Linq;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController: Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;

        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Detuch Treat";
            //var results = _repository.GetAllProducts();

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

        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts()            
            .OrderBy(p => p.Category)
            .ToList();

            return View(results);
        }
    }
}