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
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("collectstikersModel", "FK_STICKERS_REFERENCE_ALBUM", "Album", global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(CollectStickers.Models.Album), "Stickers", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(CollectStickers.Models.Stickers))]

// Original file name:
// Generation date: 13.11.2009 19:56:34
namespace CollectStickers.Models
{
    
    /// <summary>
    /// There are no comments for CollectStickersConnectionString in the schema.
    /// </summary>
    public partial class StickersStorage : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new CollectStickersConnectionString object using the connection string found in the 'CollectStickersConnectionString' section of the application configuration file.
        /// </summary>
        public StickersStorage() : 
                base("name=CollectStickersConnectionString", "CollectStickersConnectionString")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new CollectStickersConnectionString object.
        /// </summary>
        public StickersStorage(string connectionString) : 
                base(connectionString, "CollectStickersConnectionString")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new CollectStickersConnectionString object.
        /// </summary>
        public StickersStorage(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "CollectStickersConnectionString")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for Album in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Album> Album
        {
            get
            {
                if ((this._Album == null))
                {
                    this._Album = base.CreateQuery<Album>("[Album]");
                }
                return this._Album;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Album> _Album;
        /// <summary>
        /// There are no comments for Stickers in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Stickers> Stickers
        {
            get
            {
                if ((this._Stickers == null))
                {
                    this._Stickers = base.CreateQuery<Stickers>("[Stickers]");
                }
                return this._Stickers;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Stickers> _Stickers;
        /// <summary>
        /// There are no comments for Album in the schema.
        /// </summary>
        public void AddToAlbum(Album album)
        {
            base.AddObject("Album", album);
        }
        /// <summary>
        /// There are no comments for Stickers in the schema.
        /// </summary>
        public void AddToStickers(Stickers stickers)
        {
            base.AddObject("Stickers", stickers);
        }
    }
    /// <summary>
    /// There are no comments for collectstikersModel.Album in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="collectstikersModel", Name="Album")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Album : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Album object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        public static Album CreateAlbum(short id)
        {
            Album album = new Album();
            album.Id = id;
            return album;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public short Id
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
        private short _Id;
        partial void OnIdChanging(short value);
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
        /// There are no comments for Property MaxStickers in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<short> MaxStickers
        {
            get
            {
                return this._MaxStickers;
            }
            set
            {
                this.OnMaxStickersChanging(value);
                this.ReportPropertyChanging("MaxStickers");
                this._MaxStickers = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("MaxStickers");
                this.OnMaxStickersChanged();
            }
        }
        private global::System.Nullable<short> _MaxStickers;
        partial void OnMaxStickersChanging(global::System.Nullable<short> value);
        partial void OnMaxStickersChanged();
        /// <summary>
        /// There are no comments for Stickers in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("collectstikersModel", "FK_STICKERS_REFERENCE_ALBUM", "Stickers")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<Stickers> Stickers
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Stickers>("collectstikersModel.FK_STICKERS_REFERENCE_ALBUM", "Stickers");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<Stickers>("collectstikersModel.FK_STICKERS_REFERENCE_ALBUM", "Stickers", value);
                }
            }
        }
    }
    /// <summary>
    /// There are no comments for collectstikersModel.Stickers in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="collectstikersModel", Name="Stickers")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Stickers : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Stickers object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="userId">Initial value of UserId.</param>
        /// <param name="needOrFree">Initial value of NeedOrFree.</param>
        /// <param name="number">Initial value of Number.</param>
        public static Stickers CreateStickers(int id, global::System.Guid userId, byte needOrFree, short number)
        {
            Stickers stickers = new Stickers();
            stickers.Id = id;
            stickers.UserId = userId;
            stickers.NeedOrFree = needOrFree;
            stickers.Number = number;
            return stickers;
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
        /// There are no comments for Property UserId in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Guid UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this.OnUserIdChanging(value);
                this.ReportPropertyChanging("UserId");
                this._UserId = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UserId");
                this.OnUserIdChanged();
            }
        }
        private global::System.Guid _UserId;
        partial void OnUserIdChanging(global::System.Guid value);
        partial void OnUserIdChanged();
        /// <summary>
        /// There are no comments for Property NeedOrFree in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public byte NeedOrFree
        {
            get
            {
                return this._NeedOrFree;
            }
            set
            {
                this.OnNeedOrFreeChanging(value);
                this.ReportPropertyChanging("NeedOrFree");
                this._NeedOrFree = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("NeedOrFree");
                this.OnNeedOrFreeChanged();
            }
        }
        private byte _NeedOrFree;
        partial void OnNeedOrFreeChanging(byte value);
        partial void OnNeedOrFreeChanged();
        /// <summary>
        /// There are no comments for Property Number in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public short Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                this.OnNumberChanging(value);
                this.ReportPropertyChanging("Number");
                this._Number = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Number");
                this.OnNumberChanged();
            }
        }
        private short _Number;
        partial void OnNumberChanging(short value);
        partial void OnNumberChanged();
        /// <summary>
        /// There are no comments for Album in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("collectstikersModel", "FK_STICKERS_REFERENCE_ALBUM", "Album")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Album Album
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Album>("collectstikersModel.FK_STICKERS_REFERENCE_ALBUM", "Album").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Album>("collectstikersModel.FK_STICKERS_REFERENCE_ALBUM", "Album").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Album in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Album> AlbumReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Album>("collectstikersModel.FK_STICKERS_REFERENCE_ALBUM", "Album");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Album>("collectstikersModel.FK_STICKERS_REFERENCE_ALBUM", "Album", value);
                }
            }
        }
    }
}
