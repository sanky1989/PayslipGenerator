﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayslipGenerator.Model;

namespace PayslipGenerator.Web.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel(Employee employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            AnnualSalary = employee.AnnualSalary;
            SuperRate = $"{employee.SuperRate}%";
            PaymentStartDate = employee.PaymentStartDate.ToString();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public string SuperRate { get; set; }
        public string PaymentStartDate { get; set; }
    }
}

