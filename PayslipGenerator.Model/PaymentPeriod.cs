using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipGenerator.Model
{
    public sealed class PaymentPeriod
    {
        /*
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }

        internal PaymentPeriod() { }
        public PaymentPeriod(DateTime startDateTime, DateTime endDateTime)
        {
            StartDate = startDateTime;
            EndDate = endDateTime;
        }

        public static bool TryParseFromDateRangeString(string dateRange, out PaymentPeriod paymentPeriod)
        {
            DateTime _startDateTime = DateTime.Now;
            DateTime _endDateTime = DateTime.Now;

            string[] paymentDateTime = dateRange.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (paymentDateTime[0] != null)
            {
                DateTime.TryParse(paymentDateTime[0], out _startDateTime);

                if (paymentDateTime[1] != null)
                {
                    DateTime.TryParse(paymentDateTime[1], out _endDateTime);
                    paymentPeriod = new PaymentPeriod(_startDateTime, _endDateTime);
                    return true;
                }
            }
            paymentPeriod = default(PaymentPeriod);
            return false;
        }

        public override string ToString()
        {
            return $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
        }*/


        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }

        internal PaymentPeriod() { }
        public PaymentPeriod(DateTime startDateTime, DateTime endDateTime)
        {
            StartDate = startDateTime;
            EndDate = endDateTime;
        }

        public static bool TryParseFromDateRangeString(string dateRange, out PaymentPeriod paymentPeriod)
        {
            var parts = dateRange.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (DateTime.TryParse(parts[0], out var startDateTime))
            {
                if (DateTime.TryParse(parts[1], out var endDateTime))
                {
                    paymentPeriod = new PaymentPeriod(startDateTime, endDateTime);
                    return true;
                }
            }

            paymentPeriod = default(PaymentPeriod);
            return false;
        }

        public override string ToString()
        {
            return $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
        }
    }
}
