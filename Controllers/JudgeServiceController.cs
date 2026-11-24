using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Controllers
{
    public class JudgeServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
