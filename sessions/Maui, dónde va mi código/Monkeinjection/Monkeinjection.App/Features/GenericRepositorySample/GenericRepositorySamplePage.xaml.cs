namespace Monkeinjection.App.Features.GenericRepositorySample;

using Microsoft.Extensions.DependencyInjection;
using Monkeinjection.App.Features.GenericRepositorySample.Services;
using System;
using System.Collections.ObjectModel;

public partial class GenericRepositorySamplePage : ContentPage
{
	private ObservableCollection<CellModel> items;
	private IServiceProvider serviceProvider;

	public GenericRepositorySamplePage(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;

		InitializeComponent();

		BtnResolveUser.Command = new Command(() => BtnResolveUserCommandExecute());
		BtnResolveCar.Command = new Command(() => BtnResolveCarCommandExecute());

		items = new ObservableCollection<CellModel>();
		
		ListResult.ItemsSource = items;
	}

	private void BtnResolveCarCommandExecute()
	{
		IRepositoryService<CarModel> repositoryService = serviceProvider.GetService<IRepositoryService<CarModel>>();

		CellModel line = new CellModel
		{
			Title = "Resolviendo IRepositoryService<CarModel>.",
			Line1 = repositoryService.GetName(),
		};
		items.Insert(0, line);
	}

	private void BtnResolveUserCommandExecute()
	{
		IRepositoryService<UserModel> repositoryService = serviceProvider.GetService<IRepositoryService<UserModel>>();

		CellModel line = new CellModel
		{
			Title = "Resolviendo IRepositoryService<UserModel>.",
			Line1 = repositoryService.GetName(),
		};
		items.Insert(0, line);
	}
}