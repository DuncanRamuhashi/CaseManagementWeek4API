using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{
    public interface ICharge
    {
        Task<IActionResult> Index(int? courtCaseId);
        Task<IActionResult> Details(int id);
        Task<IActionResult> Create(int courtCaseId);
        Task<IActionResult> Create(Charge charge);
        Task<IActionResult> Edit(int id);
        Task<IActionResult> Edit(int id, Charge charge);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> DeleteConfirmed(int id);


    }
}
