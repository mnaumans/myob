using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PayslipGenerator.BusinessLogic.Exceptions;
using PayslipGenerator.BusinessLogic.Models;
using PayslipGenerator.BusinessLogic.Services;
using PayslipGenerator.BusinessLogic.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayslipGenerator.BusinessLogicTests
{
    public class GrossIncomeCalculatorServiceTests
    {
        private readonly GrossIncomeCalculatorService _serviceUnderTesting;
        private readonly Mock<ILogger<GrossIncomeCalculatorService>> _mockLogger;

        public GrossIncomeCalculatorServiceTests()
        {
            _mockLogger = new Mock<ILogger<GrossIncomeCalculatorService>>();
            _serviceUnderTesting = new GrossIncomeCalculatorService(_mockLogger.Object);
        }

        [Theory]
        [InlineData(60000, 5000)]
        [InlineData(120000, 10000)]
        public void GetMonthlyGrossIncome_ReturnsCorrectMonthlyGrossIncome(decimal annualSalary, decimal monthlyGrossIncome)
        {
            var result = _serviceUnderTesting.GetMonthlyGrossIncome(annualSalary);

            result.Should().BeOfType(typeof(decimal));
            Assert.Equal(result, monthlyGrossIncome);
        }
    }
}
