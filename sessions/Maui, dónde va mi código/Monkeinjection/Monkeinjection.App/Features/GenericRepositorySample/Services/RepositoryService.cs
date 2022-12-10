namespace Monkeinjection.App.Features.GenericRepositorySample.Services;
using System;

internal class RepositoryService<TModel> : IRepositoryService<TModel>
{
	private readonly IPreferences preferences;
	private readonly string name;

	public RepositoryService(IPreferences preferences)
	{
		this.preferences = preferences;
		name = $"{typeof(TModel).Name}: {Guid.NewGuid()}.";
	}

	public string GetName()
	{
		return name;
	}
}
