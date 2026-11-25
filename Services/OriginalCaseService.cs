
using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.OriginalCaseDTO;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseManagementWeek4API.Services
{
    public class OriginalCaseService : IOriginalCase
    {
        private readonly CourtCaseDatabaseContext _context;

        public OriginalCaseService(CourtCaseDatabaseContext context)
        {
            _context = context;
        }

        // GET: Get all original cases
        public async Task<List<OriginalCaseDto>> GetAllOriginalCases()
        {
            return await _context.OriginalCases
                .AsNoTracking()
                .OrderByDescending(c => c.Date)
                .Select(c => new OriginalCaseDto
                {
                    Id = c.Id,
                    CaseNumber = c.CaseNumber,
                    Name = c.Name,
                    Date = c.Date,
                    Description = c.Description,
                    CaseType = c.CaseType,
                    Priority = c.Priority,
                    ReportedBy = c.ReportedBy
                })
                .ToListAsync();
        }

        // GET: Get original case by ID
        public async Task<OriginalCaseDto?> GetOriginalCaseById(int id)
        {
            var originalCase = await _context.OriginalCases
                .AsNoTracking()
                .Include(c => c.CourtCases)
                    .ThenInclude(cc => cc.Court)
                .Include(c => c.CourtCases)
                    .ThenInclude(cc => cc.Judge)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (originalCase == null)
                return null;

            return new OriginalCaseDto
            {
                Id = originalCase.Id,
                CaseNumber = originalCase.CaseNumber,
                Name = originalCase.Name,
                Date = originalCase.Date,
                Description = originalCase.Description,
                CaseType = originalCase.CaseType,
                Priority = originalCase.Priority,
                ReportedBy = originalCase.ReportedBy
            };
        }

        // Check if original case exists
        public async Task<bool> OriginalCaseExists(int id)
        {
            return await _context.OriginalCases
                .AnyAsync(c => c.Id == id);
        }

        // Check if case number already exists
        public async Task<bool> CaseNumberExists(string? caseNumber)
        {
            return await _context.OriginalCases
                .AnyAsync(c => c.CaseNumber == caseNumber);
        }

        // Check if case number belongs to another original case
        public async Task<bool> CaseNumberExistsForOtherCase(
            string? caseNumber, int id)
        {
            return await _context.OriginalCases
                .AnyAsync(c => c.CaseNumber == caseNumber && c.Id != id);
        }

        // Check if original case has court case assignments
        public async Task<bool> HasCourtCaseAssignments(int id)
        {
            return await _context.OriginalCases
                .AnyAsync(c => c.Id == id && c.CourtCases.Any());
        }

        // POST: Create original case
        public async Task<bool> CreateOriginalCase(CreateOriginalCaseDto dto)
        {
            if (await CaseNumberExists(dto.CaseNumber))
                return false;

            var originalCase = new OriginalCase
            {
                CaseNumber = dto.CaseNumber,
                Name = dto.Name,
                Date = dto.Date ?? DateTime.Now,
                Description = dto.Description,
                CaseType = dto.CaseType,
                Priority = dto.Priority,
                ReportedBy = dto.ReportedBy
            };

            _context.OriginalCases.Add(originalCase);

            await _context.SaveChangesAsync();

            return true;
        }

        // PUT: Update original case
        public async Task<bool> UpdateOriginalCase(
            int id, UpdateOriginalCaseDto dto)
        {
            var existing = await _context.OriginalCases
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existing == null)
                return false;

            if (await CaseNumberExistsForOtherCase(dto.CaseNumber, id))
                return false;

            existing.CaseNumber = dto.CaseNumber;
            existing.Name = dto.Name;
            existing.Date = dto.Date ?? DateTime.Now;
            existing.Description = dto.Description;
            existing.CaseType = dto.CaseType;
            existing.Priority = dto.Priority;
            existing.ReportedBy = dto.ReportedBy;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE: Delete original case
        public async Task<bool> DeleteOriginalCase(int id)
        {
            var originalCase = await _context.OriginalCases
                .Include(c => c.CourtCases)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (originalCase == null)
                return false;

            // Prevent deletion if court cases are associated
            if (originalCase.CourtCases.Any())
                return false;

            _context.OriginalCases.Remove(originalCase);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
