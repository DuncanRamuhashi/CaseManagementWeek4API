using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IOriginalCase
    {
        Task<IActionResult> Index();
        Task<IActionResult> Details(int id);
        IActionResult Create();
        Task<IActionResult> Create(OriginalCase originalCase);
        Task<IActionResult> Edit(int id);
        Task<IActionResult> Edit(int id, OriginalCase originalCase);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> DeleteConfirmed(int id);
    }
}
