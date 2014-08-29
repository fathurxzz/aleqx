using System.Collections.Generic;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.Repositories
{
    public interface ISiteRepository:IRepository
    {
        IEnumerable<Content> GetContents();
        Content GetContent(string name);
    }
}