using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.Contact.Entities;
using Cms.Domain.Models.Gallery.Entities;
using Cms.Domain.Models.Info.Entities;
using Cms.Domain.Models.Language.Entities;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.Sweeper.Entities;
using Cms.Domain.Models.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cms.Infra.Contexts
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Sweeper> Sweepers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<InfoLink> InfoLink { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Domain.Models.File.Entities.File> Files { get; set; }
        public DbSet<Domain.Models.File.Entities.FileType> FileTypes { get; set; }


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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
