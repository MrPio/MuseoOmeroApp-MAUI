namespace MuseoOmero.ViewMob.Templates;

public partial class RoundedEntryViewMob : ContentView
{
	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedEntryViewMob), string.Empty);
	public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RoundedEntryViewMob), string.Empty);
	public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(RoundedEntryViewMob), string.Empty);
	public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsDate), typeof(bool), typeof(RoundedEntryViewMob), false);
	public static readonly BindableProperty IsDateProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(RoundedEntryViewMob), false);
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(RoundedEntryViewMob), DeviceManager.Instance.Colors[0]);
	public static readonly BindableProperty UnfocusedColorProperty = BindableProperty.Create(nameof(UnfocusedColor), typeof(Color), typeof(RoundedEntryViewMob), DeviceManager.Instance.Colors[5]);
	public static readonly BindableProperty MyBackgroundColorProperty = BindableProperty.Create(nameof(MyBackgroundColor), typeof(Color), typeof(RoundedEntryViewMob), DeviceManager.Instance.Colors[4]);
	public static readonly BindableProperty BorderFocusedProperty = BindableProperty.Create(nameof(BorderFocused), typeof(double), typeof(RoundedEntryViewMob), 2d);
	public static readonly BindableProperty BorderUnfocusedProperty = BindableProperty.Create(nameof(BorderUnfocused), typeof(double), typeof(RoundedEntryViewMob), 1d);
	public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(RoundedEntryViewMob), Keyboard.Default);
	public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(RoundedEntryViewMob), DateTime.Today);

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
	public bool IsDate
	{
		get => (bool)GetValue(IsDateProperty);
		set => SetValue(IsDateProperty, value);
	}
	//The color of the icon and the text when there is focus
	public Color Color
	{
		get => (Color)GetValue(ColorProperty);
		set => SetValue(ColorProperty, value);
	}
	//The color of the icon and the text when there is no focus
	public Color UnfocusedColor
	{
		get => (Color)GetValue(UnfocusedColorProperty);
		set => SetValue(UnfocusedColorProperty, value);
	}
	//Entry's background color
	public Color MyBackgroundColor
	{
		get => (Color)GetValue(MyBackgroundColorProperty);
		set => SetValue(MyBackgroundColorProperty, value);
	}
	public double BorderFocused
	{
		get => (double)GetValue(BorderFocusedProperty);
		set => SetValue(BorderFocusedProperty, value);
	}
	public double BorderUnfocused
	{
		get => (double)GetValue(BorderUnfocusedProperty);
		set => SetValue(BorderUnfocusedProperty, value);
	}
	public Keyboard Keyboard
	{
		get => (Keyboard)GetValue(KeyboardProperty);
		set => SetValue(KeyboardProperty, value);
	}
	public DateTime Date
	{
		get => (DateTime)GetValue(DateProperty);
		set => SetValue(DateProperty, value);
	}

	private DeviceManager _devM=>DeviceManager.Instance;

	public RoundedEntryViewMob()
	{
		InitializeComponent();
	}

	private void Entry_Focused(object sender, FocusEventArgs e)
	{
		((Entry)sender).SetAppThemeColor(Entry.TextColorProperty, _devM.Colors[0], _devM.Colors[4]);
		IconLabel.TextColor = Color;
		Border.Stroke = Color;
		Border.StrokeThickness = BorderFocused;
		Border.BackgroundColor=MyBackgroundColor;
	}
	private void Entry_Unfocused(object sender, FocusEventArgs e)
	{
		((Entry)sender).TextColor=UnfocusedColor;
		IconLabel.TextColor = UnfocusedColor;
		Border.Stroke = UnfocusedColor;
		Border.StrokeThickness = BorderUnfocused;
		Border.BackgroundColor = _devM.Colors[4];
	}

	private void DatePicker_Focused(object sender, FocusEventArgs e)
	{
		((DatePicker)sender).SetAppThemeColor(Entry.TextColorProperty, _devM.Colors[0], _devM.Colors[4]);
		IconLabel.TextColor = Color;
		Border.Stroke = Color;
		Border.StrokeThickness = BorderFocused;
		Border.BackgroundColor = MyBackgroundColor;
	}

	private void DatePicker_Unfocused(object sender, FocusEventArgs e)
	{
		((DatePicker)sender).TextColor = UnfocusedColor;
		IconLabel.TextColor = UnfocusedColor;
		Border.Stroke = UnfocusedColor;
		Border.StrokeThickness = BorderUnfocused;
		Border.BackgroundColor = _devM.Colors[4];
	}
}