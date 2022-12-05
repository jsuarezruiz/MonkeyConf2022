namespace BeautifulTesting.UglyTest
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullNameShouldThrowArgumentNullException()
        {
            Product product = new(null!, "text", 1, TaxType.General);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDescriptionShouldThrowArgumentNullException()
        {
            Product product = new("name", null!, 1, TaxType.General);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroPriceShouldThrowArgumentException()
        {
            Product product = new("name", "text", 0, TaxType.General);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativePriceShouldThrowArgumentException()
        {
            Product product = new("name", "text", -1, TaxType.General);
        }

        [TestMethod]
        public void CreateProductWithGeneralTaxShouldSameTax()
        {
            Product product = new("Beer", "It is BEER, yijaaaa!!!", 4, TaxType.General);

            Assert.AreEqual(TaxType.General, product.Tax);
        }

        [TestMethod]
        public void CreateProductWithReducedTaxShouldSameTax()
        {
            Product product = new ("Water", "It is pure, crystal-clear water", 2, TaxType.Reduced);

            Assert.AreEqual(TaxType.Reduced,product.Tax);
        }

        [TestMethod]
        public void CreateProductWithSuperReducedTaxShouldSameTax()
        {
            Product product = new("Bread", "It´s only bread", 0.50m , TaxType.SuperReduced);

            Assert.AreEqual(TaxType.SuperReduced, product.Tax);
        }
    }
}