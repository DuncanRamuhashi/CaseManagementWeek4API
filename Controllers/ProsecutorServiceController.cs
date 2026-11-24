using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Controllers
{
    public class ProsecutorServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
