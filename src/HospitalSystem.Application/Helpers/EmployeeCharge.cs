using Abp.Domain.Repositories;
using HospitalSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Helpers
{
    public class EmployeeCharge
    {
        public decimal EmployeeChargeCost(int employeeType)
        {
            var charge = 0;
            switch (employeeType)
            {
                case 1:
                    charge = 450;
                    break;
                case 2:
                    charge = 1000;
                    break;
                case 3:
                    charge = 2000;
                    break;
                default:
                    break;
            }
            return charge;
        }
    }
}
