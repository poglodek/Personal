using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace Notification;

public static class ExtensionsApp
{
    public static IServiceCollection AddNotification(this IServiceCollection services, IConfiguration configuration)
    {

        var optionsSendGrid = configuration.GetSection("SendGrid").Get<NotificationOptions>();
        
        ArgumentNullException.ThrowIfNull(optionsSendGrid, nameof(optionsSendGrid));

        services.AddSingleton(optionsSendGrid);
        
        services.AddSendGrid(options =>
        {
            ArgumentException.ThrowIfNullOrEmpty(optionsSendGrid.ApiKey, nameof(optionsSendGrid.ApiKey));

            options.ApiKey = optionsSendGrid.ApiKey;

        });
        return services;
    }
}