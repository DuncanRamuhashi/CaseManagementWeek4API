using CaseManagementWeek4API.DTOs.CourtDTO;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface ICourt
    {
        Task<List<CourtDto>> Index();

        Task<object?> Details(int id);

        Task<object> Create();

        Task<CourtDto?> Create(CreateCourtDto court);

        Task<CourtDto?> Edit(int id);

        Task<CourtDto?> Edit(int id, UpdateCourtDto court);

        Task<CourtDto?> Delete(int id);

        Task<(bool Success, string Message)> DeleteConfirmed(int id);
    }
}
