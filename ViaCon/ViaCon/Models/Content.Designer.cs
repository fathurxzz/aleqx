﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("viacondbModel", "ContentContent", "Content", global::System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(ViaCon.Models.Content), "Content1", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ViaCon.Models.Content))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("viacondbModel", "ContentGallery", "Content", global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(ViaCon.Models.Content), "Gallery", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ViaCon.Models.Gallery))]

// Original file name:
// Generation date: 04.05.2010 0:53:56
namespace ViaCon.Models
{
    
    /// <summary>
    /// There are no comments for ContentStorage in the schema.
    /// </summary>
    public partial class ContentStorage : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new ContentStorage object using the connection string found in the 'ContentStorage' section of the application configuration file.
        /// </summary>
        public ContentStorage() : 
                base("name=ContentStorage", "ContentStorage")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new ContentStorage object.
        /// </summary>
        public ContentStorage(string connectionString) : 
                base(connectionString, "ContentStorage")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new ContentStorage object.
        /// </summary>
        public ContentStorage(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "ContentStorage")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for Content in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Content> Content
        {
            get
            {
                if ((this._Content == null))
                {
                    this._Content = base.CreateQuery<Content>("[Content]");
                }
                return this._Content;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Content> _Content;
        /// <summary>
        /// There are no comments for Gallery in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Gallery> Gallery
        {
            get
            {
                if ((this._Gallery == null))
                {
                    this._Gallery = base.CreateQuery<Gallery>("[Gallery]");
                }
                return this._Gallery;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Gallery> _Gallery;
        /// <summary>
        /// There are no comments for Content in the schema.
        /// </summary>
        public void AddToContent(Content content)
        {
            base.AddObject("Content", content);
        }
        /// <summary>
        /// There are no comments for Gallery in the schema.
        /// </summary>
        public void AddToGallery(Gallery gallery)
        {
            base.AddObject("Gallery", gallery);
        }
    }
    /// <summary>
    /// There are no comments for viacondbModel.Content in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="viacondbModel", Name="Content")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Content : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Content object.
        /// </summary>
        /// <param name="collapsible">Initial value of Collapsible.</param>
        /// <param name="contentId">Initial value of ContentId.</param>
        /// <param name="horisontal">Initial value of Horisontal.</param>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="isGalleryItem">Initial value of IsGalleryItem.</param>
        public static Content CreateContent(bool collapsible, string contentId, bool horisontal, int id, bool isGalleryItem)
        {
            Content content = new Content();
            content.Collapsible = collapsible;
            content.ContentId = contentId;
            content.Horisontal = horisontal;
            content.Id = id;
            content.IsGalleryItem = isGalleryItem;
            return content;
        }
        /// <summary>
        /// There are no comments for Property Collapsible in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public bool Collapsible
        {
            get
            {
                return this._Collapsible;
            }
            set
            {
                this.OnCollapsibleChanging(value);
                this.ReportPropertyChanging("Collapsible");
                this._Collapsible = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Collapsible");
                this.OnCollapsibleChanged();
            }
        }
        private bool _Collapsible;
        partial void OnCollapsibleChanging(bool value);
        partial void OnCollapsibleChanged();
        /// <summary>
        /// There are no comments for Property ContentId in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string ContentId
        {
            get
            {
                return this._ContentId;
            }
            set
            {
                this.OnContentIdChanging(value);
                this.ReportPropertyChanging("ContentId");
                this._ContentId = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ContentId");
                this.OnContentIdChanged();
            }
        }
        private string _ContentId;
        partial void OnContentIdChanging(string value);
        partial void OnContentIdChanged();
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
        /// There are no comments for Property Horisontal in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public bool Horisontal
        {
            get
            {
                return this._Horisontal;
            }
            set
            {
                this.OnHorisontalChanging(value);
                this.ReportPropertyChanging("Horisontal");
                this._Horisontal = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Horisontal");
                this.OnHorisontalChanged();
            }
        }
        private bool _Horisontal;
        partial void OnHorisontalChanging(bool value);
        partial void OnHorisontalChanged();
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
        /// There are no comments for Property Keywords in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Keywords
        {
            get
            {
                return this._Keywords;
            }
            set
            {
                this.OnKeywordsChanging(value);
                this.ReportPropertyChanging("Keywords");
                this._Keywords = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Keywords");
                this.OnKeywordsChanged();
            }
        }
        private string _Keywords;
        partial void OnKeywordsChanging(string value);
        partial void OnKeywordsChanged();
        /// <summary>
        /// There are no comments for Property Text in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this.OnTextChanging(value);
                this.ReportPropertyChanging("Text");
                this._Text = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Text");
                this.OnTextChanged();
            }
        }
        private string _Text;
        partial void OnTextChanging(string value);
        partial void OnTextChanged();
        /// <summary>
        /// There are no comments for Property Title in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this.OnTitleChanging(value);
                this.ReportPropertyChanging("Title");
                this._Title = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Title");
                this.OnTitleChanged();
            }
        }
        private string _Title;
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
        /// <summary>
        /// There are no comments for Property IsGalleryItem in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsGalleryItem
        {
            get
            {
                return this._IsGalleryItem;
            }
            set
            {
                this.OnIsGalleryItemChanging(value);
                this.ReportPropertyChanging("IsGalleryItem");
                this._IsGalleryItem = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsGalleryItem");
                this.OnIsGalleryItemChanged();
            }
        }
        private bool _IsGalleryItem;
        partial void OnIsGalleryItemChanging(bool value);
        partial void OnIsGalleryItemChanged();
        /// <summary>
        /// There are no comments for Children in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("viacondbModel", "ContentContent", "Content1")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<Content> Children
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Content>("viacondbModel.ContentContent", "Content1");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<Content>("viacondbModel.ContentContent", "Content1", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for Parent in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("viacondbModel", "ContentContent", "Content")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Content Parent
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("viacondbModel.ContentContent", "Content").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("viacondbModel.ContentContent", "Content").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Parent in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Content> ParentReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("viacondbModel.ContentContent", "Content");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Content>("viacondbModel.ContentContent", "Content", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for Galleries in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("viacondbModel", "ContentGallery", "Gallery")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<Gallery> Galleries
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Gallery>("viacondbModel.ContentGallery", "Gallery");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<Gallery>("viacondbModel.ContentGallery", "Gallery", value);
                }
            }
        }
    }
    /// <summary>
    /// There are no comments for viacondbModel.Gallery in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="viacondbModel", Name="Gallery")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Gallery : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Gallery object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="imageSource">Initial value of ImageSource.</param>
        public static Gallery CreateGallery(int id, string imageSource)
        {
            Gallery gallery = new Gallery();
            gallery.Id = id;
            gallery.ImageSource = imageSource;
            return gallery;
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
        /// There are no comments for Property Title in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this.OnTitleChanging(value);
                this.ReportPropertyChanging("Title");
                this._Title = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Title");
                this.OnTitleChanged();
            }
        }
        private string _Title;
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
        /// <summary>
        /// There are no comments for Property ImageSource in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string ImageSource
        {
            get
            {
                return this._ImageSource;
            }
            set
            {
                this.OnImageSourceChanging(value);
                this.ReportPropertyChanging("ImageSource");
                this._ImageSource = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ImageSource");
                this.OnImageSourceChanged();
            }
        }
        private string _ImageSource;
        partial void OnImageSourceChanging(string value);
        partial void OnImageSourceChanged();
        /// <summary>
        /// There are no comments for Property Location in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Location
        {
            get
            {
                return this._Location;
            }
            set
            {
                this.OnLocationChanging(value);
                this.ReportPropertyChanging("Location");
                this._Location = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Location");
                this.OnLocationChanged();
            }
        }
        private string _Location;
        partial void OnLocationChanging(string value);
        partial void OnLocationChanged();
        /// <summary>
        /// There are no comments for Property Material in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Material
        {
            get
            {
                return this._Material;
            }
            set
            {
                this.OnMaterialChanging(value);
                this.ReportPropertyChanging("Material");
                this._Material = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Material");
                this.OnMaterialChanged();
            }
        }
        private string _Material;
        partial void OnMaterialChanging(string value);
        partial void OnMaterialChanged();
        /// <summary>
        /// There are no comments for Content in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("viacondbModel", "ContentGallery", "Content")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Content Content
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("viacondbModel.ContentGallery", "Content").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("viacondbModel.ContentGallery", "Content").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Content in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Content> ContentReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("viacondbModel.ContentGallery", "Content");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Content>("viacondbModel.ContentGallery", "Content", value);
                }
            }
        }
    }
}