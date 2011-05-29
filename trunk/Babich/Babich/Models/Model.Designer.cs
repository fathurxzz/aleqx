﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

// Original file name:
// Generation date: 29.05.2011 11:22:16
namespace Babich.Models
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
        /// There are no comments for content in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Data.Objects.ObjectQuery<content> content
        {
            get
            {
                if ((this._content == null))
                {
                    this._content = base.CreateQuery<content>("[content]");
                }
                return this._content;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Data.Objects.ObjectQuery<content> _content;
        /// <summary>
        /// There are no comments for content in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public void AddTocontent(content content)
        {
            base.AddObject("content", content);
        }
    }
    /// <summary>
    /// There are no comments for babichModel.content in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="babichModel", Name="content")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class content : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new content object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="name">Initial value of Name.</param>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public static content Createcontent(long id, string name)
        {
            content content = new content();
            content.Id = id;
            content.Name = name;
            return content;
        }
        /// <summary>
        /// There are no comments for property ContentTitle in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string ContentTitle
        {
            get
            {
                return this._ContentTitle;
            }
            set
            {
                this.OnContentTitleChanging(value);
                this.ReportPropertyChanging("ContentTitle");
                this._ContentTitle = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ContentTitle");
                this.OnContentTitleChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _ContentTitle;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnContentTitleChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnContentTitleChanged();
        /// <summary>
        /// There are no comments for property Description in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
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
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Description;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnDescriptionChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnDescriptionChanged();
        /// <summary>
        /// There are no comments for property Id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public long Id
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
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private long _Id;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnIdChanging(long value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for property IsGallery in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Nullable<byte> IsGallery
        {
            get
            {
                return this._IsGallery;
            }
            set
            {
                this.OnIsGalleryChanging(value);
                this.ReportPropertyChanging("IsGallery");
                this._IsGallery = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsGallery");
                this.OnIsGalleryChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Nullable<byte> _IsGallery;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnIsGalleryChanging(global::System.Nullable<byte> value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnIsGalleryChanged();
        /// <summary>
        /// There are no comments for property Lang in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string Lang
        {
            get
            {
                return this._Lang;
            }
            set
            {
                this.OnLangChanging(value);
                this.ReportPropertyChanging("Lang");
                this._Lang = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Lang");
                this.OnLangChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Lang;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnLangChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnLangChanged();
        /// <summary>
        /// There are no comments for property Name in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
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
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Name;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnNameChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for property ParentId in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Nullable<int> ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                this.OnParentIdChanging(value);
                this.ReportPropertyChanging("ParentId");
                this._ParentId = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ParentId");
                this.OnParentIdChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Nullable<int> _ParentId;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnParentIdChanging(global::System.Nullable<int> value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnParentIdChanged();
        /// <summary>
        /// There are no comments for property SeoDescription in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string SeoDescription
        {
            get
            {
                return this._SeoDescription;
            }
            set
            {
                this.OnSeoDescriptionChanging(value);
                this.ReportPropertyChanging("SeoDescription");
                this._SeoDescription = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SeoDescription");
                this.OnSeoDescriptionChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _SeoDescription;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnSeoDescriptionChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnSeoDescriptionChanged();
        /// <summary>
        /// There are no comments for property SeoKeywords in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string SeoKeywords
        {
            get
            {
                return this._SeoKeywords;
            }
            set
            {
                this.OnSeoKeywordsChanging(value);
                this.ReportPropertyChanging("SeoKeywords");
                this._SeoKeywords = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SeoKeywords");
                this.OnSeoKeywordsChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _SeoKeywords;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnSeoKeywordsChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnSeoKeywordsChanged();
        /// <summary>
        /// There are no comments for property SortOrder in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Nullable<int> SortOrder
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
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Nullable<int> _SortOrder;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnSortOrderChanging(global::System.Nullable<int> value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnSortOrderChanged();
        /// <summary>
        /// There are no comments for property Text in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
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
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Text;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnTextChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnTextChanged();
        /// <summary>
        /// There are no comments for property Title in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
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
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Title;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnTitleChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnTitleChanged();
    }
}
