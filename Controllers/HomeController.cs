using CaseManagementWeek4API.Generic;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CaseManagementWeek4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private IDashboard DashboardService =>
            HttpContext.RequestServices
                .GetRequiredService<IDashboard>();

        // GET: api/Home
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var dashboard = await DashboardService.Index();

                return Ok(dashboard);
            });
        }
    }
}