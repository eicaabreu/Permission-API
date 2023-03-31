using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Permission.Core.Interfaces.Services;
using Permission.Core.Services;
using Permission.Core.Services.Base;
using Permission.Infra.Data.Context;
using System.Reflection;

namespace Permission.Infra.CrossCutting.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
              .AddDbContext<PermissionDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<IPermissionService, PermissionService>();

            return services;
        }
       

    }
}