namespace Monkeinjection.App.Services.AppLogConfiguration;
using Microsoft.Extensions.Logging;
using System;

public sealed class CustomLogProvider : ILoggerProvider
{
	public CustomLogProvider()
	{
	}

	public ILogger CreateLogger(string categoryName)
	{
		return new CustomLog(categoryName);
	}

	public void Dispose()
	{
	}
}