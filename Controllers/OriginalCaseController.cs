
using CaseManagementWeek4API.DTOs.OriginalCaseDTO;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CaseManagementWeek4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginalCaseController : Controller
    {
        private IOriginalCase OriginalCaseService =>
                   HttpContext.RequestServices
                       .GetRequiredService<IOriginalCase>();

        // GET: api/OriginalCase
        [HttpGet]
        public async Task<IActionResult> GetAllOriginalCases()
        {
            var cases = await OriginalCaseService.GetAllOriginalCases();

            return Ok(cases);
        }

        // GET: api/OriginalCase/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOriginalCaseById(int id)
        {
            var originalCase =
                await OriginalCaseService.GetOriginalCaseById(id);

            if (originalCase == null)
                return NotFound("Original case not found.");

            return Ok(originalCase);
        }

        // POST: api/OriginalCase
        [HttpPost]
        public async Task<IActionResult> CreateOriginalCase(
            [FromBody] CreateOriginalCaseDto dto)
        {
            if (await OriginalCaseService.CaseNumberExists(dto.CaseNumber))
                return Conflict("This case number already exists.");

            var created =
                await OriginalCaseService.CreateOriginalCase(dto);

            if (!created)
                return BadRequest("Unable to register original case.");

            return Ok("Original case registered successfully.");
        }

        // PUT: api/OriginalCase/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOriginalCase(
            int id,
            [FromBody] UpdateOriginalCaseDto dto)
        {
            if (!await OriginalCaseService.OriginalCaseExists(id))
                return NotFound("Original case not found.");

            if (await OriginalCaseService.CaseNumberExistsForOtherCase(
                dto.CaseNumber, id))
            {
                return Conflict("This case number already exists.");
            }

            var updated =
                await OriginalCaseService.UpdateOriginalCase(id, dto);

            if (!updated)
                return BadRequest("Unable to update original case.");

            return Ok("Original case updated successfully.");
        }

        // DELETE: api/OriginalCase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOriginalCase(int id)
        {
            if (!await OriginalCaseService.OriginalCaseExists(id))
                return NotFound("Original case not found.");

            if (await OriginalCaseService.HasCourtCaseAssignments(id))
            {
                return Conflict(
                    "Cannot delete an original case that has associated court cases.");
            }

            var deleted =
                await OriginalCaseService.DeleteOriginalCase(id);

            if (!deleted)
                return BadRequest("Unable to delete original case.");

            return Ok("Original case deleted successfully.");
        }
    }
}
