// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structural
{
    interface IDocumentComponent
    {
        string GatherData();
        void AddComponent(IDocumentComponent documentComponent);
    }

    class DocumentComponent : IDocumentComponent
    {
        public string Name { get; private set; }
        public List<IDocumentComponent> DocumentComponents { get; private set; }
        public DocumentComponent(string name)
        {
            Name = name;
            DocumentComponents = new List<IDocumentComponent>();
        }
        public string GatherData()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<{0}>", Name));
            foreach (var documentComponent in DocumentComponents)
            {
                documentComponent.GatherData();
                stringBuilder.AppendLine(documentComponent.GatherData());
            }
            stringBuilder.AppendLine(string.Format("</{0}>", Name));
            return stringBuilder.ToString();
        }
        public void AddComponent(IDocumentComponent documentComponent)
        {
            DocumentComponents.Add(documentComponent);
        }
    }

    class CustomerDocumentComponent : IDocumentComponent
    {
        private int CustomerIdToGatherData { get; set; }
        public CustomerDocumentComponent(int customerIdToGatherData)
        {
            CustomerIdToGatherData = customerIdToGatherData;
        }
        public string GatherData()
        {
            string customerData;
            switch (CustomerIdToGatherData)
            {
                case 41:
                    customerData = "Andriy Buday";
                    break;
                default:
                    customerData = "Someone else";
                    break;
            }
            return string.Format("<Customer>{0}</Customer>", customerData);
        }
        public void AddComponent(IDocumentComponent documentComponent)
        {
            Console.WriteLine("Cannot add to leaf...");
        }
    }

    class OrderDocumentComponent : IDocumentComponent
    {
        private int OrderIdToGatherData { get; set; }

        public OrderDocumentComponent(int orderIdToGatherData)
        {
            OrderIdToGatherData = orderIdToGatherData;
        }

        public string GatherData()
        {
            string orderData;
            switch (OrderIdToGatherData)
            {
                case 0:
                    orderData = "Kindle;Book1;Book2";
                    break;
                default:
                    orderData = "Phone;Cable;Headset";
                    break;
            }

            return string.Format("<Order>{0}</Order>", orderData);
        }

        public void AddComponent(IDocumentComponent documentComponent)
        {
            Console.WriteLine("Cannot add to leaf...");
        }
    }

    class HeaderDocumentComponent : IDocumentComponent
    {
        public string GatherData()
        {
            return string.Format("<Header><MessageTime>{0}</MessageTime></Header>", DateTime.Now.ToLongTimeString());
        }
        public void AddComponent(IDocumentComponent documentComponent)
        {
            Console.WriteLine("Cannot add to leaf...");
        }
    }

    public class CompositeDemo
    {
        public static void Run()
        {
            var document = new DocumentComponent("ComposableDocument");
            var headerDocumentSection = new HeaderDocumentComponent();
            var body = new DocumentComponent("Body");
            document.AddComponent(headerDocumentSection);
            document.AddComponent(body);

            var customerDocumentSection = new CustomerDocumentComponent(41);
            var orders = new DocumentComponent("Orders");
            var order0 = new OrderDocumentComponent(0);
            var order1 = new OrderDocumentComponent(1);
            orders.AddComponent(order0);
            orders.AddComponent(order1);

            body.AddComponent(customerDocumentSection);
            body.AddComponent(orders);

            string gatheredData = document.GatherData();

            Console.WriteLine(gatheredData);
        }
    }
}