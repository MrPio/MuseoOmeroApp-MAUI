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
		IsBusy = true;
		var url = await StorageManager.Instance.GetLink($"{AccountManager.Instance.Uid}/foto_profilo/");
		ImageUrl = url is { } ? url : ImagesOnline.Anonymous;

		Dipendente = AccountManager.Instance.Dipendente;
		IsBusy = false;
	}
}
