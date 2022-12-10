namespace Monkeinjection.App.Services.AppLogConfiguration;

using Microsoft.Extensions.Logging;
using System;

internal class CustomLog : ILogger
{
	private string categoryName;

	public CustomLog(string categoryName)
	{
		this.categoryName = categoryName;
	}

	public IDisposable BeginScope<TState>(TState state) => default!;

	public bool IsEnabled(LogLevel logLevel) => true;

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
	{
		Console.WriteLine($"Esto es mi super custom log: {categoryName} - {logLevel} - {eventId.Name ?? string.Empty} - {state} - {exception?.ToString()}");
	}
}
