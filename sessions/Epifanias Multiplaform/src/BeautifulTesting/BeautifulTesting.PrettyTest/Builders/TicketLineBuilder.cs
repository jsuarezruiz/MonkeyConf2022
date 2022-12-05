using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting.PrettyTest.Builders
{
    public class TicketLineBuilder
    {
        public Product product { get; private set; }
        public int units { get; private set; }

        public TicketLineBuilder WithNullProduct()
        {
            product = null!;

            return this;
        }

        public TicketLineBuilder WithProductBeer()
        {
            product = new ProductBuilder().Beer().Build();

            return this;
        }

        public TicketLineBuilder WithProductWater()
        {
            product = new ProductBuilder().Water().Build();

            return this;
        }

        public TicketLineBuilder WithProductBread()
        {
            product = new ProductBuilder().Bread().Build();

            return this;
        }


        public TicketLineBuilder WithUnits(int unit)
        {
            units = unit;

            return this;
        }

        public TicketLine Build()
        {
            return new TicketLine(product, units);
        }
    }
}
