using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.Contact.Entities;
using Cms.Domain.Models.Info.Entities;
using Cms.Domain.Models.Language.Entities;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.Sweeper.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Contexts
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Sweeper> Sweepers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Info> Info { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore(typeof(BusinessId));

            builder.ApplyConfigurationsFromAssembly(typeof(CmsDbContext).Assembly);

            #region Query Filter
            Expression<Func<AggregateRoot, bool>> filterExprForAggregate = bm => !bm.IsDelete;
            foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseModel
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(AggregateRoot)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExprForAggregate.Parameters.First(), parameter, filterExprForAggregate.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }

            Expression<Func<Entity, bool>> filterExprForEntity = bm => !bm.IsDelete;
            foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseModel
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(Entity)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExprForEntity.Parameters.First(), parameter, filterExprForEntity.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
