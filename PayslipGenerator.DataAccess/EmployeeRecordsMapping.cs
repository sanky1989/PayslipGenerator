using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using PayslipGenerator.Model;

namespace PayslipGenerator.DataAccess
{
    public sealed class EmployeeRecordsMapping : ClassMap<Employee>
    {
        //initialize the field Names
        private const string PaymentStartDateField = "payment_start_date";
        private const string SuperRateField = "super_rate";
        //Constructor
        public EmployeeRecordsMapping()
        {

            AutoMap();

            //Convert payment date time to payment period.
            Map(s => s.PaymentStartDate).ConvertUsing(x =>
             {
                 string date = x.GetField(PaymentStartDateField);
                 PaymentPeriod.TryParseFromDateRangeString(date, out var paymentPeriod);
                 return paymentPeriod;
             }

             );

            //Trim % from inpout record and return percentage.
            Map(x => x.SuperRate).ConvertUsing(x =>
            {
                string superRate = x.GetField(SuperRateField);
                string trimmedValue = superRate.Trim('%');
                if (Decimal.TryParse(trimmedValue, out decimal result))
                {
                    return result / 100;
                }
                throw new InvalidCastException("Unable to parse Super Rate field - Unrecognized  Value : " + superRate);
            }

            );


        }
    }
}
