using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoggingTest.Models;

namespace LoggingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }         // GET api/values        

        public IActionResult Index()
        {
            return View();
        }        
        
        [HttpGet]
        public ActionResult<string> Get()
        {
            return _userService.GetUserName();
        }
    }
}
