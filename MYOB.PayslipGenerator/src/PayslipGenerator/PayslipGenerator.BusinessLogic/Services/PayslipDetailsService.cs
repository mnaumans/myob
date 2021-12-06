using Microsoft.Extensions.Logging;
using PayslipGenerator.BusinessLogic.Models;
using PayslipGenerator.BusinessLogic.Services.Contracts;

namespace PayslipGenerator.BusinessLogic.Services
{
    public class PayslipDetailsService : IPayslipDetails
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        private readonly ILogger<PayslipDetailsService> _logger;
        /// <summary>
        /// Gets GrossIncomeCalculator Service
        /// </summary>
        private readonly IGrossIncomeCalculator _grossIncomeCalculator;
        /// <summary>
        /// Gets IncomeTaxCalculator Service
        /// </summary>
        private readonly IIncomeTaxCalculator _incomeTaxCalculator;

        public PayslipDetailsService(ILogger<PayslipDetailsService> logger, IGrossIncomeCalculator grossIncomeCalculator, IIncomeTaxCalculator incomeTaxCalculator)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _grossIncomeCalculator = grossIncomeCalculator ?? throw new System.ArgumentNullException(nameof(grossIncomeCalculator));
            _incomeTaxCalculator = incomeTaxCalculator ?? throw new System.ArgumentNullException(nameof(incomeTaxCalculator));
        }

        public string GetMonthlyPayslip(EmployeeDetails employeeDetails) {
            DataValidator.ValidateEmployeeName(employeeDetails.EmployeeName?.Trim());
            DataValidator.ValidateEmployeeAnnualSalary(employeeDetails.AnnualSalary.ToString());

            var grossMonthlyIncome = _grossIncomeCalculator.GetMonthlyGrossIncome(employeeDetails.AnnualSalary);
            var monthlyIncomeTax = _incomeTaxCalculator.GetMonthlyIncomeTax(employeeDetails.AnnualSalary);
            var netMonthlyIncome = grossMonthlyIncome - monthlyIncomeTax;

            return $"Monthly Payslip for: \"{employeeDetails.EmployeeName.Trim()}\"\nGross Monthly Income: {grossMonthlyIncome:$#.##}\nMonthly Income Tax: {monthlyIncomeTax:$#.##}\nNet Monthly Income: {netMonthlyIncome:$#.##}";
        }
    }
}