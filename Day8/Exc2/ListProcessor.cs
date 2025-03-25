namespace Exc2;

public class ListProcessor
{
    public int GetElementAt(List<int> list, int index)
    {
        if (index < 0 || index >= list.Count)
            throw new IndexOutOfRangeException($"Индекс {index} выходит за пределы списка. Допустимый диапазон: 0-{list.Count - 1}");

        return list[index];
    }
}