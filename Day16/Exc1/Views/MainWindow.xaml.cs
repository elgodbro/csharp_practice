using System.Windows;
using Exc1.ViewModels;

namespace Exc1.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new FinanceViewModel();
    }
}