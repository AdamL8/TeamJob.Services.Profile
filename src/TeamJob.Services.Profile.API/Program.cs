using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TeamJob.Services.Profile.Application;
using TeamJob.Services.Profile.Application.Commands;
using TeamJob.Services.Profile.Application.DTO;
using TeamJob.Services.Profile.Application.Queries;
using TeamJob.Services.Profile.Infrastructure;

namespace TeamJob.Services.Profile.API
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                    {
                        services.AddCors(options =>
                        {
                            // Temporary
                            options.AddPolicy("CorsPolicy", builder =>
                                builder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader());
                        });

                        services.AddConvey()
                        .AddWebApi()
                        .AddApplication()
                        .AddInfrastructure()
                        .Build();
                    })
                    .Configure(app => app
                    .UseCors("CorsPolicy")
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetProfiles, IEnumerable<UserProfileDetailsDto>>("api/profile")
                        .Get<GetProfile, UserProfileDetailsDto>("api/profile/{id}")
                        .Put<UpdateProfile>("api/profile",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"api/profile/{cmd.Id}"))
                        .Post<CompleteUserProfileRegistration>("api/profile/complete",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"api/profile/{cmd.Id}"))
                        .Delete<DeleteProfile>("api/profile/{id}",
                            afterDispatch: (cmd, ctx) => ctx.Response.NoContent())

                ))
                .UseLogging()
                //.UseVault()
                .Build()
                .RunAsync();
    }
}
