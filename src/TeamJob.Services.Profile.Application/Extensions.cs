using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher();
    }
}