namespace MuseoOmero.ViewMob;

public partial class IMieiTitoliView : ContentView
{
	private IMieiTitoliViewModel _viewModel;
	private MainViewModel _mainViewModel;
	private bool wavesRestricted;
	double lastOffset;
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

		if (offset > 50 || !wavesRestricted)
			if (this.AnimationIsRunning("waves"))
				return;
		wavesRestricted = _mainViewModel.WavesExpandFactor > 1;
		var anim = new[] { 0, 2 };
		if (wavesRestricted)
			anim = anim.Reverse().ToArray();

		if (offset > lastOffset + 30 && !wavesRestricted || offset < lastOffset - 30 && wavesRestricted)
		{
			this.Animate("waves", new Animation(v => _mainViewModel.WavesExpandFactor = v, anim[0], anim[1], easing: Easing.CubicOut), length: 1200);
			wavesRestricted.Swap();
		}

		if (offset < lastOffset && !wavesRestricted || offset > lastOffset && wavesRestricted)
			lastOffset = offset;

	}
}