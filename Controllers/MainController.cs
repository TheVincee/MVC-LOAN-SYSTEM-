using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JJK.Entities;

namespace JJK.Controllers
{

    public class MainController : Controller
    {

        private readonly UrsalcoopContext _context;
        public MainController(UrsalcoopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var client = _context.ClientsInfoTbs.ToList();
            return View(client);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var client = new ClientsInfoTb(); // Create a new instance of ClientsInfoTb
            return View(client);
        }


        [HttpPost]
        public IActionResult Create(ClientsInfoTb CreateClient)
        {

            if (!ModelState.IsValid)
                return View(CreateClient);

            _context.ClientsInfoTbs.Add(CreateClient);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(int Id)
        {
            
            var client = _context.ClientsInfoTbs.Where(q => q.Id == Id).FirstOrDefault();
            return View(client);
        }
        [HttpPost]
        public IActionResult Update(ClientsInfoTb b)
        {

            if (!ModelState.IsValid)
                return View(b);

            _context.ClientsInfoTbs.Update(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = _context.ClientsInfoTbs.Where(q => q.Id == id).FirstOrDefault();
            _context.ClientsInfoTbs.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GetClients()
        {
            var clients = _context.ClientsInfoTbs.Select(c => new { Id = c.Id, Name = c.LastName }).ToList();
            return Json(clients);
        }

    }
}