using System.Security.AccessControl;

namespace MuseoOmero.ViewWin;

public partial class BiglietteriaViewWin : ContentPage
{
	BiglietteriaViewModelWin _viewModel;
	private ShellViewModelWin _shellViewModelWin;

	public BiglietteriaViewWin(BiglietteriaViewModelWin viewModel, ShellViewModelWin shellViewModelWin)
	{
		_viewModel = viewModel;
		_shellViewModelWin = shellViewModelWin;
		BindingContext = _viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_viewModel.Initialize();
		_viewModel.CardViewTransitionCallback = CardViewTransition;
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
		await DisplayAlert("Biglietto convalidato", $"Il biglietto con destinazione {b.Tipologia}, � stato acquistato il {b.DataAcquisto:m} per il giorno di validit�  {b.DataValidita:m}.", "Ok");
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

	private async void BigliettoElement_Clicked(object sender, EventArgs e)
	{
		var b = ((Button)sender).Parent.Parent.BindingContext as Biglietto;
		_viewModel.SelectedBiglietto = b;
		_viewModel.VendiBigliettoOn = false;
		CardViewTransition(true);
	}

	private void FiltroPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		_viewModel.BigliettiSortAcending = true;
		_viewModel.OrdinaBiglietti();
	}

	private async void AggiungiBiglietto_Clicked(object sender, EventArgs e)
	{
		if (this.AnimationIsRunning("translation"))
			return;

		// Chiedo chi � il compratore
		var values = new List<string>();
		_viewModel.HomeViewModelWin.Utenti.ForEach(u => values.Add($"{u.Nome} {u.Cognome}"));
		var nomeCognome = await DisplayActionSheet("Seleziona Utente", null,null, values.OrderBy(s => s).ToArray());
		if (string.IsNullOrEmpty(nomeCognome))
			return;
		VendiBigliettoTitolo.Text = $"Acquisto biglietto per: {nomeCognome}";
		var nome = nomeCognome.Split(' ')[0];
		var cognome = nomeCognome.Split(' ')[1];
		_viewModel.BuyerUid = _viewModel.HomeViewModelWin.Utenti.Find(u => u.Nome == nome && u.Cognome == cognome).Uid;

		// Inizializzo il nuovo biglietto
		_viewModel.NuovoBiglietto = new Biglietto(
						dataAcquisto: DateTime.Now,
						dataValidita: DateTime.Today,
						tipologia: TipoBiglietto.MuseoAperto,
						dataGuida: TimeSpan.FromHours(12)
					);

		//Forzo l'aggiornamento per Picker e TimePicker bugs
		_shellViewModelWin.SelectedRoute = "blank";
		_shellViewModelWin.SelectedRoute = "biglietteria";

		_viewModel.VendiBigliettoOn = true;
		CardViewTransition(true);
	}
}
