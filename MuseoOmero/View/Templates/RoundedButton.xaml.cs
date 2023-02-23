using MuseoOmero.ViewModel.Templates;

namespace MuseoOmero.View.Templates;

public partial class RoundedButton : ContentView
{
    ResourceDictionary MyColors = Application.Current.Resources.MergedDictionaries.First();

    public RoundedButton()
	{
		InitializeComponent();
	}

    private void RoundedButton_Clicked(object sender, EventArgs e)
    {
        ((RoundedButtonViewModel)BindingContext).OnClick.Invoke();
    }

    private void RoundedButton_Pressed(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Colors.White;
        ((Button)sender).TextColor = (Color)MyColors["Primary"];
        ((Button)sender).BorderWidth = 4;
    }

    private void RoundedButton_Released(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = (Color)MyColors["Primary"];
        ((Button)sender).TextColor = Colors.White;
        ((Button)sender).BorderWidth = 0;
    }
}