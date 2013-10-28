﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Site", "CategoryCategory", "Category", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Penetron.Models.Technology), "Category1", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Penetron.Models.Technology), true)]
[assembly: EdmRelationshipAttribute("Site", "TechnologyTechnologyImage", "Technology", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Penetron.Models.Technology), "TechnologyImage", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Penetron.Models.TechnologyImage), true)]

#endregion

namespace Penetron.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class SiteContext : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new SiteContext object using the connection string found in the 'SiteContext' section of the application configuration file.
        /// </summary>
        public SiteContext() : base("name=SiteContext", "SiteContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SiteContext object.
        /// </summary>
        public SiteContext(string connectionString) : base(connectionString, "SiteContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SiteContext object.
        /// </summary>
        public SiteContext(EntityConnection connection) : base(connection, "SiteContext")
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
        public ObjectSet<Technology> Technology
        {
            get
            {
                if ((_Technology == null))
                {
                    _Technology = base.CreateObjectSet<Technology>("Technology");
                }
                return _Technology;
            }
        }
        private ObjectSet<Technology> _Technology;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TechnologyImage> TechnologyImage
        {
            get
            {
                if ((_TechnologyImage == null))
                {
                    _TechnologyImage = base.CreateObjectSet<TechnologyImage>("TechnologyImage");
                }
                return _TechnologyImage;
            }
        }
        private ObjectSet<TechnologyImage> _TechnologyImage;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Technology EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTechnology(Technology technology)
        {
            base.AddObject("Technology", technology);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TechnologyImage EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTechnologyImage(TechnologyImage technologyImage)
        {
            base.AddObject("TechnologyImage", technologyImage);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Site", Name="Technology")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Technology : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Technology object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="title">Initial value of the Title property.</param>
        /// <param name="categoryId">Initial value of the CategoryId property.</param>
        /// <param name="categoryLevel">Initial value of the CategoryLevel property.</param>
        /// <param name="active">Initial value of the Active property.</param>
        public static Technology CreateTechnology(global::System.Int32 id, global::System.String name, global::System.String title, global::System.Int32 categoryId, global::System.Int32 categoryLevel, global::System.Boolean active)
        {
            Technology technology = new Technology();
            technology.Id = id;
            technology.Name = name;
            technology.Title = title;
            technology.CategoryId = categoryId;
            technology.CategoryLevel = categoryLevel;
            technology.Active = active;
            return technology;
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
        private global::System.Int32 _SortOrder = 0;
        partial void OnSortOrderChanging(global::System.Int32 value);
        partial void OnSortOrderChanged();
    
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
        public global::System.Int32 CategoryId
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
        private global::System.Int32 _CategoryId;
        partial void OnCategoryIdChanging(global::System.Int32 value);
        partial void OnCategoryIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CategoryLevel
        {
            get
            {
                return _CategoryLevel;
            }
            set
            {
                OnCategoryLevelChanging(value);
                ReportPropertyChanging("CategoryLevel");
                _CategoryLevel = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CategoryLevel");
                OnCategoryLevelChanged();
            }
        }
        private global::System.Int32 _CategoryLevel;
        partial void OnCategoryLevelChanging(global::System.Int32 value);
        partial void OnCategoryLevelChanged();
    
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

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "CategoryCategory", "Category1")]
        public EntityCollection<Technology> Children
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Technology>("Site.CategoryCategory", "Category1");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Technology>("Site.CategoryCategory", "Category1", value);
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
        public Technology Parent
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technology>("Site.CategoryCategory", "Category").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technology>("Site.CategoryCategory", "Category").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Technology> ParentReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technology>("Site.CategoryCategory", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Technology>("Site.CategoryCategory", "Category", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "TechnologyTechnologyImage", "TechnologyImage")]
        public EntityCollection<TechnologyImage> TechnologyImages
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<TechnologyImage>("Site.TechnologyTechnologyImage", "TechnologyImage");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<TechnologyImage>("Site.TechnologyTechnologyImage", "TechnologyImage", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Site", Name="TechnologyImage")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TechnologyImage : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TechnologyImage object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="technologyId">Initial value of the TechnologyId property.</param>
        /// <param name="imageSource">Initial value of the ImageSource property.</param>
        public static TechnologyImage CreateTechnologyImage(global::System.Int32 id, global::System.Int32 technologyId, global::System.String imageSource)
        {
            TechnologyImage technologyImage = new TechnologyImage();
            technologyImage.Id = id;
            technologyImage.TechnologyId = technologyId;
            technologyImage.ImageSource = imageSource;
            return technologyImage;
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
        public global::System.Int32 TechnologyId
        {
            get
            {
                return _TechnologyId;
            }
            set
            {
                OnTechnologyIdChanging(value);
                ReportPropertyChanging("TechnologyId");
                _TechnologyId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TechnologyId");
                OnTechnologyIdChanged();
            }
        }
        private global::System.Int32 _TechnologyId;
        partial void OnTechnologyIdChanging(global::System.Int32 value);
        partial void OnTechnologyIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ImageSource
        {
            get
            {
                return _ImageSource;
            }
            set
            {
                OnImageSourceChanging(value);
                ReportPropertyChanging("ImageSource");
                _ImageSource = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ImageSource");
                OnImageSourceChanged();
            }
        }
        private global::System.String _ImageSource;
        partial void OnImageSourceChanging(global::System.String value);
        partial void OnImageSourceChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "TechnologyTechnologyImage", "Technology")]
        public Technology Technology
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technology>("Site.TechnologyTechnologyImage", "Technology").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technology>("Site.TechnologyTechnologyImage", "Technology").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Technology> TechnologyReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technology>("Site.TechnologyTechnologyImage", "Technology");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Technology>("Site.TechnologyTechnologyImage", "Technology", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
