using Lab2.Pre.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Pre.Controllers
{
    public class TestDBController : Controller
    {
        public IActionResult Index()
        {
            PrenMetoder prenMetoder = new PrenMetoder();
            List<Pren> p = new List<Pren>();

            p = prenMetoder.SelectPrenLista(out string error);
            return View(p);
        }
    }
}
