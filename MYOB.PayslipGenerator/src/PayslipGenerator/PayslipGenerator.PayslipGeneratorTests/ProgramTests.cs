using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PayslipGenerator.BusinessLogic.Services;
using PayslipGenerator.BusinessLogic.Services.Contracts;
using System;
using Xunit;

namespace PayslipGenerator.PayslipGeneratorTests
{
    public class ProgramTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<IGrossIncomeCalculator> _mockGrossIncomeCalculator;
        private readonly Mock<IIncomeTaxCalculator> _mockIncomeTaxCalculator;

        public ProgramTests()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            _mockGrossIncomeCalculator = new Mock<IGrossIncomeCalculator>();
            _mockIncomeTaxCalculator = new Mock<IIncomeTaxCalculator>();

            serviceProvider.Setup(x => x.GetService(typeof(IPayslipDetails))).Returns(new PayslipDetailsService(Mock.Of<ILogger<PayslipDetailsService>>(),
                _mockGrossIncomeCalculator.Object, _mockIncomeTaxCalculator.Object));

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);

            _serviceProvider = serviceProvider.Object;
        }
        [Fact]
        public void GeneratePayslip_ShouldReturnSuccess()
        {
            _mockGrossIncomeCalculator.Setup(x => x.GetMonthlyGrossIncome(It.IsAny<decimal>())).Returns(5000.00M);
            _mockIncomeTaxCalculator.Setup(x => x.GetMonthlyIncomeTax(It.IsAny<decimal>())).Returns(500.00M);
            var result = Program.GeneratePayslip(_serviceProvider, $"GenerateMonthlyPayslip \"Mary Song\" 60000");

            result.Contains($"Monthly Payslip for: \"Mary Song\"\nGross Monthly Income: $5000.00\nMonthly Income Tax: $500.00\nNet Monthly Income: $4500.00");

        }
    }
}
