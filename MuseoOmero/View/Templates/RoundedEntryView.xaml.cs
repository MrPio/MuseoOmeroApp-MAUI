using MuseoOmero.Managers;
using MuseoOmero.ViewModel.Templates;

namespace MuseoOmero.View.Templates;

public partial class RoundedEntryView : ContentView
{
	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(RoundedButtonView), false);
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(RoundedButtonView), DeviceManager.Instance.Colors[0]);
	public static readonly BindableProperty UnfocusedColorProperty = BindableProperty.Create(nameof(UnfocusedColor), typeof(Color), typeof(RoundedButtonView), DeviceManager.Instance.Colors[4]);

	public event EventHandler Clicked;

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}
	public string Placeholder
	{
		get => (string)GetValue(PlaceholderProperty);
		set => SetValue(PlaceholderProperty, value);
	}
	public string Icon
	{
		get => (string)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}
	public bool IsPassword
	{
		get => (bool)GetValue(IsPasswordProperty);
		set => SetValue(IsPasswordProperty, value);
	}
	public Color Color
	{
		get => (Color)GetValue(ColorProperty);
		set => SetValue(ColorProperty, value);
	}
	public Color UnfocusedColor
	{
		get => (Color)GetValue(UnfocusedColorProperty);
		set => SetValue(UnfocusedColorProperty, value);
	}

	private DeviceManager DeviceManager { get => DeviceManager.Instance; }
	public RoundedEntryView()
	{
		InitializeComponent();
	}

	private void Entry_Focused(object sender, FocusEventArgs e)
	{
		((Entry)sender).TextColor = DeviceManager.Colors[0];
		IconLabel.TextColor = Color;
		Border.Stroke = Color;
		Border.StrokeThickness = 2.6;
		Border.BackgroundColor = Colors.White;
	}

	private void Entry_Unfocused(object sender, FocusEventArgs e)
	{
		((Entry)sender).TextColor = UnfocusedColor;
		IconLabel.TextColor = UnfocusedColor;
		Border.Stroke = UnfocusedColor;
		Border.StrokeThickness = 1f;
		Border.BackgroundColor = Colors.White;
	}
}