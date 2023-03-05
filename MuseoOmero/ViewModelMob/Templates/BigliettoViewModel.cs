namespace MuseoOmero.ViewModelMob.Templates
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
            TurnoGuida = biglietto.OrarioGuida == null ? "No turno guida." : "Guida alle " +
            biglietto.OrarioGuida?.ToString("HH:mm");
            Icon = IconeBiglietto.Values[(int)biglietto.Tipologia];
        }


        [RelayCommand]
        void VaiAlBigliettoClicked()
        {
            View.Button_Clicked();
        }

    }
}
