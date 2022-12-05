using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting
{
    public class TicketLine
    {
        public Product Product { get; private set; }
        public int Units { get; private set; }
        public decimal Total { get; private set; }

        public TicketLine(Product product, int units) 
        { 
            ArgumentNullException.ThrowIfNull(product);

            if (units == 0)
                throw new ArgumentException("Zero value not allowed", nameof(units));

            if (units < 0)
                throw new ArgumentException("Negative value not allowed", nameof(units));

            Product = product;
            Units = units;

            Total = Product.Price * units;
        } 
    }
}
