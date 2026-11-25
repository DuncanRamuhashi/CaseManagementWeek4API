
using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.JudgeDTO;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseManagementWeek4API.Services
{
    public class JudgeService : IJudge
    {
        private readonly CourtCaseDatabaseContext _context;

        public JudgeService(CourtCaseDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<JudgeDto>> GetAllJudges()
        {
            return await _context.Judges
                .AsNoTracking()
                .Include(j => j.Court)
                .OrderBy(j => j.Name)
                .Select(j => new JudgeDto
                {
                    Id = j.Id,
                    Name = j.Name,
                    Title = j.Title,
                    CourtId = j.CourtId,
                    CourtName = j.Court != null ? j.Court.Name : null,
                    Email = j.Email,
                    Phone = j.Phone,
                    AppointmentDate = j.AppointmentDate,
                    IsActive = j.IsActive
                })
                .ToListAsync();
        }

        public async Task<JudgeDto?> GetJudgeById(string id)
        {
            var judge = await _context.Judges
                .AsNoTracking()
                .Include(j => j.Court)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (judge == null)
                return null;

            return new JudgeDto
            {
                Id = judge.Id,
                Name = judge.Name,
                Title = judge.Title,
                CourtId = judge.CourtId,
                CourtName = judge.Court != null ? judge.Court.Name : null,
                Email = judge.Email,
                Phone = judge.Phone,
                AppointmentDate = judge.AppointmentDate,
                IsActive = judge.IsActive
            };
        }

        public async Task<bool> JudgeExists(string id)
        {
            return await _context.Judges
                .AnyAsync(j => j.Id == id);
        }

        public async Task<bool> HasCourtCaseAssignments(string id)
        {
            return await _context.Judges
                .AnyAsync(j => j.Id == id && j.CourtCases.Any());
        }

        public async Task<bool> CreateJudge(CreateJudgeDto dto)
        {
            if (await JudgeExists(dto.Id))
                return false;

            bool courtExists = await _context.Courts
                .AnyAsync(c => c.Id == dto.CourtId);

            if (!courtExists)
                return false;

            var judge = new Judge
            {
                Id = dto.Id,
                Name = dto.Name,
                Title = dto.Title,
                CourtId = dto.CourtId,
                Email = dto.Email,
                Phone = dto.Phone,
                AppointmentDate = dto.AppointmentDate,
                IsActive = dto.IsActive ?? true
            };

            _context.Judges.Add(judge);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateJudge(
            string id,
            UpdateJudgeDto dto)
        {
            if (id != dto.Id)
                return false;

            var existing = await _context.Judges
                .FirstOrDefaultAsync(j => j.Id == id);

            if (existing == null)
                return false;

            bool courtExists = await _context.Courts
                .AnyAsync(c => c.Id == dto.CourtId);

            if (!courtExists)
                return false;

            existing.Name = dto.Name;
            existing.Title = dto.Title;
            existing.CourtId = dto.CourtId;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.AppointmentDate = dto.AppointmentDate;

            if (dto.IsActive.HasValue)
                existing.IsActive = dto.IsActive.Value;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteJudge(string id)
        {
            var judge = await _context.Judges
                .Include(j => j.CourtCases)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (judge == null)
                return false;

            if (judge.CourtCases.Any())
                return false;

            _context.Judges.Remove(judge);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
