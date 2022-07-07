using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Authorization.Users;
using HospitalSystem.Domain;
using HospitalSystem.Services.Employees.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Employees
{
    /// <summary>
    /// Endpoint for all employees
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class EmployeeAppService: ApplicationService
    {
        private readonly IRepository<Employee, Guid> _employeeRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _userRepository;
        /// Injecting repositories
        public EmployeeAppService(IRepository<Employee, Guid> employeeRepository, UserManager userManager, 
                                  IRepository<User, long> userRepository)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        /// Creating employee entries
        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO input)
        {
            var checkEmployee = await _employeeRepository.GetAll()
                                                         .Where(n => n.IdentificationNumber == input.IdentificationNumber)
                                                         .FirstOrDefaultAsync();
            if (checkEmployee == null)
            {
                var user = new User()
                {
                    TenantId = AbpSession.TenantId,
                    EmailAddress = input.Email,
                    PhoneNumber = input.PhoneNumber,
                    IsEmailConfirmed = true,
                    UserName = input.Username,
                    Name = input.FirstName,
                    Surname = input.LastName
                };

                user.SetNormalizedNames();
                await _userManager.InitializeOptionsAsync(user.TenantId);
                CheckErrors(await _userManager.CreateAsync(user, input.Password));

                if (input.RoleNames != null)
                {
                    await _userManager.SetRolesAsync(user, input.RoleNames);
                }

                var generateNumber =$"{ input.FirstName.Substring(1, 3)}_{input.IdentificationNumber.Substring(6, 4)}_{ input.LastName.Substring(1, 3)}";
                var employee = ObjectMapper.Map<Employee>(input);



                switch (employee.Type)
                {
                    case Domain.RefLists.RefListEmployeeType.Receptionist:
                        employee.EmployeeNumber = $"REC{generateNumber}";
                        break;
                    case Domain.RefLists.RefListEmployeeType.Nurse:
                        employee.EmployeeNumber = $"NUR{generateNumber}";
                        break;
                    case Domain.RefLists.RefListEmployeeType.Doctor:
                        employee.EmployeeNumber = $"DOC{generateNumber}";
                        break;
                    default:
                        break;
                }

                employee.User = user;

                CurrentUnitOfWork.SaveChanges();
                return ObjectMapper.Map<EmployeeDTO>(await _employeeRepository.InsertAsync(employee));
            }

            else
            {
                throw new UserFriendlyException("Employee already exists in the hospital database. As an alternative, update the employee record.");
            }

            }
        /// Deleting employee entries
        public void DeleteAsync(string employeeNumber)
        {
            var employee = _employeeRepository.GetAllIncluding(n => n.User)
                                              .Where(x => x.EmployeeNumber == employeeNumber)
                                              .FirstOrDefault();
            var user = _userRepository.GetAll().Where(n => n.Id == employee.User.Id).FirstOrDefault();
            if (employee == null && user == null) throw new UserFriendlyException("No data found.");
            _employeeRepository.Delete(employee);
            _userRepository.Delete(user);
            
        }
        /// Get all employees
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        public async Task<List<EmployeeDTO>> GetAllAsync()
        {
            var employee = await _employeeRepository.GetAllIncluding(n => n.User, m => m.User.Roles).ToListAsync();
             return ObjectMapper.Map<List<EmployeeDTO>>(employee);
        }
        /// Filter employees
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        [HttpGet("employee-type/{employeeType}/employee-number/{employeeNumber}")]
        public async Task<List<EmployeeDTO>> GetEmployeesAsync(int employeeType, string employeeNumber)
        {
            return ObjectMapper.Map<List<EmployeeDTO>>(_employeeRepository.GetAllIncluding(n => n.User, m => m.User.Roles).Where(x => (int)x.Type == employeeType || x.EmployeeNumber == employeeNumber));
        }
        /// Update employee entries
        [HttpPut("employee-number/{employeeNumber}")]
        public async Task<EmployeeDTO> UpdateAsync(EmployeeDTO input, string employeeNumber)
        {

            var employee = await _employeeRepository.GetAll().Where(n => n.EmployeeNumber == employeeNumber).FirstOrDefaultAsync();
            if (employee == null) throw new UserFriendlyException("Employee not found.");
            User user = await _userRepository.GetAll().Where(n => n.Id == employee.User.Id).FirstOrDefaultAsync();
            if (user == null) throw new UserFriendlyException("User not found.");
            user.EmailAddress = input.Email;
            user.PhoneNumber = input.PhoneNumber;
            user.UserName = input.Username;
            user.Name = input.FirstName;
            user.Surname = input.LastName;
            user.SetNormalizedNames();
            CheckErrors(await _userManager.UpdateAsync(user));

            employee.FirstName = input.FirstName;
            employee.LastName = input.LastName;
            employee.Specialization = input.Specialization;
            employee.Sex = input.Sex;
            employee.Address = input.Address;
            employee.Email = input.Email;
            employee.Ethnicity = input.Ethnicity;
            employee.PhoneNumber = input.PhoneNumber;
            employee.IdentificationNumber = input.IdentificationNumber;
            employee.User = user;

            return ObjectMapper.Map<EmployeeDTO>(await _employeeRepository.UpdateAsync(employee));
        }
        
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}