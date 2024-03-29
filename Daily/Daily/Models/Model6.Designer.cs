﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

// Original file name:
// Generation date: 06.01.2011 10:43:24
namespace Daily.Models
{
    
    /// <summary>
    /// There are no comments for dbNews2Entities in the schema.
    /// </summary>
    public partial class dbNews2Entities : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new dbNews2Entities object using the connection string found in the 'dbNews2Entities' section of the application configuration file.
        /// </summary>
        public dbNews2Entities() : 
                base("name=dbNews2Entities", "dbNews2Entities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new dbNews2Entities object.
        /// </summary>
        public dbNews2Entities(string connectionString) : 
                base(connectionString, "dbNews2Entities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new dbNews2Entities object.
        /// </summary>
        public dbNews2Entities(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "dbNews2Entities")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for news in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Data.Objects.ObjectQuery<news> news
        {
            get
            {
                if ((this._news == null))
                {
                    this._news = base.CreateQuery<news>("[news]");
                }
                return this._news;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Data.Objects.ObjectQuery<news> _news;
        /// <summary>
        /// There are no comments for news_source_title in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Data.Objects.ObjectQuery<news_source_title> news_source_title
        {
            get
            {
                if ((this._news_source_title == null))
                {
                    this._news_source_title = base.CreateQuery<news_source_title>("[news_source_title]");
                }
                return this._news_source_title;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Data.Objects.ObjectQuery<news_source_title> _news_source_title;
        /// <summary>
        /// There are no comments for news in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public void AddTonews(news news)
        {
            base.AddObject("news", news);
        }
        /// <summary>
        /// There are no comments for news_source_title in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public void AddTonews_source_title(news_source_title news_source_title)
        {
            base.AddObject("news_source_title", news_source_title);
        }
    }
    /// <summary>
    /// There are no comments for dbNews2Model.news in the schema.
    /// </summary>
    /// <KeyProperties>
    /// pubdate
    /// sourceID
    /// title
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="dbNews2Model", Name="news")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class news : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new news object.
        /// </summary>
        /// <param name="id">Initial value of id.</param>
        /// <param name="pubdate">Initial value of pubdate.</param>
        /// <param name="sourceID">Initial value of sourceID.</param>
        /// <param name="title">Initial value of title.</param>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public static news Createnews(int id, global::System.DateTime pubdate, byte sourceID, string title)
        {
            news news = new news();
            news.id = id;
            news.pubdate = pubdate;
            news.sourceID = sourceID;
            news.title = title;
            return news;
        }
        /// <summary>
        /// There are no comments for property id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this.OnidChanging(value);
                this.ReportPropertyChanging("id");
                this._id = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("id");
                this.OnidChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private int _id;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnidChanging(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnidChanged();
        /// <summary>
        /// There are no comments for property pubdate in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.DateTime pubdate
        {
            get
            {
                return this._pubdate;
            }
            set
            {
                this.OnpubdateChanging(value);
                this.ReportPropertyChanging("pubdate");
                this._pubdate = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("pubdate");
                this.OnpubdateChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.DateTime _pubdate;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnpubdateChanging(global::System.DateTime value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnpubdateChanged();
        /// <summary>
        /// There are no comments for property sourceID in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public byte sourceID
        {
            get
            {
                return this._sourceID;
            }
            set
            {
                this.OnsourceIDChanging(value);
                this.ReportPropertyChanging("sourceID");
                this._sourceID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("sourceID");
                this.OnsourceIDChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private byte _sourceID;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnsourceIDChanging(byte value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnsourceIDChanged();
        /// <summary>
        /// There are no comments for property title in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this.OntitleChanging(value);
                this.ReportPropertyChanging("title");
                this._title = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("title");
                this.OntitleChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _title;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OntitleChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OntitleChanged();
        /// <summary>
        /// There are no comments for property link in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string link
        {
            get
            {
                return this._link;
            }
            set
            {
                this.OnlinkChanging(value);
                this.ReportPropertyChanging("link");
                this._link = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("link");
                this.OnlinkChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _link;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnlinkChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnlinkChanged();
        /// <summary>
        /// There are no comments for property author in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string author
        {
            get
            {
                return this._author;
            }
            set
            {
                this.OnauthorChanging(value);
                this.ReportPropertyChanging("author");
                this._author = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("author");
                this.OnauthorChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _author;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnauthorChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnauthorChanged();
        /// <summary>
        /// There are no comments for property category in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string category
        {
            get
            {
                return this._category;
            }
            set
            {
                this.OncategoryChanging(value);
                this.ReportPropertyChanging("category");
                this._category = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("category");
                this.OncategoryChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _category;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OncategoryChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OncategoryChanged();
        /// <summary>
        /// There are no comments for property htmlcontent in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string htmlcontent
        {
            get
            {
                return this._htmlcontent;
            }
            set
            {
                this.OnhtmlcontentChanging(value);
                this.ReportPropertyChanging("htmlcontent");
                this._htmlcontent = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("htmlcontent");
                this.OnhtmlcontentChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _htmlcontent;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnhtmlcontentChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnhtmlcontentChanged();
    }
    /// <summary>
    /// There are no comments for dbNews2Model.news_source_title in the schema.
    /// </summary>
    /// <KeyProperties>
    /// id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="dbNews2Model", Name="news_source_title")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class news_source_title : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new news_source_title object.
        /// </summary>
        /// <param name="id">Initial value of id.</param>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public static news_source_title Createnews_source_title(byte id)
        {
            news_source_title news_source_title = new news_source_title();
            news_source_title.id = id;
            return news_source_title;
        }
        /// <summary>
        /// There are no comments for property id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public byte id
        {
            get
            {
                return this._id;
            }
            set
            {
                this.OnidChanging(value);
                this.ReportPropertyChanging("id");
                this._id = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("id");
                this.OnidChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private byte _id;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnidChanging(byte value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnidChanged();
        /// <summary>
        /// There are no comments for property title in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this.OntitleChanging(value);
                this.ReportPropertyChanging("title");
                this._title = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("title");
                this.OntitleChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _title;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OntitleChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OntitleChanged();
    }
}
