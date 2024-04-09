using Cms.Domain.Common.Repositories;
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
    }
}
