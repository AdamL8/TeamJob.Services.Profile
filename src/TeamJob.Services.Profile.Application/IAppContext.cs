namespace TeamJob.Services.Profile.Application
{
    public interface IAppContext
    {
        string RequestId          { get; }
        IIdentityContext Identity { get; }
    }
}