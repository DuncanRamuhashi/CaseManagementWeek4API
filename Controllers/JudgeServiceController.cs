using CaseManagementWeek4API.DTOs.JudgeDTO;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace CaseManagementWeek4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgeServiceController : Controller
    {
        private IJudge JudgeService =>
                  HttpContext.RequestServices
                      .GetRequiredService<IJudge>();

        // GET: api/JudgeService
        [HttpGet]
        public async Task<IActionResult> GetAllJudges()
        {
            var judges = await JudgeService.GetAllJudges();

            return Ok(judges);
        }

        // GET: api/JudgeService/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJudgeById(string id)
        {
            var judge = await JudgeService.GetJudgeById(id);

            if (judge == null)
                return NotFound("Judge not found.");

            return Ok(judge);
        }

        // POST: api/JudgeService
        [HttpPost]
        public async Task<IActionResult> CreateJudge(
            [FromBody] CreateJudgeDto dto)
        {
            if (await JudgeService.JudgeExists(dto.Id))
                return Conflict("This Judge ID already exists.");

            var created = await JudgeService.CreateJudge(dto);

            if (!created)
                return BadRequest("Unable to create judge. Check that the Court ID exists.");

            return Ok("Judge registered successfully.");
        }

        // PUT: api/JudgeService/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJudge(
            string id,
            [FromBody] UpdateJudgeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Judge ID does not match.");

            var updated = await JudgeService.UpdateJudge(id, dto);

            if (!updated)
                return BadRequest("Unable to update judge. Check the Judge ID and Court ID.");

            return Ok("Judge updated successfully.");
        }

        // DELETE: api/JudgeService/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJudge(string id)
        {
            if (!await JudgeService.JudgeExists(id))
                return NotFound("Judge not found.");

            if (await JudgeService.HasCourtCaseAssignments(id))
                return Conflict("Cannot delete a judge assigned to court cases. Deactivate instead.");

            var deleted = await JudgeService.DeleteJudge(id);

            if (!deleted)
                return BadRequest("Unable to delete judge.");

            return Ok("Judge deleted successfully.");
        }
    }
}
