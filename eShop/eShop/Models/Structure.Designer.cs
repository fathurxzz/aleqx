﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("ShopStorage", "FK_CATEGORY_REFERENCE_CATEGORY", "Category", global::System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(eShop.Models.Category), "Category1", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(eShop.Models.Category))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("ShopStorage", "FK_CATEGORYPROP_REF_CATEGORY", "Category", global::System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(eShop.Models.Category), "CategorytProperties", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(eShop.Models.CategoryProperties))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("ShopStorage", "FK_PRODUCT_REFERENCE_CATEGORY", "Category", global::System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(eShop.Models.Category), "Product", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(eShop.Models.Product))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("ShopStorage", "FK_PRODUCTP_REFERENCE_CATEGORY", "CategorytProperties", global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(eShop.Models.CategoryProperties), "ProductProperties", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(eShop.Models.ProductProperties))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("ShopStorage", "FK_PRODUCTP_REFERENCE_PRODUCT", "Product", global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(eShop.Models.Product), "ProductProperties", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(eShop.Models.ProductProperties))]

// Original file name:
// Generation date: 17.09.2009 15:31:34
namespace eShop.Models
{
    
    /// <summary>
    /// There are no comments for shopEntities in the schema.
    /// </summary>
    public partial class ShopStorage : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new shopEntities object using the connection string found in the 'shopEntities' section of the application configuration file.
        /// </summary>
        public ShopStorage() : 
                base("name=shopEntities", "shopEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new shopEntities object.
        /// </summary>
        public ShopStorage(string connectionString) : 
                base(connectionString, "shopEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new shopEntities object.
        /// </summary>
        public ShopStorage(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "shopEntities")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for Categories in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Category> Categories
        {
            get
            {
                if ((this._Categories == null))
                {
                    this._Categories = base.CreateQuery<Category>("[Categories]");
                }
                return this._Categories;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Category> _Categories;
        /// <summary>
        /// There are no comments for CategoryProperties in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<CategoryProperties> CategoryProperties
        {
            get
            {
                if ((this._CategoryProperties == null))
                {
                    this._CategoryProperties = base.CreateQuery<CategoryProperties>("[CategoryProperties]");
                }
                return this._CategoryProperties;
            }
        }
        private global::System.Data.Objects.ObjectQuery<CategoryProperties> _CategoryProperties;
        /// <summary>
        /// There are no comments for Product in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Product> Product
        {
            get
            {
                if ((this._Product == null))
                {
                    this._Product = base.CreateQuery<Product>("[Product]");
                }
                return this._Product;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Product> _Product;
        /// <summary>
        /// There are no comments for ProductProperties in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<ProductProperties> ProductProperties
        {
            get
            {
                if ((this._ProductProperties == null))
                {
                    this._ProductProperties = base.CreateQuery<ProductProperties>("[ProductProperties]");
                }
                return this._ProductProperties;
            }
        }
        private global::System.Data.Objects.ObjectQuery<ProductProperties> _ProductProperties;
        /// <summary>
        /// There are no comments for Categories in the schema.
        /// </summary>
        public void AddToCategories(Category category)
        {
            base.AddObject("Categories", category);
        }
        /// <summary>
        /// There are no comments for CategoryProperties in the schema.
        /// </summary>
        public void AddToCategoryProperties(CategoryProperties categoryProperties)
        {
            base.AddObject("CategoryProperties", categoryProperties);
        }
        /// <summary>
        /// There are no comments for Product in the schema.
        /// </summary>
        public void AddToProduct(Product product)
        {
            base.AddObject("Product", product);
        }
        /// <summary>
        /// There are no comments for ProductProperties in the schema.
        /// </summary>
        public void AddToProductProperties(ProductProperties productProperties)
        {
            base.AddObject("ProductProperties", productProperties);
        }
    }
    /// <summary>
    /// There are no comments for ShopStorage.Category in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="ShopStorage", Name="Category")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Category : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="enabled">Initial value of Enabled.</param>
        public static Category CreateCategory(int id, bool enabled)
        {
            Category category = new Category();
            category.Id = id;
            category.Enabled = enabled;
            return category;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this.ReportPropertyChanging("Id");
                this._Id = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Id");
                this.OnIdChanged();
            }
        }
        private int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this.ReportPropertyChanging("Name");
                this._Name = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Name");
                this.OnNameChanged();
            }
        }
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Enabled in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public bool Enabled
        {
            get
            {
                return this._Enabled;
            }
            set
            {
                this.OnEnabledChanging(value);
                this.ReportPropertyChanging("Enabled");
                this._Enabled = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Enabled");
                this.OnEnabledChanged();
            }
        }
        private bool _Enabled;
        partial void OnEnabledChanging(bool value);
        partial void OnEnabledChanged();
        /// <summary>
        /// There are no comments for Categories in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_CATEGORY_REFERENCE_CATEGORY", "Category1")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<Category> Categories
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Category>("ShopStorage.FK_CATEGORY_REFERENCE_CATEGORY", "Category1");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<Category>("ShopStorage.FK_CATEGORY_REFERENCE_CATEGORY", "Category1", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for Parent in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_CATEGORY_REFERENCE_CATEGORY", "Category")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Category Parent
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_CATEGORY_REFERENCE_CATEGORY", "Category").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_CATEGORY_REFERENCE_CATEGORY", "Category").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Parent in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Category> ParentReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_CATEGORY_REFERENCE_CATEGORY", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Category>("ShopStorage.FK_CATEGORY_REFERENCE_CATEGORY", "Category", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for CategorytProperties in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_CATEGORYPROP_REF_CATEGORY", "CategorytProperties")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<CategoryProperties> CategorytProperties
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<CategoryProperties>("ShopStorage.FK_CATEGORYPROP_REF_CATEGORY", "CategorytProperties");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<CategoryProperties>("ShopStorage.FK_CATEGORYPROP_REF_CATEGORY", "CategorytProperties", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for Product in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_PRODUCT_REFERENCE_CATEGORY", "Product")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<Product> Product
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Product>("ShopStorage.FK_PRODUCT_REFERENCE_CATEGORY", "Product");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<Product>("ShopStorage.FK_PRODUCT_REFERENCE_CATEGORY", "Product", value);
                }
            }
        }
    }
    /// <summary>
    /// There are no comments for ShopStorage.CategoryProperties in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="ShopStorage", Name="CategoryProperties")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class CategoryProperties : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new CategoryProperties object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        public static CategoryProperties CreateCategoryProperties(int id)
        {
            CategoryProperties categoryProperties = new CategoryProperties();
            categoryProperties.Id = id;
            return categoryProperties;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this.ReportPropertyChanging("Id");
                this._Id = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Id");
                this.OnIdChanged();
            }
        }
        private int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this.ReportPropertyChanging("Name");
                this._Name = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Name");
                this.OnNameChanged();
            }
        }
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Category in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_CATEGORYPROP_REF_CATEGORY", "Category")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Category Category
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_CATEGORYPROP_REF_CATEGORY", "Category").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_CATEGORYPROP_REF_CATEGORY", "Category").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Category in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Category> CategoryReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_CATEGORYPROP_REF_CATEGORY", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Category>("ShopStorage.FK_CATEGORYPROP_REF_CATEGORY", "Category", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for ProductProperties in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_PRODUCTP_REFERENCE_CATEGORY", "ProductProperties")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<ProductProperties> ProductProperties
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<ProductProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_CATEGORY", "ProductProperties");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<ProductProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_CATEGORY", "ProductProperties", value);
                }
            }
        }
    }
    /// <summary>
    /// There are no comments for ShopStorage.Product in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="ShopStorage", Name="Product")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Product : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Product object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        public static Product CreateProduct(int id)
        {
            Product product = new Product();
            product.Id = id;
            return product;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this.ReportPropertyChanging("Id");
                this._Id = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Id");
                this.OnIdChanged();
            }
        }
        private int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this.ReportPropertyChanging("Name");
                this._Name = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Name");
                this.OnNameChanged();
            }
        }
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Description in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.OnDescriptionChanging(value);
                this.ReportPropertyChanging("Description");
                this._Description = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Description");
                this.OnDescriptionChanged();
            }
        }
        private string _Description;
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        /// <summary>
        /// There are no comments for Property Enabled in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<bool> Enabled
        {
            get
            {
                return this._Enabled;
            }
            set
            {
                this.OnEnabledChanging(value);
                this.ReportPropertyChanging("Enabled");
                this._Enabled = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Enabled");
                this.OnEnabledChanged();
            }
        }
        private global::System.Nullable<bool> _Enabled;
        partial void OnEnabledChanging(global::System.Nullable<bool> value);
        partial void OnEnabledChanged();
        /// <summary>
        /// There are no comments for Category in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_PRODUCT_REFERENCE_CATEGORY", "Category")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Category Category
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_PRODUCT_REFERENCE_CATEGORY", "Category").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_PRODUCT_REFERENCE_CATEGORY", "Category").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Category in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Category> CategoryReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Category>("ShopStorage.FK_PRODUCT_REFERENCE_CATEGORY", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Category>("ShopStorage.FK_PRODUCT_REFERENCE_CATEGORY", "Category", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for ProductProperties in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_PRODUCTP_REFERENCE_PRODUCT", "ProductProperties")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<ProductProperties> ProductProperties
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<ProductProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_PRODUCT", "ProductProperties");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<ProductProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_PRODUCT", "ProductProperties", value);
                }
            }
        }
    }
    /// <summary>
    /// There are no comments for ShopStorage.ProductProperties in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ProductId
    /// ProductPropertyId
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="ShopStorage", Name="ProductProperties")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class ProductProperties : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new ProductProperties object.
        /// </summary>
        /// <param name="productId">Initial value of ProductId.</param>
        /// <param name="productPropertyId">Initial value of ProductPropertyId.</param>
        public static ProductProperties CreateProductProperties(int productId, int productPropertyId)
        {
            ProductProperties productProperties = new ProductProperties();
            productProperties.ProductId = productId;
            productProperties.ProductPropertyId = productPropertyId;
            return productProperties;
        }
        /// <summary>
        /// There are no comments for Property ProductId in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int ProductId
        {
            get
            {
                return this._ProductId;
            }
            set
            {
                this.OnProductIdChanging(value);
                this.ReportPropertyChanging("ProductId");
                this._ProductId = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProductId");
                this.OnProductIdChanged();
            }
        }
        private int _ProductId;
        partial void OnProductIdChanging(int value);
        partial void OnProductIdChanged();
        /// <summary>
        /// There are no comments for Property ProductPropertyId in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int ProductPropertyId
        {
            get
            {
                return this._ProductPropertyId;
            }
            set
            {
                this.OnProductPropertyIdChanging(value);
                this.ReportPropertyChanging("ProductPropertyId");
                this._ProductPropertyId = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProductPropertyId");
                this.OnProductPropertyIdChanged();
            }
        }
        private int _ProductPropertyId;
        partial void OnProductPropertyIdChanging(int value);
        partial void OnProductPropertyIdChanged();
        /// <summary>
        /// There are no comments for Property Value in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this.OnValueChanging(value);
                this.ReportPropertyChanging("Value");
                this._Value = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Value");
                this.OnValueChanged();
            }
        }
        private string _Value;
        partial void OnValueChanging(string value);
        partial void OnValueChanged();
        /// <summary>
        /// There are no comments for CategorytProperties in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_PRODUCTP_REFERENCE_CATEGORY", "CategorytProperties")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public CategoryProperties CategorytProperties
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<CategoryProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_CATEGORY", "CategorytProperties").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<CategoryProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_CATEGORY", "CategorytProperties").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for CategorytProperties in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<CategoryProperties> CategorytPropertiesReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<CategoryProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_CATEGORY", "CategorytProperties");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<CategoryProperties>("ShopStorage.FK_PRODUCTP_REFERENCE_CATEGORY", "CategorytProperties", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for Product in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("ShopStorage", "FK_PRODUCTP_REFERENCE_PRODUCT", "Product")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Product Product
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Product>("ShopStorage.FK_PRODUCTP_REFERENCE_PRODUCT", "Product").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Product>("ShopStorage.FK_PRODUCTP_REFERENCE_PRODUCT", "Product").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Product in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Product> ProductReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Product>("ShopStorage.FK_PRODUCTP_REFERENCE_PRODUCT", "Product");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Product>("ShopStorage.FK_PRODUCTP_REFERENCE_PRODUCT", "Product", value);
                }
            }
        }
    }
}
