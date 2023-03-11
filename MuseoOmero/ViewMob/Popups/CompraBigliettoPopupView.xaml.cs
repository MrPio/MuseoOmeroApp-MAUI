using Mopups.Interfaces;
using MuseoOmero.ViewModelMob;

namespace MuseoOmero.ViewMob.Popups;

public partial class CompraBigliettoPopupView
{
	private readonly IPopupNavigation _popupNavigation;

	public CompraBigliettoPopupView(IPopupNavigation popupNavigation)
	{
		_popupNavigation = popupNavigation;
		InitializeComponent();
	}
	private bool _conGuida;
	public bool ConGuida
	{
		get => _conGuida;
		set
		{
			_conGuida = value;
			OnPropertyChanged(nameof(ConGuida));
		}
	}

	private async void AcquistaBiglietto_Clicked(object sender, EventArgs e)
	{
		var biglietto = BindingContext as Biglietto;
		var utente = AccountManager.Instance.Utente;

		if (biglietto.DataValidita < DateTime.Today)
		{
			IsBusy.IsVisible = false;
			await App.Current.MainPage.DisplayAlert("Acquisto non effettuato", "Per favore inserisci un giorno di validità del biglietto valido.", "Ok");
			return;
		}
		int costo = biglietto.Tipologia switch
		{
			TipoBiglietto.Mostra => 5,
			TipoBiglietto.Laboratorio => 7,
			_ => 0,
		};
		if (biglietto.OrarioGuida is { })
			costo += 5;

			if (!await App.Current.MainPage.DisplayAlert("Conferma acquisto", $"Sei sicuro di voler procedere con l'acquisto del biglietto destinato a {biglietto.Tipologia}, al costo di € {costo:N2}.","Si", "Annulla"))
		{
			return;
		}

		IsBusy.IsVisible = true;

		utente.Biglietti.Add(biglietto);
		await DatabaseManager.Instance.Put($"utenti/{utente.Uid}/biglietti/{utente.Biglietti.Count - 1}", biglietto);
		Service.Get<IMieiTitoliViewModel>().FetchBiglietti();
		IsBusy.IsVisible = false;
		await App.Current.MainPage.DisplayAlert("Acquisto effettuato", "Acquisto del biglietto effettuato con successo!", "Ok");

		await _popupNavigation.PopAllAsync();
	}
}