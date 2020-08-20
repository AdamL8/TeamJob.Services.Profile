using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TeamJob.Services.Profile.Infrastructure.Mongo.Documents;

namespace TeamJob.Services.Profile.Infrastructure.Mongo
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMongo(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var users = scope.ServiceProvider.GetService<IMongoRepository<UserProfileDocument, Guid>>().Collection;
                var userBuilder = Builders<UserProfileDocument>.IndexKeys;
                Task.Run(async () => await users.Indexes.CreateOneAsync(
                    new CreateIndexModel<UserProfileDocument>(userBuilder.Ascending(i => i.Email),
                        new CreateIndexOptions
                        {
                            Unique = true
                        })));
            }

            return builder;
        }
    }
}
