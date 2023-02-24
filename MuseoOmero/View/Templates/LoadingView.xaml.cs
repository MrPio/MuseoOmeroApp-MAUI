namespace MuseoOmero.View.TemplatesWin;

public partial class LoadingView : ContentView
{
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(LoadingView), DeviceManager.Instance.Colors[1]);
	public Color Color
	{
		get => (Color)GetValue(ColorProperty);
		set => SetValue(ColorProperty, value);
	}

	public LoadingView()
	{
		InitializeComponent();
	}
}