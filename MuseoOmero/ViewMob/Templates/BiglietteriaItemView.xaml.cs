using static System.Net.Mime.MediaTypeNames;

namespace MuseoOmero.ViewMob.Templates;

public partial class BiglietteriaItemView : ContentView
{
	private static DeviceManager _devM => DeviceManager.Instance;

	private bool _isPreferred;
	private int _price;
	public bool IsExpanded;

	public static readonly BindableProperty TextProperty = BindableProperty.Create(
		nameof(Title),
		typeof(string),
		typeof(BiglietteriaItemView),
		string.Empty
	);
	public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
		nameof(Subtitle),
		typeof(string),
		typeof(BiglietteriaItemView),
		string.Empty
	);
	public static readonly BindableProperty ItemIconProperty = BindableProperty.Create(
		nameof(ItemIcon),
		typeof(string),
		typeof(BiglietteriaItemView),
		string.Empty
	);
	public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
		nameof(BackgroundColor),
		typeof(Color),
		typeof(BiglietteriaItemView),
		_devM.Colors[0]
	);
	public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(
		nameof(ForegroundColor),
		typeof(Color),
		typeof(BiglietteriaItemView),
		_devM.Colors[4]
	);
	public static readonly BindableProperty BackgroundFocusedColorProperty = BindableProperty.Create(
		nameof(BackgroundFocusedColor),
		typeof(Color),
		typeof(BiglietteriaItemView),
		_devM.Colors[9]
	);

	public string Title
	{
		get => (string)GetValue(TextProperty);
		set
		{
			SetValue(TextProperty, value);

			//CONTROLLO IN MEMORIA IL VALORE DI PREFERRED
			if (value != string.Empty)
			{
				Task.Run(async () =>
				{
					if ((await SecureStorage.GetAsync($"BiglietteriaItemView/{value}/IsPreferred")) is { } isPreferred && isPreferred.Equals(true.ToString()))
						Stella_Clicked(null, null);
				});
			}
		}
	}
	public string Subtitle
	{
		get => (string)GetValue(SubtitleProperty);
		set => SetValue(SubtitleProperty, value);
	}
	public string ItemIcon
	{
		get => (string)GetValue(ItemIconProperty);
		set => SetValue(ItemIconProperty, value);
	}
	public new Color BackgroundColor
	{
		get => (Color)GetValue(BackgroundColorProperty);
		set => SetValue(BackgroundColorProperty, value);
	}
	public Color ForegroundColor
	{
		get => (Color)GetValue(ForegroundColorProperty);
		set => SetValue(ForegroundColorProperty, value);
	}

	public Color BackgroundFocusedColor
	{
		get => (Color)GetValue(BackgroundFocusedColorProperty);
		set => SetValue(BackgroundFocusedColorProperty, value);
	}
	public int Price
	{
		get => _price;
		set
		{
			_price = value;
			Euro1.IsVisible = value > 1;
			Euro2.IsVisible = value > 2;
			Euro0.Text = value == 0 ? IconFont.CurrencyEurOff : IconFont.CurrencyEur;
		}
	}

	public event EventHandler OnItemExpanding, OnOptionClicked;

	public BiglietteriaItemView()
	{
		InitializeComponent();

	}

	private void Stella_Clicked(object sender, EventArgs e)
	{
		_isPreferred.Swap();
		StarIcon.Text = _isPreferred ? IconFont.Star : IconFont.StarOutline;
		SecureStorage.SetAsync($"BiglietteriaItemView/{Title}/IsPreferred", _isPreferred.ToString());
	}
	public void Item_Clicked(object sender, EventArgs e)
	{
		// FIX necessario per refresh-are la scrollview.
		// NON PIù! RISOLTO METTENDO LA SCROLLVIEW FUORI DALLA GRID

		//if (!_scrollSet)
		//{
		//	//var scroll = (ScrollView)Parent.Parent;
		//	//var content = scroll.Content;
		//	//scroll.Content = null;
		//	//scroll.Content = content;
		//	//_scrollSet = true;
		//}

		if (!IsExpanded && OnItemExpanding is { })
			OnItemExpanding.Invoke(sender, e);

		var newBackgroundColor = BackgroundFocusedColor;
		var newIconBackgroundColor = App.Current.PlatformAppTheme == AppTheme.Light ? _devM.Colors[3] : Colors.Black;
		newIconBackgroundColor = newIconBackgroundColor.WithAlpha(0);

		var anim = new[]{
			new[]{120, 208 },
			new[]{ 40, 36 },
			new[]{ 0, 6 },
			new[]{ 0, 1 },
		};
		if (IsExpanded)
		{
			anim[0] = anim[0].Reverse().ToArray();
			anim[1] = anim[1].Reverse().ToArray();
			anim[2] = anim[2].Reverse().ToArray();
			anim[3] = anim[3].Reverse().ToArray();
			newBackgroundColor = BackgroundColor;
			newIconBackgroundColor = newIconBackgroundColor.WithAlpha(1);
		}

		var animation = new Animation
		{
			{0,1,new(v => Frame.HeightRequest = v, anim[0][0], anim[0][1], Easing.CubicOut) },
			{0.5, 1, new(v => Frame.CornerRadius = (float)v, anim[1][0], anim[1][1], Easing.CubicOut)},
			{0, 1, new(v => Frame.Padding = new(v,0,v,0), anim[2][0], anim[2][1], Easing.CubicOut)},
			{0.2, 0.75, new(v => GridInside.Opacity = v, anim[3][0], anim[3][1], Easing.CubicOut)}
		};
		animation.Commit(this, "TransitionAnimation", 16, 500, Easing.CubicOut);
		Task.WhenAll(new[]{
			Frame.ColorTo(Frame.BackgroundColor, newBackgroundColor, c => Frame.BackgroundColor = c, 850, Easing.CubicOut),
			IconFrame.ColorTo(IconFrame.BackgroundColor, newIconBackgroundColor, c => IconFrame.BackgroundColor = c, 850, Easing.CubicOut)
		});

		IsExpanded = !IsExpanded;
	}
	private void SmallItem_Clicked(object sender, EventArgs e)
	{
		var button = (Button)sender;
		if (IsExpanded && OnOptionClicked is { })
			OnOptionClicked.Invoke($"{Title}:{((HighlightView)button.Parent).Tag}", e);
	}
}