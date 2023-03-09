namespace MuseoOmero.Converters;
public class IsListEmptyConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		//var c = value.GetType();
		//CAST NON VALIDO
		return ((IList<dynamic>)value).Count == 0;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
