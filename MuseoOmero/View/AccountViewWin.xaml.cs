namespace MuseoOmero.ViewWin;

public partial class AccountViewWin : ContentPage
{
	private AccountViewModelWin _viewModel;
	public AccountViewWin(AccountViewModelWin viewModel)
	{
		_viewModel = viewModel;
		_viewModel.Initialize();
		BindingContext = _viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_viewModel.Initialize();
	}

	private void HighlightView_Pressed(object sender, EventArgs e)
	{
		PhotoFrame.ColorTo(DeviceManager.Instance.Colors[4], DeviceManager.Instance.Colors[0], c => PhotoFrame.BackgroundColor = c, 350, Easing.CubicOut);
		PhotoIcon.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => PhotoIcon.TextColor = c, 350, Easing.CubicOut);
		PhotoIcon.FadeTo(1, 350, Easing.CubicOut);
		PhotoImage.FadeTo(0, 350, Easing.CubicOut);
	}

	private void HighlightView_Released(object sender, EventArgs e)
	{
		PhotoFrame.CancelAnimation();
		PhotoIcon.CancelAnimation();
		PhotoFrame.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[4], c => PhotoFrame.BackgroundColor = c, 350, Easing.CubicOut);
		PhotoIcon.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => PhotoIcon.TextColor = c, 350, Easing.CubicOut);
		PhotoIcon.FadeTo(0, 350, Easing.CubicOut);
		PhotoImage.FadeTo(1, 350, Easing.CubicOut);
	}

	private async void HighlightView_Clicked(object sender, EventArgs e)
	{
		_viewModel.IsBusy = true;
		var fileResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Scegli una nuova foto di profilo" });
		if (fileResult is { })
		{
			var stream = UtiliesManager.Instance.CropImageToSquare(fileResult.FullPath);
			await StorageManager.Instance.Upload(
				resource: $"{AccountManager.Instance.Uid}/foto_profilo/",
				stream: stream
			);
			TopMenu.UrlSet = false;
			foreach (var m in TopMenu.TopMenus)
				m.Initialize();
			_viewModel.Initialize();
		}
		_viewModel.IsBusy = false;
	}

	private async void RoundedButtonView_Clicked(object sender, EventArgs e)
	{
		await AccountManager.Instance.ResetPassword();
	}

	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		_viewModel.IsBusy = true;
		await DatabaseManager.Instance.Put(
			resource: $"dipendenti/{AccountManager.Instance.Uid}", _viewModel.Dipendente);
		_viewModel.IsBusy = false;
	}

	private void PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
	{

	}
}