namespace WebZooShop.Model
{
    public class OrderItemAddViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int BuyPrice { get; set; }
        public int Suma { get; set; }
    }

    public class OrderAddViewModel
    {

        public string ConsumerFirstName { get; set; }

        public string ConsumerSecondName { get; set; }

        public string ConsumerPhone { get; set; }

        public string Region { get; set; }
        public string City { get; set; }
        public string PostOffice { get; set; }

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
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Suma { get; set; }
    }

    public class OrderChangeStatusViewModel
    {
        public int Id { get; set; }
        public int StatusId { get; set; }

    }

    public class OrderListItemsViewModel
    {
        public int Id { get; set; }
       

    }

    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ConsumerFirstName { get; set; }

        public string ConsumerSecondName { get; set; }

        public string ConsumerPhone { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostOffice { get; set; }

        public string StatusName { get; set; }
        public string DateCreated { get; set; }

        public List<OrderItemViewModel> Items { get; set; }
    }
}
