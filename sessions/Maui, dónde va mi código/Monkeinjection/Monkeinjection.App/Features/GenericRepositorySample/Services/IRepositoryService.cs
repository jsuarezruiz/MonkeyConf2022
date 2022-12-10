namespace Monkeinjection.App.Features.GenericRepositorySample.Services;

internal interface IRepositoryService<TModel>
{
	string GetName();
}