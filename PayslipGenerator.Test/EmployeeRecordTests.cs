using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xunit;
using PayslipGenerator.Model;
using PayslipGenerator.DataAccess;
using System.Linq;

namespace PayslipGenerator.Tests
{
    public class DataAccessModelTests
    {
        //Get input records


        #region Write Records Testing
        [Fact]
        public void WritePayslipRecords_ReadRecords()
        {
            PaySlip[] payslips = new[]
            {
                new PaySlip
                {Name = PayslipExtension.RandomString(),
                 PayPeriod = PayslipExtension.RandomString(),
                 GrossIncome = 1245678M,
                 IncomeTax =  4567M,
                 NetIncome = 1245678M,
                 Super = 1245M } };

            //Writing to Bytes
            Byte[] recordByte = new EmployeeRecords().WriteRecordsToBytes(payslips);
            //Read and verify records
            using (StreamReader reader = new StreamReader(new MemoryStream(recordByte)))
            {
                //First line is the header
                var Header = reader.ReadLine();
                foreach (var payrecord in payslips)
                {
                    var item = reader.ReadLine();
                    Assert.Equal(item, $"{payrecord.Name},{payrecord.PayPeriod},{payrecord.GrossIncome},{payrecord.IncomeTax},{payrecord.NetIncome},{payrecord.Super}");

                }
            }

        }


        [Fact]
        public void WritePayslipRecords_ReadHeader()
        {
            PaySlip[] payslips = new[]
           {
                new PaySlip
                {Name = PayslipExtension.RandomString(),
                 PayPeriod = PayslipExtension.RandomString(),
                 GrossIncome = 1245678M,
                 IncomeTax =  4567M,
                 NetIncome = 1245678M,
                 Super = 1245M } };

            //Writing to Bytes
            Byte[] recordByte = new EmployeeRecords().WriteRecordsToBytes(payslips);
            //Read and verify records
            using (StreamReader reader = new StreamReader(new MemoryStream(recordByte)))
            {
                //First line is the header
                var Header = reader.ReadLine();
                Assert.Equal("Name,PayPeriod,GrossIncome,IncomeTax,NetIncome,Super", Header);
            }
        }

        #endregion


        #region Read Records
        [Fact]
        public void ReadRecords_CSVFile_ReturnResult()
        {
            FileStream inputFile = PayslipExtension.ReadInputFile();
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
            var EmployeeRecordsinFile = new EmployeeRecords().ReadRecords<Employee>(inputFile).ToArray();
            for(int i= 0; i < EmployeeRecords.Length; i++)
            {
                Assert.Equal(EmployeeRecords[i].FirstName, EmployeeRecordsinFile[i].FirstName);
                Assert.Equal(EmployeeRecords[i].LastName, EmployeeRecordsinFile[i].LastName);
                Assert.Equal(EmployeeRecords[i].AnnualSalary, EmployeeRecordsinFile[i].AnnualSalary);
                Assert.Equal(EmployeeRecords[i].SuperRate, EmployeeRecordsinFile[i].SuperRate);
                Assert.Equal(EmployeeRecords[i].PaymentStartDate.EndDate, EmployeeRecordsinFile[i].PaymentStartDate.EndDate);
                Assert.Equal(EmployeeRecords[i].PaymentStartDate.StartDate, EmployeeRecordsinFile[i].PaymentStartDate.StartDate);

            }


        }

        #endregion

    }
}
