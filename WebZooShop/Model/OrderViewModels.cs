namespace WebZooShop.Model
{
    public class OrderItemAddViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal BuyPrice { get; set; }
    }

    public class OrderAddViewModel
    {

        public string ConsumerFirstName { get; set; }

        public string ConsumerSecondName { get; set; }

        public string ConsumerPhone { get; set; }

        
        public int StatusId { get; set; }
        public List<OrderItemAddViewModel> OrderItems { get; set; }
    }



    public class OrderStatusItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal BuyPrice { get; set; }
    }

    public class OrderChangeStatusViewModel
    {
        public int Id { get; set; }
        public int StatusId { get; set; }

    }

    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ConsumerFirstName { get; set; }

        public string ConsumerSecondName { get; set; }

        public string ConsumerPhone { get; set; }

        public string StatusName { get; set; }
        public string DateCreated { get; set; }

        public List<OrderItemViewModel> Items { get; set; }
    }
}
