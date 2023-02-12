using CommunityToolkit.Mvvm.ComponentModel;
using MuseoOmero.Managers;

namespace MuseoOmero.ViewModel.TemplatesWin;

public partial class PanoramicaElementViewModelWin : ObservableObject
{
    [ObservableProperty]
    Color backgroundColor = DeviceManager.Instance.Colors[0],
        foregroundColor = DeviceManager.Instance.Colors[4],
        frameColor =Color.FromArgb("#40ffffff");

    [ObservableProperty] 
    string icon, trendingIcon, title, subtitle, content, underContent;

    public PanoramicaElementViewModelWin(string icon,
        string title, bool dark)
    {
        if (!dark)
        {
            backgroundColor = DeviceManager.Instance.Colors[4];
			foregroundColor = DeviceManager.Instance.Colors[6];
            frameColor = Color.FromArgb("#12000000");
		}
        Icon = icon;
        Title = title;
    }
}