using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.ChargeDTO;
using CaseManagementWeek4API.Generic;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseManagementWeek4API.Services
{
    public class ChargeService : ICharge
    {
 
            private readonly CourtCaseDatabaseContext Database;

            public ChargeService(IHttpContextAccessor httpContextAccessor)
            {
                Database = httpContextAccessor.HttpContext!
                    .RequestServices
                    .GetRequiredService<CourtCaseDatabaseContext>();
            }

            // GET: api/Charges
            // GET: api/Charges?courtCaseId=5
            public async Task<List<ChargeDto>> Index(int? courtCaseId)
            {
                var query = Database.Charges
                    .AsNoTracking()
                    .AsQueryable();

                if (courtCaseId.HasValue)
                {
                    query = query.Where(c =>
                        c.CourtCaseId == courtCaseId.Value);
                }

                return await query
                    .OrderBy(c => c.Name)
                    .Select(c => new ChargeDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CourtCaseId = c.CourtCaseId,
                        ChargeCode = c.ChargeCode,
                        Statute = c.Statute,
                        Description = c.Description,
                        Plea = c.Plea,
                        Severity = c.Severity
                    })
                    .ToListAsync();
            }

            // GET: api/Charges/5
            public async Task<ChargeDto?> Details(int id)
            {
                var charge = await Database.Charges
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (charge == null)
                    return null;

                return new ChargeDto
                {
                    Id = charge.Id,
                    Name = charge.Name,
                    CourtCaseId = charge.CourtCaseId,
                    ChargeCode = charge.ChargeCode,
                    Statute = charge.Statute,
                    Description = charge.Description,
                    Plea = charge.Plea,
                    Severity = charge.Severity
                };
            }

            // Check whether the associated court case exists
            public async Task<bool> Create(int courtCaseId)
            {
                if (courtCaseId <= 0)
                    return false;

                return await Database.CourtCases
                    .AnyAsync(c => c.Id == courtCaseId);
            }

            // POST: api/Charges
            public async Task<ChargeDto?> Create(CreateChargeDto charge)
            {
                var courtCaseExists = await Create(charge.CourtCaseId);

                if (!courtCaseExists)
                    return null;

                var newCharge = new Charge
                {
                    Name = charge.Name,
                    CourtCaseId = charge.CourtCaseId,
                    ChargeCode = charge.ChargeCode,
                    Statute = charge.Statute,
                    Description = charge.Description,
                    Plea = charge.Plea,
                    Severity = charge.Severity
                };

                Database.Charges.Add(newCharge);

                await Database.SaveChangesAsync();

                return new ChargeDto
                {
                    Id = newCharge.Id,
                    Name = newCharge.Name,
                    CourtCaseId = newCharge.CourtCaseId,
                    ChargeCode = newCharge.ChargeCode,
                    Statute = newCharge.Statute,
                    Description = newCharge.Description,
                    Plea = newCharge.Plea,
                    Severity = newCharge.Severity
                };
            }

            // GET: api/Charges/5/edit
            public async Task<ChargeDto?> Edit(int id)
            {
                return await Details(id);
            }

            // PUT: api/Charges/5
            public async Task<ChargeDto?> Edit(
                int id,
                UpdateChargeDto charge)
            {
                var existing = await Database.Charges
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (existing == null)
                    return null;

                existing.Name = charge.Name;
                existing.ChargeCode = charge.ChargeCode;
                existing.Statute = charge.Statute;
                existing.Description = charge.Description;
                existing.Plea = charge.Plea;
                existing.Severity = charge.Severity;

                await Database.SaveChangesAsync();

                return new ChargeDto
                {
                    Id = existing.Id,
                    Name = existing.Name,
                    CourtCaseId = existing.CourtCaseId,
                    ChargeCode = existing.ChargeCode,
                    Statute = existing.Statute,
                    Description = existing.Description,
                    Plea = existing.Plea,
                    Severity = existing.Severity
                };
            }

            // GET: api/Charges/5/delete
            public async Task<ChargeDto?> Delete(int id)
            {
                return await Details(id);
            }

            // DELETE: api/Charges/5
            public async Task<bool> DeleteConfirmed(int id)
            {
                var charge = await Database.Charges
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (charge == null)
                    return false;

                Database.Charges.Remove(charge);

                await Database.SaveChangesAsync();

                return true;
            }
        }
    }
