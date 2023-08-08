using MuseoOmero.ViewModelWin;

namespace MuseoOmero.ViewWin;

public partial class ShellViewWin : Shell
{
	private ShellViewModelWin _viewModel;
	private bool _shellExpanded = false;
	private float _shellMaxWidth = 246;
	private float _shellMinWidth = 80;
	public ShellViewWin(ShellViewModelWin viewModel)
	{
		//DeviceManager.Instance.ResizeWin(1330, 850);
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
		InitRoutes();
		var t = Task.Run(async delegate
		{
			await Task.Delay(1400);
			EspandiRiduciFlyoutLabel_Tapped(null, null);
		});
	}
	private void InitRoutes()
	{
		Routing.RegisterRoute(nameof(HomeViewWin), typeof(HomeViewWin));
	}

	async void OnMenuItemChanged(object sender, CheckedChangedEventArgs e)
	{
		var vm = (ShellViewModelWin)BindingContext;
		if (!String.IsNullOrEmpty(vm.SelectedRoute))
			await Shell.Current.GoToAsync($"//{vm.SelectedRoute}");
	}

	private void EspandiRiduciFlyoutLabel_Tapped(object sender, EventArgs e)
	{
		var animation = new Animation();
		var a = _shellExpanded ? _shellMaxWidth : _shellMinWidth;
		var b = _shellExpanded ? _shellMinWidth : _shellMaxWidth;

		if (this.AnimationIsRunning("Shell"))
			return;

		_shellExpanded = !_shellExpanded;
		new Animation(
				callback: v => Shell.FlyoutWidth = (a * (1 - v) + b * v),
				easing: Easing.CubicOut
			).Commit(this, "Shell", length: 400);
	}
}