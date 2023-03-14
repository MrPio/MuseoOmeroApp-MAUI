using Mopups.Interfaces;

namespace MuseoOmero.ViewMob;

public partial class StatisticheView : ContentView
{
	StatisticheViewModel viewModel => Service.Get<StatisticheViewModel>();
	private IPopupNavigation _popupNavigation => Service.Get<IPopupNavigation>();

	public StatisticheView()
	{
		InitializeComponent();
		viewModel.Initialize();
	}

	private void VisitaConQuestionario_Clicked(object sender, EventArgs e)
	{
		var visita = ((VisualElement)sender).BindingContext as Visita;
		App.Current.MainPage.DisplayAlert("Questionario già compilato", "Spiacente, " +
			$"hai già compilato il questionario per questa visita in data {visita.Questionario.DataCompilazione:dd MMMM yyyy}.", "Ok");
	}

	private void VisitaSenzaQuestionario_Clicked(object sender, EventArgs e)
	{
		var visita = ((VisualElement)sender).BindingContext as Visita;
		var nuovoQuestionario = new Questionario(
			tipologiaVisita: TipologiaVisita.Values[1],
			accompagnatoriVisita: AccompagnatoriVisita.Values[1],
			motivazioneVisita: MotivazioneVisita.Values[1],
			titoloStudi: TitoloStudi.Values[1],
			numeroVisite: viewModel.Visite.Count,
			ritorno: Ritorno.Values[1],
			valutazioneVisita: 5,
			valutazioneEsperienza: 5,
			valutazioneStruttura: 5,
			dataCompilazione: DateTime.Now,
			dataVisita:visita.Data
		);
		var popup = new QuestionarioView() { BindingContext = nuovoQuestionario, Visita=visita };
		_popupNavigation.PushAsync(popup);
	}
}