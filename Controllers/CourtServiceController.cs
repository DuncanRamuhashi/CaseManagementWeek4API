using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Controllers
{

    public class CourtServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
