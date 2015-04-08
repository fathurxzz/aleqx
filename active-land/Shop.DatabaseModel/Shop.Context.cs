﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop.DatabaseModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShopContainer : DbContext
    {
        public ShopContainer()
            : base("name=ShopContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Language> Language { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryLang> CategoryLang { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductAttribute> ProductAttribute { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductLang> ProductLang { get; set; }
        public DbSet<ProductAttributeLang> ProductAttributeLang { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }
        public DbSet<ProductAttributeValueLang> ProductAttributeValueLang { get; set; }
        public DbSet<ProductAttributeValueTag> ProductAttributeValueTag { get; set; }
        public DbSet<ProductAttributeValueTagLang> ProductAttributeValueTagLang { get; set; }
        public DbSet<ProductAttributeStaticValue> ProductAttributeStaticValue { get; set; }
        public DbSet<ProductAttributeStaticValueLang> ProductAttributeStaticValueLang { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<ContentLang> ContentLang { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleLang> ArticleLang { get; set; }
        public DbSet<ArticleItem> ArticleItem { get; set; }
        public DbSet<ArticleItemLang> ArticleItemLang { get; set; }
        public DbSet<ArticleItemImage> ArticleItemImage { get; set; }
        public DbSet<ContentItem> ContentItem { get; set; }
        public DbSet<ContentItemImage> ContentItemImage { get; set; }
        public DbSet<ContentItemLang> ContentItemLang { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<QuickAdvice> QuickAdvice { get; set; }
        public DbSet<QuickAdviceLang> QuickAdviceLang { get; set; }
        public DbSet<ShopSetting> ShopSetting { get; set; }
        public DbSet<ProductStock> ProductStock { get; set; }
        public DbSet<MainPageBanner> MainPageBanner { get; set; }
        public DbSet<SiteProperty> SiteProperty { get; set; }
    }
}
