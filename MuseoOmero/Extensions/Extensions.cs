namespace MuseoOmero.Extensions;

public static class VisualElementExtensions
{
	public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 160, Easing easing = null)
	{
		Func<double, Color> transform = (t) =>
			Color.FromRgba(fromColor.Red + t * (toColor.Red - fromColor.Red),
						   fromColor.Green + t * (toColor.Green - fromColor.Green),
						   fromColor.Blue + t * (toColor.Blue - fromColor.Blue),
						   fromColor.Alpha + t * (toColor.Alpha - fromColor.Alpha));
		return ColorAnimation(self, "ColorTo", transform, callback, length, easing);
	}

	public static void CancelAnimation(this VisualElement self)
	{
		self.AbortAnimation("ColorTo");
	}

	static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
	{
		easing = easing ?? Easing.Linear;
		var taskCompletionSource = new TaskCompletionSource<bool>();

		element.Animate(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));
		return taskCompletionSource.Task;
	}
}
public static class RandomExtensions
{
	public static T NextEnum<T>(this Random random)
	{
		var values = Enum.GetValues(typeof(T));
		return (T)values.GetValue(random.Next(values.Length));
	}

	public static dynamic NextElement(this Random random, dynamic[] array)
	{
		return array[random.Next(array.Length)];
	}
}
public static class ArrayExtensions
{
	public static int IndexOf<T>(this T[] array, T value)
	{
		return Array.IndexOf(array, value);
	}
}
public static class DateTimeExtension
{
	public static bool IsBetween(this DateTime datetime, DateTime start, DateTime end)
	{
		return datetime < end && datetime > start;
	}
}
public static class BoolExtension
{
	public static void Swap(this ref bool boolean)
	{
		boolean = !boolean;
	}
}
public static class IListExtension
{
	public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
	{
		return listToClone.Select(item => (T)item.Clone()).ToList();
	}
}
public static class Service
{
	public static T Get<T>()
	{
		return App.Current.Handler.MauiContext.Services.GetService<T>();
	}
}
public class Metadata
{
	public static readonly BindableProperty TagProperty = BindableProperty.Create("Tag", typeof(string), typeof(Metadata), null);

	public static string GetTag(BindableObject bindable)
		=> (string)bindable.GetValue(TagProperty);

	public static void SetTag(BindableObject bindable, string value)
		=> bindable.SetValue(TagProperty, value);
}

