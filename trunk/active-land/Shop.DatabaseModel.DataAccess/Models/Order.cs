using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class Order
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Completed { get; set; }
        public string Info { get; set; }
        public bool Subscribed { get; set; }
        public int DeliveryMethod { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryOffice { get; set; }
        public int PaymentMethod { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
