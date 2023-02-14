using CommunityToolkit.Mvvm.ComponentModel;
using MuseoOmero.Managers;
using MuseoOmero.Resources.Material;
using MuseoOmero.ViewModel.TemplatesWin;

namespace MuseoOmero.ViewModelWin;

public partial class HomeViewModelWin : ObservableObject
{
    [ObservableProperty]
    PanoramicaElementViewModelWin iMieiTitoliPanoramica = new(
        icon: IconFont.Ticket,
        title: "I miei titoli",
		dark: true
	);

    [ObservableProperty]
    PanoramicaElementViewModelWin questionariPanoramica = new(
        icon: IconFont.Account,
        title: "Questionari",
		dark: false
	);

    [ObservableProperty]
    PanoramicaElementViewModelWin chatPanoramica = new(
        icon: IconFont.Chat,
        title: "Chat",
        dark: false
    );
}