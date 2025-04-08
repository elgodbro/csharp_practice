using System.Globalization;
using System.Windows.Data;

namespace Exc1.Converters;

public class NullToBooleanConverter : IValueConverter
{
    public bool IsInverted { get; set; } = false;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var result = value != null;
        return IsInverted ? !result : result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}