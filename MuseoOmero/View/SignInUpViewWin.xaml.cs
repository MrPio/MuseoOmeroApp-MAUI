namespace MuseoOmero.ViewWin;

public partial class SignInUpViewWin : ContentPage
{
	private readonly SignInUpViewModelWin _viewModel;
	public SignInUpViewWin(SignInUpViewModelWin viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		//DeviceManager.Instance.ResizeWin(1000, 650);
		InitializeComponent();
	}
}