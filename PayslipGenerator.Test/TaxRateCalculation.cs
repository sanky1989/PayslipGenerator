using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PayslipGenerator.Model;

namespace PayslipGenerator.Tests
{
    public class TaxRateCalculation
    {
        [Fact]
        public void CalculateTax()
        {
            Assert.Equal(922M, TaxRate.CreateTaxRate(37001M, 87000M, 0.325M, 3572M).CalculateTax(60050M));
            Assert.Equal(650M, TaxRate.CreateTaxRate(37001M, 87000M, 0.325M, 3572M).CalculateTax(50000M));

        }

        [Fact]
        public void CalculateMinimumTax_ReturnZero()
        {
            Assert.Equal(0M, TaxRate.CreateTaxRate(0M, 18200M).CalculateTax(18200M));
            Assert.Equal(0M, TaxRate.CreateTaxRate(37001M, 87000M).CalculateTax(60050M));
        }

        [Fact]
        public void CalculateTax_ReturnException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TaxRate.CreateTaxRate(-1M, 0M));
            Assert.Throws<ArgumentOutOfRangeException>(() => TaxRate.CreateTaxRate(0M, -1M));
            Assert.Throws<ArgumentOutOfRangeException>(() => TaxRate.CreateTaxRate(2M, 1M));
            Assert.Throws<ArgumentOutOfRangeException>(() => TaxRate.CreateTaxRate(1M, 2M, null, -1M));
        }

        [Fact]
        public void CalculateTax_ReturnTaxOverMinIncome()
        {
            Assert.Equal(0M, TaxRate.CreateTaxRate(18201M, 37000M).CalculateTaxMinIncome(51000M));
            Assert.Equal(6232M, TaxRate.CreateTaxRate(18201M, 37000M, 0.19M).CalculateTaxMinIncome(51000M));
        }

        [Fact]
        public void CalculateTax_WithinMinimumRange()
        {
            Assert.True(TaxRate.CreateTaxRate(37001M, 87000M).IsWithinIncomeRange(51000M));
            Assert.False(TaxRate.CreateTaxRate(18001M, 37000M).IsWithinIncomeRange(51000M));
        }


        [Fact]
        public void CalculateTax_ReturnBaseTax()
        {

            Assert.Equal(187M, TaxRate.CreateTaxRate(37001M, 87000M, baseTaxAmount: 2242M).CalculateTax(51000M));
        }


    }
}
