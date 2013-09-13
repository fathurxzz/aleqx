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

namespace Listelli.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class CustomerContainer : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new CustomerContainer object using the connection string found in the 'CustomerContainer' section of the application configuration file.
        /// </summary>
        public CustomerContainer() : base("name=CustomerContainer", "CustomerContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new CustomerContainer object.
        /// </summary>
        public CustomerContainer(string connectionString) : base(connectionString, "CustomerContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new CustomerContainer object.
        /// </summary>
        public CustomerContainer(EntityConnection connection) : base(connection, "CustomerContainer")
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
        public ObjectSet<Subscriber> Subscriber
        {
            get
            {
                if ((_Subscriber == null))
                {
                    _Subscriber = base.CreateObjectSet<Subscriber>("Subscriber");
                }
                return _Subscriber;
            }
        }
        private ObjectSet<Subscriber> _Subscriber;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Subscriber EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToSubscriber(Subscriber subscriber)
        {
            base.AddObject("Subscriber", subscriber);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Customer", Name="Subscriber")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Subscriber : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Subscriber object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="email">Initial value of the Email property.</param>
        /// <param name="guid">Initial value of the Guid property.</param>
        /// <param name="active">Initial value of the Active property.</param>
        public static Subscriber CreateSubscriber(global::System.Int32 id, global::System.String email, global::System.String guid, global::System.Boolean active)
        {
            Subscriber subscriber = new Subscriber();
            subscriber.Id = id;
            subscriber.Email = email;
            subscriber.Guid = guid;
            subscriber.Active = active;
            return subscriber;
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
                _Email = StructuralObject.SetValidValue(value, false);
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
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Guid
        {
            get
            {
                return _Guid;
            }
            set
            {
                OnGuidChanging(value);
                ReportPropertyChanging("Guid");
                _Guid = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Guid");
                OnGuidChanged();
            }
        }
        private global::System.String _Guid;
        partial void OnGuidChanging(global::System.String value);
        partial void OnGuidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean Active
        {
            get
            {
                return _Active;
            }
            set
            {
                OnActiveChanging(value);
                ReportPropertyChanging("Active");
                _Active = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Active");
                OnActiveChanged();
            }
        }
        private global::System.Boolean _Active;
        partial void OnActiveChanging(global::System.Boolean value);
        partial void OnActiveChanged();

        #endregion
    
    }

    #endregion
    
}