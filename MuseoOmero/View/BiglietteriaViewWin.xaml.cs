namespace MuseoOmero.ViewWin;

public partial class BiglietteriaViewWin : ContentPage
{
	BiglietteriaViewModelWin _viewModel;
	public BiglietteriaViewWin(BiglietteriaViewModelWin viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_viewModel.Initialize();
	}

	private void HighlightView_Pressed(object sender, EventArgs e)
	{
		QrCodeFrame.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => QrCodeFrame.BackgroundColor = c, 350, Easing.CubicOut);
		QrCodeIcon.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => QrCodeIcon.TextColor = c, 350, Easing.CubicOut);
	}

	private void HighlightView_Released(object sender, EventArgs e)
	{
		QrCodeFrame.CancelAnimation();
		QrCodeIcon.CancelAnimation();
		QrCodeFrame.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => QrCodeFrame.BackgroundColor = c, 350, Easing.CubicOut);
		QrCodeIcon.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => QrCodeIcon.TextColor = c, 350, Easing.CubicOut);
	}

	private async void HighlightView_Clicked(object sender, EventArgs e)
	{
		var b = ((Button)sender).Parent.Parent.BindingContext as Biglietto;
		await DisplayAlert("Biglietto convalidato", $"Il biglietto con destinazione {b.Tipologia}, è stato acquistato il {b.DataAcquisto:m} per il giorno di validità  {b.DataValidita:m}.", "Ok");
	}

	private void CardViewClose_Clicked(object sender, EventArgs e)
	{

		CardViewTransition(false);
	}
	public async void CardViewTransition(bool show)
	{
		if (show)
		{
			CardView.TranslationX = 0;
			Overlay.TranslationX = 0;
		}
		CardView.FadeTo((show ? 1 : 0), 450, Easing.CubicOut);
		CardView.TranslateTo(0, 50 * (show ? 0 : 1), 450, Easing.CubicOut);
		await Overlay.FadeTo((show ? 0.6 : 0), 450, Easing.CubicOut);

		if (!show)
		{
			CardView.TranslationX = 9999;
			Overlay.TranslationX = 9999;
		}
	}

	private void BigliettoElement_Clicked(object sender, EventArgs e)
	{
		var b = ((Button)sender).Parent.Parent.BindingContext as Biglietto;
		_viewModel.SelectedBiglietto = b;
		CardViewTransition(true);
	}

	private void FiltroPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		_viewModel.BigliettiSortAcending = true;
		_viewModel.OrdinaBiglietti();
	}
}
