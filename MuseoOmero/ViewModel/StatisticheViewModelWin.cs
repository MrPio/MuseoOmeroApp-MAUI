using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.Easing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Maui;

namespace MuseoOmero.ViewModelWin;

public partial class StatisticheViewModelWin : ObservableObject
{
	private HomeViewModelWin _homeViewModelWin;

	[ObservableProperty]
	DateTime dataInizio = DateTime.Today.AddMonths(-3), dataFine = DateTime.Today;

	// BIGLIETTI series
	[ObservableProperty]
	ColumnSeries<float>[] bigliettiSeries;
	[ObservableProperty]
	Axis[] bigliettiXAxes =
	{
		new()
		{
			Labels = new[] { "L", "M", "MM","G","V","S","D" },
			LabelsRotation = 30,
			SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
			SeparatorsAtCenter = false,
			TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
			TicksAtCenter = true
		}
	};

	// QUESTIONARI series
	[ObservableProperty]
	IEnumerable<ISeries> questionariValutazioniSeries;
	[ObservableProperty]
	ISeries[] questionariTipologieVisiteSeries, questionariAccompagnatoriVisitaSeries, questionariMotivazioneVisitaSeries, questionariTitoloStudiSeries, questionariRitornoSeries;
	[ObservableProperty]
	ColumnSeries<float>[] questionariSeries;
	[ObservableProperty]
	Axis[] questionariXAxes =
	{
		new()
		{
			Labels = new[] { "L", "M", "MM","G","V","S","D" },
			LabelsRotation = 30,
			SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
			SeparatorsAtCenter = false,
			TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
			TicksAtCenter = true,
		}
	};

	// OPERE series
	[ObservableProperty]
	public ISeries[] opereSeries;
	[ObservableProperty]
	public PolarAxis[] opereAxis;
	public StatisticheViewModelWin(HomeViewModelWin homeViewModelWin)
	{
		_homeViewModelWin = homeViewModelWin;
	}

