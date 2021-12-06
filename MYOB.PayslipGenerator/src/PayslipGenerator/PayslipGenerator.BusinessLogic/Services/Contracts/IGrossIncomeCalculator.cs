using PayslipGenerator.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipGenerator.BusinessLogic.Services.Contracts
{
    public interface IGrossIncomeCalculator
    {
        public decimal GetMonthlyGrossIncome(decimal annualSalary);
    }
}
