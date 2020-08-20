namespace TeamJob.Services.Profile.Application.Services
{
    public interface IDateTimeProvider
    {
        long Now { get; }
    }
}