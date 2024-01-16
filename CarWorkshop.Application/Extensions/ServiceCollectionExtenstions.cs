using CarWorkshop.Application.CarWorkhsop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtenstions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateCarWorkshopCommand));

            services.AddAutoMapper(typeof(CarWorkshopMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                  .AddFluentValidationAutoValidation()
                  .AddFluentValidationClientsideAdapters();
        }
    }
}
