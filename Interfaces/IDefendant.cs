using CaseManagementWeek4API.DTOs.DefetendantDTO;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IDefendant
    {
        Task<List<DefendantDto>> GetAllDefendants();

        Task<DefendantDto?> GetDefendantById(string id);

        Task<bool> DefendantExists(string id);

        Task<bool> HasCourtCaseAssignments(string id);

        Task<bool> CreateDefendant(CreateDefendantDto dto);

        Task<bool> UpdateDefendant(string id, UpdateDefendantDto dto);

        Task<bool> DeleteDefendant(string id);
    }
}
