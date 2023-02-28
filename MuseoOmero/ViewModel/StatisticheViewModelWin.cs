using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.Easing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace MuseoOmero.ViewModelWin;

public partial class StatisticheViewModelWin : ObservableObject
{
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

	public async void Initialize()
	{

	}

	public List<ISeries> Series4 { get; set; }
	public StatisticheViewModelWin()
	{
		var values1 = new List<float>();
		var values2 = new List<float>();

		var fx = EasingFunctions.BounceInOut; // this is the function we are going to plot
		var x = 0f;

		while (x <= 1)
		{
			values1.Add(fx(x));
			values2.Add(fx(x - 0.15f));
			x += 0.025f;
		}

		var columnSeries1 = new ColumnSeries<float>
		{
			Values = values1,
			Stroke = null,
			Padding = 2
		};

		var columnSeries2 = new ColumnSeries<float>
		{
			Values = values2,
			Stroke = null,
			Padding = 2
		};

		columnSeries1.PointMeasured += OnPointMeasured;
		columnSeries2.PointMeasured += OnPointMeasured;

		Series4 = new List<ISeries> { columnSeries1, columnSeries2 };
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
