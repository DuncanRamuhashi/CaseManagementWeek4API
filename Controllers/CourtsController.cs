using CaseManagementWeek4API.DTOs.CourtDTO;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace CaseManagementWeek4API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourtsController : Controller
    {
        private ICourt CourtService =>
            HttpContext.RequestServices.GetRequiredService<ICourt>();

        // GET: api/Courts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courts = await CourtService.Index();

            return Ok(courts);
        }

        // GET: api/Courts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var court = await CourtService.Details(id);

            if (court == null)
                return NotFound("Court not found.");

            return Ok(court);
        }

        // GET: api/Courts/create
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return Ok(await CourtService.Create());
        }

        // POST: api/Courts
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourtDto court)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCourt = await CourtService.Create(court);

            if (createdCourt == null)
                return BadRequest("Unable to create court.");

            return CreatedAtAction(
                nameof(Details),
                new { id = createdCourt.Id },
                createdCourt);
        }

        // GET: api/Courts/5/edit
        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var court = await CourtService.Edit(id);

            if (court == null)
                return NotFound("Court not found.");

            return Ok(court);
        }

        // PUT: api/Courts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(
            int id,
            UpdateCourtDto court)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCourt = await CourtService.Edit(id, court);

            if (updatedCourt == null)
                return NotFound("Court not found.");

            return Ok(updatedCourt);
        }

        // GET: api/Courts/5/delete
        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var court = await CourtService.Delete(id);

            if (court == null)
                return NotFound("Court not found.");

            return Ok(court);
        }

        // DELETE: api/Courts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await CourtService.DeleteConfirmed(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
