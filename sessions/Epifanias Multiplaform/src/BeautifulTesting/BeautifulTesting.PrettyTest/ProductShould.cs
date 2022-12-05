using BeautifulTesting.PrettyTest.Builders;
using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace BeautifulTesting.PrettyTest
{
    public class ProductShould
    {
        [Fact]
        public void NullNameThrowArgumentNullException()
        {
            Action create = () => _ = new ProductBuilder().NullName().Build();
            create.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void NullDescriptionThrowArgumentNullException()
        {
            Action create = () => _ = new ProductBuilder().NullDescription().Build(); ;
            create.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ZeroPriceThrowArgumentException()
        {
            Action create = () => _ = new ProductBuilder().ZeroPrice().Build();
            create.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void NegativePriceThrowArgumentException()
        {
            Action create = () => _ = new ProductBuilder().NegativePrice().Build();
            create.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GeneralTaxIsCorrectly()
        {
            Product product = new ProductBuilder().Beer().Build();
            product.Tax.Should().Be(TaxType.General);
        }

        [Fact]
        public void ReducedTaxIsCorrectly()
        {
            Product product = new ProductBuilder().Water().Build();
            product.Tax.Should().Be(TaxType.Reduced);
        }

        [Fact]
        public void SuperReducedTaxIsCorrectly()
        {
            Product product = new ProductBuilder().Bread().Build();
            product.Tax.Should().Be(TaxType.SuperReduced);
        }
    }
}