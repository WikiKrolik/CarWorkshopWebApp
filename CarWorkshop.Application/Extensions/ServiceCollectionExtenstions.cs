using CarWorkshop.Application.CarWorkhsop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using CarWorkshop.Application.ApplicationUser;
using AutoMapper;


namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtenstions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddMediatR(typeof(CreateCarWorkshopCommand));



            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper()
            );

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                  .AddFluentValidationAutoValidation()
                  .AddFluentValidationClientsideAdapters();
        }
    }
}
