using System.Globalization;

namespace MuseoOmero.Converters;
public class RadioButtonToImageWeight : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return false;
	}


	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (string)value;
	}
}
