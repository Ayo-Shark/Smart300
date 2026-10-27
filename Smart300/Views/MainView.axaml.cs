using Avalonia.Controls;
using Smart300.ViewModels;

namespace Smart300.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        this.DataContext = new AuthViewModel();
    }
}