namespace MuseoOmero.ViewMob;

public partial class IMieiTitoliView : ContentView
{
	private IMieiTitoliViewModel _viewModel;
	private MainViewModel _mainViewModel;
	public IMieiTitoliView()
	{
		_viewModel = App.Current.Handler.MauiContext.Services.GetService<IMieiTitoliViewModel>();
		_mainViewModel = App.Current.Handler.MauiContext.Services.GetService<MainViewModel>();
		InitializeComponent();
		BindingContext = _viewModel;

		// QUALSIASI TENTATIVO FALLISCE, LA COLLECTION VIEW NON SI AGGIORNA
		// SOLUZIONE ATTRAVERSO SHARPNADO IO RI-ASSEGNAVO IL BINDING CONTEXT DOPO QUESTO COSTRUTTORE!
		_viewModel.FetchBiglietti();
		Task.Run(_viewModel.ObserveBiglietti);

	}

	private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
	{
		_mainViewModel.WavesExpandFactor = e.VerticalOffset / 160d;
	}
}