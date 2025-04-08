using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Exc1.Converters;

public class BalanceToBrushConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length <= 0 || values[0] is not double balance) return Brushes.Black;
        return balance switch
        {
            <= 0 => Brushes.Red,
            _ => Brushes.Green
        };
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}