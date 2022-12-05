using Features.Offline;
using Moq;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xunit;

namespace Tests.Features.Offline
{
    public class OfflineModeServiceShould
    {
        private readonly Builder builder;
        private OfflineModeService service;

        public OfflineModeServiceShould()
        {
            builder = new Builder();
        }

        [Theory]
        [InlineData(false, false, NetworkAccess.None, false)]
        [InlineData(false, true, NetworkAccess.Internet, false)]
        [InlineData(true, false, NetworkAccess.Internet, false)]
        [InlineData(true, false, NetworkAccess.None, false)]
        [InlineData(true, true, NetworkAccess.Internet, false)]
        [InlineData(true, true, NetworkAccess.None, true)]
        [InlineData(true, true, NetworkAccess.Local, true)]
        [InlineData(true, true, NetworkAccess.Unknown, true)]
        [InlineData(true, true, NetworkAccess.ConstrainedInternet, true)]
        public void CorrectlyEvaluateIsOfflineDependingOnSeveralParameters(
            bool offlineFeatureActivated,
            bool switchActivated,
            NetworkAccess networkAccess,
            bool offlineModeActivateExpected)
        {
            service = builder
                .EnabledFeature(offlineFeatureActivated)
                .SwitchIsActive(switchActivated)
                .WithInternet(networkAccess)
                .WithServersAvailable(true)
                .Build();

            Assert.Equal(offlineModeActivateExpected, service.IsOffline);
        }

        [Theory]
        [InlineData(NetworkAccess.Internet, NetworkAccess.None, true)]
        [InlineData(NetworkAccess.None, NetworkAccess.Internet, true)]
        [InlineData(NetworkAccess.None, NetworkAccess.None, false)]
        [InlineData(NetworkAccess.Internet, NetworkAccess.Internet, false)]
        public void EnsureIsOfflineChangedIsInvokedWhenConnectivityChanged(
            NetworkAccess previousNetworkAccess,
            NetworkAccess postNetworkAccess,
            bool isOfflineChangedEventMustBeCalled)
        {
            service = builder
                .EnabledFeature(true)
                .SwitchIsActive(true)
                .WithInternet(previousNetworkAccess)
                .WithServersAvailable(true)
                .Build();

            var isCalled = false;

            service.IsOfflineChanged += (sender, value) =>
            {
                isCalled = true;
            };

            builder.ConnectivityService
                .Raise(
                c => c.ConnectivityChanged +=
                    null,
                new ConnectivityChangedEventArgs(postNetworkAccess, null));

            Assert.Equal(isOfflineChangedEventMustBeCalled, isCalled);
        }

        [Theory]
        [InlineData(false, NetworkAccess.Internet, false, false)]
        [InlineData(false, NetworkAccess.Internet, true, false)]
        [InlineData(false, NetworkAccess.None, true, true)]
        [InlineData(false, NetworkAccess.None, false, false)]
        [InlineData(true, NetworkAccess.Internet, true, false)]
        [InlineData(true, NetworkAccess.Internet, false, false)]
        [InlineData(true, NetworkAccess.None, false, true)]
        [InlineData(true, NetworkAccess.None, true, false)]
        public void EnsureIsOfflineChangedIsInvokedWhenIsActiveChanged(
            bool previousSwitchIsActive,
            NetworkAccess previousNetworkAccess,
            bool postSwitchIsActive,
            bool isOfflineChangedEventMustBeCalled)
        {
            service = builder
                .EnabledFeature(true)
                .SwitchIsActive(previousSwitchIsActive)
                .WithInternet(previousNetworkAccess)
                .WithServersAvailable(true)
                .Build();

            var isCalled = false;

            service.IsOfflineChanged += (sender, value) =>
            {
                isCalled = true;
            };

            service.Active = postSwitchIsActive;

            Assert.Equal(isOfflineChangedEventMustBeCalled, isCalled);
        }

        [Fact]
        public void BeOfflineWhenThereArePendingActions()
        {
            service = builder
                .EnabledFeature(true)
                .SwitchIsActive(true)
                .WithInternet(NetworkAccess.Internet)
                .Build();

            service.ThereArePendingActionsToSend = true;

            Assert.True(service.IsOffline);
        }

        [Fact]
        public void NotBeInOfflineWhenThereAreNoPendingActions()
        {
            service = builder
                .EnabledFeature(true)
                .SwitchIsActive(true)
                .WithInternet(NetworkAccess.Internet)
                .WithServersAvailable(true)
                .Build();

            service.ThereArePendingActionsToSend = false;

            Assert.False(service.IsOffline);
        }

