using BeautifulTesting.PrettyTest.Builders;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting.PrettyTest
{
    public class TicketShould
    {
        [Fact]
        public void NewTicketShouldHaveCorrectProductsNumber()
        {
            Ticket ticket = new TicketBuilder().WithSpainTaxes().Build();

            ticket.LinesNumber.Should().Be(0);
        }

        [Fact]
        public void AddOneLineToTicketShouldHaveCorrectProductsNumber()
        {
            Ticket ticket = new TicketBuilder().WithSpainTaxes().WithOneTicketLine().Build();

            ticket.LinesNumber.Should().Be(1);
        }

        [Fact]
        public void AddTwoLineToTicketShouldHaveCorrectProductsNumber()
        {
            Ticket ticket = new TicketBuilder().WithSpainTaxes().WithTwoTicketLine().Build();

            ticket.LinesNumber.Should().Be(2);
        }

        [Fact]
        public void FinaliceSpanishTicketShouldCalculateTotalsCorrectly()
        {
            Ticket ticket = new TicketBuilder().WithSpainTaxes().WithLinesTestTotals().Build();
            
            ticket.CalculateTotals();

            ticket.Total.Should().Be(110.0m);
            ticket.TotalGeneralTax.Should().Be(20.16m);
            ticket.TotalReducedTax.Should().Be(1.2m);
            ticket.TotalSuperReducedTax.Should().Be(0.08m);
        }

        [Fact]
        public void FinalicePortugalTicketShouldCalculateTotalsCorrectly()
        {
            Ticket ticket = new TicketBuilder()
                .WithPortugalTaxes()
                .WithLinesTestTotals()
                .Build();

            ticket.CalculateTotals();

            ticket.Total.Should().Be(110.0m);
            ticket.TotalGeneralTax.Should().Be(22.08m);
            ticket.TotalReducedTax.Should().Be(1.56m);
            ticket.TotalSuperReducedTax.Should().Be(0.12m);
        }
    }
}
