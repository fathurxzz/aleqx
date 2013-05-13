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

[assembly: EdmRelationshipAttribute("Site", "ContentContentLang", "Content", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Listelli.Models.Content), "ContentLang", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Listelli.Models.ContentLang), true)]
[assembly: EdmRelationshipAttribute("Site", "LanguageContentLang", "Language", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Listelli.Models.Language), "ContentLang", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Listelli.Models.ContentLang), true)]

#endregion

namespace Listelli.Models
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
        public ObjectSet<Language> Language
        {
            get
            {
                if ((_Language == null))
                {
                    _Language = base.CreateObjectSet<Language>("Language");
                }
                return _Language;
            }
        }
        private ObjectSet<Language> _Language;
    
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
        public ObjectSet<ContentLang> ContentLang
        {
            get
            {
                if ((_ContentLang == null))
                {
                    _ContentLang = base.CreateObjectSet<ContentLang>("ContentLang");
                }
                return _ContentLang;
            }
        }
        private ObjectSet<ContentLang> _ContentLang;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Language EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLanguage(Language language)
        {
            base.AddObject("Language", language);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Content EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToContent(Content content)
        {
            base.AddObject("Content", content);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ContentLang EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToContentLang(ContentLang contentLang)
        {
            base.AddObject("ContentLang", contentLang);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
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
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="mainPage">Initial value of the MainPage property.</param>
        public static Content CreateContent(global::System.Int32 id, global::System.String name, global::System.Boolean mainPage)
        {
            Content content = new Content();
            content.Id = id;
            content.Name = name;
            content.MainPage = mainPage;
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

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "ContentContentLang", "ContentLang")]
        public EntityCollection<ContentLang> ContentLangs
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<ContentLang>("Site.ContentContentLang", "ContentLang");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<ContentLang>("Site.ContentContentLang", "ContentLang", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Site", Name="ContentLang")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ContentLang : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ContentLang object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="title">Initial value of the Title property.</param>
        /// <param name="text">Initial value of the Text property.</param>
        /// <param name="contentId">Initial value of the ContentId property.</param>
        /// <param name="languageId">Initial value of the LanguageId property.</param>
        public static ContentLang CreateContentLang(global::System.Int32 id, global::System.String title, global::System.String text, global::System.Int32 contentId, global::System.Int32 languageId)
        {
            ContentLang contentLang = new ContentLang();
            contentLang.Id = id;
            contentLang.Title = title;
            contentLang.Text = text;
            contentLang.ContentId = contentId;
            contentLang.LanguageId = languageId;
            return contentLang;
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
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
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
                _Text = StructuralObject.SetValidValue(value, false);
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
        public global::System.Int32 ContentId
        {
            get
            {
                return _ContentId;
            }
            set
            {
                OnContentIdChanging(value);
                ReportPropertyChanging("ContentId");
                _ContentId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ContentId");
                OnContentIdChanged();
            }
        }
        private global::System.Int32 _ContentId;
        partial void OnContentIdChanging(global::System.Int32 value);
        partial void OnContentIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 LanguageId
        {
            get
            {
                return _LanguageId;
            }
            set
            {
                OnLanguageIdChanging(value);
                ReportPropertyChanging("LanguageId");
                _LanguageId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LanguageId");
                OnLanguageIdChanged();
            }
        }
        private global::System.Int32 _LanguageId;
        partial void OnLanguageIdChanging(global::System.Int32 value);
        partial void OnLanguageIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "ContentContentLang", "Content")]
        public Content Content
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Content>("Site.ContentContentLang", "Content").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Content>("Site.ContentContentLang", "Content").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Content> ContentReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Content>("Site.ContentContentLang", "Content");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Content>("Site.ContentContentLang", "Content", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "LanguageContentLang", "Language")]
        public Language Language
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Language>("Site.LanguageContentLang", "Language").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Language>("Site.LanguageContentLang", "Language").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Language> LanguageReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Language>("Site.LanguageContentLang", "Language");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Language>("Site.LanguageContentLang", "Language", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Site", Name="Language")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Language : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Language object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static Language CreateLanguage(global::System.Int32 id, global::System.String code, global::System.String name)
        {
            Language language = new Language();
            language.Id = id;
            language.Code = code;
            language.Name = name;
            return language;
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
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code;
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();
    
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

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Site", "LanguageContentLang", "ContentLang")]
        public EntityCollection<ContentLang> ContentLangs
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<ContentLang>("Site.LanguageContentLang", "ContentLang");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<ContentLang>("Site.LanguageContentLang", "ContentLang", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}