using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeSoftCase.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Blocked")]
        public IActionResult Blocked()
        {
            return View();
        }
    }
}
