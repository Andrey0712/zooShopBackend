namespace WebZooShop.Model
{
    public class CartViewModels
    {
        public class CartAddViewModel
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public class CartItemViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            
            public string ProductName { get; set; }
            public string ProductImage { get; set; }
            public decimal ProductPrice { get; set; }
            public int Quantity { get; set; }
        }

        
    }
}
