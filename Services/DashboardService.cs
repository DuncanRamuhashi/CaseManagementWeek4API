using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.DashboardDTO;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.DTOs.DashboardDTO;
using CaseManagementWeek4API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaseManagementWeek4API.Services
{
    public class DashboardService : IDashboard
    {
        private readonly CourtCaseDatabaseContext Database;

        public DashboardService(CourtCaseDatabaseContext database)
        {
            Database = database;
        }

        public async Task<DashboardDto> Index()
        {
            var dashboard = new DashboardDto();

            dashboard.TotalOriginalCases =
                await Database.OriginalCases.CountAsync();

            dashboard.TotalCourtCases =
                await Database.CourtCases.CountAsync();

            dashboard.OpenCases =
                await Database.CourtCases.CountAsync(c =>
                    c.Status == "Open" ||
                    c.Status == "In Progress" ||
                    c.Status == "Pending");

            dashboard.TotalDefendants =
                await Database.Defendants.CountAsync();

            dashboard.TotalCharges =
                await Database.Charges.CountAsync();

            dashboard.TotalJudges =
                await Database.Judges.CountAsync();

            dashboard.TotalProcecutors =
                await Database.Prosecutors.CountAsync();

            dashboard.UpcomingHearings =
                await Database.CourtCases
                    .Where(c => c.NextHearingDate != null &&
                                c.NextHearingDate >= DateTime.UtcNow)
                    .OrderBy(c => c.NextHearingDate)
                    .Take(5)
                    .Include(c => c.OriginalCase)
                    .Include(c => c.Court)
                    .ToListAsync();

            return dashboard;
        }
    }
}
