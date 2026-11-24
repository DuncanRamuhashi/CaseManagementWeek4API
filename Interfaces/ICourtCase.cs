using CaseManagementWeek4API.DTOs.CourtCaseDTO;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface ICourtCase
    {

        Task<List<CourtCaseDto>> Index();

        Task<object?> Manage(int originalCaseId);

        Task<object?> Create(int originalCaseId);

        Task<CourtCaseDto?> Create(CreateCourtCaseDto courtCase);

        Task<CourtCaseDto?> Edit(int id);

        Task<CourtCaseDto?> Edit(
            int id,
            UpdateCourtCaseDto courtCase);

        Task<CourtCaseDto?> Delete(int id);

        Task<bool> DeleteConfirmed(int id);

        Task<object?> AddDefendant(int courtCaseId);

        Task<(bool Success, string Message)> AddDefendant(
            AddCourtCaseDefendantDto defendant);

        Task<(bool Success, string Message)> RemoveDefendant(
            RemoveCourtCaseDefendantDto defendant);
    }
}
