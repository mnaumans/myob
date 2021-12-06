using Microsoft.Extensions.Logging;
using PayslipGenerator.BusinessLogic.Models;
using PayslipGenerator.BusinessLogic.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayslipGenerator.BusinessLogic.Services
{
    public class IncomeTaxCalculatorService : IIncomeTaxCalculator
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        private readonly ILogger<IncomeTaxCalculatorService> _logger;

        public IncomeTaxCalculatorService(ILogger<IncomeTaxCalculatorService> logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        private static readonly List<TaxBracket> _taxBrackets = new()
        {
            new()
            {
                LowerLimit = 0,
                UpperLimit = 20000,
                PreviousBandUpperLimit = 0,
                DeductionRate = 0
            },
            new()
            {
                LowerLimit = 20001,
                UpperLimit = 40000,
                PreviousBandUpperLimit = 20000,
                DeductionRate = 0.1M
            },
            new()
            {
                LowerLimit = 40001,
                UpperLimit = 80000,
                PreviousBandUpperLimit = 40000,
                DeductionRate = 0.2M
            },
            new()
            {
                LowerLimit = 80001,
                UpperLimit = 180000,
                PreviousBandUpperLimit = 80000,
                DeductionRate = 0.3M
            },
            new()
            {
                LowerLimit = 180001,
                UpperLimit = decimal.MaxValue,
                PreviousBandUpperLimit = 80000,
                DeductionRate = 0.4M
            }
        };

        public decimal GetMonthlyIncomeTax(decimal annualSalary)
        {
            DataValidator.ValidateEmployeeAnnualSalary(annualSalary.ToString());

            var annualIncomeTax = (from t in _taxBrackets
                                   where annualSalary >= t.LowerLimit || (annualSalary >= t.LowerLimit && annualSalary <= t.UpperLimit)
                                   select new
                                   {
                                       taxPerBracket = CalculateDeductionPerBracket(t, annualSalary)
                                   }
                                   ).Sum(x => x.taxPerBracket);

            var monthlyIncomeTax = annualIncomeTax / 12;

            return monthlyIncomeTax;
        }

        private static decimal CalculateDeductionPerBracket(TaxBracket t, decimal annualSalary)
        {
            return ((annualSalary > t.UpperLimit ? t.UpperLimit : annualSalary) - t.PreviousBandUpperLimit) * t.DeductionRate;
        }
    }
}