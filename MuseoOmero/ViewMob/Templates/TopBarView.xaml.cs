using Mopups.Interfaces;
using System.Windows.Input;

namespace MuseoOmero.ViewMob.Templates;

public partial class TopBarView : ContentView
{
	private DeviceManager _devM => DeviceManager.Instance;

	public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(TopBarView), DateTime.Today.AddDays(2));
	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(TopBarView), string.Empty);
	public static readonly BindableProperty FilterCommandProperty = BindableProperty.Create(nameof(FilterCommand), typeof(ICommand), typeof(TopBarView), null);

	private bool _isFilterOn = false;
	public bool IsFilterOn
	{
		get => _isFilterOn;
		set { _isFilterOn = value; OnPropertyChanged(); }
	}

	public DateTime Date
	{
		get => (DateTime)GetValue(DateProperty);
		set => SetValue(DateProperty, value);
	}
	public ICommand FilterCommand
	{
		get => (ICommand)GetValue(FilterCommandProperty);
		set => SetValue(FilterCommandProperty, value);

	}
	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public TopBarView()
	{
		InitializeComponent();
	}
	private async void Logout_Tapped(object sender, EventArgs e)
	{
		if (await Application.Current.MainPage.DisplayAlert("Vuoi davvero uscire?", "Dovrai eseguire  di nuovo il login per rientrare.", "Yes", "No"))
		{
			AccountManager.Instance.DeleteCache();

			var mainViewModel = Service.Get<MainViewModel>();
			var popup = Service.Get<IPopupNavigation>();
			Application.Current.MainPage = new SignInUpView(mainViewModel, popup);
		}
	}

	private void DeleteFilter_Pressed(object sender, EventArgs e)
	{
		IsFilterOn = !IsFilterOn;
		if (IsFilterOn)
		{
			CloseIcon.Text = IconFont.Filter;
			CloseIcon.TextColor = _devM.Colors[4];
			RoundedEntryViewMob.MyBackgroundColor = _devM.Colors[4];
			RoundedEntryViewMob.Entry_Unfocused(null, null);
			RoundedEntryViewMob.DatePicker_Unfocused(null, null);
		}
		else
		{
			CloseIcon.Text = IconFont.FilterOff;
			CloseIcon.TextColor = _devM.Colors[5];
			RoundedEntryViewMob.MyBackgroundColor = _devM.Colors[5];
			RoundedEntryViewMob.Entry_Focused(null, null);
			RoundedEntryViewMob.DatePicker_Focused(null, null);
		}

		if (FilterCommand is { })
			FilterCommand.Execute(IsFilterOn);
	}
}