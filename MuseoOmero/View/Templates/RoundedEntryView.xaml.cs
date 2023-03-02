namespace MuseoOmero.View.Templates;

public partial class RoundedEntryView : ContentView
{
	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(RoundedButtonView), false);
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(RoundedButtonView), DeviceManager.Instance.Colors[0]);
	public static readonly BindableProperty UnfocusedColorProperty = BindableProperty.Create(nameof(UnfocusedColor), typeof(Color), typeof(RoundedButtonView), DeviceManager.Instance.Colors[5]);
	public static readonly BindableProperty MyBackgroundColorProperty = BindableProperty.Create(nameof(MyBackgroundColor), typeof(Color), typeof(RoundedButtonView), DeviceManager.Instance.Colors[4]);
	public static readonly BindableProperty BorderSelectedProperty = BindableProperty.Create(nameof(BorderSelected), typeof(double), typeof(RoundedButtonView), 2.6);
	public static readonly BindableProperty BorderUnselectedProperty = BindableProperty.Create(nameof(BorderUnselected), typeof(double), typeof(RoundedButtonView), 1.0);
	public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(RoundedButtonView), Keyboard.Default);

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
	public Color MyBackgroundColor
	{
		get => (Color)GetValue(MyBackgroundColorProperty);
		set => SetValue(MyBackgroundColorProperty, value);
	}
	public double BorderSelected
	{
		get => (double)GetValue(BorderSelectedProperty);
		set => SetValue(BorderSelectedProperty, value);
	}
	public double BorderUnselected
	{
		get => (double)GetValue(BorderUnselectedProperty);
		set => SetValue(BorderSelectedProperty, value);
	}
	public Keyboard Keyboard
	{
		get => (Keyboard)GetValue(KeyboardProperty);
		set => SetValue(KeyboardProperty, value);
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
		Border.StrokeThickness = BorderSelected;
		Border.BackgroundColor = MyBackgroundColor;
	}

	private void Entry_Unfocused(object sender, FocusEventArgs e)
	{
		((Entry)sender).TextColor = UnfocusedColor;
		IconLabel.TextColor = UnfocusedColor;
		Border.Stroke = UnfocusedColor;
		Border.StrokeThickness = BorderUnselected;
		Border.BackgroundColor = MyBackgroundColor;
	}
}