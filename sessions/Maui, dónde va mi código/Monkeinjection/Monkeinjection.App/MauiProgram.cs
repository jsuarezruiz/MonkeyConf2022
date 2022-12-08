namespace Monkeinjection.App;

using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Maui.Controls;
using Monkeinjection.App.Common;
using Monkeinjection.App.Features.GenericRepositorySample;
using Monkeinjection.App.Features.GenericRepositorySample.Services;
using Monkeinjection.App.Features.LogSample;
using Monkeinjection.App.Features.ScopesSample;
using Monkeinjection.App.Features.ScopesSample.Services;
using Monkeinjection.App.Services.AppLogConfiguration;
using System.Diagnostics;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.Configure<TelemetryConfiguration>(config => config.TelemetryChannel = new InMemoryChannel());

		builder.Services.AddLogging(logginBuilder =>
			{
				// Custom
				logginBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CustomLogProvider>());

				// Microsoft.Extensions.Logging.Debug
				logginBuilder.AddDebug();

				// Microsoft.Extensions.Logging.EventSource
				logginBuilder.AddEventSourceLogger();

				// Microsoft.Extensions.Logging.Console
				logginBuilder.AddConsole();
				logginBuilder.AddJsonConsole();
				logginBuilder.AddSimpleConsole();
				logginBuilder.AddSystemdConsole();

				// Microsoft.Extensions.Logging.EventLog
				// Android: System.PlatformNotSupportedException
				// logginBuilder.AddEventLog();

				// Microsoft.Extensions.Logging.TraceSource
				logginBuilder.AddTraceSource(new SourceSwitch(""));

				// Microsoft.Extensions.Logging.Applicationinsights
				logginBuilder.Services.Configure<TelemetryConfiguration>(config => config.TelemetryChannel = new InMemoryChannel());
				logginBuilder.AddApplicationInsights(t => t.ConnectionString = AppKeys.InsightsConnectionString,
					options =>
					{
						options.IncludeScopes = true;
						options.FlushOnDispose = true;
					});
			});

		builder.Services.AddSingleton<AppShell>()
						.AddSingleton<MainPage>()
						.AddSingleton<LogSamplePage>()
						.AddSingleton<GenericRepositorySamplePage>();

		// Log sample

		// Repository sample
		builder.Services.AddTransient(_ => Preferences.Default);
		builder.Services.AddSingleton(typeof(IRepositoryService<>), typeof(RepositoryService<>));

		// Scopes sample
		builder.Services.AddTransient<ITransientService>(_ => new TransientService())
						.AddScoped<IScopeService>(_ => new ScopeService())
						.AddSingleton<ISingletonService>(_ => new SingletonService());

		return builder.Build();
	}
}
