using TeamJob.Services.Profile.Application;

namespace TeamJob.Services.Profile.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}