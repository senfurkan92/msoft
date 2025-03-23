using Microsoft.AspNetCore.Mvc;

namespace MeSoftCase.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignOut()
        {
            HttpContext.Response.Cookies.Delete("MeSoftToken");

            return RedirectToAction("Index");
        }
    }
}
