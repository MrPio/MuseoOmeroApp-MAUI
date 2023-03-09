using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace MuseoOmero.ViewMob.Templates;

public partial class SmallIconedButton : ContentView
{
	public static readonly DeviceManager Dev = DeviceManager.Instance;

	public static readonly BindableProperty TextProperty = BindableProperty.Create(
		nameof(Text),
		typeof(string),
		typeof(SmallIconedButton),
		string.Empty);
	public static readonly BindableProperty IconProperty = BindableProperty.Create(
		nameof(Icon),
		typeof(string),
		typeof(SmallIconedButton),
		string.Empty);
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(
		nameof(Color),
		typeof(Color),
		typeof(SmallIconedButton),
		Dev.Colors[0]);
	public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(
		nameof(BackgroundColor),
		typeof(Color),
		typeof(SmallIconedButton),
		Dev.Colors[4]);
	public static readonly BindableProperty CommandProperty = BindableProperty.Create(
		nameof(Command),
		typeof(ICommand),
		typeof(SmallIconedButton),
		null);
	public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
		nameof(CommandParameter), 
		typeof(object), 
		typeof(SmallIconedButton), 
		null);


	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}
	public string Icon
	{
		get => (string)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}
	public Color Color
	{
		get => (Color)GetValue(ColorProperty);
		set => SetValue(ColorProperty, value);
	}
	public new Color BackgroundColor
	{
		get => (Color)GetValue(BackgroundColorProperty);
		set => SetValue(BackgroundColorProperty, value);
	}
	public ICommand Command
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);

	}
	public object CommandParameter
	{
		get => (object)GetValue(CommandParameterProperty);
		set => SetValue(CommandParameterProperty, value);
	}
	public SmallIconedButton()
	{
		InitializeComponent();
	}
}