using Microsoft.AspNetCore.Mvc;

namespace PrefinalsWebSys1.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
