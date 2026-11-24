using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.ChargeDTO;
using CaseManagementWeek4API.DTOs.CourtCaseDTO;
using CaseManagementWeek4API.Generic;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaseManagementWeek4API.Services
{
    public class CourtCaseService : ICourtCase
    {
        private readonly CourtCaseDatabaseContext Database;

        public CourtCaseService(IHttpContextAccessor httpContextAccessor)
        {
            Database = httpContextAccessor.HttpContext!
                .RequestServices
                .GetRequiredService<CourtCaseDatabaseContext>();
        }

        // Convert DateOnly? to DateTime?
        private static DateTime? ConvertToDateTime(DateOnly? date)
        {
            return date.HasValue
                ? date.Value.ToDateTime(TimeOnly.MinValue)
                : null;
        }

        // Convert DateTime? to DateOnly?
        private static DateOnly? ConvertToDateOnly(DateTime? date)
        {
            return date.HasValue
                ? DateOnly.FromDateTime(date.Value)
                : null;
        }

        // Convert string ID to int?
        private static int? ConvertToInt(string? value)
        {
            return int.TryParse(value, out int result)
                ? result
                : null;
        }

        // Convert int? to string
        private static string? ConvertToString(int? value)
        {
            return value?.ToString();
        }

        // Maps CourtCase entity to CourtCaseDto
        private static CourtCaseDto MapCourtCase(CourtCase courtCase)
        {
            return new CourtCaseDto
            {
                Id = courtCase.Id,
                OriginalCaseId = courtCase.OriginalCaseId,
                CaseNumber = courtCase.CaseNumber,
                Status = courtCase.Status,
                CourtId = (int)courtCase.CourtId,

                JudgeId = ConvertToInt(courtCase.JudgeId),
                ProsecutorId = ConvertToInt(courtCase.ProsecutorId),

                FilingDate = ConvertToDateTime(courtCase.FilingDate),
                NextHearingDate = courtCase.NextHearingDate,
                ClosedDate = ConvertToDateTime(courtCase.ClosedDate),

                Outcome = courtCase.Outcome,
                Notes = courtCase.Notes,
                IsConfidential = courtCase.IsConfidential,
                CreatedAt = courtCase.CreatedAt,
                UpdatedAt = courtCase.UpdatedAt
            };
        }

        // GET: api/CourtCases
        public async Task<List<CourtCaseDto>> Index()
        {
            var courtCases = await Database.CourtCases
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return courtCases.Select(MapCourtCase).ToList();
        }

        // GET: api/CourtCases/manage/5
        public async Task<object?> Manage(int originalCaseId)
        {
            var originalCase = await Database.OriginalCases
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == originalCaseId);

            if (originalCase == null)
                return null;

            var courtCase = await Database.CourtCases
                .AsNoTracking()
                .Include(c => c.Charges)
                .Include(c => c.CourtCaseDefendants)
                    .ThenInclude(cd => cd.Defendant)
                .FirstOrDefaultAsync(c =>
                    c.OriginalCaseId == originalCaseId);

            return new
            {
                OriginalCaseId = originalCase.Id,

                CourtCase = courtCase == null
                    ? null
                    : MapCourtCase(courtCase),

                Charges = courtCase == null
                    ? new List<ChargeDto>()
                    : courtCase.Charges.Select(c => new ChargeDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CourtCaseId = c.CourtCaseId,
                        ChargeCode = c.ChargeCode,
                        Statute = c.Statute,
                        Description = c.Description,
                        Plea = c.Plea,
                        Severity = c.Severity
                    }).ToList(),

                Defendants = courtCase == null
                    ? new List<object>()
                    : courtCase.CourtCaseDefendants.Select(cd =>
                        (object)new
                        {
                            DefendantId = cd.DefendantId,
                            DefendantName = cd.Defendant.Name,
                            cd.Role,
                            cd.IsPrimary
                        }).ToList()
            };
        }

        // GET: api/CourtCases/create/5
        public async Task<object?> Create(int originalCaseId)
        {
            var originalCase = await Database.OriginalCases
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == originalCaseId);

            if (originalCase == null)
                return null;

            var exists = await Database.CourtCases
                .AnyAsync(c => c.OriginalCaseId == originalCaseId);

            return new
            {
                OriginalCaseId = originalCase.Id,
                CourtCaseAlreadyExists = exists,
                Message = exists
                    ? "A court case already exists for this original case."
                    : "Original case is valid. You can create a court case."
            };
        }

        // POST: api/CourtCases
        public async Task<CourtCaseDto?> Create(
            CreateCourtCaseDto courtCase)
        {
            var originalCase = await Database.OriginalCases
                .FindAsync(courtCase.OriginalCaseId);

            if (originalCase == null)
                return null;

            var alreadyExists = await Database.CourtCases
                .AnyAsync(c =>
                    c.OriginalCaseId == courtCase.OriginalCaseId);

            if (alreadyExists)
                return null;

            var courtExists = await Database.Courts
                .AnyAsync(c =>
                    c.Id == courtCase.CourtId && c.IsActive);

            if (!courtExists)
                return null;

            var newCourtCase = new CourtCase
            {
                OriginalCaseId = courtCase.OriginalCaseId,
                CourtId = courtCase.CourtId,

                JudgeId = ConvertToString(courtCase.JudgeId),
                ProsecutorId = ConvertToString(courtCase.ProsecutorId),

                CaseNumber = courtCase.CaseNumber,
                Status = courtCase.Status,

                FilingDate = ConvertToDateOnly(courtCase.FilingDate),
                NextHearingDate = courtCase.NextHearingDate,
                ClosedDate = ConvertToDateOnly(courtCase.ClosedDate),

                Outcome = courtCase.Outcome,
                Notes = courtCase.Notes,
                IsConfidential = courtCase.IsConfidential,
                CreatedAt = DateTime.UtcNow
            };

            Database.CourtCases.Add(newCourtCase);

            await Database.SaveChangesAsync();

            return MapCourtCase(newCourtCase);
        }

        // GET: api/CourtCases/5/edit
        public async Task<CourtCaseDto?> Edit(int id)
        {
            var courtCase = await Database.CourtCases
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courtCase == null)
                return null;

            return MapCourtCase(courtCase);
        }

        // PUT: api/CourtCases/5
        public async Task<CourtCaseDto?> Edit(
            int id,
            UpdateCourtCaseDto courtCase)
        {
            var existing = await Database.CourtCases
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existing == null)
                return null;

            var courtExists = await Database.Courts
                .AnyAsync(c =>
                    c.Id == courtCase.CourtId && c.IsActive);

            if (!courtExists)
                return null;

            existing.Status = courtCase.Status;
            existing.CourtId = courtCase.CourtId;

            existing.JudgeId = ConvertToString(courtCase.JudgeId);
            existing.ProsecutorId = ConvertToString(courtCase.ProsecutorId);

            existing.CaseNumber = courtCase.CaseNumber;

            existing.FilingDate = ConvertToDateOnly(courtCase.FilingDate);
            existing.NextHearingDate = courtCase.NextHearingDate;
            existing.ClosedDate = ConvertToDateOnly(courtCase.ClosedDate);

            existing.Outcome = courtCase.Outcome;
            existing.Notes = courtCase.Notes;
            existing.IsConfidential = courtCase.IsConfidential;
            existing.UpdatedAt = DateTime.UtcNow;

            await Database.SaveChangesAsync();

            return MapCourtCase(existing);
        }

        // GET: api/CourtCases/5/delete
        public async Task<CourtCaseDto?> Delete(int id)
        {
            var courtCase = await Database.CourtCases
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courtCase == null)
                return null;

            return MapCourtCase(courtCase);
        }

        // DELETE: api/CourtCases/5
        public async Task<bool> DeleteConfirmed(int id)
        {
            var courtCase = await Database.CourtCases
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courtCase == null)
                return false;

            Database.CourtCases.Remove(courtCase);

            await Database.SaveChangesAsync();

            return true;
        }

        // GET: api/CourtCases/5/defendants/available
        public async Task<object?> AddDefendant(int courtCaseId)
        {
            var courtCase = await Database.CourtCases
                .AsNoTracking()
                .Include(c => c.CourtCaseDefendants)
                .FirstOrDefaultAsync(c => c.Id == courtCaseId);

            if (courtCase == null)
                return null;

            var existingIds = courtCase.CourtCaseDefendants
                .Select(cd => cd.DefendantId)
                .ToList();

            var availableDefendants = await Database.Defendants
                .AsNoTracking()
                .Where(d => !existingIds.Contains(d.Id))
                .OrderBy(d => d.Name)
                .Select(d => new
                {
                    DefendantId = d.Id,
                    DefendantName = d.Name
                })
                .ToListAsync();

            return new
            {
                CourtCaseId = courtCase.Id,
                OriginalCaseId = courtCase.OriginalCaseId,
                AvailableDefendants = availableDefendants
            };
        }

        // POST: api/CourtCases/defendants
        public async Task<(bool Success, string Message)> AddDefendant(
            AddCourtCaseDefendantDto defendant)
        {
            var courtCase = await Database.CourtCases
                .FindAsync(defendant.CourtCaseId);

            if (courtCase == null)
                return (false, "Court case not found.");

            var defendantExists = await Database.Defendants
                .AnyAsync(d => d.Id == defendant.DefendantId);

            if (!defendantExists)
                return (false, "Defendant not found.");

            var alreadyAssigned = await Database.CourtCaseDefendants
                .AnyAsync(cd =>
                    cd.CourtCaseId == defendant.CourtCaseId &&
                    cd.DefendantId == defendant.DefendantId);

            if (alreadyAssigned)
                return (false,
                    "This defendant is already assigned to this case.");

            var relationship = new CourtCaseDefendant
            {
                CourtCaseId = defendant.CourtCaseId,
                DefendantId = defendant.DefendantId,
                Role = string.IsNullOrWhiteSpace(defendant.Role)
                    ? "Accused"
                    : defendant.Role,
                IsPrimary = defendant.IsPrimary
            };

            Database.CourtCaseDefendants.Add(relationship);

            await Database.SaveChangesAsync();

            return (true, "Defendant added to court case successfully.");
        }

        // DELETE: api/CourtCases/defendants
        public async Task<(bool Success, string Message)> RemoveDefendant(
            RemoveCourtCaseDefendantDto defendant)
        {
            var relationship = await Database.CourtCaseDefendants
                .FirstOrDefaultAsync(cd =>
                    cd.CourtCaseId == defendant.CourtCaseId &&
                    cd.DefendantId == defendant.DefendantId);

            if (relationship == null)
                return (false, "Defendant assignment not found.");

            Database.CourtCaseDefendants.Remove(relationship);

            await Database.SaveChangesAsync();

            return (true,
                "Defendant removed from court case. Defendant record preserved.");
        }
    }
    }
