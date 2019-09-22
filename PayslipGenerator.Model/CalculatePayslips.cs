using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PayslipGenerator.Model
{
    public class CalculatePayslips
    {
        private IEnumerable<TaxRate> _taxRates;
        //private const int MonthsInYear = 12;
        private const decimal MonthsInYear = 12M;//So the value calculates to decimal.
        public CalculatePayslips(IEnumerable<TaxRate> taxRates)
        {
            if (taxRates == null || !taxRates.Any())
            {
                throw new Exception($"Tax Rates must be specified");

            }
            _taxRates = taxRates;
        }

        public IEnumerable<PaySlip> Calculate(IEnumerable<Employee> employees, IValidation validation)
        {
            List<PaySlip> payslip = new List<PaySlip>();
            CheckValidation(employees.ToList(), validation);
            if (validation.IsValid)
            {
                foreach (Employee Emp in employees)
                {
                    PaySlip employeePayslip = new PaySlip();
                    var Name = Emp.FirstName + " " + Emp.LastName;
                    var AnnualSalary = Emp.AnnualSalary;
                    var taxrate = _taxRates.Single(s => s.IsWithinIncomeRange(AnnualSalary));
                    var superRate = Emp.SuperRate;
                    var incomeTax = taxrate.CalculateTax(AnnualSalary);
                    var grossSalary = PaymentAmountsRounded(AnnualSalary);
                    var totalSalary = (grossSalary - incomeTax).RoundToNearestDollarAmount();
                    var totalSuper = CalculateSuper(grossSalary, superRate);
                    //var payPeriod = CalculatePaymentPeriods(Emp);

                    var payPeriod = Emp.PaymentStartDate.StartDate.ToString("MMMM dd") + " - " + Emp.PaymentStartDate.EndDate.ToString("MMMM dd");

                    employeePayslip.Name = Name;
                    employeePayslip.PayPeriod = payPeriod.ToString();
                    employeePayslip.GrossIncome = grossSalary;
                    employeePayslip.IncomeTax = incomeTax;
                    employeePayslip.NetIncome = totalSalary;
                    employeePayslip.Super = totalSuper;
                    payslip.Add(employeePayslip);
                }
            }

            return payslip;
        }

        private void CheckValidation(IEnumerable<Employee> employees, IValidation validation)
        {
            foreach (var e in employees)
            {
             
                if (String.IsNullOrWhiteSpace(e.FirstName))
                {
                    validation.AddErrorMessage($"{nameof(e.FirstName)} must not be empty");
                }
                if (String.IsNullOrWhiteSpace(e.LastName))
                {
                    validation.AddErrorMessage($"{nameof(e.LastName)} must not be empty");
                }

                if (e.PaymentStartDate == null)
                {
                    validation.AddErrorMessage($"{nameof(e.PaymentStartDate)} payment period must be specified");
                }

                if (e.SuperRate < 0M || e.SuperRate > 0.50M)
                {
                    validation.AddErrorMessage($"{nameof(e.SuperRate)} must be between 0% and 50% inclusive");
                }

                #region Salary check
                var annualSalary = e.AnnualSalary;
                if (annualSalary < 0M)
                {
                    validation.AddErrorMessage($"{nameof(annualSalary)}, value:{annualSalary}  must be positive number.");
                }
                if (!annualSalary.IsWholeNumber())
                {
                    validation.AddErrorMessage($"{nameof(annualSalary)}, value:{annualSalary}  must be a whole number.");
                }
                #endregion


            }
        }


        #region Calculate Amounts & Date Time
        public static decimal PaymentAmountsRounded(decimal amount)
        {
            var result = amount / MonthsInYear;
            return result.RoundToNearestDollarAmount();
        }

        public static decimal CalculateSuper(decimal grossSalary, decimal superRate)
        {
            var super = grossSalary * superRate;
            return super.RoundToNearestDollarAmount();
        }


        #endregion
    }
}
