using RealEstate_Dapper_Api.Repositories.AppUserRepositories;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Repositories.CategoryRepository;
using RealEstate_Dapper_Api.Repositories.ContactRepository;
using RealEstate_Dapper_Api.Repositories.EmployeeRepositories;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.ChartRepository;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.StatisticsRepository;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.LastProductsRepository;
using RealEstate_Dapper_Api.Repositories.ImageRepository;
using RealEstate_Dapper_Api.Repositories.MessageRepositories;
using RealEstate_Dapper_Api.Repositories.PopularLocationRepositories;
using RealEstate_Dapper_Api.Repositories.ProductCategory;
using RealEstate_Dapper_Api.Repositories.PropertyAmenityRepositories;
using RealEstate_Dapper_Api.Repositories.ServiceRepository;
using RealEstate_Dapper_Api.Repositories.StatisticsRepository;
using RealEstate_Dapper_Api.Repositories.SubFeatureRepositories;
using RealEstate_Dapper_Api.Repositories.TestimonialRepositories;
using RealEstate_Dapper_Api.Repositories.ToDoListRepository;
using RealEstate_Dapper_Api.Repositories.WhoWeAreRepository;

namespace RealEstate_Dapper_Api.Containers
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
           services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
           services.AddTransient<IWhoWeAreRepository, WhoWeAreRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
           services.AddTransient<IBottomGridRepository, BottomGridRepository>();
           services.AddTransient<IPopularLocationRepository, PopularLocationRepositories>();
            services.AddTransient<ITestimonialRepository, TestimonialRepository>();
           services.AddTransient<IEmployeeRepository
                , EmployeeRepository>();
            services.AddTransient<IStatisticsRepository
              , StatisticsRepository>();
           services.AddTransient<IContactRepository
            , ContactRepository>();
           services.AddTransient<IToDoListRepository
           , ToDoListRepository>(); 
            services.AddTransient<IStatisticRepository, StatisticRepository>();
           services.AddTransient<IChartRepository, ChartRepository>();
           services.AddTransient<ILast5ProductsRepository, Last5ProductsRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IProductImageRepositories, ProductImageRepositories>();
           services.AddTransient<IAppUserRepository, AppUserRepository>();
            services.AddTransient<IPropertyAmenityRepository, PropertyAmenityRepository>();
           services.AddTransient<ISubFeatureRepository, SubFeatureRepository>();
        }
    }
}
