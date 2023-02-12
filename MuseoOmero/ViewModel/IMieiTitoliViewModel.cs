using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MuseoOmero.View;
using MuseoOmero.ViewModel.Templates;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MuseoOmero.ViewModel
{
    public partial class IMieiTitoliViewModel : ObservableObject
    {
        public IMieiTitoliView view;
        [ObservableProperty]
        ObservableCollection<BigliettoViewModel> biglietti=new();

        [RelayCommand]
        async void AggiornaClicked()
        {
            var mainPage = (MainView)view.Parent.Parent.Parent.Parent.Parent; //TODO
            var mainPageViewModel = (MainViewModel)mainPage.BindingContext;
            mainPageViewModel.Loading = true;
            //Biglietti = new ObservableCollection<BigliettoViewModel>(await mainPage.DatabaseManager.LoadBiglietti());
            mainPageViewModel.Loading = false;
        }
    }
}
