using Microsoft.AspNetCore.Mvc;

namespace TestTattooStore.Controllers
{
    public class ArtistaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
