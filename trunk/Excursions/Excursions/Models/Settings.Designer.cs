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
// Generation date: 05.10.2010 11:56:32
namespace Excursions.Models
{
    
    /// <summary>
    /// There are no comments for SettingsStorage in the schema.
    /// </summary>
    public partial class SettingsStorage : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new SettingsStorage object using the connection string found in the 'SettingsStorage' section of the application configuration file.
        /// </summary>
        public SettingsStorage() : 
                base("name=SettingsStorage", "SettingsStorage")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new SettingsStorage object.
        /// </summary>
        public SettingsStorage(string connectionString) : 
                base(connectionString, "SettingsStorage")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new SettingsStorage object.
        /// </summary>
        public SettingsStorage(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "SettingsStorage")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for ApplicationSettings in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Data.Objects.ObjectQuery<ApplicationSettings> ApplicationSettings
        {
            get
            {
                if ((this._ApplicationSettings == null))
                {
                    this._ApplicationSettings = base.CreateQuery<ApplicationSettings>("[ApplicationSettings]");
                }
                return this._ApplicationSettings;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Data.Objects.ObjectQuery<ApplicationSettings> _ApplicationSettings;
        /// <summary>
        /// There are no comments for ApplicationSettings in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public void AddToApplicationSettings(ApplicationSettings applicationSettings)
        {
            base.AddObject("ApplicationSettings", applicationSettings);
        }
    }
    /// <summary>
    /// There are no comments for excursionsModel1.ApplicationSettings in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Key
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="excursionsModel1", Name="ApplicationSettings")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class ApplicationSettings : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new ApplicationSettings object.
        /// </summary>
        /// <param name="key">Initial value of Key.</param>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public static ApplicationSettings CreateApplicationSettings(string key)
        {
            ApplicationSettings applicationSettings = new ApplicationSettings();
            applicationSettings.Key = key;
            return applicationSettings;
        }
        /// <summary>
        /// There are no comments for property Key in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this.OnKeyChanging(value);
                this.ReportPropertyChanging("Key");
                this._Key = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Key");
                this.OnKeyChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Key;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnKeyChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnKeyChanged();
        /// <summary>
        /// There are no comments for property Value in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
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
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Value;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnValueChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnValueChanged();
    }
}