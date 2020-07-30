using System;
using TeamJob.Services.Profile.Application.Services;

namespace TeamJob.Services.Profile.Infrastructure.Services
{
    public class DateTimeProvider  : IDateTimeProvider
    {
        public long Now  => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}