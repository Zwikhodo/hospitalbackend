using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Domain;
using HospitalSystem.Services.EmployeeLogs.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.EmployeeLogs
{
    /// <summary>
    /// Endpoints for creating employee logs 
    /// </summary>
    public class EmployeeLogAppService : ApplicationService
    {
        private readonly IRepository<EmployeeLog, Guid> _employeeLogRepository;
        private readonly IRepository<Employee, Guid> _employeeRepository;

        /// Injecting repositories
        public EmployeeLogAppService(IRepository<EmployeeLog, Guid> employeeLogRepository, IRepository<Employee, Guid> employeeRepository)
        {
            _employeeLogRepository = employeeLogRepository;
            _employeeRepository = employeeRepository;
        }

        ///Creating employee log
        public async Task<EmployeeLogDTO> Create(EmployeeLogDTO input)
        {
            var employee = await _employeeRepository.GetAll().Where(n => n.Id == input.EmployeeId).FirstOrDefaultAsync();
            var log = ObjectMapper.Map<EmployeeLog>(input);
            log.Employee = employee;
            log.Availability = true;
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<EmployeeLogDTO>(await _employeeLogRepository.InsertAsync(log));
        }

        /// Deleting Entries
        public async Task DeleteAsync(Guid id)
        {
            await _employeeLogRepository.DeleteAsync(id);
        }

        /// Filtering entity
        
        public async Task<List<EmployeeLogDTO>> GetEmployeeLogAsync(string employeeNumber, Guid employeeId, bool availability)
        {
            var entityFilter = await _employeeLogRepository.GetAllIncluding(n => n.Employee)
                                                     .Where(x => x.Employee.Id == employeeId
                                                     || x.Employee.EmployeeNumber == employeeNumber
                                                     || x.Availability == availability).ToListAsync();
            return ObjectMapper.Map<List<EmployeeLogDTO>>(entityFilter);
        }

        /// Getting all entity entries
        public async Task<List<EmployeeLogDTO>> GetAllAsync()
        {
            var entity = await _employeeLogRepository.GetAllIncluding(x => x.Employee).ToListAsync();
            return ObjectMapper.Map<List<EmployeeLogDTO>>(entity);
        }

        /// Updating entity entry
   
        public async Task<EmployeeLogDTO> UpdateAsync(EmployeeLogDTO input, string employeeNumber)
        {
            var entity = await _employeeLogRepository.GetAllIncluding(n => n.Employee)
                                                   .Where(n => n.Employee.EmployeeNumber == employeeNumber)
                                                   .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _employeeLogRepository.UpdateAsync(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<EmployeeLogDTO>(entity);
        }
    }
}
