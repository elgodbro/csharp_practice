using System.ComponentModel;

namespace Exc1.Models;

public class TransactionModel : INotifyPropertyChanged
{
    private double _amount;
    private string _category;
    private DateTime _date;
    private string _type;
    private string _userId;
    private DateTime? _dueDate;
    private string _userName;
    
    public double Amount
    {
        get => _amount;
        set { _amount = value; OnPropertyChanged(nameof(Amount)); }
    }

    public DateTime Date
    {
        get => _date;
        set { _date = value; OnPropertyChanged(nameof(Date)); }
    }

    public string Category
    {
        get => _category;
        set { _category = value; OnPropertyChanged(nameof(Category)); }
    }

    public string Type
    {
        get => _type;
        set { _type = value; OnPropertyChanged(nameof(Type)); }
    }

    public string UserId
    {
        get => _userId;
        set { _userId = value; OnPropertyChanged(nameof(UserId)); }
    }

    public DateTime? DueDate
    {
        get => _dueDate;
        set { _dueDate = value; OnPropertyChanged(nameof(DueDate)); }
    }

    public string UserName
    {
        get => _userName;
        set 
        { 
            _userName = value; 
            OnPropertyChanged(nameof(UserName)); 
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
