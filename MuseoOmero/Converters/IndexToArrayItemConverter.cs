namespace MuseoOmero.Converters;
public class IndexToArrayItemConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return ((object[])parameter)[(int)value];
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return ((object[])parameter).IndexOf(value);
	}
}
