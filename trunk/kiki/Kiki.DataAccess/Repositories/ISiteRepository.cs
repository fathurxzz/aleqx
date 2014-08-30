using System;
using System.Collections.Generic;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.Repositories
{
    public interface ISiteRepository:IRepository
    {
        IEnumerable<Article> GetArticles(bool showOnlyActive = false);
        Article GetArticle(int id);
        Article GetArticle(string name);
        void DeleteArticle(int id, Action<string> deleteImages);
        int AddArticle(Article article);
        void SaveArticle(Article article);


        // Content
        IEnumerable<Content> GetContents();
        Content GetContent(int id);
        Content GetContent(string name);
        Content GetContent();
        void DeleteContent(int id);
        void SaveContent(Content content);
        int AddContent(Content content);

        // Reason
        IEnumerable<Reason> GetReasons();
        Content GetReason(int id);
        Content GetReason();
        void DeleteReason(int id);
        void SaveReason(Reason content);
        int AddReason(Reason content);

        // Attention 
        IEnumerable<Attention> GetAttentions();
        Content GetAttention(int id);
        Content GetAttention();
        void DeleteAttention(int id);
        void SaveAttention(Attention content);
        int AddAttention(Attention content);

    }
}