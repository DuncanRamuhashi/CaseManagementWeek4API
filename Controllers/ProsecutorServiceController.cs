using CaseManagementWeek4API.DTOs.ProsecutorDTO;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CaseManagementWeek4API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProsecutorServiceController : Controller
    {
        private IProsecutor ProsecutorService =>
            HttpContext.RequestServices
                .GetRequiredService<IProsecutor>();
        // GET: api/ProsecutorService
        [HttpGet]
        public async Task<IActionResult> GetAllProsecutors()
        {
            var prosecutors =
                await ProsecutorService.GetAllProsecutors();

            return Ok(prosecutors);
        }

        // GET: api/ProsecutorService/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProsecutorById(string id)
        {
            var prosecutor =
                await ProsecutorService.GetProsecutorById(id);

            if (prosecutor == null)
                return NotFound("Prosecutor not found.");

            return Ok(prosecutor);
        }

        // POST: api/ProsecutorService
        [HttpPost]
        public async Task<IActionResult> CreateProsecutor(
            [FromBody] CreateProsecutorDto dto)
        {
            if (await ProsecutorService.ProsecutorExists(dto.Id))
                return Conflict("This Prosecutor ID already exists.");

            var result =
                await ProsecutorService.CreateProsecutor(dto);

            if (!result)
                return BadRequest("Unable to register prosecutor.");

            return CreatedAtAction(
                nameof(GetProsecutorById),
                new { id = dto.Id },
                dto);
        }

        // PUT: api/ProsecutorService/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProsecutor(
            string id,
            [FromBody] UpdateProsecutorDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Prosecutor ID does not match.");

            if (!await ProsecutorService.ProsecutorExists(id))
                return NotFound("Prosecutor not found.");

            var result =
                await ProsecutorService.UpdateProsecutor(id, dto);

            if (!result)
                return BadRequest("Unable to update prosecutor.");

            return Ok("Prosecutor updated successfully.");
        }

        // DELETE: api/ProsecutorService/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProsecutor(string id)
        {
            if (!await ProsecutorService.ProsecutorExists(id))
                return NotFound("Prosecutor not found.");

            if (await ProsecutorService.HasCourtCaseAssignments(id))
            {
                return Conflict(
                    "Cannot delete a prosecutor assigned to court cases. Deactivate instead.");
            }

            var result =
                await ProsecutorService.DeleteProsecutor(id);

            if (!result)
                return BadRequest("Unable to delete prosecutor.");

            return Ok("Prosecutor deleted successfully.");
        }
    }
}
