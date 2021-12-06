using Microsoft.Extensions.Logging;
using Moq;
using PayslipGenerator.BusinessLogic.Exceptions;
using PayslipGenerator.BusinessLogic.Models;
using PayslipGenerator.BusinessLogic.Services;
using PayslipGenerator.BusinessLogic.Services.Contracts;
using Xunit;

namespace PayslipGenerator.BusinessLogicTests
{
    public class PayslipDetailsServiceTests
    {
        private readonly PayslipDetailsService _serviceUnderTesting;
        private readonly Mock<ILogger<PayslipDetailsService>> _mockLogger;
        private readonly Mock<IIncomeTaxCalculator> _mockIncomeTaxCalculator;
        private readonly Mock<IGrossIncomeCalculator> _mockGrossIncomeCalculator;

        public PayslipDetailsServiceTests()
        {
            _mockLogger = new Mock<ILogger<PayslipDetailsService>>();
            _mockGrossIncomeCalculator = new Mock<IGrossIncomeCalculator>();
            _mockIncomeTaxCalculator = new Mock<IIncomeTaxCalculator>();

            _serviceUnderTesting = new PayslipDetailsService(_mockLogger.Object, _mockGrossIncomeCalculator.Object, _mockIncomeTaxCalculator.Object);
        }

        [Fact]
        public void GetMonthlyPayslip_GeneratesPayslipStringSuccessfully()
        {
            var employeeDetails = new EmployeeDetails()
            {
                EmployeeName = "Nauman Shaukat",
                AnnualSalary = 155000
            };
            _mockGrossIncomeCalculator.Setup(x =>x.GetMonthlyGrossIncome(employeeDetails.AnnualSalary)).Returns(12916.67M);
            _mockIncomeTaxCalculator.Setup(x => x.GetMonthlyIncomeTax(employeeDetails.AnnualSalary)).Returns(2708.33M);
            var result = _serviceUnderTesting.GetMonthlyPayslip(employeeDetails);

            result.Contains($"Monthly Payslip for: \"Nauman Shaukat\"\nGross Monthly Income: $12916.67\nMonthly Income Tax: $2708.33\nNet Monthly Income: $10208.33");
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void GetMonthlyPayslip_InvalidEmployeeNameThrowsException(string employeeName)
        {
            var employeeDetails = new EmployeeDetails()
            {
                EmployeeName = employeeName,
                AnnualSalary = 60000
            };

            var result = Assert.Throws<PayslipGeneratorExceptions.EmployeeNameEmptyException>(() => _serviceUnderTesting.GetMonthlyPayslip(employeeDetails));

            Assert.IsType<PayslipGeneratorExceptions.EmployeeNameEmptyException>(result);
            Assert.Contains("Employee Name is required.", result.ErrorMessage);
        }

        [Fact]
        public void GetMonthlyPayslip_NegativeAnnualSalaryThrowsException()
        {
            var employeeDetails = new EmployeeDetails()
            {
                EmployeeName = "Mary Song",
                AnnualSalary = -20
            };

            var result = Assert.Throws<PayslipGeneratorExceptions.AnnualSalaryLessThanZeroException>(() => _serviceUnderTesting.GetMonthlyPayslip(employeeDetails));

            Assert.IsType<PayslipGeneratorExceptions.AnnualSalaryLessThanZeroException>(result);
            Assert.Contains("Annual Salary cannot be less than 0.", result.ErrorMessage);
        }


    }
}
