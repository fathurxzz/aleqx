// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural.State
{
    class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    class Order
    {
        private OrderState _state;
        private List<Product> _products = new List<Product>();

        public Order()
        {
            _state = new NewOrder(this);
        }
        public void SetOrderState(OrderState state)
        {
            _state = state;
        }
        public void WriteCurrentStateName()
        {
            Console.WriteLine("Current Order's state: {0}", _state.GetType().Name);
        }
        //
        public void AddProduct(Product product)
        {
            _products.Add(product);
            _state.AddProduct();
        }

        public void Register()
        {
            _state.Register();
        }

        public void Grant()
        {
            _state.Grant();
        }

        public void Ship()
        {
            _state.Ship();
        }

        public void Invoice()
        {
            _state.Invoice();
        }

        public void Cancel()
        {
            _state.Cancel();
        }


        public void DoShipping()
        {
            Console.WriteLine("Shipping...");
        }

        public void DoAddProduct()
        {
            Console.WriteLine("Adding product...");
        }

        public void DoCancel()
        {
            Console.WriteLine("Cancelation...");
        }

        public void DoGrant()
        {
            Console.WriteLine("Granting...");
        }

        public void DoRegister()
        {
            Console.WriteLine("Registration...");
        }

        public void DoInvoice()
        {
            Console.WriteLine("Invoicing...");
        }
    }

    #region States
    class Registered : OrderState
    {
        public Registered(Order order)
            : base(order)
        {
        }

        public override void AddProduct()
        {
            _order.DoAddProduct();
        }

        public override void Grant()
        {
            _order.DoGrant();
            _order.SetOrderState(new Granted(_order));
        }

        public override void Cancel()
        {
            _order.DoCancel();
            _order.SetOrderState(new Cancelled(_order));
        }
    }
    class Shipped : OrderState
    {
        public Shipped(Order order)
            : base(order)
        {
        }

        public override void Invoice()
        {
            _order.DoInvoice();
            _order.SetOrderState(new Invoiced(_order));
        }
    }
    class Invoiced : OrderState
    {
        public Invoiced(Order order)
            : base(order)
        {
        }
    }
    class NewOrder : OrderState
    {
        public NewOrder(Order order)
            : base(order)
        {
        }

        public override void AddProduct()
        {
            _order.DoAddProduct();
        }

        public override void Register()
        {
            _order.DoRegister();
            _order.SetOrderState(new Registered(_order));
        }

        public override void Cancel()
        {
            _order.DoCancel();
            _order.SetOrderState(new Cancelled(_order));
        }
    }

    class OrderState
    {
        public Order _order;

        public OrderState(Order order)
        {
            _order = order;
        }
        public virtual void AddProduct()
        {
            OperationIsNotAllowed("AddProduct");
        }
        public virtual void Register()
        {
            OperationIsNotAllowed("AddProduct");
        }
        public virtual void Grant()
        {
            OperationIsNotAllowed("Grant");
        }
        public virtual void Ship()
        {
            OperationIsNotAllowed("Ship");
        }
        public virtual void Invoice()
        {
            OperationIsNotAllowed("Invoice");
        }
        public virtual void Cancel()
        {
            OperationIsNotAllowed("Cancel");
        }
        private void OperationIsNotAllowed(string operationName)
        {
            Console.WriteLine("Operation {0} is not allowed for Order's state {1}", operationName, this.GetType().Name);
        }
    }
    class Cancelled : OrderState
    {
        public Cancelled(Order order)
            : base(order)
        {
        }
    }
    class Granted : OrderState
    {
        public Granted(Order order)
            : base(order)
        {
        }
        public override void AddProduct()
        {
            _order.DoAddProduct();
        }
        public override void Ship()
        {
            _order.DoShipping();
            _order.SetOrderState(new Shipped(_order));
        }
        public override void Cancel()
        {
            _order.DoCancel();
            _order.SetOrderState(new Cancelled(_order));
        }
    }
    #endregion States
    class StateDemo
    {
        public static void Run()
        {
            Product beer = new Product();
            beer.Name = "MyBestBeer";
            beer.Price = 78000;

            Order order = new Order();
            order.WriteCurrentStateName();

            order.AddProduct(beer);
            order.WriteCurrentStateName();

            order.Register();
            order.WriteCurrentStateName();

            order.Grant();
            order.WriteCurrentStateName();

            order.Ship();
            order.WriteCurrentStateName();

            //trying to add more beer to already shipped order
            order.AddProduct(beer);
            order.WriteCurrentStateName();

            //order.Invoice();
            //order.WriteCurrentStateName();
        }
    }
}