using CaseManagementWeek4API.DTOs.OriginalCaseDTO;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IOriginalCase
    {
        Task<List<OriginalCaseDto>> GetAllOriginalCases();

        Task<OriginalCaseDto?> GetOriginalCaseById(int id);

        Task<bool> OriginalCaseExists(int id);

        Task<bool> CaseNumberExists(string? caseNumber);

        Task<bool> CaseNumberExistsForOtherCase(
            string? caseNumber, int id);

        Task<bool> HasCourtCaseAssignments(int id);

        Task<bool> CreateOriginalCase(CreateOriginalCaseDto dto);

        Task<bool> UpdateOriginalCase(
            int id, UpdateOriginalCaseDto dto);

        Task<bool> DeleteOriginalCase(int id);
    }
}
