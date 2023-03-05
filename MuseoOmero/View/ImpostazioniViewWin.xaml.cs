using MuseoOmero.Messages;

namespace MuseoOmero.ViewWin;

public partial class ImpostazioniViewWin : ContentPage
{
	public ImpostazioniViewWin()
	{
		InitializeComponent();
	}

	private void HighlightView_Clicked(object sender, EventArgs e)
	{
		var tag= Metadata.GetTag(sender as BindableObject);
		WeakReferenceMessenger.Default.Send(new ThemeChangedMessage(tag));
		Preferences.Set("AppTheme", tag);
	}
}