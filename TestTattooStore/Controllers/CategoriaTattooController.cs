using Microsoft.AspNetCore.Mvc;

namespace TestTattooStore.Controllers
{
    public class CategoriaTattooController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
