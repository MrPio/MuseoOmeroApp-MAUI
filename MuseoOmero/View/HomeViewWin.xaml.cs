using Microcharts;
using SkiaSharp.Views.Maui;

namespace MuseoOmero.ViewWin;

public partial class HomeViewWin : ContentPage
{
	private readonly HomeViewModelWin _viewModel = new();
	public HomeViewWin(HomeViewModelWin viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		_viewModel.Initialize();
		InitializeComponent();
		Picker_SelectedIndexChanged(RepartoPicker, null);

	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		DrawChart();
		new Task(async delegate
		{
			await Task.Delay(1000);
			Loading.IsVisible = false;
		}).RunSynchronously();
	}

	private void DrawChart()
	{
		var dv = DeviceManager.Instance;
		TipologiaVisiteChart.Chart = new Microcharts.DonutChart
		{
			LabelTextSize = 22,
			BackgroundColor = Colors.Transparent.ToSKColor(),
			IsAnimated = true,
			LabelMode = LabelMode.LeftAndRight,
			Margin = 46,
			HoleRadius = 1.4f,
			Entries = new List<ChartEntry>
			{
				new ChartEntry(51)
				{
					Color = dv.Colors[0].ToSKColor()
				},
				new ChartEntry(8)
				{
					Color = dv.Colors[2].ToSKColor()
				},
				new ChartEntry(28)
				{
					Color = dv.Colors[3].ToSKColor()
				}
			}
		};


	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (sender == RepartoPicker)
		{
			var elems = RepartoPicker.SelectedIndex == 0 ? _viewModel.Sale : _viewModel.NomiMostre;
			SalaMostraPicker.ItemsSource = elems;
			SalaMostraPicker.ItemsSource = SalaMostraPicker.GetItemsAsArray(); //https://github.com/dotnet/maui/issues/9739
			if (elems.Count > 0)
				SalaMostraPicker.SelectedIndex = 0;
		}
		_viewModel.FiltraOpere();
	}

	private void OpereFrame_Tapped(object sender, EventArgs e)
	{
		var shellViewModel = this.Handler.MauiContext.Services.GetService<ShellViewModelWin>();
		shellViewModel.SelectedRoute = "opere";
	}

	private void VisiteFrame_Tapped(object sender, EventArgs e)
	{
		var shellViewModel = this.Handler.MauiContext.Services.GetService<ShellViewModelWin>();
		shellViewModel.SelectedRoute = "statistiche";
	}
}