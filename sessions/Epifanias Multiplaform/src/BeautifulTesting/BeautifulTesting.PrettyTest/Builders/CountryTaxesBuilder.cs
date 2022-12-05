using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting.PrettyTest.Builders
{
    public class CountryTaxesBuilder
    {
        public string isoCode { get; private set; }
        public decimal generalTax { get; private set; }
        public decimal reducedTax { get; private set; }
        public decimal superReduceTax { get; private set; }

        public CountryTaxesBuilder Spain()
        {
            isoCode = "ES";
            generalTax = 21;
            reducedTax = 10;
            superReduceTax = 4;

            return this;
        }

        public CountryTaxesBuilder Portugal()
        {
            isoCode = "PT";
            generalTax = 23;
            reducedTax = 13;
            superReduceTax = 6;

            return this;
        }

        public CountryTaxes Build()
        {
            return new CountryTaxes(isoCode, generalTax, reducedTax, superReduceTax);
        }
    }
}
