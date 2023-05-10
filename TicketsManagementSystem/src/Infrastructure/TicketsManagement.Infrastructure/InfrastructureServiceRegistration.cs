using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketsManagement.Application.Contracts.Infrastructure;
using TicketsManagement.Application.Models.Mail;
using TicketsManagement.Infrastructure.Mail;

namespace TicketsManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
