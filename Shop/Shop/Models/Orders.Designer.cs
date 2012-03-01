﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Orders", "OrderOrderItem", "Order", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Shop.Models.Order), "OrderItem", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Shop.Models.OrderItem), true)]

#endregion

namespace Shop.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class OrdersContainer : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new OrdersContainer object using the connection string found in the 'OrdersContainer' section of the application configuration file.
        /// </summary>
        public OrdersContainer() : base("name=OrdersContainer", "OrdersContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new OrdersContainer object.
        /// </summary>
        public OrdersContainer(string connectionString) : base(connectionString, "OrdersContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new OrdersContainer object.
        /// </summary>
        public OrdersContainer(EntityConnection connection) : base(connection, "OrdersContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Order> Order
        {
            get
            {
                if ((_Order == null))
                {
                    _Order = base.CreateObjectSet<Order>("Order");
                }
                return _Order;
            }
        }
        private ObjectSet<Order> _Order;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<OrderItem> OrderItem
        {
            get
            {
                if ((_OrderItem == null))
                {
                    _OrderItem = base.CreateObjectSet<OrderItem>("OrderItem");
                }
                return _OrderItem;
            }
        }
        private ObjectSet<OrderItem> _OrderItem;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Order EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToOrder(Order order)
        {
            base.AddObject("Order", order);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the OrderItem EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToOrderItem(OrderItem orderItem)
        {
            base.AddObject("OrderItem", orderItem);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Orders", Name="Order")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Order : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Order object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="orderDate">Initial value of the OrderDate property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="phone">Initial value of the Phone property.</param>
        /// <param name="deliveryAddress">Initial value of the DeliveryAddress property.</param>
        /// <param name="processed">Initial value of the Processed property.</param>
        public static Order CreateOrder(global::System.Int32 id, global::System.DateTime orderDate, global::System.String name, global::System.String phone, global::System.String deliveryAddress, global::System.Boolean processed)
        {
            Order order = new Order();
            order.Id = id;
            order.OrderDate = orderDate;
            order.Name = name;
            order.Phone = phone;
            order.DeliveryAddress = deliveryAddress;
            order.Processed = processed;
            return order;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime OrderDate
        {
            get
            {
                return _OrderDate;
            }
            set
            {
                OnOrderDateChanging(value);
                ReportPropertyChanging("OrderDate");
                _OrderDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("OrderDate");
                OnOrderDateChanged();
            }
        }
        private global::System.DateTime _OrderDate;
        partial void OnOrderDateChanging(global::System.DateTime value);
        partial void OnOrderDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                OnPhoneChanging(value);
                ReportPropertyChanging("Phone");
                _Phone = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Phone");
                OnPhoneChanged();
            }
        }
        private global::System.String _Phone;
        partial void OnPhoneChanging(global::System.String value);
        partial void OnPhoneChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String DeliveryAddress
        {
            get
            {
                return _DeliveryAddress;
            }
            set
            {
                OnDeliveryAddressChanging(value);
                ReportPropertyChanging("DeliveryAddress");
                _DeliveryAddress = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("DeliveryAddress");
                OnDeliveryAddressChanged();
            }
        }
        private global::System.String _DeliveryAddress;
        partial void OnDeliveryAddressChanging(global::System.String value);
        partial void OnDeliveryAddressChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean Processed
        {
            get
            {
                return _Processed;
            }
            set
            {
                OnProcessedChanging(value);
                ReportPropertyChanging("Processed");
                _Processed = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Processed");
                OnProcessedChanged();
            }
        }
        private global::System.Boolean _Processed;
        partial void OnProcessedChanging(global::System.Boolean value);
        partial void OnProcessedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                OnEmailChanging(value);
                ReportPropertyChanging("Email");
                _Email = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Email");
                OnEmailChanged();
            }
        }
        private global::System.String _Email;
        partial void OnEmailChanging(global::System.String value);
        partial void OnEmailChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Info
        {
            get
            {
                return _Info;
            }
            set
            {
                OnInfoChanging(value);
                ReportPropertyChanging("Info");
                _Info = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Info");
                OnInfoChanged();
            }
        }
        private global::System.String _Info;
        partial void OnInfoChanging(global::System.String value);
        partial void OnInfoChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Orders", "OrderOrderItem", "OrderItem")]
        public EntityCollection<OrderItem> OrderItems
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<OrderItem>("Orders.OrderOrderItem", "OrderItem");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<OrderItem>("Orders.OrderOrderItem", "OrderItem", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Orders", Name="OrderItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class OrderItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new OrderItem object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="price">Initial value of the Price property.</param>
        /// <param name="quantity">Initial value of the Quantity property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="orderId">Initial value of the OrderId property.</param>
        public static OrderItem CreateOrderItem(global::System.Int32 id, global::System.Decimal price, global::System.Int32 quantity, global::System.String name, global::System.Int32 orderId)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.Id = id;
            orderItem.Price = price;
            orderItem.Quantity = quantity;
            orderItem.Name = name;
            orderItem.OrderId = orderId;
            return orderItem;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Image
        {
            get
            {
                return _Image;
            }
            set
            {
                OnImageChanging(value);
                ReportPropertyChanging("Image");
                _Image = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Image");
                OnImageChanged();
            }
        }
        private global::System.String _Image;
        partial void OnImageChanging(global::System.String value);
        partial void OnImageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                OnPriceChanging(value);
                ReportPropertyChanging("Price");
                _Price = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Price");
                OnPriceChanged();
            }
        }
        private global::System.Decimal _Price;
        partial void OnPriceChanging(global::System.Decimal value);
        partial void OnPriceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                OnProductIdChanging(value);
                ReportPropertyChanging("ProductId");
                _ProductId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ProductId");
                OnProductIdChanged();
            }
        }
        private Nullable<global::System.Int32> _ProductId;
        partial void OnProductIdChanging(Nullable<global::System.Int32> value);
        partial void OnProductIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                OnQuantityChanging(value);
                ReportPropertyChanging("Quantity");
                _Quantity = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Quantity");
                OnQuantityChanged();
            }
        }
        private global::System.Int32 _Quantity;
        partial void OnQuantityChanging(global::System.Int32 value);
        partial void OnQuantityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                OnOrderIdChanging(value);
                ReportPropertyChanging("OrderId");
                _OrderId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("OrderId");
                OnOrderIdChanged();
            }
        }
        private global::System.Int32 _OrderId;
        partial void OnOrderIdChanging(global::System.Int32 value);
        partial void OnOrderIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Orders", "OrderOrderItem", "Order")]
        public Order Order
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.OrderOrderItem", "Order").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.OrderOrderItem", "Order").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Order> OrderReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.OrderOrderItem", "Order");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Order>("Orders.OrderOrderItem", "Order", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
