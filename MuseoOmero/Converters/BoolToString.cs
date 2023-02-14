using MuseoOmero.Managers;
using System.Globalization;

namespace MuseoOmero.Converters;
public class BoolToString : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (bool)value ? DeviceManager.Instance.Colors[4] : DeviceManager.Instance.Colors[5];
	}


	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (string)value;
	}
}