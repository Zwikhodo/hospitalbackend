using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Services.Bills.DTO;
using HospitalSystem.Services.Bills.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Bills

{
    /// <summary>
    /// Endpoints for admitting the patient 
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_BillCreate)]
    public class BillAppService : ApplicationService
    {
        private readonly IRepository<Bill, Guid> _billRepository;
        private readonly IRepository<PatientReport, Guid> _patientReportRepository;
        private readonly IBillingHelper _helper;

        /// Injecting repositories
        public BillAppService(
            IRepository<Bill, Guid> billRepository,
            IRepository<PatientReport, Guid> patientReportRepository,
            IBillingHelper helper)
        {
            _billRepository = billRepository;
            _patientReportRepository = patientReportRepository;
            _helper = helper;
        }

        /// creating bill
        public async Task<BillDTO> CreateAsync(InputBillDTO input)
        {
            var checkPatientReport = await _patientReportRepository.GetAllIncluding(n => n.Procedure, m => m.Examination.Employee, 
                                                                                    z => z.PrescribedTest.Employee, x => x.Patient)
                                                                   .Where(n => n.Id == input.PatientReportId)
                                                                   .FirstOrDefaultAsync();
            decimal employeeCharge = 0;
            decimal medicineCharge = 0;
            decimal prescribedTestCharge = 0;
            decimal procedureCharge = 0;
            decimal costPerNight = 0;
            var bill = ObjectMapper.Map<Bill>(input);
            bill.PatientReport = checkPatientReport;

            bill.EmployeeCharge = _helper.CalculateEmployeeCharge(checkPatientReport, employeeCharge);
            bill.MedicineCharge = _helper.CalculateMedicineCharge(checkPatientReport, medicineCharge);
            bill.PrescribedTestCharge = _helper.CalculatePrescribedTestCharge(checkPatientReport, prescribedTestCharge);
            bill.ProcedureCharge = _helper.CalculateProcedureCharge(checkPatientReport, procedureCharge);
            bill.CostPerNight = _helper.CalculateCostPerNight(checkPatientReport, costPerNight);
            bill.TotalBill = bill.EmployeeCharge + bill.MedicineCharge + bill.PrescribedTestCharge + bill.ProcedureCharge + bill.CostPerNight;
            return ObjectMapper.Map<BillDTO>(await _billRepository.InsertAsync(bill));
        }

        /// Deleting Entries
        public async Task DeleteAsync(Guid id)
        {
            await _billRepository.DeleteAsync(id);
        }

        /// Getting all entity entries
        public async Task<List<BillDTO>> GetAllAsync()
        {
            var entries = await _billRepository.GetAllIncluding(n => n.PatientReport).ToListAsync();
            return ObjectMapper.Map<List<BillDTO>>(entries);
        }

        /// Filtering entity
        
        public async Task<List<BillDTO>> GetBillsAsync(string patientNumber)
        {
            var entityFilter = await _billRepository.GetAllIncluding(n => n.PatientReport)
                                         .Where(x => x.PatientReport.Patient.PatientNumber == patientNumber)
                                         .ToListAsync();
            return ObjectMapper.Map<List<BillDTO>>(entityFilter);
        }

        /// Updating entity entry
        
        public async Task<BillDTO> UpdateAsync(BillDTO input, string patientNumber)
        {
            var entity = await _billRepository.GetAllIncluding(n => n.PatientReport)
                                              .Where(n => n.PatientReport.Patient.PatientNumber == patientNumber)
                                              .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _billRepository.UpdateAsync(entity);
            return ObjectMapper.Map<BillDTO>(entity);
        }
    }
}
