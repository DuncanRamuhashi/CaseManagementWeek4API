using CaseManagementWeek4API.DTOs.JudgeDTO;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IJudge
    {
        Task<List<JudgeDto>> GetAllJudges();

        Task<JudgeDto?> GetJudgeById(string id);

        Task<bool> JudgeExists(string id);

        Task<bool> HasCourtCaseAssignments(string id);

        Task<bool> CreateJudge(CreateJudgeDto dto);

        Task<bool> UpdateJudge(string id, UpdateJudgeDto dto);

        Task<bool> DeleteJudge(string id);
    }
}
