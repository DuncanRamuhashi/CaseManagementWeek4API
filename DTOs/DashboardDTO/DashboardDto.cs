using CaseManagementWeek4API.Models;

namespace CaseManagementWeek4API.DTOs.DashboardDTO
{
    public class DashboardDto
    {
        public int TotalOriginalCases { get; set; }
        public int TotalCourtCases { get; set; }
        public int OpenCases { get; set; }
        public int TotalDefendants { get; set; }
        public int TotalCharges { get; set; }
        public int TotalJudges { get; set; }
        public int TotalProcecutors { get; set; }

        public List<CourtCase> UpcomingHearings { get; set; } = new();
    }
}
