using System;
using System.Collections.Generic;
using System.Text;
using PayslipGenerator.Model;

namespace PayslipGenerator.Model
{
    public class TaxRate
    {
        private decimal MinIncome { get; }
        private decimal? MaxIncome { get; }
        private decimal? BaseTaxAmount { get; }
        private decimal? RateValue { get; }
        private const int TotalMonths = 12;

        //Constructors
        private TaxRate(decimal minIncomeAmount, decimal? maxIncomeAmount = null, decimal? rateValue = null, decimal? baseTaxAmount = null)
        {
            #region Validations
            if (minIncomeAmount < 0M)
            {
                throw new ArgumentOutOfRangeException(nameof(minIncomeAmount), $"{nameof(minIncomeAmount)} should be a positive value");
            }
            if (maxIncomeAmount.HasValue && maxIncomeAmount.Value < 0M)
            {
                throw new ArgumentOutOfRangeException(nameof(maxIncomeAmount), $"{nameof(maxIncomeAmount)} should be a positive value");
            }
            if (baseTaxAmount.HasValue && baseTaxAmount < 0M)
            {
                throw new ArgumentOutOfRangeException(nameof(baseTaxAmount), $"{nameof(baseTaxAmount)} should be a positive value");
            }

            if (maxIncomeAmount < minIncomeAmount)
            {
                throw new ArgumentOutOfRangeException(nameof(maxIncomeAmount), $"{nameof(maxIncomeAmount)} should be larger than {nameof(minIncomeAmount)}");
            }

            #endregion

            MinIncome = minIncomeAmount;
            MaxIncome = maxIncomeAmount;
            BaseTaxAmount = baseTaxAmount;
            RateValue = rateValue;
        }

        public static TaxRate CreateTaxRate(decimal minIncomeAmount, decimal? maxIncomeAmount = null, decimal? rateValue = null, decimal? baseTaxAmount = null)
        {
            return new TaxRate(minIncomeAmount, maxIncomeAmount, rateValue, baseTaxAmount);
        }

        public bool IsWithinIncomeRange(decimal salary)
        {
            return (salary >= MinIncome && (!MaxIncome.HasValue || (salary <= MaxIncome)));
        }

        public decimal CalculateTax(decimal salary)
        {

            if (!BaseTaxAmount.HasValue && !RateValue.HasValue)
            {
                //Tax amount is 0

                return 0.0M;
            }

            var basetax = BaseTaxAmount.Value;
            var incomeTax = (basetax + CalculateTaxMinIncome(salary)) / TotalMonths;

            return incomeTax.RoundToNearestDollarAmount();

        }
        public decimal CalculateTaxMinIncome(decimal salary)
        {
            decimal minIncomeThresholdAmount;
            if (!RateValue.HasValue)
            {
                return 0.0M;
            }
            else
            {
                var taxRate = RateValue.Value;
                if (MinIncome > 0)
                {
                    minIncomeThresholdAmount = MinIncome - 1M;
                }
                else
                {
                    minIncomeThresholdAmount = MinIncome;
                }
                return ((salary - minIncomeThresholdAmount) * taxRate);
            }
        }

    }
}
