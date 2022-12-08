namespace Monkeinjection.App.Features.ScopesSample;

using Monkeinjection.App;
using Monkeinjection.App.Features.ScopesSample.Services;
using System;
using System.Collections.ObjectModel;

public partial class MainPage : ContentPage
{
	private readonly IServiceProvider serviceProvider;
	private IServiceScope firstScopeService;
	private IServiceScope lastScopeService;
	private ObservableCollection<CellModel> items;
	private int countScopes = 1;

	public MainPage(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
		firstScopeService = serviceProvider.CreateScope();
		lastScopeService = serviceProvider.CreateScope();

		InitializeComponent();

		BtnResolveRoot.Command = new Command(() => BtnResolveRootCommandExecute());
		BtnResolveScope.Command = new Command(() => BtnResolveScopeCommandExecute());
		BtnCreateOtherScope.Command = new Command(() => BtnCreateOtherScopeCommandExecute());
		BtnResolveLastScope.Command = new Command(() => BtnResolveLastScopeCommandExecute());
		items = new ObservableCollection<CellModel>();
		ListResult.ItemsSource = items;
	}

	private void BtnResolveRootCommandExecute()
	{
		var singletonService = serviceProvider.GetService<ISingletonService>();
		var scopeService = serviceProvider.GetService<IScopeService>();
		var transientService = serviceProvider.GetService<ITransientService>();

		CellModel line = new CellModel
		{
			Title = "Resuelto por el ServiceProvider Root.",
			Line1 = singletonService.GetName(),
			Line2 = scopeService.GetName(),
			Line3 = transientService.GetName(),
		};
		items.Insert(0, line);
	}

	private void BtnResolveScopeCommandExecute()
	{
		var singletonService = firstScopeService.ServiceProvider.GetService<ISingletonService>();
		var scopeService = firstScopeService.ServiceProvider.GetService<IScopeService>();
		var transientService = firstScopeService.ServiceProvider.GetService<ITransientService>();

		CellModel line = new CellModel
		{
			Title = "Resuelto por el primer Scope.",
			Line1 = singletonService.GetName(),
			Line2 = scopeService.GetName(),
			Line3 = transientService.GetName(),
		};
		items.Insert(0, line);
	}

	private void BtnResolveLastScopeCommandExecute()
	{
		var singletonService = lastScopeService.ServiceProvider.GetService<ISingletonService>();
		var scopeService = lastScopeService.ServiceProvider.GetService<IScopeService>();
		var transientService = lastScopeService.ServiceProvider.GetService<ITransientService>();

		CellModel line = new CellModel
		{
			Title = $"Resuelto por el Scope numero {countScopes}.",
			Line1 = singletonService.GetName(),
			Line2 = scopeService.GetName(),
			Line3 = transientService.GetName(),
		};
		items.Insert(0, line);
	}

	private void BtnCreateOtherScopeCommandExecute()
	{
		lastScopeService?.Dispose();
		lastScopeService = serviceProvider.CreateScope();
		countScopes++;
	}
}

