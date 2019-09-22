using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PayslipGenerator.Model;
using System.Linq;
using System.IO;
using PayslipGenerator.DataAccess;


namespace PayslipGenerator.Tests
{
    public class CalculateSalary
    {
        [Fact]
        public void Calculate_EmployeeAnnualSalary_ReturnsValidationError()
        {
            var validation = new Validation();
            decimal annualSalary = 1.5M;
            var employee = new Employee(PayslipExtension.RandomString(), PayslipExtension.RandomString(), annualSalary, 0M, new PaymentPeriod(new DateTime(2019,2,01), new DateTime(2019, 2, 28)));
            PayslipExtension.BuildPayslip().Calculate(new[] { employee }, validation);
            //validation.Errors
            var Errors = validation.Errors.FirstOrDefault();
            Assert.Equal(Errors, $"{nameof(annualSalary)}, value:{annualSalary}  must be a whole number.");
         
        }

        [Fact]
        public void Calculate_EmployeeAnnualSalary_ReturnsValidationErrorForNegativeInteger()
        {
            var validation = new Validation();
            decimal annualSalary = -1.5M;
            var employee = new Employee(PayslipExtension.RandomString(), PayslipExtension.RandomString(), annualSalary, 0M, new PaymentPeriod(new DateTime(2019, 2, 01), new DateTime(2019, 2, 28)));
            PayslipExtension.BuildPayslip().Calculate(new[] { employee }, validation);
            //validation.Errors
            var Errors = validation.Errors.FirstOrDefault();
            var ExpectedErrors = $"{nameof(annualSalary)}, value:{annualSalary}  must be positive number.";
            Assert.Equal(Errors, ExpectedErrors);

        }

        [Fact]
        public void CalculateRecords_ReturnException()
        {
            Assert.Throws<Exception>(() => new CalculatePayslips(new TaxRate[0]));
        }

        #region Calculate Records valid
        [Fact]
        public void CalculateRecords_ReturnTrue()
        {
            FileStream inputFile = PayslipExtension.ReadInputFile();
            var validation = new Validation();
            Employee[] EmployeeRecords = new[]
            {
                new Employee
                {
                    FirstName="David",
                    LastName="Rudd",
                    AnnualSalary= 60050M,
                    SuperRate=0.09M,
                    PaymentStartDate = new PaymentPeriod(new DateTime(DateTime.Now.Year, 3, 1),new DateTime(DateTime.Now.Year, 3, 31))
                }
            };

            PayslipExtension.BuildPayslip().Calculate(EmployeeRecords, validation);

            Assert.True(validation.IsValid);

        }

        #endregion

    }
}
