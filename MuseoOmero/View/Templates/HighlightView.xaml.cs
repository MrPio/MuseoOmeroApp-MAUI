using System.Windows.Input;

namespace MuseoOmero.View.TemplatesWin;

public partial class HighlightView : ContentView
{
	public static readonly BindableProperty AlphaProperty = BindableProperty.Create(nameof(Alpha), typeof(float), typeof(HighlightView), 0.058f);
	public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(HighlightView), 0);
	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(HighlightView), null);
	public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(HighlightView), null);

	public float Alpha
	{
		get => (float)GetValue(AlphaProperty);
		set => SetValue(AlphaProperty, value);
	}
	public int CornerRadius
	{
		get => (int)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
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

	public event EventHandler Clicked, Pressed, Released;
	public HighlightView()
	{
		InitializeComponent();
	}

	private void Button_Pressed(object sender, EventArgs e)
	{
		Button.BackgroundColor = DeviceManager.Instance.Colors[0].WithAlpha(Alpha);
		Pressed?.Invoke(sender, e);
	}

	private void Button_Released(object sender, EventArgs e)
	{
		Button.BackgroundColor = Colors.Transparent;
		Released?.Invoke(sender, e);
	}
	private void Button_Clicked(object sender, EventArgs e)
	{
		if (Command is { })
		{
			Command.Execute(CommandParameter);
		}
		else Clicked?.Invoke(sender, e);
	}
}