
using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.DefetendantDTO;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CaseManagementWeek4API.Services
{
    public class DefendantService : IDefendant
    {
        private readonly CourtCaseDatabaseContext _context;

        public DefendantService(CourtCaseDatabaseContext context)
        {
            _context = context;
        }

        // GET: Get all defendants
        public async Task<List<DefendantDto>> GetAllDefendants()
        {
            return await _context.Defendants
                .AsNoTracking()
                .OrderBy(d => d.Name)
                .Select(d => new DefendantDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Idnumber = d.Idnumber,
                    DateOfBirth = d.DateOfBirth,
                    Gender = d.Gender,
                    Address = d.Address,
                    Phone = d.Phone,
                    Nationality = d.Nationality,
                    IsInCustody = d.IsInCustody
                })
                .ToListAsync();
        }

        // GET: Get defendant by ID
        public async Task<DefendantDto?> GetDefendantById(string id)
        {
            var defendant = await _context.Defendants
                .AsNoTracking()
                .Include(d => d.CourtCaseDefendants)
                    .ThenInclude(cd => cd.CourtCase)
                        .ThenInclude(cc => cc.OriginalCase)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (defendant == null)
                return null;

            return new DefendantDto
            {
                Id = defendant.Id,
                Name = defendant.Name,
                Idnumber = defendant.Idnumber,
                DateOfBirth = defendant.DateOfBirth,
                Gender = defendant.Gender,
                Address = defendant.Address,
                Phone = defendant.Phone,
                Nationality = defendant.Nationality,
                IsInCustody = defendant.IsInCustody
            };
        }

        // Check if defendant exists
        public async Task<bool> DefendantExists(string id)
        {
            return await _context.Defendants
                .AnyAsync(d => d.Id == id);
        }

        // Check if defendant is assigned to court cases
        public async Task<bool> HasCourtCaseAssignments(string id)
        {
            return await _context.Defendants
                .Where(d => d.Id == id)
                .AnyAsync(d => d.CourtCaseDefendants.Any());
        }

        // POST: Create defendant
        public async Task<bool> CreateDefendant(CreateDefendantDto dto)
        {
            if (await DefendantExists(dto.Id))
                return false;

            var defendant = new Defendant
            {
                Id = dto.Id,
                Name = dto.Name,
                Idnumber = dto.Idnumber,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                Phone = dto.Phone,
                Nationality = dto.Nationality,
                IsInCustody = dto.IsInCustody ?? false
            };

            _context.Defendants.Add(defendant);

            await _context.SaveChangesAsync();

            return true;
        }

        // PUT: Update defendant
        public async Task<bool> UpdateDefendant(
            string id,
            UpdateDefendantDto dto)
        {
            if (id != dto.Id)
                return false;

            var existing = await _context.Defendants
                .FirstOrDefaultAsync(d => d.Id == id);

            if (existing == null)
                return false;

            existing.Name = dto.Name;
            existing.Idnumber = dto.Idnumber;
            existing.DateOfBirth = dto.DateOfBirth;
            existing.Gender = dto.Gender;
            existing.Address = dto.Address;
            existing.Phone = dto.Phone;
            existing.Nationality = dto.Nationality;
            existing.IsInCustody = dto.IsInCustody ?? false;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE: Delete defendant
        public async Task<bool> DeleteDefendant(string id)
        {
            var defendant = await _context.Defendants
                .Include(d => d.CourtCaseDefendants)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (defendant == null)
                return false;

            // Prevent deletion if defendant is assigned to a court case
            if (defendant.CourtCaseDefendants.Any())
                return false;

            _context.Defendants.Remove(defendant);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
