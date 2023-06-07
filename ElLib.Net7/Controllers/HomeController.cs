using ElLib.Net7.Domain;
using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElLib.Net7.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<TextField> textFieldRepository;
        public HomeController(IRepository<TextField> textFieldRepository)
        {
            this.textFieldRepository = textFieldRepository;
        }
        public IActionResult Index()
        {
            return View(textFieldRepository.GetByCodeWord("PageIndex"));
        }
        public IActionResult Contacts()
        {
            return View(textFieldRepository.GetByCodeWord("PageContacts"));
        }
        public IActionResult MobileHeader()
        {
            return View();
        }
    }
}
