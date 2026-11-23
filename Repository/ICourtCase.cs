using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface ICourtCase
    {

        Task<IActionResult> Index();
        Task<IActionResult> Manage(int originalCaseId);
        Task<IActionResult> Create(int originalCaseId);
        Task<IActionResult> Create(CourtCase courtCase);
        Task<IActionResult> Edit(int id);
        Task<IActionResult> Edit(int id, CourtCase courtCase);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> DeleteConfirmed(int id);
        Task<IActionResult> AddDefendant(int courtCaseId);
        Task<IActionResult> AddDefendant(int courtCaseId, string defendantId, string role, bool isPrimary);
        Task<IActionResult> RemoveDefendant(int courtCaseId, string defendantId);
    }
}
