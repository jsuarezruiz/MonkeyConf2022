namespace BeautifulTesting
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public TaxType Tax { get; set; }

        public Product(string name, string description, Decimal price, TaxType tax) 
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(description);
            
            if (price == decimal.Zero) 
                throw new ArgumentException("Zero value is not allowed" , nameof(price));

            if (price < decimal.Zero) 
                throw new ArgumentException("Negative value is not allowed", nameof(price));
            
            Name = name;
            Description= description;
            Price= price;
            Tax= tax;
        }
    }
}