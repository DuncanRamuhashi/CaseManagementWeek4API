
using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.ProsecutorDTO;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseManagementWeek4API.Services
{
    public class ProsecutorService : IProsecutor
    {
        private readonly CourtCaseDatabaseContext _context;

        public ProsecutorService(CourtCaseDatabaseContext context)
        {
            _context = context;
        }

        // GET: All Prosecutors
        public async Task<List<ProsecutorDto>> GetAllProsecutors()
        {
            return await _context.Prosecutors
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(p => new ProsecutorDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Rank = p.Rank,
                    Office = p.Office,
                    Email = p.Email,
                    Phone = p.Phone,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }

        // GET: Prosecutor by ID
        public async Task<ProsecutorDto?> GetProsecutorById(string id)
        {
            var prosecutor = await _context.Prosecutors
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prosecutor == null)
                return null;

            return new ProsecutorDto
            {
                Id = prosecutor.Id,
                Name = prosecutor.Name,
                Rank = prosecutor.Rank,
                Office = prosecutor.Office,
                Email = prosecutor.Email,
                Phone = prosecutor.Phone,
                IsActive = prosecutor.IsActive
            };
        }

        // CHECK: Prosecutor exists
        public async Task<bool> ProsecutorExists(string id)
        {
            return await _context.Prosecutors
                .AnyAsync(p => p.Id == id);
        }

        // CHECK: Prosecutor has court case assignments
        public async Task<bool> HasCourtCaseAssignments(string id)
        {
            return await _context.Prosecutors
                .AnyAsync(p => p.Id == id && p.CourtCases.Any());
        }

        // CREATE: Prosecutor
        public async Task<bool> CreateProsecutor(CreateProsecutorDto dto)
        {
            if (await ProsecutorExists(dto.Id))
                return false;

            var prosecutor = new Prosecutor
            {
                Id = dto.Id,
                Name = dto.Name,
                Rank = dto.Rank,
                Office = dto.Office,
                Email = dto.Email,
                Phone = dto.Phone,
                IsActive = dto.IsActive ?? true
            };

            _context.Prosecutors.Add(prosecutor);

            await _context.SaveChangesAsync();

            return true;
        }

        // UPDATE: Prosecutor
        public async Task<bool> UpdateProsecutor(
            string id,
            UpdateProsecutorDto dto)
        {
            if (id != dto.Id)
                return false;

            var existing = await _context.Prosecutors
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existing == null)
                return false;

            existing.Name = dto.Name;
            existing.Rank = dto.Rank;
            existing.Office = dto.Office;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;

            if (dto.IsActive.HasValue)
                existing.IsActive = dto.IsActive.Value;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE: Prosecutor
        public async Task<bool> DeleteProsecutor(string id)
        {
            var prosecutor = await _context.Prosecutors
                .Include(p => p.CourtCases)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prosecutor == null || prosecutor.CourtCases.Any())
                return false;

            _context.Prosecutors.Remove(prosecutor);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
