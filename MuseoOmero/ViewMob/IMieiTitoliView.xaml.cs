using MuseoOmero.ViewModelMob;

namespace MuseoOmero.ViewMob;

public partial class IMieiTitoliView : ContentView
{
	private readonly IMieiTitoliViewModel _viewModel;
	private readonly MainViewModel _mainViewModel;
	public IMieiTitoliView()
	{
		_viewModel = App.Current.Handler.MauiContext.Services.GetService<IMieiTitoliViewModel>();
		_mainViewModel = App.Current.Handler.MauiContext.Services.GetService<MainViewModel>();
		BindingContext = _viewModel;
		InitializeComponent();

		// QUALSIASI TENTATIVO FALLISCE LA COLLECTION VIEW NON SI AGGIORNA
		_viewModel.ObserveBiglietti();
		_viewModel.Initialize();
	}

	private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
	{
		_mainViewModel.WavesExpandFactor = e.ScrollY / 160d;
	}
}