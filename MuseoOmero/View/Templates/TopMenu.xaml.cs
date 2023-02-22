namespace MuseoOmero.View.TemplatesWin;

public partial class TopMenu : ContentView
{
	public TopMenu()
	{
		InitializeComponent();
	}

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		var shellViewModel = this.Handler.MauiContext.Services.GetService<ShellViewModelWin>();
		shellViewModel.SelectedRoute = "account";
	}
}