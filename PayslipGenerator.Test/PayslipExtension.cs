using System;
using System.Collections.Generic;
using System.Text;
using PayslipGenerator.Model;
using System.IO;

namespace PayslipGenerator.Tests
{
    public static class PayslipExtension
    {
        public static string RandomString()
        {
            return Guid.NewGuid().ToString("n").Substring(0, 8);
        }

        public static CalculatePayslips BuildPayslip()
        {
            return new CalculatePayslips(Enumerations.TaxRates);
        }

        public static FileStream ReadInputFile()
        {
            return File.OpenRead(Path.Combine(@"..\..\..\TestDocument\EmployeeDetails.csv"));
        }

    }
}
