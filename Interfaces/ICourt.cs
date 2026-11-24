using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface ICourt
    {
        Task<IActionResult> Index();
        Task<IActionResult> Details(int id);
        IActionResult Create();
        Task<IActionResult> Create(Court court);
        Task<IActionResult> Edit(int id);
        Task<IActionResult> Edit(int id, Court court);
            Task<IActionResult> Delete(int id);
        Task<IActionResult> DeleteConfirmed(int id); 
    }
}