	public void Initialize()
	{
		var utenti = _homeViewModelWin.Utenti;
		var bigliettiTot = utenti.SelectMany(u => u.Biglietti);
		var biglietti = bigliettiTot.Where(b => b.DataAcquisto.IsBetween(DataInizio, DataFine));
		var questionariTot = utenti.SelectMany(u => u.Questionari);
		var questionari = questionariTot.Where(q => q.DataCompilazione.IsBetween(DataInizio, DataFine));
		var opere = _homeViewModelWin.Opere;

		//BIGLIETTI - vendite/convalide
		{
			var vendite = new float[7];
			var convalide = new float[7];
			foreach (var b in biglietti)
			{
				++vendite[(int)b.DataAcquisto.DayOfWeek];
				if (b.DataConvalida is { })
					++convalide[(int)b.DataConvalida?.DayOfWeek];
			}

			//Per triggherare il refresh
			BigliettiSeries = Array.Empty<ColumnSeries<float>>();
			BigliettiSeries = new ColumnSeries<float>[]
			{
			new()
			{
				Name = "Venduti",
				Values = vendite
			},
			new()
			{
				Name = "Convalidati",
				Values = convalide
			}
			};
			foreach (var s in BigliettiSeries)
			{
				s.PointMeasured += OnPointMeasuredBar; //Ritardo nella comparsa
				s.ChartPointPointerHover += OnPointerHoverBar;
				//s.ChartPointPointerDown += OnPointerDown;
				s.ChartPointPointerHoverLost += OnPointerHoverLostBar;
			}
		}

		//QUESTIONARI - valutazioni
		{
			var sommaVisita = 0;
			var sommaEsperienza = 0;
			var sommaStruttura = 0;
			foreach (var q in questionari)
			{
				sommaVisita += q.ValutazioneVisita;
				sommaEsperienza += q.ValutazioneEsperienza;
				sommaStruttura += q.ValutazioneStruttura;
			}
			QuestionariValutazioniSeries = new GaugeBuilder()
			.WithLabelsSize(20)
			.WithLabelsPosition(PolarLabelsPosition.Start)
			.WithLabelFormatter(point => string.Format("{0:0.##}", point.PrimaryValue))
			.WithInnerRadius(20)
			.WithOffsetRadius(8)
			.WithCornerRadius(8)
			.WithBackground(null)
			.WithBackgroundInnerRadius(8)
			.AddValue(Math.Round((double)sommaVisita / questionari.Count(), 2), "Visita")
			.AddValue(Math.Round((double)sommaEsperienza / questionari.Count(), 2), "Esperienza")
			.AddValue(Math.Round((double)sommaStruttura / questionari.Count(), 2), "Struttura")
			.BuildSeries();
		}

		//QUESTIONARI - tipologia_visita
		{
			var values = TipologiaVisita.Values;
			var occorrenze = new int[values.Length];
			foreach (var q in questionari)
			{
				if (values.Contains(q.TipologiaVisita))
					++occorrenze[values.IndexOf(q.TipologiaVisita)];
			}
			QuestionariTipologieVisiteSeries = new PieSeries<double>[]{
				new() { Values= new List<double>() {occorrenze[0] },Name=values[0]},
				new() { Values= new List<double>() {occorrenze[1] },Name=values[1]},
				new() { Values= new List<double>() {occorrenze[2] },Name=values[2]},
			};
			foreach (PieSeries<double> series in QuestionariTipologieVisiteSeries)
			{
				series.Pushout = 4;
				series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
				series.DataLabelsPosition= LiveChartsCore.Measure.PolarLabelsPosition.Middle;
				series.DataLabelsFormatter = p => p.Context.Series.Name;
			}

		}

		//QUESTIONARI - accompagnatori_visita
		{
			var values = AccompagnatoriVisita.Values;
			var occorrenze = new int[values.Length];
			foreach (var q in questionari)
			{
				if (values.Contains(q.AccompagnatoriVisita))
					++occorrenze[values.IndexOf(q.AccompagnatoriVisita)];
			}
			QuestionariAccompagnatoriVisitaSeries = new PieSeries<double>[values.Length];
			for (var i = 0; i < values.Length; ++i)
			{
				PieSeries<double> series = new() { Values = new List<double>() { occorrenze[i] }, Name = values[i] };
				series.PointMeasured += OnPointMeasuredPie;
				series.Pushout = 4;
				series.InnerRadius = 60;
				series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
				series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
				series.DataLabelsFormatter = p => p.Context.Series.Name;
				QuestionariAccompagnatoriVisitaSeries[i] = series;
			}
		}

		//QUESTIONARI - motivazione_visita
		{
			var values = MotivazioneVisita.Values;
			var occorrenze = new int[values.Length];
			foreach (var q in questionari)
			{
				if (values.Contains(q.MotivazioneVisita))
					++occorrenze[values.IndexOf(q.MotivazioneVisita)];
			}
			QuestionariMotivazioneVisitaSeries = new PieSeries<double>[values.Length];
			for (var i = 0; i < values.Length; ++i)
			{
				PieSeries<double> series = new() { Values = new List<double>() { occorrenze[i] }, Name = values[i] };
				series.PointMeasured += OnPointMeasuredPie;
				series.Pushout = 4;
				series.MaxOuterRadius = ((double)i / values.Length) * 0.2 + 0.8;
				series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
				series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
				series.DataLabelsFormatter = p => p.Context.Series.Name.Substring(0,Math.Min(7, p.Context.Series.Name.Length))+"..";
				QuestionariMotivazioneVisitaSeries[i] = series;
			}
		}

		//QUESTIONARI - titolo_studi
		{
			var values = TitoloStudi.Values;
			var occorrenze = new int[values.Length];
			foreach (var q in questionari)
			{
				if (values.Contains(q.TitoloStudi))
					++occorrenze[values.IndexOf(q.TitoloStudi)];
			}
			QuestionariTitoloStudiSeries = new PieSeries<double>[values.Length];
			for (var i = 0; i < values.Length; ++i)
			{
				PieSeries<double> series = new() { Values = new List<double>() { occorrenze[i] }, Name = values[i] };
				series.PointMeasured += OnPointMeasuredPie;
				series.Pushout = 4;
				series.InnerRadius = 60;
				series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
				series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
				series.DataLabelsFormatter = p => p.Context.Series.Name;
				series.MaxOuterRadius = ((double)i / values.Length) * 0.2 + 0.8;

				QuestionariTitoloStudiSeries[i] = series;
			}
		}

		//QUESTIONARI - ritorno
		{
			var values = Ritorno.Values;
			var occorrenze = new int[values.Length];
			foreach (var q in questionari)
			{
				if (values.Contains(q.Ritorno))
					++occorrenze[values.IndexOf(q.Ritorno)];
			}
			QuestionariRitornoSeries = new PieSeries<double>[values.Length];
			for (var i = 0; i < values.Length; ++i)
			{
				PieSeries<double> series = new() { Values = new List<double>() { occorrenze[i] }, Name = values[i] };
				series.PointMeasured += OnPointMeasuredPie;
				series.Pushout = 4;
				series.InnerRadius = 30;
				series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
				series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
				series.DataLabelsFormatter = p => p.Context.Series.Name;
				QuestionariRitornoSeries[i] = series;
			}
		}

		//QUESTIONARI - visite/compilazioni
		{
			var numeroMesi = 12;
			var visite = new float[numeroMesi];
			var compilazioni = new float[numeroMesi];
			var xAxisLabels = new string[numeroMesi];
			for (var i = 0; i > -numeroMesi; --i)
			{
				var startDate = DateTime.Now.AddMonths(i - 1);
				var endDate = DateTime.Now.AddMonths(i);
				xAxisLabels[numeroMesi + i - 1] = startDate.ToString("dd MMM yy");
				// Andrebbe usato per correttezza b.DataConvalida, e non b.DataValidita, ma al momento
				// ho solo 1 biglietto convalidato e il risultato non sarebbe apprezzabile
				visite[numeroMesi + i - 1] = bigliettiTot.Where(b => b.DataValidita.IsBetween(startDate, endDate)).Count();
				compilazioni[numeroMesi + i - 1] = questionariTot.Where(q => q.DataCompilazione.IsBetween(startDate, endDate)).Count();
			}
			QuestionariSeries = Array.Empty<ColumnSeries<float>>(); //Per triggherare il refresh
			QuestionariSeries = new ColumnSeries<float>[]
			{
				new()
				{
					Name = "Visite",
					Values = visite,
					MaxBarWidth = double.MaxValue,
				IgnoresBarPosition = true,
					IsVisibleAtLegend = true,
				},
				new()
				{
					Name = "Compilazioni",
					Values = compilazioni,
					MaxBarWidth = 40,
				IgnoresBarPosition = true
				}
			};
			QuestionariXAxes[0].Labels = xAxisLabels;
			foreach (var s in BigliettiSeries)
			{
				s.PointMeasured += OnPointMeasuredBar; //Ritardo nella comparsa
				s.ChartPointPointerHover += OnPointerHoverBar;
				//s.ChartPointPointerDown += OnPointerDown;
				s.ChartPointPointerHoverLost += OnPointerHoverLostBar;
			}
		}

		//OPERE - visualizzazioni per sala
		{
			var sale = _homeViewModelWin.Sale;
			var visualsTot = new int[sale.Count()];
			foreach (var o in opere)
				visualsTot[sale.IndexOf(o.Sala)] += o.Visualizzazioni;

			OpereSeries = new PolarLineSeries<int>[]
			{
				new PolarLineSeries<int>
				{
					Values = visualsTot,
					LineSmoothness = 0,
					GeometrySize= 0,
					Stroke= new SolidColorPaint(SKColors.Blue),
					Fill = new SolidColorPaint(SKColors.Blue.WithAlpha(80))
				}
			};
			OpereAxis = new[] {
				new PolarAxis
				{
					LabelsRotation = LiveCharts.TangentAngle,
					Labels = sale
				}
			};
		}
	}
	private void OnPointMeasuredBar(ChartPoint<float, RoundedRectangleGeometry, LabelGeometry> point)
	{
		var visual = point.Visual;
		if (visual is null) return;

		var delayedFunction = new DelayedFunction(EasingFunctions.BuildCustomElasticOut(1.85f, 0.80f), point, 25f);

		_ = visual
			.TransitionateProperties(
				nameof(visual.Y),
				nameof(visual.Height))
			.WithAnimation(animation =>
				animation
					.WithDuration(delayedFunction.Speed)
					.WithEasingFunction(delayedFunction.Function));
	}


