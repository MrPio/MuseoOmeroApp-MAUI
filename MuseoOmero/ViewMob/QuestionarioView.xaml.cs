using Mopups.Interfaces;

namespace MuseoOmero.ViewMob;

public partial class QuestionarioView
{
	IPopupNavigation popupNavigation => Service.Get<IPopupNavigation>();
	StatisticheViewModel statisticheViewModel => Service.Get<StatisticheViewModel>();

	public Visita Visita { get; set; }

	public QuestionarioView()
	{
		InitializeComponent();
	}

	private void ConfermaButton_Clicked(object sender, EventArgs e)
	{
		var questionario = BindingContext as Questionario;
		var utente = AccountManager.Instance.Utente;
		utente.Questionari.Add(questionario);
		DatabaseManager.Instance.Put($"utenti/{utente.Uid}/questionari/{utente.Questionari.Count - 1}", questionario);
		Visita.Questionario = questionario;
		popupNavigation.PopAllAsync();
		statisticheViewModel.Initialize();
		App.Current.MainPage.DisplayAlert("Compilazione inviata", "Hai compilato con successo il questionario. Grazie per il tuo prezioso contributo!", "Ok");

	}
}