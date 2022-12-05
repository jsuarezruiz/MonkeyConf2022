using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting
{
    public class Ticket
    {
        private CountryTaxes countryTaxes;

        private List<TicketLine> lines;

        private decimal total = 0;
         
        private decimal totalGeneralTax = 0;
        private decimal totalReducedTax = 0;
        private decimal totalSuperReducedTax = 0;

        public decimal Total { get { return total; } }
        public decimal TotalGeneralTax { get { return totalGeneralTax; } }
        public decimal TotalReducedTax { get { return totalReducedTax; } }
        public decimal TotalSuperReducedTax { get { return totalSuperReducedTax; } }

        public int LinesNumber { get { return lines.Count; } }

        public Ticket(CountryTaxes countryTaxes)
        {
            this.countryTaxes = countryTaxes;

            lines = new List<TicketLine>();
        }

        public void Add(TicketLine line)
        { 
            lines.Add(line);
        }

        public void CalculateTotals()
        {
            total = lines.Sum(x => x.Units * x.Product.Price);
            totalGeneralTax = lines.Where(x=> x.Product.Tax == TaxType.General).Sum(x => x.Units * x.Product.Price)*(countryTaxes.GeneralTax/100);
            totalReducedTax = lines.Where(x => x.Product.Tax == TaxType.Reduced).Sum(x => x.Units * x.Product.Price) * (countryTaxes.ReducedTax / 100);
            totalSuperReducedTax = lines.Where(x => x.Product.Tax == TaxType.SuperReduced).Sum(x => x.Units * x.Product.Price) * (countryTaxes.SuperReduceTax / 100);
        }
    }
}
