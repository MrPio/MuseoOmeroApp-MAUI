using Mopups.Interfaces;
using Mopups.Services;
using MuseoOmero.ViewMob.Popups;

namespace MuseoOmero.ViewModelMob.Templates;

public partial class BigliettoViewModel : ObservableObject
{
	public BigliettoView View;
	private readonly IPopupNavigation _popupNavigation;


	[ObservableProperty]
	DateTime data;

	[ObservableProperty]
	string tipologiaBiglietto, turnoGuida, icon;

	[ObservableProperty]
	Biglietto biglietto;

	public BigliettoViewModel(Biglietto biglietto)
	{
		Biglietto = biglietto;
		Data = biglietto.DataValidita;
		TipologiaBiglietto = Enum.GetName(biglietto.Tipologia);
		TurnoGuida = biglietto.OrarioGuida == null ? "No turno guida." : "Guida alle " +
		biglietto.OrarioGuida?.ToString(@"hh\:mm");
		Icon = IconeBiglietto.Values[(int)biglietto.Tipologia];
		_popupNavigation = Service.Get<IPopupNavigation>();
	}


	[RelayCommand]
	void VaiAlBigliettoClicked()
	{
		var popup = new BigliettoPopupView( _popupNavigation);
		popup.BindingContext = ((BigliettoViewModel)View.BindingContext).Biglietto;
		_popupNavigation.PushAsync(popup);
	}

}