        [Fact]
        public void InvokeOfflineChangedWhenPendingActionsAreSetted()
        {
            service = builder
               .EnabledFeature(true)
               .SwitchIsActive(true)
               .WithInternet(NetworkAccess.Internet)
               .WithServersAvailable(true)
               .Build();

            var invoked = false;
            service.IsOfflineChanged += (sender, value) =>
            {
                invoked = true;
            };

            service.ThereArePendingActionsToSend = true;

            Assert.True(invoked);
        }

        [Fact]
        public void EvaluatePendingActionsWhenEvaluatinOffline()
        {
            service = builder
               .EnabledFeature(true)
               .SwitchIsActive(true)
               .WithInternet(NetworkAccess.Internet)
               .WithPendingActions()
               .Build();

            Assert.True(service.IsOffline);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void MatchOfflineModeWithServersAvailability(
            bool serversAvailable,
            bool expectedOffline)
        {
            service = builder
                .EnabledFeature(true)
                .SwitchIsActive(true)
                .WithInternet(NetworkAccess.Internet)
                .WithServersAvailable(serversAvailable)
                .Build();

            Assert.Equal(expectedOffline, service.IsOffline);
        }

        [Fact]
        public void MainTainIsOfflineIfServersAreNotAvailable()
        {
            service = builder
               .WithServersAvailable(false)
               .WithInternet(NetworkAccess.Internet)
               .EnabledFeature(true)
               .SwitchIsActive(true)
               .Build();

            service.IsOffline = true;

            service.ServersAvailable = false;

            Assert.True(service.IsOffline);
        }

        [Fact]
        public void ReturnToOnlineWhenServersBecomeAvailable()
        {
            service = builder
              .WithServersAvailable(false)
              .WithInternet(NetworkAccess.Internet)
              .EnabledFeature(true)
              .SwitchIsActive(true)
              .Build();

            service.IsOffline = true;

            service.ServersAvailable = true;

            Assert.False(service.IsOffline);
        }

        [Fact]
        public void PendingActionsChangedEventMustBeInvoked()
        {
            service = builder
                .Build();

            var invoked = false;
            service.PendingActionsChanged += (sender, value) =>
            {
                invoked = true;
            };

            service.ThereArePendingActionsToSend = true;

            Assert.True(invoked);
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void LogEvaluateIsOfflineOnlyWhenSwitchIsActive(
            bool switchIsActive,
            int expectedTimesInvoked)
        {
            service = builder
              .SwitchIsActive(switchIsActive)
              .Build();

            builder.VerifyLoggerInvokedWith("evaluating if the device is in offline mode", expectedTimesInvoked);
        }

        public class Builder
        {
            private readonly Mock<OfflineModeSettingsProvider> offlineModeSettingsProvider;
            private readonly Mock<IConfigurationService> configurationService;
            internal readonly Mock<IConnectivity> ConnectivityService;
            private readonly Mock<IActionsRepository> actionsRepository;
            internal readonly Mock<IServersChecker> ServersService;
            internal readonly Mock<ILogService> LogService;

            public Builder()
            {
                this.offlineModeSettingsProvider = new Mock<OfflineModeSettingsProvider>();
                this.configurationService = new Mock<IConfigurationService>();
                this.ConnectivityService = new Mock<IConnectivity>();
                this.actionsRepository = new Mock<IActionsRepository>();
                this.ServersService = new Mock<IServersChecker>();
                this.LogService = new Mock<ILogService>();
            }

            public OfflineModeService Build()
            {
                return new OfflineModeService(
                    this.offlineModeSettingsProvider.Object,
                    this.configurationService.Object,
                    this.ConnectivityService.Object,
                    this.actionsRepository.Object,
                    this.ServersService.Object,
                    this.LogService.Object);
            }

            internal Builder EnabledFeature(bool offlineFeatureEnabled = true)
            {
                this.configurationService
                    .SetupGet(c => c.OfflineModeFeatureEnabled)
                    .Returns(offlineFeatureEnabled);

                return this;
            }

            internal Builder SwitchIsActive(bool active = true)
            {
                this.offlineModeSettingsProvider
                    .SetupGet(c => c.IsActive)
                    .Returns(active);

                return this;
            }

            internal Builder WithInternet(NetworkAccess internet)
            {
                this.ConnectivityService
                    .Setup(c => c.NetworkAccess)
                    .Returns(internet);

                return this;
            }

            internal Builder WithPendingActions()
            {
                this.actionsRepository
                    .Setup(c => c.ThereAreActionsToSendAsync())
                    .ReturnsAsync(true);
                return this;
            }

            internal Builder WithServersAvailable(bool available)
            {
                this.ServersService
                    .Setup(c => c.AreServersAvailableAsync())
                    .ReturnsAsync(available);

                return this;
            }

            internal void VerifyLoggerInvokedWith(
                string containingMessage,
                int callCount)
            {
                this.LogService.Verify(
                    c => c.LogInfo(It.Is<string>(s => s.Contains(containingMessage))),
                    Times.Exactly(callCount));
            }
        }
    }
}