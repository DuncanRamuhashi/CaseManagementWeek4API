using CaseManagementWeek4API.DTOs.ChargeDTO;
using CaseManagementWeek4API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagementWeek4API.Interfaces
{

        //service will return DTOs i
        public interface ICharge
        {
            Task<List<ChargeDto>> Index(int? courtCaseId);

            Task<ChargeDto?> Details(int id);

            Task<bool> Create(int courtCaseId);

            Task<ChargeDto?> Create(CreateChargeDto charge);

            Task<ChargeDto?> Edit(int id);

            Task<ChargeDto?> Edit(int id, UpdateChargeDto charge);

            Task<ChargeDto?> Delete(int id);

            Task<bool> DeleteConfirmed(int id);
        }

    
}
