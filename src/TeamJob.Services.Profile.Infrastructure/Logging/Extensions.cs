using Convey;
using Convey.Logging.CQRS;
using Microsoft.Extensions.DependencyInjection;
using TeamJob.Services.Profile.Application.Commands;

namespace TeamJob.Services.Profile.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IConveyBuilder AddHandlersLogging(this IConveyBuilder builder)
        {
            var assembly = typeof(CompleteUserProfileRegistration).Assembly;
            
            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());
            
            return builder
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly);
        }
    }
}