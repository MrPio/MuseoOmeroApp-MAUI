using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.Easing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Linq;

namespace MuseoOmero.ViewModelWin;

public partial class StatisticheViewModelWin : ObservableObject
{
	private HomeViewModelWin _homeViewModelWin;
	[ObservableProperty]
	IEnumerable<ISeries> series
		= new GaugeBuilder()
		.WithLabelsSize(20)
		.WithLabelsPosition(PolarLabelsPosition.Start)
		.WithLabelFormatter(point => $"{point.PrimaryValue} {point.Context.Series.Name}")
		.WithInnerRadius(20)
		.WithOffsetRadius(8)
		.WithBackgroundInnerRadius(20)

		.AddValue(30, "Vanessa")
		.AddValue(50, "Charles")
		.AddValue(70, "Ana")

		.BuildSeries();


	public ISeries[] Series2 { get; set; } =
{
		new PieSeries<double> { Values = new List<double> { 3 }, Pushout = 4 },
		new PieSeries<double> { Values = new List<double> { 3 }, Pushout = 4 },
		new PieSeries<double> { Values = new List<double> { 3 }, Pushout = 4 },
		new PieSeries<double> { Values = new List<double> { 2 }, Pushout = 4 },
		new PieSeries<double> { Values = new List<double> { 5 }, Pushout = 30 }
	};

	public ISeries[] Series3 { get; set; } = new ISeries[]
  {
		new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50,MaxOuterRadius = 0.8 },
		new PieSeries<double> { Values = new List<double> { 4 }, InnerRadius = 50,MaxOuterRadius = 0.85 },
		new PieSeries<double> { Values = new List<double> { 1 }, InnerRadius = 50,MaxOuterRadius = 0.9 },
		new PieSeries<double> { Values = new List<double> { 4 }, InnerRadius = 50,MaxOuterRadius = 0.95 },
		new PieSeries<double> { Values = new List<double> { 3 }, InnerRadius = 50,MaxOuterRadius = 1 }
  };

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

	public StatisticheViewModelWin(HomeViewModelWin homeViewModelWin)
	{
		_homeViewModelWin = homeViewModelWin;
		foreach (var s in BigliettiSeries)
			s.PointMeasured += OnPointMeasured;
	}

	public async void Initialize()
	{
		//BIGLIETTI
		var vendite = new float[7];
		var convalide = new float[7];
		var utenti = _homeViewModelWin.Utenti;
		foreach (var b in utenti.SelectMany(u => u.Biglietti))
		{
			++vendite[(int)b.DataAcquisto.DayOfWeek];
			if (b.DataConvalida is { })
				++convalide[(int)b.DataConvalida?.DayOfWeek];
		}
		BigliettiSeries = new ColumnSeries<float>[]
		{
			new()
			{
				Name = "Venduti",
				Values = vendite
			},
			new()
			{
				Name = "Venduti",
				Values = convalide
			}
		};
	}
	private void OnPointMeasured(ChartPoint<float, RoundedRectangleGeometry, LabelGeometry> point)
	{
		var visual = point.Visual;
		if (visual is null) return;

		var delayedFunction = new DelayedFunction(EasingFunctions.BuildCustomElasticOut(1.5f, 0.60f), point, 30f);

		_ = visual
			.TransitionateProperties(
				nameof(visual.Y),
				nameof(visual.Height))
			.WithAnimation(animation =>
				animation
					.WithDuration(delayedFunction.Speed)
					.WithEasingFunction(delayedFunction.Function));
	}
}
