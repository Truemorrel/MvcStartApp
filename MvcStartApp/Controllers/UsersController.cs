using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models;
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

        public async Task<IActionResult> Register(string FirstName, string LastName)
        {
            if (Request.HasFormContentType)
            {
                WebUser newUser = new()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                };
                await _repo.AddUser(newUser);
            }
            return View();
        }
    }
}
