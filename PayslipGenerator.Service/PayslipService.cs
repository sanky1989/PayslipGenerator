using System;
using PayslipGenerator.Model;
using System.IO;
using System.Collections.Generic;
using PayslipGenerator.DataAccess;
using Microsoft.Extensions.Logging;
using CsvHelper;
using System.Linq;

namespace PayslipGenerator.Service
{
    public class PayslipService : IPayslipService
    {
 
        private IEmployeeRecords _employeeRecords;
        private ILogger<PayslipService> _logger;
        private CalculatePayslips _calculatePayslips;
        //Constructor
        public PayslipService(ILogger<PayslipService> logger,IEmployeeRecords employeeRecords)
        {
            _logger = logger;
            _employeeRecords = employeeRecords;
            _calculatePayslips = new CalculatePayslips(Enumerations.TaxRates);
        }

        //Interface Service records
        public IEnumerable<Employee> ReadRecords(Stream stream)
        {
            try
            {
                return _employeeRecords.ReadRecords<Employee>(stream);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "Exception " + Ex.GetType() + $" thrown reading records of  {nameof(Employee)} from the file");
                throw;
            }
        }

        public IEnumerable<PaySlip> GeneratePayslips(IEnumerable<Employee> employees, IValidation validation)
        {
            _logger.LogInformation($"Processing {employees.ToList().Count} Employee records");
            return _calculatePayslips.Calculate(employees.ToList(), validation);
        }

        public byte[] WriteToByteArray(IEnumerable<PaySlip> payslips)
        {
            return _employeeRecords.WriteRecordsToBytes(payslips);
        }
    }
}
