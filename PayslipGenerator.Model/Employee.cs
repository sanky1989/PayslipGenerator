﻿using System;

namespace PayslipGenerator.Model
{
    public class Employee
    {
        public Employee() { }
        public Employee(string firstName, string lastName, decimal annualSalary, decimal superRate, PaymentPeriod paymentStartDate)
        {
            FirstName = firstName;
            LastName = lastName;
            AnnualSalary = annualSalary;
            SuperRate = superRate;
            PaymentStartDate = paymentStartDate;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal SuperRate { get; set; }
        public PaymentPeriod PaymentStartDate { get; set; }
    }
}
