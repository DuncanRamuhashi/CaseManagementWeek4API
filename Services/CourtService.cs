using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.CourtDTO;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace CaseManagementWeek4API.Services
{
    public class CourtService : ICourt
    {
        private readonly CourtCaseDatabaseContext Database;

        public CourtService(IHttpContextAccessor httpContextAccessor)
        {
            Database = httpContextAccessor.HttpContext!
                .RequestServices
                .GetRequiredService<CourtCaseDatabaseContext>();
        }


        // Maps Court entity to CourtDto
        private static CourtDto MapCourt(Court court)
        {
            return new CourtDto
            {
                Id = court.Id,
                Name = court.Name,
                Location = court.Location,
                CourtCode = court.CourtCode,
                CourtLevel = court.CourtLevel,
                Province = court.Province,
                Phone = court.Phone,
                Email = court.Email,
                Address = court.Address,
                IsActive = court.IsActive,
                CreatedAt = court.CreatedAt
            };
        }

        // GET: api/Courts
        public async Task<List<CourtDto>> Index()
        {
            var courts = await Database.Courts
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();

            return courts.Select(MapCourt).ToList();
        }

        // GET: api/Courts/5
        public async Task<object?> Details(int id)
        {
            var court = await Database.Courts
                .AsNoTracking()
                .Include(c => c.Judges)
                .Include(c => c.CourtCases)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (court == null)
                return null;

            return new
            {
                Court = MapCourt(court),
                JudgesCount = court.Judges.Count,
                CourtCasesCount = court.CourtCases.Count
            };
        }

        // GET: api/Courts/create
        public Task<object> Create()
        {
            return Task.FromResult<object>(new
            {
                Message = "You can submit a new court using POST api/Courts."
            });
        }

        // POST: api/Courts
        public async Task<CourtDto?> Create(CreateCourtDto court)
        {
            var newCourt = new Court
            {
                Name = court.Name,
                Location = court.Location,
                CourtCode = court.CourtCode,
                CourtLevel = court.CourtLevel,
                Province = court.Province,
                Phone = court.Phone,
                Email = court.Email,
                Address = court.Address,
                IsActive = court.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            Database.Courts.Add(newCourt);

            await Database.SaveChangesAsync();

            return MapCourt(newCourt);
        }

        // GET: api/Courts/5/edit
        public async Task<CourtDto?> Edit(int id)
        {
            var court = await Database.Courts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (court == null)
                return null;

            return MapCourt(court);
        }

        // PUT: api/Courts/5
        public async Task<CourtDto?> Edit(
            int id,
            UpdateCourtDto court)
        {
            var existing = await Database.Courts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existing == null)
                return null;

            existing.Name = court.Name;
            existing.Location = court.Location;
            existing.CourtCode = court.CourtCode;
            existing.CourtLevel = court.CourtLevel;
            existing.Province = court.Province;
            existing.Phone = court.Phone;
            existing.Email = court.Email;
            existing.Address = court.Address;
            existing.IsActive = court.IsActive;

            await Database.SaveChangesAsync();

            return MapCourt(existing);
        }

        // GET: api/Courts/5/delete
        public async Task<CourtDto?> Delete(int id)
        {
            var court = await Database.Courts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (court == null)
                return null;

            return MapCourt(court);
        }

        // DELETE: api/Courts/5
        public async Task<(bool Success, string Message)> DeleteConfirmed(int id)
        {
            var court = await Database.Courts
                .Include(c => c.Judges)
                .Include(c => c.CourtCases)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (court == null)
                return (false, "Court not found.");

            if (court.Judges.Any() || court.CourtCases.Any())
            {
                return (false,
                    "Cannot delete a court that has assigned judges or court cases. Deactivate it instead.");
            }

            Database.Courts.Remove(court);

            await Database.SaveChangesAsync();

            return (true, "Court deleted successfully.");
        }

    }
}
