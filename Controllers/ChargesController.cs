using CaseManagementWeek4API.DTOs.ChargeDTO;
using CaseManagementWeek4API.Generic;
using CaseManagementWeek4API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Controllers
{

    //this is for setting the route
    [Route("api/[controller]")]
      [ApiController]
    public class ChargesController : Controller
    {
                    //the controller will return HTTP responses such as 200 OK, 201 Created, 404 Not Found and 400 Bad Request.
         
              
            private  ICharge ChargeService =>
                     HttpContext.RequestServices
                    .GetRequiredService<ICharge>();

            // GET: api/Charges
            // GET: api/Charges?courtCaseId=5
            [HttpGet]
            public async Task<IActionResult> Index(int? courtCaseId)
            {

               return await ErrorHandler.ExecuteAsync(async () =>
               { 
                  var charges = await ChargeService.Index(courtCaseId);

                  return Ok(charges);
                 });
            }

            // GET: api/Charges/5
            [HttpGet("{id}")]
            public async Task<IActionResult> Details(int id)
            {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var charge = await ChargeService.Details(id);

                if (charge == null)
                    return NotFound("Charge not found.");

                return Ok(charge);
            });
 
           }

        // GET: api/Charges/5/create
        [HttpGet("{courtCaseId}/create")]
            public async Task<IActionResult> Create(int courtCaseId)
            {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var exists = await ChargeService.Create(courtCaseId);

                if (!exists)
                    return NotFound("Court case not found.");

                return Ok(new
                {
                    Message = "Court case is valid. You can create a charge.",
                    CourtCaseId = courtCaseId
                });
            });
            }

            // POST: api/Charges
            [HttpPost]
            public async Task<IActionResult> Create(
                [FromBody] CreateChargeDto charge)
            {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var created = await ChargeService.Create(charge);

                if (created == null)
                    return NotFound("Court case not found.");

                return CreatedAtAction(
                    nameof(Details),
                    new { id = created.Id },
                    created);
            });
             }
 
            // GET: api/Charges/5/edit
            [HttpGet("{id}/edit")]
            public async Task<IActionResult> Edit(int id)
            {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var charge = await ChargeService.Edit(id);

                if (charge == null)
                    return NotFound("Charge not found.");

                return Ok(charge);
            });
            }

            // PUT: api/Charges/5
            [HttpPut("{id}")]
            public async Task<IActionResult> Edit(
                int id,
                [FromBody] UpdateChargeDto charge)
            {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var updated = await ChargeService.Edit(id, charge);

                if (updated == null)
                    return NotFound("Charge not found.");

                return Ok(updated);
            });
            }

            // GET: api/Charges/5/delete
            [HttpGet("{id}/delete")]
            public async Task<IActionResult> Delete(int id)
            {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var charge = await ChargeService.Delete(id);

                if (charge == null)
                    return NotFound("Charge not found.");

                return Ok(charge);
            });
            }

            // DELETE: api/Charges/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
            return await ErrorHandler.ExecuteAsync(async () =>
            {
                var deleted = await ChargeService.DeleteConfirmed(id);

                if (!deleted)
                    return NotFound("Charge not found.");

                return Ok(new
                {
                    Message = "Charge deleted successfully."
                });
            });
            }
       }
    
}
