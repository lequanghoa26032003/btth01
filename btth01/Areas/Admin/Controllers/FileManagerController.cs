using btth01.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace btth01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/file-manager")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            return View();
        }
    }
}
