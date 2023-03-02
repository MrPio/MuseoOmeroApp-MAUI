namespace MuseoOmero.ViewModelWin;

public partial class AccountViewModelWin : ObservableObject
{
	[ObservableProperty]
	string imageUrl;
	[ObservableProperty]
	bool isBusy;
	[ObservableProperty]
	Dipendente dipendente;

	public async void Initialize()
	{
		var account = AccountManager.Instance;
		IsBusy = true;
		var url = await StorageManager.Instance.GetLink($"{AccountManager.Instance.Uid}/foto_profilo/");
		ImageUrl = url is { } ? url : ImagesOnline.Anonymous;
		await account.LoadDipendente();
		Dipendente = account.Dipendente;
		IsBusy = false;
	}
}
