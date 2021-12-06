using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipGenerator.BusinessLogic.Exceptions
{
    public static class PayslipGeneratorExceptions
    {
        public abstract class PayslipGeneratorException : Exception
        {
            public string ErrorMessage { get; set; }
        }

        public class InputCannotBeNullException : PayslipGeneratorException
        {
            public InputCannotBeNullException()
            {
                ErrorMessage = "Input cannot be null or empty.";
            }
        }

        public class InputFormatIncorrectException : PayslipGeneratorException
        {
            public InputFormatIncorrectException()
            {
                ErrorMessage = "Input should be of the following format 'GenerateMonthlyPayslip \"Employee Name\" #####'.";
            }
        }

        public class InputCommandKeywordMissingException : PayslipGeneratorException
        {
            public InputCommandKeywordMissingException()
            {
                ErrorMessage = "No matching command keyword found.";
            }
        }

        public class InputIncorrectAnnualSalaryException : PayslipGeneratorException
        {
            public InputIncorrectAnnualSalaryException()
            {
                ErrorMessage = "Invalid Annual Salary provided.";
            }
        }

        public class AnnualSalaryLessThanZeroException : PayslipGeneratorException
        {
            public AnnualSalaryLessThanZeroException()
            {
                ErrorMessage = "Annual Salary cannot be less than 0.";
            }
        }

        public class EmployeeNameInvalidException : PayslipGeneratorException
        {
            public EmployeeNameInvalidException()
            {
                ErrorMessage = "Invalid Employee Name, can only have alphabets, spaces, hyphen or apostrophe.";
            }
        }

        public class EmployeeDetailsObjectEmptyException : PayslipGeneratorException
        {
            public EmployeeDetailsObjectEmptyException()
            {
                ErrorMessage = "Invalid Employee Details object.";
            }
        }

        public class EmployeeNameEmptyException : PayslipGeneratorException
        {
            public EmployeeNameEmptyException()
            {
                ErrorMessage = "Employee Name is required.";
            }
        }

    }
}
