using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTesting.PrettyTest.Builders
{
    public class ProductBuilder
    {
        private string name { get; set; }
        private string description { get; set; }
        private decimal price { get; set; }
        private TaxType tax { get; set; }

        public ProductBuilder NullName()
        {
            name = null!;
            description = "text";
            price = 1;
            tax = TaxType.General;

            return this;
        }

        public ProductBuilder NullDescription()
        {
            name = "test";
            description = null!;
            price = 1;
            tax = TaxType.General;

            return this;
        }

        public ProductBuilder ZeroPrice()
        {
            name = "test";
            description = null!;
            price = 1;
            tax = TaxType.General;

            return this;
        }

        public ProductBuilder NegativePrice()
        {
            name = "test";
            description = null!;
            price = 1;
            tax = TaxType.General;

            return this;
        }

        public ProductBuilder Beer()
        {
            name = "Beer";
            description = "It is BEER, yijaaaa!!!";
            price = 4;
            tax = TaxType.General;

            return this;
        }

        public ProductBuilder Water()
        {
            name = "Water";
            description = "It is pure, crystal-clear water";
            price = 2;
            tax = TaxType.Reduced;

            return this;
        }

        public ProductBuilder Bread()
        {
            name = "Bread";
            description = "It´s only bread";
            price = 0.50m;
            tax = TaxType.SuperReduced;

            return this;
        }

        public Product Build()
        {
            return new Product(name, description, price, tax);
        }
    }
}
