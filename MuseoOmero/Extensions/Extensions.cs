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
