using Mopups.Interfaces;

namespace MuseoOmero.ViewMob;

public partial class AccountView
{
	IPopupNavigation _popupNavigation => Service.Get<IPopupNavigation>();

	public AccountView()
	{
		InitializeComponent();
	}

	private async void FotoProfilo_Clicked(object sender, EventArgs e)
	{
		Loading.IsVisible = true;
		var fileResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Scegli una nuova foto di profilo" });
		if (fileResult is { })
		{
			var utente = (Utente)BindingContext;
			
			//var stream = UtiliesManager.Instance.ImageToStream(fileResult.FullPath, true);
			await StorageManager.Instance.Upload(
				resource: $"{utente.Uid}/foto_profilo/",
				stream: File.OpenRead(fileResult.FullPath)
			);
			utente.FotoProfilo = await StorageManager.Instance.GetLink($"{utente.Uid}/foto_profilo/");
			FotoProfilo.Source = utente.FotoProfilo;

			AccountManager.Instance.Utente.FotoProfilo = utente.FotoProfilo;
			await DatabaseManager.Instance.Put($"utenti/{utente.Uid}/foto_profilo", utente.FotoProfilo);
			Service.Get<MainViewModel>().FotoProfilo = utente.FotoProfilo;
		}
		Loading.IsVisible = false;
	}

	private async void SalvaEChiudi_Clicked(object sender, EventArgs e)
	{
		Loading.IsVisible = true;
		var utente = (Utente)BindingContext;
		AccountManager.Instance.Utente = utente;
		await DatabaseManager.Instance.SaveUtente(utente);
		Loading.IsVisible = false;
		_popupNavigation.PopAllAsync();
		App.Current.MainPage.DisplayAlert("Account aggiornato", "Modifiche salvate con successo!", "Ok");

	}
}