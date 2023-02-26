using System;
using System.Globalization;

namespace MuseoOmero.Converters;

public class BoolToObjectConverter<T> : IValueConverter
{
	public T TrueObject { get; set; }
	public T FalseObject { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (parameter is { } && (string)parameter == "true")
		{
			return (bool)value ? FalseObject : TrueObject;
		}
		return (bool)value ? TrueObject : FalseObject;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return ((T)value).Equals(TrueObject);
	}
}
