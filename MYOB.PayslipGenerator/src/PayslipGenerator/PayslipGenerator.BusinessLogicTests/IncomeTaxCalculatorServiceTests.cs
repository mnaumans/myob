using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PayslipGenerator.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayslipGenerator.BusinessLogicTests
{
    public class IncomeTaxCalculatorServiceTests
    {
        private readonly IncomeTaxCalculatorService _serviceUnderTesting;
        private readonly Mock<ILogger<IncomeTaxCalculatorService>> _mockLogger;

        public IncomeTaxCalculatorServiceTests()
        {
            _mockLogger = new Mock<ILogger<IncomeTaxCalculatorService>>();
            _serviceUnderTesting = new IncomeTaxCalculatorService(_mockLogger.Object);
        }

        [Theory]
        [InlineData(60000, "500")]
        [InlineData(120000, "1833.33")]
        [InlineData(140000, "2333.33")]
        public void GetMonthlyIncomeTax_ReturnsCorrectMonthlyIncomeTaxDeduction(decimal annualSalary, string monthlyIncomeTax)
        {
            var result = _serviceUnderTesting.GetMonthlyIncomeTax(annualSalary);

            result.Should().BeOfType(typeof(decimal));
            Assert.Equal(result.ToString("#.##"), monthlyIncomeTax);
        }
    }
}
