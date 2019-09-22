using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipGenerator.Model
{
    public static class DecimalRounding
    {
        public static bool IsWholeNumber(this decimal @decimalValue)
        {
            return (@decimalValue % 1M) == 0M;
        }

        public static decimal RoundToNearestDollarAmount(this decimal @decimalValue)
        {
            if ((@decimalValue % 1) >= 0.5M)
            {
                return Math.Ceiling(@decimalValue);
            
            }
            return Math.Floor(@decimalValue);
        }

    }
}
