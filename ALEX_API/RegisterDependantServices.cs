using BLL.Services.Background;

namespace ALEX_API
{
    public static class RegisterDependantServices
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHostedService<StockUpdaterService>();

            return builder;
        }



    }
}
