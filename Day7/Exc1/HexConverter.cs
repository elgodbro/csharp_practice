namespace Exc1;

public class HexConverter
{
    public string Convert(int number) => System.Convert.ToString(number, 16).ToUpper();
}