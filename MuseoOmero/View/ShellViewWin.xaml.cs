using MuseoOmero.ViewModelWin;

namespace MuseoOmero.ViewWin;

public partial class ShellViewWin : Shell
{
	public ShellViewWin()
	{
		BindingContext = new ShellViewModelWin();
		InitializeComponent();
	}
}