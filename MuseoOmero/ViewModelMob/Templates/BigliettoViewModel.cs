namespace MuseoOmero.ViewModelMob.Templates
{
    public partial class BigliettoViewModel : ObservableObject
    {
        public BigliettoView View;

        [ObservableProperty]
        DateTime data;

        [ObservableProperty]
        string tipologiaBiglietto, turnoGuida, icon;

        [ObservableProperty]
        Biglietto biglietto;

        public BigliettoViewModel(Biglietto biglietto)
        {
            Biglietto=biglietto;
            Data = biglietto.DataValidita;
            TipologiaBiglietto = Enum.GetName(biglietto.Tipologia);
            TurnoGuida = biglietto.OrarioGuida == null ? "No turno guida." : "Guida alle " +
            biglietto.OrarioGuida?.ToString(@"hh\:mm");
            Icon = IconeBiglietto.Values[(int)biglietto.Tipologia];

        }


        [RelayCommand]
        void VaiAlBigliettoClicked()
        {
            View.Button_Clicked();
        }

    }
}
