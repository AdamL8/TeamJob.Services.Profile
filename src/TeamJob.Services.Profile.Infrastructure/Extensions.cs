using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.MongoDB;
using Convey.Security;
using Convey.WebApi;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TeamJob.Services.Profile.Application.Events.External;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Repositories;
using TeamJob.Services.Profile.Infrastructure.Contexts;
using TeamJob.Services.Profile.Infrastructure.Decorators;
using TeamJob.Services.Profile.Infrastructure.Exceptions;
using TeamJob.Services.Profile.Infrastructure.Logging;
using TeamJob.Services.Profile.Infrastructure.Mongo;
using TeamJob.Services.Profile.Infrastructure.Mongo.Documents;
using TeamJob.Services.Profile.Infrastructure.Mongo.Repositories;
using TeamJob.Services.Profile.Infrastructure.Services;

namespace TeamJob.Services.Profile.Infrastructure
{
public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IEventMapper, EventMapper>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(IEventHandler<>),   typeof(OutboxEventHandlerDecorator<>));

            string mongoConnectionString = Environment.GetEnvironmentVariable("PROFILE_DATABASE_CONNECTION_STRING");

            // Set the mongo parameters
            MongoDbOptions mongoOptions = new MongoDbOptions
            {
                ConnectionString = mongoConnectionString,
                Database         = "profile-service",
                Seed             = false
            };

            builder.AddErrorHandler<ExceptionToResponseMapper>()
                   .AddQueryHandlers()
                   .AddInMemoryQueryDispatcher()
                   .AddHttpClient()
                   .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                   .AddRabbitMq()
                   .AddMessageOutbox(o => o.AddMongo())
                   .AddMetrics()
                   .AddHandlersLogging()
                   .AddMongoRepository<UserProfileDocument, string>("Profiles")
                   .AddWebApiSwaggerDocs()
                   .AddSecurity();

            return mongoConnectionString is null
                            ? builder.AddMongo()
                            : builder.AddMongo(mongoOptions);
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseSwaggerDocs()
                .UseConvey()
                .UseMongo()
                .UseMetrics()
                .UseRabbitMq()
                .SubscribeEvent<AcceptCandidate>()
                .SubscribeEvent<AddCandidate>()
                .SubscribeEvent<RemoveCandidate>()
                .SubscribeEvent<RemoveMember>()
                .SubscribeEvent<TeamCreated>()
                .SubscribeEvent<TeamDeleted>()
                .SubscribeEvent<Registered>();

            return app;
        }

        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
                : null;

        internal static IDictionary<string, object> GetHeadersToForward(this IMessageProperties messageProperties)
        {
            const string sagaHeader = "Saga";
            if (messageProperties?.Headers is null || !messageProperties.Headers.TryGetValue(sagaHeader, out var saga))
            {  return null; }

            return saga is null
                ? null
                : new Dictionary<string, object>
                {
                    [sagaHeader] = saga
                };
        }

        internal static string GetSpanContext(this IMessageProperties messageProperties, string header)
        {
            if (messageProperties is null)
            {
                return string.Empty;
            }

            if (messageProperties.Headers.TryGetValue(header, out var span) && span is byte[] spanBytes)
            {
                return Encoding.UTF8.GetString(spanBytes);
            }

            return string.Empty;
        }
    }
}