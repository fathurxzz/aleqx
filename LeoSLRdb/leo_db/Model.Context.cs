﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace leo_db
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelContainer : DbContext
    {
        public ModelContainer()
            : base("name=ModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Category> Category { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<CategoryLang> CategoryLang { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleLang> ArticleLang { get; set; }
        public DbSet<ArticleItem> ArticleItem { get; set; }
        public DbSet<ArticleItemImage> ArticleItemImage { get; set; }
        public DbSet<ArticleItemLang> ArticleItemLang { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductLang> ProductLang { get; set; }
        public DbSet<CategoryImage> CategoryImage { get; set; }
        public DbSet<SpecialContent> SpecialContent { get; set; }
        public DbSet<SpecialContentLang> SpecialContentLang { get; set; }
        public DbSet<ProductTextBlock> ProductTextBlock { get; set; }
        public DbSet<ProductTextBlockLang> ProductTextBlockLang { get; set; }
        public DbSet<ProductTextBlockFile> ProductTextBlockFile { get; set; }
    }
}
