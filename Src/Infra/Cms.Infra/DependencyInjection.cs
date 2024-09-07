using Cms.Domain;
using Cms.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
using Cms.Domain.Models.User.Repositories;
using Cms.Infra.Models.User.Repositories;
using Cms.Domain.Models.Token.Repositories;
using Cms.Infra.Models.Token.Repositories;
using Cms.Domain.Models.Sms.Repositories;
using Cms.Infra.Models.Sms.Repositories;

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
        services.AddTransient<IInfoLinkRepository, InfoRepository>();
        services.AddTransient<IFileRepository, FileRepository>();
        services.AddTransient<IGalleryRepository, GalleryRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITokenRepository, TokenRepository>();
        services.AddTransient<ISmsRepository, SmsRepository>();


        return services;
    }
}
