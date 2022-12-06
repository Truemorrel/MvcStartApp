using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models.DB;
using System;
using System.Threading.Tasks;

namespace MvcStartApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBlogRepository _repo;

        public UsersController(IBlogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Register(WebUser newUser)
		{
			await _repo.AddUser(newUser);
			return View(newUser);
		}
		//public async Task<IActionResult> Register(string FirstName, string LastName)
		//      {
		//          if (Request.HasFormContentType)
		//          {
		//              WebUser newUser = new()
		//              {
		//                  FirstName = FirstName,
		//                  LastName = LastName,
		//              };
		//              await _repo.AddUser(newUser);
		//          }
		//          return View();
		//      }
	}
}
