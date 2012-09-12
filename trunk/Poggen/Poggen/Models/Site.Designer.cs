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

[assembly: EdmRelationshipAttribute("Site", "CategoryCategory", "Category", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Poggen.Models.Category), "Category1", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Poggen.Models.Category), true)]

#endregion

namespace Poggen.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class SiteContainer : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new SiteContainer object using the connection string found in the 'SiteContainer' section of the application configuration file.
        /// </summary>
        public SiteContainer() : base("name=SiteContainer", "SiteContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SiteContainer object.
        /// </summary>
        public SiteContainer(string connectionString) : base(connectionString, "SiteContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SiteContainer object.
        /// </summary>
        public SiteContainer(EntityConnection connection) : base(connection, "SiteContainer")
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
        public ObjectSet<Content> Content
        {
            get
            {
                if ((_Content == null))
                {
                    _Content = base.CreateObjectSet<Content>("Content");
                }
                return _Content;
            }
        }
        private ObjectSet<Content> _Content;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Category> Category
        {
            get
            {
                if ((_Category == null))
                {
                    _Category = base.CreateObjectSet<Category>("Category");
                }
                return _Category;
            }
        }
        private ObjectSet<Category> _Category;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Content EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToContent(Content content)
        {
            base.AddObject("Content", content);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Category EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCategory(Category category)
        {
            base.AddObject("Category", category);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Site", Name="Category")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Category : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="title">Initial value of the Title property.</param>
        /// <param name="sortOrder">Initial value of the SortOrder property.</param>
        /// <param name="mainPage">Initial value of the MainPage property.</param>
        /// <param name="categoryType">Initial value of the CategoryType property.</param>
        public static Category CreateCategory(global::System.Int32 id, global::System.String name, global::System.String title, global::System.Int32 sortOrder, global::System.Boolean mainPage, global::System.Int32 categoryType)
        {
            Category category = new Category();
            category.Id = id;
            category.Name = name;
            category.Title = title;
            category.SortOrder = sortOrder;
            category.MainPage = mainPage;
            category.CategoryType = categoryType;
            return category;
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
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 SortOrder
        {
            get
            {
                return _SortOrder;
            }
            set
            {
                OnSortOrderChanging(value);
                ReportPropertyChanging("SortOrder");
                _SortOrder = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SortOrder");
                OnSortOrderChanged();
            }
        }
        private global::System.Int32 _SortOrder;
        partial void OnSortOrderChanging(global::System.Int32 value);
        partial void OnSortOrderChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SeoDescription
        {
            get
            {
                return _SeoDescription;
            }
            set
            {
                OnSeoDescriptionChanging(value);
                ReportPropertyChanging("SeoDescription");
                _SeoDescription = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("SeoDescription");
                OnSeoDescriptionChanged();
            }
        }
        private global::System.String _SeoDescription;
        partial void OnSeoDescriptionChanging(global::System.String value);
        partial void OnSeoDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SeoKeywords
        {
            get
            {
                return _SeoKeywords;
            }
            set
            {
                OnSeoKeywordsChanging(value);
                ReportPropertyChanging("SeoKeywords");
                _SeoKeywords = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("SeoKeywords");
                OnSeoKeywordsChanged();
            }
        }
        private global::System.String _SeoKeywords;
        partial void OnSeoKeywordsChanging(global::System.String value);
        partial void OnSeoKeywordsChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                OnTextChanging(value);
                ReportPropertyChanging("Text");
                _Text = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Text");
                OnTextChanged();
            }
        }
        private global::System.String _Text;
        partial void OnTextChanging(global::System.String value);
        partial void OnTextChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean MainPage
        {
            get
            {
                return _MainPage;
            }
            set
            {
                OnMainPageChanging(value);
                ReportPropertyChanging("MainPage");
                _MainPage = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MainPage");
                OnMainPageChanged();
            }
        }
        private global::System.Boolean _MainPage;
        partial void OnMainPageChanging(global::System.Boolean value);
        partial void OnMainPageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set
            {
                OnCategoryIdChanging(value);
                ReportPropertyChanging("CategoryId");
                _CategoryId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CategoryId");
                OnCategoryIdChanged();
            }
        }
        private Nullable<global::System.Int32> _CategoryId;
        partial void OnCategoryIdChanging(Nullable<global::System.Int32> value);
        partial void OnCategoryIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CategoryType
        {
            get
            {
                return _CategoryType;
            }
            set
            {
                OnCategoryTypeChanging(value);
                ReportPropertyChanging("CategoryType");
                _CategoryType = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CategoryType");
                OnCategoryTypeChanged();
            }
        }
        private global::System.Int32 _CategoryType;
        partial void OnCategoryTypeChanging(global::System.Int32 value);
        partial void OnCategoryTypeChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "CategoryCategory", "Category1")]
        public EntityCollection<Category> Children
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Category>("Site.CategoryCategory", "Category1");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Category>("Site.CategoryCategory", "Category1", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "CategoryCategory", "Category")]
        public Category Parent
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Site.CategoryCategory", "Category").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Site.CategoryCategory", "Category").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Category> ParentReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Site.CategoryCategory", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Category>("Site.CategoryCategory", "Category", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Site", Name="Content")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Content : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Content object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="title">Initial value of the Title property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="mainPage">Initial value of the MainPage property.</param>
        /// <param name="sortOrder">Initial value of the SortOrder property.</param>
        public static Content CreateContent(global::System.Int32 id, global::System.String title, global::System.String name, global::System.Boolean mainPage, global::System.Int32 sortOrder)
        {
            Content content = new Content();
            content.Id = id;
            content.Title = title;
            content.Name = name;
            content.MainPage = mainPage;
            content.SortOrder = sortOrder;
            return content;
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
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                OnTextChanging(value);
                ReportPropertyChanging("Text");
                _Text = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Text");
                OnTextChanged();
            }
        }
        private global::System.String _Text;
        partial void OnTextChanging(global::System.String value);
        partial void OnTextChanged();
    
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
        public global::System.Boolean MainPage
        {
            get
            {
                return _MainPage;
            }
            set
            {
                OnMainPageChanging(value);
                ReportPropertyChanging("MainPage");
                _MainPage = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MainPage");
                OnMainPageChanged();
            }
        }
        private global::System.Boolean _MainPage;
        partial void OnMainPageChanging(global::System.Boolean value);
        partial void OnMainPageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SeoDescription
        {
            get
            {
                return _SeoDescription;
            }
            set
            {
                OnSeoDescriptionChanging(value);
                ReportPropertyChanging("SeoDescription");
                _SeoDescription = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("SeoDescription");
                OnSeoDescriptionChanged();
            }
        }
        private global::System.String _SeoDescription;
        partial void OnSeoDescriptionChanging(global::System.String value);
        partial void OnSeoDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SeoKeywords
        {
            get
            {
                return _SeoKeywords;
            }
            set
            {
                OnSeoKeywordsChanging(value);
                ReportPropertyChanging("SeoKeywords");
                _SeoKeywords = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("SeoKeywords");
                OnSeoKeywordsChanged();
            }
        }
        private global::System.String _SeoKeywords;
        partial void OnSeoKeywordsChanging(global::System.String value);
        partial void OnSeoKeywordsChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 SortOrder
        {
            get
            {
                return _SortOrder;
            }
            set
            {
                OnSortOrderChanging(value);
                ReportPropertyChanging("SortOrder");
                _SortOrder = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SortOrder");
                OnSortOrderChanged();
            }
        }
        private global::System.Int32 _SortOrder;
        partial void OnSortOrderChanging(global::System.Int32 value);
        partial void OnSortOrderChanged();

        #endregion
    
    }

    #endregion
    
}
