using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting
{
    public class CountryTaxes
    {
        public string IsoCode { get; private set; }
        public decimal GeneralTax { get; private set; }
        public decimal ReducedTax { get; private set; }
        public decimal SuperReduceTax { get; private set; }

        public CountryTaxes(string isoCode, decimal generalTax, decimal reducedTax, decimal superReduceTax)
        {
            IsoCode = isoCode;
            GeneralTax = generalTax;
            ReducedTax = reducedTax;
            SuperReduceTax = superReduceTax;
        }
    }
}
