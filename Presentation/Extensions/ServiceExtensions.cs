using Application.Interfaces;                
using FluentValidation;                       
using Infrastructure.Persistence;               
using Infrastructure.Persistence.Repositories;  
using Infrastructure.Services;                  
using MediatR;                                   
using Microsoft.EntityFrameworkCore;            
using Microsoft.Extensions.Configuration;        
using Microsoft.Extensions.DependencyInjection; 
using AutoMapper;
using Domain.Interfaces;

namespace Presentation.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
          
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Application.Features.Accounts.CreateAccountCommand).Assembly)
            );

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Application.Mappings.MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddValidatorsFromAssembly(typeof(Application.Validators.CreateAccountValidator).Assembly);

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
            services.AddScoped<ITrialBalanceRepository, TrialBalanceRepository>();  // ← updated interface
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
