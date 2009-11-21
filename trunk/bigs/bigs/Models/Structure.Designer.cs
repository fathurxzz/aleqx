﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

// Original file name:
// Generation date: 21.11.2009 15:10:42
namespace bigs.Models
{
    
    /// <summary>
    /// There are no comments for DataStorage in the schema.
    /// </summary>
    public partial class DataStorage : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new DataStorage object using the connection string found in the 'DataStorage' section of the application configuration file.
        /// </summary>
        public DataStorage() : 
                base("name=DataStorage", "DataStorage")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new DataStorage object.
        /// </summary>
        public DataStorage(string connectionString) : 
                base(connectionString, "DataStorage")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new DataStorage object.
        /// </summary>
        public DataStorage(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "DataStorage")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for SiteContent in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<SiteContent> SiteContent
        {
            get
            {
                if ((this._SiteContent == null))
                {
                    this._SiteContent = base.CreateQuery<SiteContent>("[SiteContent]");
                }
                return this._SiteContent;
            }
        }
        private global::System.Data.Objects.ObjectQuery<SiteContent> _SiteContent;
        /// <summary>
        /// There are no comments for ButtonStatuses in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<ButtonStatuses> ButtonStatuses
        {
            get
            {
                if ((this._ButtonStatuses == null))
                {
                    this._ButtonStatuses = base.CreateQuery<ButtonStatuses>("[ButtonStatuses]");
                }
                return this._ButtonStatuses;
            }
        }
        private global::System.Data.Objects.ObjectQuery<ButtonStatuses> _ButtonStatuses;
        /// <summary>
        /// There are no comments for SiteContent in the schema.
        /// </summary>
        public void AddToSiteContent(SiteContent siteContent)
        {
            base.AddObject("SiteContent", siteContent);
        }
        /// <summary>
        /// There are no comments for ButtonStatuses in the schema.
        /// </summary>
        public void AddToButtonStatuses(ButtonStatuses buttonStatuses)
        {
            base.AddObject("ButtonStatuses", buttonStatuses);
        }
    }
    /// <summary>
    /// There are no comments for bigskie_contentModel.SiteContent in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="bigskie_contentModel", Name="SiteContent")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class SiteContent : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new SiteContent object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="name">Initial value of Name.</param>
        /// <param name="language">Initial value of Language.</param>
        /// <param name="url">Initial value of Url.</param>
        public static SiteContent CreateSiteContent(int id, string name, string language, string url)
        {
            SiteContent siteContent = new SiteContent();
            siteContent.Id = id;
            siteContent.Name = name;
            siteContent.Language = language;
            siteContent.Url = url;
            return siteContent;
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
        /// There are no comments for Property Language in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Language
        {
            get
            {
                return this._Language;
            }
            set
            {
                this.OnLanguageChanging(value);
                this.ReportPropertyChanging("Language");
                this._Language = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Language");
                this.OnLanguageChanged();
            }
        }
        private string _Language;
        partial void OnLanguageChanging(string value);
        partial void OnLanguageChanged();
        /// <summary>
        /// There are no comments for Property Url in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this.OnUrlChanging(value);
                this.ReportPropertyChanging("Url");
                this._Url = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Url");
                this.OnUrlChanged();
            }
        }
        private string _Url;
        partial void OnUrlChanging(string value);
        partial void OnUrlChanged();
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
        /// There are no comments for Property SortOrder in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
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
        private global::System.Nullable<int> _SortOrder;
        partial void OnSortOrderChanging(global::System.Nullable<int> value);
        partial void OnSortOrderChanged();
        /// <summary>
        /// There are no comments for Property ParentId in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
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
        private global::System.Nullable<int> _ParentId;
        partial void OnParentIdChanging(global::System.Nullable<int> value);
        partial void OnParentIdChanged();
    }
    /// <summary>
    /// There are no comments for bigskie_contentModel.ButtonStatuses in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="bigskie_contentModel", Name="ButtonStatuses")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class ButtonStatuses : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new ButtonStatuses object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="name">Initial value of Name.</param>
        /// <param name="language">Initial value of Language.</param>
        /// <param name="switchedOn">Initial value of SwitchedOn.</param>
        public static ButtonStatuses CreateButtonStatuses(int id, string name, string language, bool switchedOn)
        {
            ButtonStatuses buttonStatuses = new ButtonStatuses();
            buttonStatuses.Id = id;
            buttonStatuses.Name = name;
            buttonStatuses.Language = language;
            buttonStatuses.SwitchedOn = switchedOn;
            return buttonStatuses;
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
        /// There are no comments for Property Language in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Language
        {
            get
            {
                return this._Language;
            }
            set
            {
                this.OnLanguageChanging(value);
                this.ReportPropertyChanging("Language");
                this._Language = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Language");
                this.OnLanguageChanged();
            }
        }
        private string _Language;
        partial void OnLanguageChanging(string value);
        partial void OnLanguageChanged();
        /// <summary>
        /// There are no comments for Property SwitchedOn in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public bool SwitchedOn
        {
            get
            {
                return this._SwitchedOn;
            }
            set
            {
                this.OnSwitchedOnChanging(value);
                this.ReportPropertyChanging("SwitchedOn");
                this._SwitchedOn = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SwitchedOn");
                this.OnSwitchedOnChanged();
            }
        }
        private bool _SwitchedOn;
        partial void OnSwitchedOnChanging(bool value);
        partial void OnSwitchedOnChanged();
        /// <summary>
        /// There are no comments for Property SortOrder in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
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
        private global::System.Nullable<int> _SortOrder;
        partial void OnSortOrderChanging(global::System.Nullable<int> value);
        partial void OnSortOrderChanged();
    }
}
