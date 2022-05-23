using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Logging;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamJob.Services.Profile.Domain;
using System;
using Convey.WebApi;
using TeamJob.Services.Profile.Filters;
using FluentValidation.AspNetCore;
using TeamJob.Services.Profile.Commands.Validations;
using TeamJob.Services.Profile.Events.External;
using Convey.MessageBrokers.CQRS;

namespace TeamJob.Services.Profile
{
    public class Program
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };
        public static Task Main(string[] args)
                    => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddDistributedMemoryCache();

                    services.AddCors(options =>
                    {
                        options.AddPolicy("CorsPolicy", builder =>
                                builder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader());
                    });

                    services.AddMvc(options =>
                    {
                        options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                        options.Filters.Add(typeof(ValidatorActionFilter));
                    }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProfileValidator>());

                    // First check if env vars exist
                    string mongoConnectionString = Environment.GetEnvironmentVariable("PROFILE_DATABASE_CONNECTION_STRING");

                    if (mongoConnectionString != null)
                    {
                        // Set the mongo parameters
                        MongoDbOptions mongoOptions = new MongoDbOptions
                        {
                            ConnectionString = mongoConnectionString,
                            Database = "profile-service",
                            Seed = false
                        };
                        services.AddConvey()
                        .AddWebApi()
                        .AddMongo(mongoOptions)
                        .AddMongoRepository<UserProfile, string>("Profiles")
                        .AddEventHandlers()
                        .AddCommandHandlers()
                        .AddQueryHandlers()
                        .AddInMemoryCommandDispatcher()
                        .AddInMemoryEventDispatcher()
                        .AddInMemoryQueryDispatcher()
                        .AddRabbitMq()
                        .Build();
                    } else {
                        services.AddConvey()
                        .AddWebApi()
                        .AddMongo()
                        .AddMongoRepository<UserProfile, string>("Profiles")
                        .AddEventHandlers()
                        .AddCommandHandlers()
                        .AddQueryHandlers()
                        .AddInMemoryCommandDispatcher()
                        .AddInMemoryEventDispatcher()
                        .AddInMemoryQueryDispatcher()
                        .AddRabbitMq()
                        .Build();
                    }

                    
                })
                    .Configure(app => app
                        .UseConvey()
                        .UseErrorHandler()
                        .UseCors("CorsPolicy")
                        .UseRouting()
                        .UseEndpoints(r => r.MapControllers())
                        .UseRabbitMq()
                        .SubscribeEvent<AddCandidate>()
                        .SubscribeEvent<AcceptCandidate>()
                        .SubscribeEvent<RemoveCandidate>()
                        .SubscribeEvent<TeamCreated>()
                        .SubscribeEvent<TeamDeleted>()
                        .SubscribeEvent<RemoveMember>())
                    .UseLogging();
            });
    }
}
