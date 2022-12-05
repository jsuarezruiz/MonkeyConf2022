namespace Features.Offline
{
    public interface IConfigurationService
    {
        bool OfflineModeFeatureEnabled { get; }
    }
}