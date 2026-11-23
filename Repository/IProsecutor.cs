using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IProsecutor
    {
        Task<IActionResult> Index();
        Task<IActionResult> Details(string id);
        IActionResult Create();
        Task<IActionResult> Create(Prosecutor prosecutor);
        Task<IActionResult> Edit(string id);
        Task<IActionResult> Edit(string id, Prosecutor prosecutor);
        Task<IActionResult> Delete(string id);
        Task<IActionResult> DeleteConfirmed(string id);
    }
}
