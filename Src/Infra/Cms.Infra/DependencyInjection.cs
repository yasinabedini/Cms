using Cms.Domain;
using Cms.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cms.Infra.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using System.Runtime.CompilerServices;
using Cms.Domain.Models.Sweeper.Repositories;
using Cms.Infra.Models.Sweeper.Repositories;
using Cms.Domain.Models.Language.Repositories;
using Cms.Infra.Models.Language.Repositories;
using Cms.Domain.Models.News.Repository;
using Cms.Infra.Models.News.Repositories;
using Cms.Domain.Models.Contact.Repositories;
using Cms.Infra.Models.Contacts.Repositories;
using Cms.Domain.Models.Info.Repositories;
using Cms.Infra.Models.Info.Reositories;
using Cms.Domain.Models.File.Repositories;
using Cms.Infra.Models.File.Repositories;
using Cms.Domain.Models.Gallery.Repositories;
using Cms.Infra.Models.Gallery.Repositories;

namespace Cms.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddDomain();

        services.AddDbContext<CmsDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

        services.AddTransient<ISweeperRepository, SweeperRepository>();
        services.AddTransient<ILanguageRepository, LanguageRepository>();        
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddTransient<INewsTypeRepository, NewsRepository>();        
        services.AddTransient<IContactRepository, ContactRepository>();
        services.AddTransient<IInfoRepository, InfoRepository>();
        services.AddTransient<IFileRepository, FileRepository>();
        services.AddTransient<IGalleryRepository, GalleryRepository>();


        return services;
    }
}
