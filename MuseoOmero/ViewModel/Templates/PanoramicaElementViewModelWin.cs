using CommunityToolkit.Mvvm.ComponentModel;
using MuseoOmero.Managers;

namespace MuseoOmero.ViewModel.TemplatesWin;

public partial class PanoramicaElementViewModelWin : ObservableObject
{
    [ObservableProperty]
    Color backgroundColor = DeviceManager.Instance.Colors[0],
        foregroundColor = DeviceManager.Instance.Colors[4],
        frameColor =Color.FromArgb("#22ffffff");

    [ObservableProperty] 
    string icon, trendingIcon, title, subtitle, content, underContent;

    public string Route;


	public PanoramicaElementViewModelWin(string icon,
        string title, bool dark, string route)
	{
		if (!dark)
		{
			backgroundColor = DeviceManager.Instance.Colors[4];
			foregroundColor = DeviceManager.Instance.Colors[0];
			frameColor = Color.FromArgb("#12000000");
		}
		Icon = icon;
		Title = title;
		Route = route;
	}
}