namespace Monkeinjection.App.Features.LogSample;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Monkeinjection.App.Services.AppLogConfiguration;
using System;

public partial class LogSamplePage
{
	private readonly IServiceProvider serviceProvider;
	private readonly ILogger<LogSamplePage> logService;

	public LogSamplePage(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
		logService = serviceProvider.GetService<ILogger<LogSamplePage>>();

		InitializeComponent();

		BtnWriteInfo.Command = new Command(() => BtnWriteLogCommandExecute("info"));
		BtnWriteTrace.Command = new Command(() => BtnWriteLogCommandExecute("trace"));
		BtnWriteWarning.Command = new Command(() => BtnWriteLogCommandExecute("warning"));
		BtnWriteError.Command = new Command(() => BtnWriteLogCommandExecute("error"));
		BtnWriteCritical.Command = new Command(() => BtnWriteLogCommandExecute("critical"));
	}

	private void BtnWriteLogCommandExecute(string level)
	{
		switch (level)
		{
			case "debug":
				logService.LogDebug(TxtEditor.Text);
				break;
			case "info":
				logService.LogInformation(TxtEditor.Text);
				break;
			case "trace":
				logService.LogTrace(TxtEditor.Text);
				break;
			case "warning":
				logService.LogWarning(TxtEditor.Text);
				break;
			case "error":
				logService.LogError(TxtEditor.Text);
				break;
			case "critical":
				logService.LogCritical(TxtEditor.Text);
				break;
			default:
				break;
		}
	}
}