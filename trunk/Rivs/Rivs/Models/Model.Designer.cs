﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("rivsModel", "ContentContent", "Content", global::System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Rivs.Models.Content), "Content1", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Rivs.Models.Content))]

// Original file name:
// Generation date: 05.09.2010 10:28:37
namespace Rivs.Models
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
        /// There are no comments for Article in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Article> Article
        {
            get
            {
                if ((this._Article == null))
                {
                    this._Article = base.CreateQuery<Article>("[Article]");
                }
                return this._Article;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Article> _Article;
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
        /// There are no comments for Article in the schema.
        /// </summary>
        public void AddToArticle(Article article)
        {
            base.AddObject("Article", article);
        }
        /// <summary>
        /// There are no comments for Content in the schema.
        /// </summary>
        public void AddToContent(Content content)
        {
            base.AddObject("Content", content);
        }
    }
    /// <summary>
    /// There are no comments for rivsModel.Article in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="rivsModel", Name="Article")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Article : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Article object.
        /// </summary>
        /// <param name="date">Initial value of Date.</param>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="name">Initial value of Name.</param>
        /// <param name="text">Initial value of Text.</param>
        /// <param name="title">Initial value of Title.</param>
        public static Article CreateArticle(global::System.DateTime date, int id, string name, string text, string title)
        {
            Article article = new Article();
            article.Date = date;
            article.Id = id;
            article.Name = name;
            article.Text = text;
            article.Title = title;
            return article;
        }
        /// <summary>
        /// There are no comments for Property Date in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                this.OnDateChanging(value);
                this.ReportPropertyChanging("Date");
                this._Date = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Date");
                this.OnDateChanged();
            }
        }
        private global::System.DateTime _Date;
        partial void OnDateChanging(global::System.DateTime value);
        partial void OnDateChanged();
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
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
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
                this._Name = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Name");
                this.OnNameChanged();
            }
        }
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Preview in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Preview
        {
            get
            {
                return this._Preview;
            }
            set
            {
                this.OnPreviewChanging(value);
                this.ReportPropertyChanging("Preview");
                this._Preview = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Preview");
                this.OnPreviewChanged();
            }
        }
        private string _Preview;
        partial void OnPreviewChanging(string value);
        partial void OnPreviewChanged();
        /// <summary>
        /// There are no comments for Property Text in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
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
                this._Text = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
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
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
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
                this._Title = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Title");
                this.OnTitleChanged();
            }
        }
        private string _Title;
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
    }
    /// <summary>
    /// There are no comments for rivsModel.Content in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="rivsModel", Name="Content")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Content : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Content object.
        /// </summary>
        /// <param name="contentId">Initial value of ContentId.</param>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="sortOrder">Initial value of SortOrder.</param>
        /// <param name="text">Initial value of Text.</param>
        /// <param name="title">Initial value of Title.</param>
        public static Content CreateContent(string contentId, int id, int sortOrder, string text, string title)
        {
            Content content = new Content();
            content.ContentId = contentId;
            content.Id = id;
            content.SortOrder = sortOrder;
            content.Text = text;
            content.Title = title;
            return content;
        }
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
        /// There are no comments for Property SortOrder in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                this.OnSortOrderChanging(value);
                this.ReportPropertyChanging("SortOrder");
                this._SortOrder = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SortOrder");
                this.OnSortOrderChanged();
            }
        }
        private int _SortOrder;
        partial void OnSortOrderChanging(int value);
        partial void OnSortOrderChanged();
        /// <summary>
        /// There are no comments for Property Text in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
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
                this._Text = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
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
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
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
                this._Title = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Title");
                this.OnTitleChanged();
            }
        }
        private string _Title;
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
        /// <summary>
        /// There are no comments for Children in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("rivsModel", "ContentContent", "Content1")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<Content> Children
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Content>("rivsModel.ContentContent", "Content1");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<Content>("rivsModel.ContentContent", "Content1", value);
                }
            }
        }
        /// <summary>
        /// There are no comments for Parent in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("rivsModel", "ContentContent", "Content")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Content Parent
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("rivsModel.ContentContent", "Content").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("rivsModel.ContentContent", "Content").Value = value;
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
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Content>("rivsModel.ContentContent", "Content");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Content>("rivsModel.ContentContent", "Content", value);
                }
            }
        }
    }
}
