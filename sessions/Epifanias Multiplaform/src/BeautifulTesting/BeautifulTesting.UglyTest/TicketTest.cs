using System.Xml.Schema;

namespace BeautifulTesting.UglyTest
{
    [TestClass]
    public class TicketTest
    {
        [TestMethod]
        public void NewTicketShouldHaveCorrectProductsNumber()
        {
            CountryTaxes countryTaxes = new CountryTaxes("ES", 21, 10, 4);

            Ticket ticket = new(countryTaxes);

            Assert.AreEqual(0, ticket.LinesNumber);
        }

        [TestMethod]
        public void AddOneLineToTicketShouldHaveCorrectProductsNumber()
        {
            CountryTaxes countryTaxes = new CountryTaxes("ES", 21, 10, 4);

            Product product = new ("Water", "It is pure, crystal-clear water", 2, TaxType.Reduced);

            TicketLine ticketLine = new(product, 6);

            Ticket ticket = new(countryTaxes);

            ticket.Add(ticketLine);

            Assert.AreEqual( 1, ticket.LinesNumber);
        }

        [TestMethod]
        public void AddTwoLineToTicketShouldHaveCorrectProductsNumber()
        {
            CountryTaxes countryTaxes = new CountryTaxes("ES", 21, 10, 4);

            Product product = new("Water", "It is pure, crystal-clear water", 2, TaxType.Reduced);
            Product product2 = new("Beer", "It is BEER, yijaaaa!!!", 4, TaxType.General);

            TicketLine ticketLine = new(product, 6);
            TicketLine ticketLine2 = new(product2, 24);

            Ticket ticket = new(countryTaxes);

            ticket.Add(ticketLine);
            ticket.Add(ticketLine);

            Assert.AreEqual(2, ticket.LinesNumber);
        }

        [TestMethod]
        public void FinaliceSpanishTicketShouldCalculateTotalsCorrectly()
        {
            CountryTaxes countryTaxes = new CountryTaxes("ES",21,10,4);
            
            Product product = new("Water", "It is pure, crystal-clear water", 2, TaxType.Reduced);
            Product product2 = new("Beer", "It is BEER, yijaaaa!!!", 4, TaxType.General);
            Product product3 = new("Bread", "It´s only bread", 0.50m, TaxType.SuperReduced);

            TicketLine ticketLine = new(product, 6);
            TicketLine ticketLine2 = new(product2, 24);
            TicketLine ticketLine3 = new(product3, 4);

            Ticket ticket = new(countryTaxes);

            ticket.Add(ticketLine);
            ticket.Add(ticketLine2);
            ticket.Add(ticketLine3);

            ticket.CalculateTotals();   

            Assert.AreEqual(110.0m, ticket.Total);
            Assert.AreEqual(20.16m, ticket.TotalGeneralTax);
            Assert.AreEqual(1.2m, ticket.TotalReducedTax);
            Assert.AreEqual(0.08m, ticket.TotalSuperReducedTax);
        }

        [TestMethod]
        public void FinalicePortugalTicketShouldCalculateTotalsCorrectly()
        {
            CountryTaxes countryTaxes = new CountryTaxes("PT", 23, 13, 6);

            Product product = new("Water", "It is pure, crystal-clear water", 2, TaxType.Reduced);
            Product product2 = new("Beer", "It is BEER, yijaaaa!!!", 4, TaxType.General);
            Product product3 = new("Bread", "It´s only bread", 0.50m, TaxType.SuperReduced);

            TicketLine ticketLine = new(product, 6);
            TicketLine ticketLine2 = new(product2, 24);
            TicketLine ticketLine3 = new(product3, 4);

            Ticket ticket = new(countryTaxes);

            ticket.Add(ticketLine);
            ticket.Add(ticketLine2);
            ticket.Add(ticketLine3);

            ticket.CalculateTotals();

            Assert.AreEqual(110.0m, ticket.Total);
            Assert.AreEqual(22.08m, ticket.TotalGeneralTax);
            Assert.AreEqual(1.56m, ticket.TotalReducedTax);
            Assert.AreEqual(0.12m, ticket.TotalSuperReducedTax);
        }
    }
}