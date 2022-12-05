using Interfaces;
using System;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Features.Offline
{
    public class OfflineModeService : IOfflineModeService
    {
        private static readonly string Tag = $"[{nameof(OfflineModeService)}]";

        private readonly OfflineModeSettingsProvider offlineModeSettingsProvider;
        private readonly IConfigurationService configurationService;
        private readonly IConnectivity connectivityService;
        private readonly IActionsRepository actionsRepository;
        private readonly IServersChecker serversService;
        private readonly ILogService logService;
        private bool isOffline;
        private bool thereArePendingActionsToSend;
        private bool serversAvailable;

        public OfflineModeService(
            OfflineModeSettingsProvider offlineModeSettingsPovider,
            IConfigurationService configurationService,
            IConnectivity connectivityService,
            IActionsRepository actionsRepository,
            IServersChecker serversService,
            ILogService logService)
        {
            this.offlineModeSettingsProvider = offlineModeSettingsPovider;
            this.configurationService = configurationService;
            this.connectivityService = connectivityService;
            this.actionsRepository = actionsRepository;
            this.serversService = serversService;
            this.logService = logService;

            connectivityService.ConnectivityChanged += OnConnectivityChanged;

            ThereAreActionsToSendAsync();
            ChecksServersAvailableAsync();
            EvaluateIsOffline(connectivityService.NetworkAccess, Active);
        }

        public event EventHandler<bool> IsOfflineChanged;

        public event EventHandler<bool> PendingActionsChanged;

        public bool FeatureEnabled => this.configurationService.OfflineModeFeatureEnabled;

        public bool Active
        {
            get => this.offlineModeSettingsProvider.IsActive;
            set
            {
                this.offlineModeSettingsProvider.IsActive = value;
                EvaluateIsOffline(connectivityService.NetworkAccess, value);
            }
        }

        public bool IsOffline
        {
            get => isOffline;
            set
            {
                if (value != isOffline)
                {
                    logService.LogInfo($"{Tag} new value for offline mode: {value}");
                    isOffline = value;
                    IsOfflineChanged?.Invoke(this, value);
                }
            }
        }

        public bool ThereArePendingActionsToSend
        {
            get => thereArePendingActionsToSend;
            set
            {
                if (value != this.thereArePendingActionsToSend)
                {
                    PendingActionsChanged?.Invoke(this, value);
                    this.thereArePendingActionsToSend = value;
                    EvaluateIsOffline(this.connectivityService.NetworkAccess, Active);
                }
            }
        }

        public bool ServersAvailable
        {
            get => !Active || serversAvailable;

            set
            {
                if (value != this.serversAvailable)
                {
                    this.serversAvailable = value;
                    EvaluateIsOffline(this.connectivityService.NetworkAccess, Active);
                }
            }
        }

        private async void ThereAreActionsToSendAsync()
        {
            if (Active)
            {
                ThereArePendingActionsToSend = await this.actionsRepository.ThereAreActionsToSendAsync();
            }
        }

        private async void ChecksServersAvailableAsync()
        {
            if (Active)
            {
                ServersAvailable = await this.serversService.AreServersAvailableAsync();
            }
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            EvaluateIsOffline(e.NetworkAccess, Active);
        }

        private void EvaluateIsOffline(NetworkAccess networkAccess, bool isActiveSwitch)
        {
            bool newStateIsOffline;
            if (!isActiveSwitch)
            {
                newStateIsOffline = false;
            }
            else
            {
                logService.LogInfo($"{Tag} evaluating if the device is in offline mode");
                var hasInternet = networkAccess != NetworkAccess.Internet;
                newStateIsOffline = hasInternet
                    || ThereArePendingActionsToSend
                    || !ServersAvailable;

                logService.LogInfo($"{Tag} " +
                    $"HasInternet: {hasInternet}, " +
                    $"IsActive: {isActiveSwitch}, " +
                    $"ThereArePendingActionsToSend: {ThereArePendingActionsToSend}, " +
                    $"ServersAvailable: {serversAvailable}");
            }

            IsOffline = newStateIsOffline;
        }
    }
}
