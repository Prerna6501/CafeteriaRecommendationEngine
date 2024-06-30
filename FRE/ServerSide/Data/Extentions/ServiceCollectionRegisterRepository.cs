using Microsoft.Extensions.DependencyInjection;
using ServerSide.Repositories;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services;
using ServerSide.Services.Interfaces;

namespace ServerSide.Data.Extentions
{
    public static class ServiceCollectionRegisterRepository
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IVotingResultRepository, VotingResultRepository>();
            services.AddScoped<IFixedMealRepository, FixedMealRepository>();
            services.AddScoped<IMealTypeRepository, MealTypeRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
