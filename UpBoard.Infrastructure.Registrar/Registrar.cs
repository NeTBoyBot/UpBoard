using Board.Application.AppData.Contexts.Mail;
using Board.Infrastucture.DataAccess.Contexts.File;
using Board.Infrastucture.DataAccess.Interfaces;
using Board.Infrastucture.Repository;
using Doska.AppServices.Services.Ad;
using Doska.AppServices.Services.Categories;
using Doska.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UpBoard.Application.AppData.Contexts.Advertisement.Repositories;
using UpBoard.Application.AppData.Contexts.Advertisement.Services;
using UpBoard.Application.AppData.Contexts.Categories.Repositories;
using UpBoard.Application.AppData.Contexts.Comment.Repositories;
using UpBoard.Application.AppData.Contexts.FavoriteAd.Repositories;
using UpBoard.Application.AppData.Contexts.FavoriteAd.Services;
using UpBoard.Application.AppData.Contexts.File.Repositories;
using UpBoard.Application.AppData.Contexts.File.Services;
using UpBoard.Application.AppData.Contexts.User.Repositories;
using UpBoard.Application.AppData.Contexts.User.Services;
using UpBoard.AppServices.Services.Comment;
using UpBoard.Infrastructure.DataAccess;
using UpBoard.Infrastructure.DataAccess.Contexts.Advertisement;
using UpBoard.Infrastucture.Repository;

namespace UpBoard.Infrastructure.Registrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextOptionsConfigurator<UpBoardDbContext>, UpBoardDbContextConfiguration>();

            services.AddDbContext<UpBoardDbContext>((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<UpBoardDbContext>>()
                .Configure((DbContextOptionsBuilder<UpBoardDbContext>)dbOptions));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<UpBoardDbContext>()));

            services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();
            services.AddTransient<IAdvertisementService, AdvertisementService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IFavoriteAdRepository, FavoriteAdRepository>();
            services.AddTransient<IFavoriteAdService, FavoriteAdService>();

            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IFileService, FileService>();

            services.AddTransient<IMailService, MailService>();

            services.AddLogging();

            services.AddMemoryCache();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
