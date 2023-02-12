using MuseoOmero.Managers;
using MuseoOmero.Model.Enums;
using MuseoOmero.Models;
using MuseoOmero.ViewModelWin;
using System.Globalization;
using System.Text.Json;

namespace MuseoOmero.ViewWin;

public partial class MainViewWin : ContentPage
{
	public MainViewWin()
	{
		BindingContext = new MainViewModelWin();
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		var shellViewModelWin = (ShellViewModelWin)Parent.Parent.Parent.Parent.BindingContext;
		shellViewModelWin.ChangeIndex(0);
	}
}