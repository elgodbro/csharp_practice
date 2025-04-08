using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Exc1.Models;

public class TransactionModel : INotifyPropertyChanged
{
    private double _amount;
    private Category _category;

    private int _categoryId;
    private DateTime _date;
    private DateTime? _dueDate;
    private int _id;
    private string _type;
    private string _userId;
    private string _userName;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public double Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public string Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }

    public string UserId
    {
        get => _userId;
        set => SetProperty(ref _userId, value);
    }

    public DateTime? DueDate
    {
        get => _dueDate;
        set => SetProperty(ref _dueDate, value);
    }

    public int CategoryId
    {
        get => _categoryId;
        set
        {
            if (SetProperty(ref _categoryId, value)) Category = null;
        }
    }

    public virtual Category Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
        storage = value;
        OnPropertyChanged(propertyName);

        if (propertyName != nameof(Category) || value is not Category cat) return true;
        if (_categoryId != cat.Id)
            CategoryId = cat.Id;

        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}