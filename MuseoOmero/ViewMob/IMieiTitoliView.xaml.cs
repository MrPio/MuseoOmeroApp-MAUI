namespace MuseoOmero.ViewMob;

public partial class IMieiTitoliView : ContentView
{
	private IMieiTitoliViewModel _viewModel;
	private MainViewModel _mainViewModel;
	private bool wavesRestricted;
	public IMieiTitoliView()
	{
		_viewModel = Service.Get<IMieiTitoliViewModel>();
		_mainViewModel = Service.Get<MainViewModel>();
		InitializeComponent();
		BindingContext = _viewModel;

		// QUALSIASI TENTATIVO FALLISCE, LA COLLECTION VIEW NON SI AGGIORNA
		// SOLUZIONE: ATTRAVERSO SHARPNADO IO RI-ASSEGNAVO IL BINDING CONTEXT DOPO QUESTO COSTRUTTORE!
		_viewModel.FetchBiglietti();
		Task.Run(_viewModel.ObserveBiglietti);
	}

	private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
	{
		var offset = e.VerticalOffset;
		AggiornaRow.TranslationY = -offset;

		var anim = new[] { 0, 2 };
		if (wavesRestricted)
			anim = anim.Reverse().ToArray();

		if (offset > 100 && !wavesRestricted || offset < 100 && wavesRestricted)
		{
			if (this.AnimationIsRunning("waves"))
				return;
			this.Animate("waves", new Animation(v => _mainViewModel.WavesExpandFactor = v, anim[0], anim[1], easing: Easing.CubicOut), length: 1100);
			wavesRestricted.Swap();
		}
	}
}