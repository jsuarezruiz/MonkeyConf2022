using System;

namespace Interfaces
{
    public interface IOfflineModeService
    {
        event EventHandler<bool> IsOfflineChanged;

        event EventHandler<bool> PendingActionsChanged;

        bool FeatureEnabled { get; }

        bool Active { get; set; }

        bool IsOffline { get; }

        bool ThereArePendingActionsToSend { get; set; }

        bool ServersAvailable { get; set; }
    }
}
