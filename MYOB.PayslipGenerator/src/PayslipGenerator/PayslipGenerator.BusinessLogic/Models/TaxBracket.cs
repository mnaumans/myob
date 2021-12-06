using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipGenerator.BusinessLogic.Models
{
    public class TaxBracket
    {
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal PreviousBandUpperLimit { get; set; }
        public decimal DeductionRate { get; set; }
    }
}
