using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using Microsoft.Maui.Controls;
using SkiaSharp;
using System.Linq;

namespace MuseoOmero.ViewModelMob;
public partial class StatisticheViewModel : ObservableObject
{
	MainViewModel mainViewModel => Service.Get<MainViewModel>();
	[ObservableProperty]
	ObservableCollection<Visita> visite = new();

	[ObservableProperty]
	bool noVisite = true, isRefreshing;

	[ObservableProperty]
	public ISeries[] visiteConvalideSeries, tipologiaBigliettiSeries;

	[RelayCommand]
	async void Refresh()
	{
		mainViewModel.IsBusy = true;
		await DatabaseManager.Instance.ReloadUtente();
		Initialize();
		mainViewModel.IsBusy = false;
		IsRefreshing = false;
	}

	public void Initialize()
	{
		var utente = AccountManager.Instance.Utente;
		var biglietti = utente.Biglietti;
		var convalidati = biglietti.Where(b => b.IsConvalidato).ToList();
		var visite = convalidati.Select(b => new Visita(b.DataValidita, b.Tipologia, b.OrarioGuida is { })).ToList();
		var questionari = utente.Questionari;

		//Questionari
		{
			foreach (var (q, v) in from q in questionari
								   from v in visite
								   where q.DataVisita is { } && v.Data.Date == q.DataVisita?.Date
								   select (q, v))
			{
				v.Questionario = q;
			}
			NoVisite = visite.Count == 0;
			Visite = new(visite);
		}

		//Statistiche
		{
			//VISITE - Visite/Questionari
			{
				//Per triggherare il refresh
				VisiteConvalideSeries = Array.Empty<RowSeries<int>>();
				VisiteConvalideSeries = new RowSeries<int>[]
				{
					//Questionari
					new RowSeries<int>
					{
						Values = new List<int> { visite.Where(v => v.Questionario is { }).Count()  },
						Name="Questionari",
						Stroke = new SolidColorPaint
						{
							Color = new(52, 38, 92),
						},
					},
					//Visite
					new RowSeries<int>
					{
						Values = new List<int> { visite.Count},
						Name="Visite",
						Stroke = new SolidColorPaint
						{
							Color = new(252, 214, 61),
						},
					}
				};
				foreach (var s in VisiteConvalideSeries.Cast<RowSeries<int>>())
				{
					s.Stroke = null;
					s.DataLabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255));
					s.DataLabelsSize = 34;
					s.DataLabelsPosition = DataLabelsPosition.Middle;
				}
			}

			//BIGLIETTI - TipoBiglietti
			{
				var values = Enum.GetNames(typeof(TipoBiglietto)).ToList();
				var occorrenze = new int[values.Count];
				foreach (var b in biglietti)
					if (values.Contains(Enum.GetName(b.Tipologia)))
						++occorrenze[values.IndexOf(Enum.GetName(b.Tipologia))];

				TipologiaBigliettiSeries = new PieSeries<double>[values.Count];
				for (var i = 0; i < values.Count; ++i)
				{
					PieSeries<double> series = new()
					{
						Values = new List<double>() { occorrenze[i] },
						Name = values[i],
						Pushout = 10,
						DataLabelsPaint = new SolidColorPaint(new SKColor(33, 33, 33)),
						DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
						DataLabelsFormatter = p => p.Context.Series.Name,
						DataLabelsSize = 36
					};
					TipologiaBigliettiSeries[i] = series;
				}
			}
		}
	}
}
public class Visita
{
	public DateTime Data { get; set; }
	public TipoBiglietto Tipologia { get; set; }
	public bool ConGuida { get; set; }
	public Questionario? Questionario { get; set; }

	public Visita(DateTime data, TipoBiglietto tipologia, bool conGuida, Questionario questionario = null)
	{
		Data = data;
		Tipologia = tipologia;
		ConGuida = conGuida;
		Questionario = questionario;
	}
}
