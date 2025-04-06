using System.Globalization;
using System.Windows.Data;

namespace Exc1.Converters;

public class TypeToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string type && parameter is string targetTypeString)
            return type.Equals(targetTypeString, StringComparison.InvariantCultureIgnoreCase);
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isChecked && isChecked && parameter is string targetTypeString)
            return targetTypeString;
        return Binding.DoNothing;
    }
}