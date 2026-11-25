using CaseManagementWeek4API.DTOs.DefetendantDTO;
using CaseManagementWeek4API.Generic;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefendantServiceController : Controller
    {
        private IDefendant DefendantService =>
    HttpContext.RequestServices.GetRequiredService<IDefendant>();



        // GET: api/Defendants
        [HttpGet]
        public async Task<IActionResult> GetAllDefendants()
        {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var defendants = await DefendantService.GetAllDefendants();

            return Ok(defendants);
            });
        }

        // GET: api/Defendants/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDefendantById(string id)
        {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var defendant = await DefendantService.GetDefendantById(id);

            if (defendant == null)
                return NotFound("Defendant not found.");

            return Ok(defendant);
            });
        }

        // POST: api/Defendants
        [HttpPost]
        public async Task<IActionResult> CreateDefendant(
            [FromBody] CreateDefendantDto dto)
        {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await DefendantService.DefendantExists(dto.Id))
                return Conflict("This Defendant ID already exists.");

            var result = await DefendantService.CreateDefendant(dto);

            if (!result)
                return BadRequest("Unable to register defendant.");

            return Ok("Defendant registered successfully.");
            });
        }

        // PUT: api/Defendants/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDefendant(
            string id,
            [FromBody] UpdateDefendantDto dto)
        {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                if (id != dto.Id)
                return BadRequest("Defendant ID does not match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await DefendantService.DefendantExists(id))
                return NotFound("Defendant not found.");

            var result = await DefendantService.UpdateDefendant(id, dto);

            if (!result)
                return BadRequest("Unable to update defendant.");

            return Ok("Defendant updated successfully.");
            });
        }

        // DELETE: api/Defendants/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDefendant(string id)
        {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                if (!await DefendantService.DefendantExists(id))
                return NotFound("Defendant not found.");

            if (await DefendantService.HasCourtCaseAssignments(id))
            {
                return Conflict(
                    "Cannot delete a defendant who is assigned to court cases. Remove assignments first.");
            }

            var result = await DefendantService.DeleteDefendant(id);

            if (!result)
                return BadRequest("Unable to delete defendant.");

            return Ok("Defendant deleted successfully.");
            });
        }
    }
}
