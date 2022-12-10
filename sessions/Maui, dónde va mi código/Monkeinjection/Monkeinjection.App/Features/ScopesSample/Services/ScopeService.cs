namespace Monkeinjection.App.Features.ScopesSample.Services;

using System;

internal class ScopeService : IScopeService
{
	private string name;

	public ScopeService()
	{
		name = $"Scope: {Guid.NewGuid()}.";
	}

	public string GetName()
	{
		return name;
	}
}
