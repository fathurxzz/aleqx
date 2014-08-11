using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
        public int AddOrder(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                _store.OrderItems.Add(orderItem);
            }
            _store.Orders.Add(order);
            _store.SaveChanges();
            return order.Id;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = _store.Orders.ToList();
            return orders;
        }

        public Order GetOrder(int id)
        {
            var order = _store.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
            {
                throw new Exception(string.Format("Order with id={0} not found", id));
            }
            return order;
        }

        public void DeleteOrder(int id)
        {
            var order = _store.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
            {
                throw new Exception(string.Format("Order with id={0} not found", id));
            }
            while (order.OrderItems.Any())
            {
                var orderItem = order.OrderItems.First();
                _store.OrderItems.Remove(orderItem);
            }
            _store.Orders.Remove(order);
            _store.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            var cache = _store.Orders.Single(c => c.Id == order.Id);
            cache.Completed = order.Completed;
            cache.CustomerEmail = order.CustomerEmail;
            cache.CustomerName = order.CustomerName;
            cache.CustomerPhone = order.CustomerPhone;
            cache.DeliveryAddress = order.DeliveryAddress;
            cache.DeliveryCity = order.DeliveryCity;
            cache.DeliveryMethod = order.DeliveryMethod;
            cache.DeliveryOffice = order.DeliveryOffice;
            cache.DeliveryStreet = order.DeliveryStreet;
            cache.Info = order.Info;
            cache.PaymentMethod = order.PaymentMethod;
            cache.Subscribed = order.Subscribed;
            _store.SaveChanges();
        }
    }
}
