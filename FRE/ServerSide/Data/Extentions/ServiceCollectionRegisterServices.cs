using Microsoft.Extensions.DependencyInjection;
using ServerSide.Services;
using ServerSide.Services.Interfaces;

namespace ServerSide.Data.Extentions
{
    public static class ServiceCollectionRegisterServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IRequestHandler, RequestHandler>();
            services.AddScoped<IVotingResultService, VotingResultService>();
            services.AddScoped<IFixedMealService, FixedMealService>();
            services.AddScoped<VotingResultService>();
            services.AddScoped<AuthService>();
            services.AddScoped<MenuItemService>();
            services.AddScoped<FeedbackService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<RequestHandler>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            return services;
        }
    }
}
