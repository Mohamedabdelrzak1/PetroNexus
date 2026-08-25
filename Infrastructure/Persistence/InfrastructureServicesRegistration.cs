using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositorys;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<PetroNexusDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }); // Allow DI for StoreDbContext 

            var mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
            services.AddSingleton(mailSettings);



            services.AddMemoryCache();
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();     // Allow DI for UnitOfWork







            return services;
        }
    }
}
