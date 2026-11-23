using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IDefendant
    {
        Task<IActionResult> Index();
        Task<IActionResult> Details(string id);
        IActionResult Create();
        Task<IActionResult> Create(Defendant defendant);
        Task<IActionResult> Edit(string id);
        Task<IActionResult> Edit(string id, Defendant defendant);
        Task<IActionResult> Delete(string id);
        Task<IActionResult> DeleteConfirmed(string id);
    }
}
