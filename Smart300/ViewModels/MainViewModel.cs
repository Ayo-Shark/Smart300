using CommunityToolkit.Mvvm.ComponentModel;

namespace Smart300.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}
