using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting.UglyTest
{
    [TestClass]
    public class TicketLineTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullProductShouldThrowArgumentNullException()
        {
            _ = new TicketLine(null!, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroUnitsShouldThrowArgumentException()
        {
            Product product = new("Beer", "It is BEER, yijaaaa!!!", 4, TaxType.General);

            _ = new TicketLine(product, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeUnitsShouldThrowArgumentException()
        {
            Product product = new("Beer", "It is BEER, yijaaaa!!!", 4, TaxType.General);

            _ = new TicketLine(product, -1);
        }

        [TestMethod]
        public void AddProductShouldCalculateCorrectTotal()
        {
            Product product = new("Beer", "It is BEER, yijaaaa!!!", 4, TaxType.General);

            TicketLine line = new(product, 5);

            Assert.AreEqual(20, line.Total);
        }
    }
}
