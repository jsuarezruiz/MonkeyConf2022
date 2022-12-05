using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting.PrettyTest.Builders
{
    public class TicketBuilder
    {
        private CountryTaxes countryTaxes;
        private List<TicketLine> lines;

        public TicketBuilder()
        {
            lines = new List<TicketLine>();
        }

        public TicketBuilder WithSpainTaxes()
        {
            countryTaxes = new CountryTaxesBuilder().Spain().Build();
            return this;
        }

        public TicketBuilder WithPortugalTaxes()
        {
            countryTaxes = new CountryTaxesBuilder().Portugal().Build();
            return this;
        }

        public TicketBuilder WithOneTicketLine()
        {
            lines.Add(new TicketLineBuilder().WithProductBeer().WithUnits(6).Build());

            return this;
        }

        public TicketBuilder WithTwoTicketLine()
        {
            lines.Add(new TicketLineBuilder()
                .WithProductBeer()
                .WithUnits(6)
                .Build());
            lines.Add(new TicketLineBuilder()
                .WithProductBeer()
                .WithUnits(24)
                .Build());

            return this;
        }

        public TicketBuilder WithLinesTestTotals()
        {
            lines.Add(new TicketLineBuilder()
                .WithProductWater()
                .WithUnits(6)
                .Build());
            lines.Add(new TicketLineBuilder()
                .WithProductBeer()
                .WithUnits(24)
                .Build());
            lines.Add(new TicketLineBuilder()
                .WithProductBread()
                .WithUnits(4)
                .Build());

            return this;
        }

        public Ticket Build()
        {
            Ticket ticket = new Ticket(countryTaxes);
            foreach (TicketLine line in lines)
                ticket.Add(line);

            return ticket;
        }
    }
}
