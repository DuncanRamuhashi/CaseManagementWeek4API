using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface IJudge
    {
        Task<IActionResult> Index();
        Task<IActionResult> Details(string id);
        Task<IActionResult> Create();
        Task<IActionResult> Create(Judge judge);
        Task<IActionResult> Edit(string id);
        Task<IActionResult> Edit(string id, Judge judge);
        Task<IActionResult> Delete(string id);
        Task<IActionResult> DeleteConfirmed(string id); 
    }
}
