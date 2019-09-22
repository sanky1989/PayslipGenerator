using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipGenerator.Model
{
    public static class Enumerations
    {
        public static TaxRate[] TaxRates => new[]
            {
             TaxRate.CreateTaxRate(0M,18_200M,null,null),
             TaxRate.CreateTaxRate(18_201M,37_000M,0.19M,null),
             TaxRate.CreateTaxRate(37_001M,87_000M,0.325M,3_572M),
             TaxRate.CreateTaxRate(87_001M,180_000M,0.37M,19_822M),
             TaxRate.CreateTaxRate(180_001M,null,0.45M,54_232M),
            };
    }
}
