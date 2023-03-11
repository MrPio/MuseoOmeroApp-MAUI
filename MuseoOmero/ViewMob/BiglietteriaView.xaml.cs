using Mopups.Interfaces;
using MuseoOmero.ViewMob.Popups;
using MuseoOmero.ViewModelMob;

namespace MuseoOmero.ViewMob;

public partial class BiglietteriaView : ContentView
{
	private readonly IPopupNavigation _popupNavigation;
	public BiglietteriaView()
	{
		InitializeComponent();
		_popupNavigation = Service.Get<IPopupNavigation>();
		MuseoAperto.Item_Clicked(null, null);
	}

	private void BiglietteriaItemView_OnItemExpanding(object sender, EventArgs e)
	{
		var items = new[] { MuseoAperto, Mostra, Laboratorio };
		foreach (var item in items.Where(i => i.IsExpanded))
			item.Item_Clicked(null, null);
	}

	private async void BiglietteriaItemView_OnOptionClicked(object sender, EventArgs e)
	{
		var tipologiaStr = ((string)sender).Split(':')[0];
		var visita = ((string)sender).Split(':')[1];

		var tipologiaConv = new Dictionary<string, TipoBiglietto>()
		{
			[MuseoAperto.Title] = TipoBiglietto.MuseoAperto,
			[Mostra.Title] = TipoBiglietto.Mostra,
			[Laboratorio.Title] = TipoBiglietto.Laboratorio
		};

		var biglietto = new Biglietto(
			dataAcquisto: DateTime.Now,
			dataValidita: DateTime.Today,
			tipologia: tipologiaConv[tipologiaStr]
		);

		if (tipologiaStr == Mostra.Title)
		{
			var mostre = Service.Get<MainViewModel>().Mostre;
			var titolo = await App.Current.MainPage.DisplayActionSheet(
				"Seleziona la mostra alla quale desideri partecipare", null, null,
				mostre.Select(m => m.Titolo).ToArray());
			if (string.IsNullOrEmpty(titolo))
				return;
			var mostra = mostre.Find(m => m.Titolo == titolo)!;
			biglietto.DataValidita = mostra.DataInizio;
			if(DateTime.Today>mostra.DataInizio)
				biglietto.DataValidita = DateTime.Today;
			if (DateTime.Today > mostra.DataFine)
			{
				await App.Current.MainPage.DisplayAlert("Mostra passata",
					"Si prega di selezionare una mostra non già conclusa.", "Ok");
				return;
			}
			OnPropertyChanged(nameof(biglietto.DataValidita));
		}
		if (visita == "Con Guida")
			biglietto.OrarioGuida = TimeSpan.FromHours(12);
		var popup = new CompraBigliettoPopupView(_popupNavigation)
		{
			BindingContext = biglietto
		};
		if (visita == "Con Guida")
			popup.ConGuida = true;

		_popupNavigation.PushAsync(popup);
	}
}