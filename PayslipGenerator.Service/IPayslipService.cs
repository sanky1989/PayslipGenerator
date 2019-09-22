using System;
using System.Collections.Generic;
using System.Text;
using PayslipGenerator.Model;
using System.IO;

namespace PayslipGenerator.Service
{
    public interface IPayslipService
    {
        IEnumerable<Employee> ReadRecords(Stream stream);
        IEnumerable<PaySlip> GeneratePayslips(IEnumerable<Employee> employees, IValidation validation);
        byte[] WriteToByteArray(IEnumerable<PaySlip> payslips);
    }
}
