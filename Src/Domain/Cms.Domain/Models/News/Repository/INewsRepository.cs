using Cms.Domain.Common.Repositories;
using Cms.Domain.Models.News.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.News.Repository
{
    public interface INewsRepository:IRepository<News.Entities.News>
    {
        List<Entities.News> GetAllWithRelations();

        List<Asnad> GetAllAsnad();

        void AddAsnad(long id, string title, string description, string imageName);

        void DeleteAllAsnad();
    }
}
