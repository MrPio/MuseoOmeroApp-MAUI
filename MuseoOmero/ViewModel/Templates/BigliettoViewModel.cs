using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MuseoOmero.Model;
using MuseoOmero.View.Templates;

namespace MuseoOmero.ViewModel.Templates
{
    public partial class BigliettoViewModel : ObservableObject
    {
        public BigliettoView View;

        [ObservableProperty]
        DateTime data;

        [ObservableProperty]
        string tipologiaBiglietto, turnoGuida, icon;

        public BigliettoViewModel(Biglietto biglietto)
        {
            Data = biglietto.DataValidita;
            TipologiaBiglietto = nameof(biglietto.Tipologia);
            TurnoGuida = biglietto.DataGuida == null ? "No turno guida." : "Guida alle " +
            biglietto.DataGuida?.ToString("HH:mm");
            Icon = IconeBiglietto.Values[(int)biglietto.Tipologia];
        }


        [RelayCommand]
        void VaiAlBigliettoClicked()
        {
            View.Button_Clicked();
        }

    }
}
