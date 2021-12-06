using EnsureThat;
using PayslipGenerator.BusinessLogic.Exceptions;
using PayslipGenerator.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PayslipGenerator.BusinessLogic
{
    public static class DataValidator
    {
        public static EmployeeDetails IsInputStringValid(string inputString)
        {
            Ensure.String.IsNotNullOrEmpty(inputString?.Trim(), optsFn: o => o.WithException(new PayslipGeneratorExceptions.InputCannotBeNullException()));
            List<string> inputParameters = inputString.Split(" ").ToList();
            Ensure.Comparable.IsGte(inputParameters.Count, 3, optsFn: o => o.WithException(new PayslipGeneratorExceptions.InputFormatIncorrectException()));
            Ensure.String.IsEqualTo(inputParameters[0], "GenerateMonthlyPayslip", optsFn: o => o.WithException(new PayslipGeneratorExceptions.InputCommandKeywordMissingException()));
            var employeeName = string.Join(" ", inputParameters.Skip(1).Take(inputParameters.Count - 2)).Replace("\"","");
            ValidateEmployeeName(employeeName);
            var annualSalary = ValidateEmployeeAnnualSalary(inputParameters.Last());

            return new EmployeeDetails()
            {
                EmployeeName = employeeName,
                AnnualSalary = annualSalary
            };
        }

        public static decimal ValidateEmployeeAnnualSalary(string annualSalary)
        {
            if (!decimal.TryParse(annualSalary, out var convertedAnnualSalary))
                throw new PayslipGeneratorExceptions.InputIncorrectAnnualSalaryException();
            Ensure.Comparable.IsGte(convertedAnnualSalary,0, optsFn: o => o.WithException(new PayslipGeneratorExceptions.AnnualSalaryLessThanZeroException()));

            return convertedAnnualSalary;

        }

        public static bool ValidateEmployeeName(string employeeName)
        { 
            Ensure.String.IsNotNullOrEmpty(employeeName, optsFn: o => o.WithException(new PayslipGeneratorExceptions.EmployeeNameEmptyException()));
            Ensure.String.Matches(employeeName, "^[A-Za-z '-]+$", optsFn: o => o.WithException(new PayslipGeneratorExceptions.EmployeeNameInvalidException()));
            return true;
        }
    }
}