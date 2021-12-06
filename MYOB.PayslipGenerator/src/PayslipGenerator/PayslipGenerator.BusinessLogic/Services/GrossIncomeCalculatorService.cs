using Microsoft.Extensions.Logging;
using PayslipGenerator.BusinessLogic.Models;
using PayslipGenerator.BusinessLogic.Services.Contracts;

namespace PayslipGenerator.BusinessLogic.Services
{
    public class GrossIncomeCalculatorService : IGrossIncomeCalculator
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        private readonly ILogger<GrossIncomeCalculatorService> _logger;

        public GrossIncomeCalculatorService(ILogger<GrossIncomeCalculatorService> logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public decimal GetMonthlyGrossIncome(decimal annualSalary)
        {
            DataValidator.ValidateEmployeeAnnualSalary(annualSalary.ToString());
            return annualSalary / 12;
        }

    }
}