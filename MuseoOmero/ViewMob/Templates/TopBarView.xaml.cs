using Mopups.Interfaces;

namespace MuseoOmero.ViewMob.Templates;

public partial class TopBarView : ContentView
{
	public TopBarView()
	{
		InitializeComponent();
	}
	private async void Logout_Tapped(object sender, EventArgs e)
	{
		if (await Application.Current.MainPage.DisplayAlert("Vuoi davvero uscire?", "Dovrai eseguire  di nuovo il login per rientrare.", "Yes", "No"))
		{
			AccountManager.Instance.DeleteCache();

			var mainViewModel = App.Current.Handler.MauiContext.Services.GetService<MainViewModel>();
			var popup = App.Current.Handler.MauiContext.Services.GetService<IPopupNavigation>();
			Application.Current.MainPage = new SignInUpView(mainViewModel, popup);
		}
	}
}