	private void OnPointerDownBar(IChartView chart, ChartPoint<float, RoundedRectangleGeometry, LabelGeometry>? point)
	{
		if (point?.Visual is null) return;
		point.Visual.Fill = new SolidColorPaint(SKColors.Orange);
		chart.Invalidate(); // <- ensures the canvas is redrawn after we set the fill
	}

	private void OnPointerHoverBar(IChartView chart, ChartPoint<float, RoundedRectangleGeometry, LabelGeometry>? point)
	{
		if (point?.Visual is null) return;
		point.Visual.Fill = new SolidColorPaint(SKColors.Yellow);
		chart.Invalidate();
	}
	private void OnPointerHoverLostBar(IChartView chart, ChartPoint<float, RoundedRectangleGeometry, LabelGeometry>? point)
	{
		if (point?.Visual is null) return;
		point.Visual.Fill = null;
		chart.Invalidate();
	}

	//PIE
	private void OnPointMeasuredPie(ChartPoint<double, DoughnutGeometry, LabelGeometry> point)
	{
		var visual = point.Visual;
		if (visual is null) return;

		var delayedFunction = new DelayedFunction(EasingFunctions.BuildCustomElasticOut(4.85f, 2.80f), point, 1225f);

		_ = visual
			.TransitionateProperties(
				nameof(visual.Y),
				nameof(visual.Height))
			.WithAnimation(animation =>
				animation
					.WithDuration(delayedFunction.Speed)
					.WithEasingFunction(delayedFunction.Function));
	}

	private void OnPointerHoverPie(IChartView chart, ChartPoint<double, DoughnutGeometry, LabelGeometry>? point)
	{
		if (point?.Visual is null) return;
		point.Visual.PushOut = 30;
		chart.Invalidate();
	}

	private void OnPointerDownPie(IChartView chart, ChartPoint<double, DoughnutGeometry, LabelGeometry>? point)
	{
		if (point?.Visual is null) return;
		((PieChart)chart).InitialRotation += 40;
		//((PieChart)chart).ShowTooltip();
		//chart.Invalidate(); // <- ensures the canvas is redrawn after we set the fill
	}

	private void OnPointerHoverLostPie(IChartView chart, ChartPoint<double, DoughnutGeometry, LabelGeometry>? point)
	{
		if (point?.Visual is null) return;
		point.Visual.PushOut = 4;
		chart.Invalidate();
	}

}
