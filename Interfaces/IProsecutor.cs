using CaseManagementWeek4API.DTOs.ProsecutorDTO;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IProsecutor
    {
        Task<List<ProsecutorDto>> GetAllProsecutors();

        Task<ProsecutorDto?> GetProsecutorById(string id);

        Task<bool> ProsecutorExists(string id);

        Task<bool> HasCourtCaseAssignments(string id);

        Task<bool> CreateProsecutor(CreateProsecutorDto dto);

        Task<bool> UpdateProsecutor(string id, UpdateProsecutorDto dto);

        Task<bool> DeleteProsecutor(string id);
    }
}
