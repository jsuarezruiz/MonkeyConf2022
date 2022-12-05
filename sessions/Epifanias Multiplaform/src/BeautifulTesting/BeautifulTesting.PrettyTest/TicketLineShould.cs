using BeautifulTesting.PrettyTest.Builders;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting.PrettyTest
{
    public class TicketLineShould
    {
        [Fact]
        public void NullProductShouldThrowArgumentNullException()
        {
            Action create = () => _ = new TicketLineBuilder().WithNullProduct().WithUnits(1).Build();
            create.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ZeroUnitsShouldThrowArgumentException()
        {
            Action create = () => _ = new TicketLineBuilder().WithProductBeer().WithUnits(0).Build();
            create.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void NegativeUnitsShouldThrowArgumentException()
        {
            Action create = () => _ = new TicketLineBuilder().WithProductBeer().WithUnits(-1).Build();
            create.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AddProductShouldCalculateCorrectTotal()
        {
            TicketLine line = new TicketLineBuilder().WithProductBeer().WithUnits(5).Build();
            line.Total.Should().Be(20);
        }
    }
}
