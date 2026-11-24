using CaseManagementWeek4API.DTOs.CourtCaseDTO;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Controllers
{
    
    
        [Route("api/[controller]")]
        [ApiController]
        public class CourtCasesController : ControllerBase
        {
            private ICourtCase CourtCaseService =>
                HttpContext.RequestServices
                    .GetRequiredService<ICourtCase>();

            // GET: api/CourtCases
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var courtCases = await CourtCaseService.Index();

                return Ok(courtCases);
            }

            // GET: api/CourtCases/manage/5
            [HttpGet("manage/{originalCaseId}")]
            public async Task<IActionResult> Manage(int originalCaseId)
            {
                var result = await CourtCaseService.Manage(originalCaseId);

                if (result == null)
                    return NotFound("Original case not found.");

                return Ok(result);
            }

            // GET: api/CourtCases/create/5
            [HttpGet("create/{originalCaseId}")]
            public async Task<IActionResult> Create(int originalCaseId)
            {
                var result = await CourtCaseService.Create(originalCaseId);

                if (result == null)
                    return NotFound("Original case not found.");

                return Ok(result);
            }

            // POST: api/CourtCases
            [HttpPost]
            public async Task<IActionResult> Create(
                [FromBody] CreateCourtCaseDto courtCase)
            {
                var created = await CourtCaseService.Create(courtCase);

                if (created == null)
                {
                    return Conflict(
                        "Court case could not be created. Check that the original case exists, no court case already exists for it, and the selected court is active.");
                }

                return CreatedAtAction(
                    nameof(Edit),
                    new { id = created.Id },
                    created);
            }

            // GET: api/CourtCases/5/edit
            [HttpGet("{id}/edit")]
            public async Task<IActionResult> Edit(int id)
            {
                var courtCase = await CourtCaseService.Edit(id);

                if (courtCase == null)
                    return NotFound("Court case not found.");

                return Ok(courtCase);
            }

            // PUT: api/CourtCases/5
            [HttpPut("{id}")]
            public async Task<IActionResult> Edit(
                int id,
                [FromBody] UpdateCourtCaseDto courtCase)
            {
                var updated = await CourtCaseService.Edit(id, courtCase);

                if (updated == null)
                    return BadRequest(
                        "Court case could not be updated. Check that the court case exists and the selected court is active.");

                return Ok(updated);
            }

            // GET: api/CourtCases/5/delete
            [HttpGet("{id}/delete")]
            public async Task<IActionResult> Delete(int id)
            {
                var courtCase = await CourtCaseService.Delete(id);

                if (courtCase == null)
                    return NotFound("Court case not found.");

                return Ok(courtCase);
            }

            // DELETE: api/CourtCases/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var deleted = await CourtCaseService.DeleteConfirmed(id);

                if (!deleted)
                    return NotFound("Court case not found.");

                return Ok(new
                {
                    Message = "Court case deleted successfully."
                });
            }

            // GET: api/CourtCases/5/defendants/available
            [HttpGet("{courtCaseId}/defendants/available")]
            public async Task<IActionResult> AddDefendant(int courtCaseId)
            {
                var result = await CourtCaseService.AddDefendant(courtCaseId);

                if (result == null)
                    return NotFound("Court case not found.");

                return Ok(result);
            }

            // POST: api/CourtCases/defendants
            [HttpPost("defendants")]
            public async Task<IActionResult> AddDefendant(
                [FromBody] AddCourtCaseDefendantDto defendant)
            {
                var result = await CourtCaseService.AddDefendant(defendant);

                if (!result.Success)
                {
                    if (result.Message.Contains("not found"))
                        return NotFound(new { result.Message });

                    return Conflict(new { result.Message });
                }

                return Ok(new
                {
                    result.Message
                });
            }

            // DELETE: api/CourtCases/defendants
            [HttpDelete("defendants")]
            public async Task<IActionResult> RemoveDefendant(
                [FromBody] RemoveCourtCaseDefendantDto defendant)
            {
                var result = await CourtCaseService.RemoveDefendant(defendant);

                if (!result.Success)
                    return NotFound(new { result.Message });

                return Ok(new
                {
                    result.Message
                });
            }
        }
    }

