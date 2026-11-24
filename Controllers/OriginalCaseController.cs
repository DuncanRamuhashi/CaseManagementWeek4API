using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Controllers
{
    public class OriginalCaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
