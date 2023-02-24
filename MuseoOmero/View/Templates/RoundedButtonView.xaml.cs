using MuseoOmero.ViewModel.Templates;
using System.Windows.Input;

namespace MuseoOmero.View.Templates;

public partial class RoundedButtonView : ContentView
{
	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(RoundedButtonView), DeviceManager.Instance.Colors[1]);
	public static readonly BindableProperty NewHeightProperty = BindableProperty.Create(nameof(Height), typeof(double), typeof(RoundedButtonView), 48d);
	public static readonly BindableProperty NewFontSizeProperty = BindableProperty.Create(nameof(NewFontSize), typeof(double), typeof(RoundedButtonView), 18d);

	public event EventHandler Clicked;

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}
	public Color Color
	{
		get => (Color)GetValue(ColorProperty);
		set => SetValue(ColorProperty, value);
	}
	public  double NewHeight
	{
		get => (double)GetValue(NewHeightProperty);
		set => SetValue(NewHeightProperty, value);
	}
	public double NewFontSize
	{
		get => (double)GetValue(NewFontSizeProperty);
		set => SetValue(NewFontSizeProperty, value);
	}


	public RoundedButtonView()
	{
		InitializeComponent();
		Button.HeightRequest= (double)GetValue(NewHeightProperty);
	}

	private void RoundedButton_Pressed(object sender, EventArgs e)
	{
		((Button)sender).BackgroundColor = Colors.White;
		((Button)sender).TextColor = Color;
		((Button)sender).BorderWidth = 4;
	}

	private void RoundedButton_Released(object sender, EventArgs e)
	{
		((Button)sender).BackgroundColor = Color;
		((Button)sender).TextColor = Colors.White;
		((Button)sender).BorderWidth = 0;
	}

	private void RoundedButton_Clicked(object sender, EventArgs e)
	{
		Clicked.Invoke(sender, e);
	}
}