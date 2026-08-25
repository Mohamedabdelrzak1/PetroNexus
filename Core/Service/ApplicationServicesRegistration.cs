using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Helpers;
using Service.Services.JournalPosting;
using ServiceAbstraction;
using ServiceAbstraction.IJournalPosting;
using Shared.Common;
using Shared.Dto.Auth;
using Shared.Validators.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AssemblyReference).Assembly)); // Corrected AutoMapper registration
            services.AddScoped<IServiceManager, ServiceManager>(); //Allow DI  for ServiceManager
            services.AddScoped<IMailingService, MailingService>(); //Allow DI for MailingService
            services.AddScoped<IJournalPostingService, JournalPostingService>(); //Allow DI for JournalPostingService
            services.AddValidatorsFromAssemblyContaining<CreateClientDtoValidator>(); // Register FluentValidation validators from Shared
            return services;
        }
    }
}
