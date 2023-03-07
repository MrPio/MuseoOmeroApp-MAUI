using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MuseoOmero.View.Templates;

public partial class RoundedButtonView : ContentView
{
	public static readonly DeviceManager Dev = DeviceManager.Instance;

	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedButtonView), string.Empty);
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(RoundedButtonView), Dev.Colors[1]);
	public static readonly BindableProperty ReleasedTextColorProperty = BindableProperty.Create(nameof(ReleasedTextColor), typeof(Color), typeof(RoundedButtonView), Dev.Colors[4]);
	public static readonly BindableProperty PressedBackgroundColorProperty = BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(RoundedButtonView), Dev.Colors[4]);
	public static readonly BindableProperty NewHeightProperty = BindableProperty.Create(nameof(Height), typeof(double), typeof(RoundedButtonView), 50d);
	public static readonly BindableProperty NewFontSizeProperty = BindableProperty.Create(nameof(NewFontSize), typeof(double), typeof(RoundedButtonView), 18d);
	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RoundedButtonView), null);
	public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RoundedButtonView), null);
	public static readonly BindableProperty StyleInvertProperty = BindableProperty.Create(nameof(StyleInvert), typeof(bool), typeof(RoundedButtonView), false);
	public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(RoundedButtonView), 2d);
	public static readonly BindableProperty ReleasedColorAlphaProperty = BindableProperty.Create(nameof(ReleasedColorAlpha), typeof(double), typeof(RoundedButtonView), 1d);


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
	public Color ReleasedTextColor
	{
		get => (Color)GetValue(ReleasedTextColorProperty);
		set => SetValue(ReleasedTextColorProperty, value);
	}
	public Color PressedBackgroundColor
	{
		get => (Color)GetValue(PressedBackgroundColorProperty);
		set => SetValue(PressedBackgroundColorProperty, value);
	}
	public double NewHeight
	{
		get => (double)GetValue(NewHeightProperty);
		set => SetValue(NewHeightProperty, value);
	}
	public double NewFontSize
	{
		get => (double)GetValue(NewFontSizeProperty);
		set => SetValue(NewFontSizeProperty, value);
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
	public bool StyleInvert
	{
		get => (bool)GetValue(StyleInvertProperty);
		set => SetValue(StyleInvertProperty, value);
	}
	public double BorderWidth
	{
		get => (double)GetValue(BorderWidthProperty);
		set => SetValue(BorderWidthProperty, value);
	}
	public double ReleasedColorAlpha
	{
		get => (double)GetValue(ReleasedColorAlphaProperty);
		set => SetValue(ReleasedColorAlphaProperty, value);
	}
	public RoundedButtonView()
	{
		InitializeComponent();
	}

	protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		base.OnPropertyChanged(propertyName);
		if (propertyName == nameof(StyleInvert))
		{
			if (StyleInvert)
			{
				Button.RemoveBinding(BackgroundColorProperty);
				Button.BackgroundColor = PressedBackgroundColor;

				Button.BorderWidth = BorderWidth;
				Button.SetBinding(BorderWidthProperty, nameof(BorderWidth));

				Button.TextColor = Color;

				Button.FontFamily = "LatoLight";
			}
			else
			{
				Button.BackgroundColor = Color;
				Button.SetBinding(BackgroundColorProperty, nameof(Color));

				Button.RemoveBinding(BorderWidthProperty);
				Button.BorderWidth = 0;

				Button.TextColor = ReleasedTextColor;

				Button.FontFamily = "Lato";
			}
		}

	}

	private void RoundedButton_Pressed(object sender, EventArgs e)
	{
		if (!StyleInvert)
		{
			((Button)sender).BackgroundColor = PressedBackgroundColor;
			((Button)sender).TextColor = Color;
			((Button)sender).BorderWidth = BorderWidth;
		}
	}

	private void RoundedButton_Released(object sender, EventArgs e)
	{
		if (!StyleInvert)
		{
			((Button)sender).BackgroundColor = Color.WithAlpha((float)ReleasedColorAlpha);
			((Button)sender).TextColor = ReleasedTextColor;
			((Button)sender).BorderWidth = 0;
		}
	}

	private void RoundedButton_Clicked(object sender, EventArgs e)
	{
		if (Command is { })
		{
			Command.Execute(CommandParameter);
		}
		Clicked?.Invoke(sender, e);
	}
}