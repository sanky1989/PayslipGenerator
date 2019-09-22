using System;
using Xunit;
using PayslipGenerator.Model;
namespace PayslipGenerator.Tests
{
    public class DecimalRoundingTests
    {
        [Fact]
        public void CheckIsWholeNumber_ReturnFalse()
        {   
            Assert.False(0.05M.IsWholeNumber());
            Assert.False(1.5M.IsWholeNumber());
        }

        [Fact]
        public void CheckIsWholeNumber_ReturnTrue()
        {
            Assert.True(1.0M.IsWholeNumber());
        }

        [Fact]
        public void CheckRounding_CheckValue_ToNearDollarAmount()
        {
            Assert.Equal(1.0M, 0.51M.RoundToNearestDollarAmount());
            Assert.Equal(1.0M,0.5M.RoundToNearestDollarAmount());
            Assert.Equal(0.0M, 0.49M.RoundToNearestDollarAmount());
        }
    }
}
