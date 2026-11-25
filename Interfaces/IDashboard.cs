using CaseManagementWeek4API.DTOs.DashboardDTO;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IDashboard
    {
        Task<DashboardDto> Index();
    }
}
