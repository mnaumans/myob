using FluentAssertions;
using PayslipGenerator.BusinessLogic;
using PayslipGenerator.BusinessLogic.Exceptions;
using PayslipGenerator.BusinessLogic.Models;
using Xunit;

namespace PayslipGenerator.BusinessLogicTests
{
    public class DataValidatorTests
    {
        [Fact]
        public void IsInputStringValid_ReturnsEmployeeDetailsObjectSuccessfully()
        {
            var result = DataValidator.IsInputStringValid($"GenerateMonthlyPayslip \"Mary Song\" 60000");

            result.Should().BeOfType(typeof(EmployeeDetails));
            result.AnnualSalary.Should().Be(60000);
            result.EmployeeName.Should().Be("Mary Song");
        }

        [Fact]
        public void IsInputStringValid_ThrowsExceptionOnMissingCommandKeyword()
        {
            var result = Assert.Throws<PayslipGeneratorExceptions.InputCommandKeywordMissingException>(() => DataValidator.IsInputStringValid($"GenerateMonthlyPay \"Mary Song\" 60000"));

            Assert.IsType<PayslipGeneratorExceptions.InputCommandKeywordMissingException>(result);
            Assert.Contains("No matching command keyword found.", result.ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void IsInputStringValid_EmptyOrNullInputString(string inputString)
        {
            var result = Assert.Throws<PayslipGeneratorExceptions.InputCannotBeNullException>( () => DataValidator.IsInputStringValid(inputString));

            Assert.IsType<PayslipGeneratorExceptions.InputCannotBeNullException>(result);
            Assert.Contains("Input cannot be null or empty.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("GenerateMonthlyPayslip")]
        [InlineData("GenerateMonthlyPayslip 60000")]
        public void IsInputStringValid_InvalidInputString(string inputString)
        {
            var result = Assert.Throws<PayslipGeneratorExceptions.InputFormatIncorrectException>(() => DataValidator.IsInputStringValid(inputString));

            Assert.IsType<PayslipGeneratorExceptions.InputFormatIncorrectException>(result);
            Assert.Contains("Input should be of the following format 'GenerateMonthlyPayslip \"Employee Name\" #####'.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("6000")]
        [InlineData("600000")]
        [InlineData("6000.00")]
        [InlineData("6000.1")]
        [InlineData("0.1")]
        public void ValidateEmployeeAnnualSalary_ValidAnnualSalaryString(string annualSalary)
        {
            var result = DataValidator.ValidateEmployeeAnnualSalary(annualSalary);

            result.Should().BeOfType(typeof(decimal));
            Assert.Equal(result.ToString(), annualSalary);
        }

        [Theory]
        [InlineData("60.00.00")]
        [InlineData("a00.1")]
        public void ValidateEmployeeAnnualSalary_InValidAnnualSalaryString(string annualSalary)
        {
            var result = Assert.Throws<PayslipGeneratorExceptions.InputIncorrectAnnualSalaryException>(() => DataValidator.ValidateEmployeeAnnualSalary(annualSalary));

            Assert.IsType<PayslipGeneratorExceptions.InputIncorrectAnnualSalaryException>(result);
            Assert.Contains("Invalid Annual Salary provided", result.ErrorMessage);
        }

        [Theory]
        [InlineData("600000")]
        [InlineData("a00.1")]
        [InlineData("a/b")]
        [InlineData("a.b")]
        [InlineData("a+b")]
        public void ValidateEmployeeName_InValidEmployeeNameString(string employeeName)
        {
            var result = Assert.Throws<PayslipGeneratorExceptions.EmployeeNameInvalidException>(() => DataValidator.ValidateEmployeeName(employeeName));

            Assert.IsType<PayslipGeneratorExceptions.EmployeeNameInvalidException>(result);
            Assert.Contains("Invalid Employee Name, can only have alphabets, spaces, hyphen or apostrophe.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("Mary")]
        [InlineData("Mary Song")]
        [InlineData("Mary'O Song")]
        [InlineData("Mary-Song")]
        public void ValidateEmployeeName_ValidEmployeeNameString(string employeeName)
        {
            var result = DataValidator.ValidateEmployeeName(employeeName);

            result.Should().BeTrue();
        }
    }
}
