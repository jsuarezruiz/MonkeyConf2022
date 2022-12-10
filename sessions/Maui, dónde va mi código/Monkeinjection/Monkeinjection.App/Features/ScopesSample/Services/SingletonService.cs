namespace Monkeinjection.App.Features.ScopesSample.Services;
using System;

internal class SingletonService : ISingletonService
{
	private string name;

	public SingletonService()
	{
		name = $"Singleton: {Guid.NewGuid()}.";
	}

	public string GetName()
	{
		return name;
	}
}
