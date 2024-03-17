using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using pr4.ViewModels;

namespace pr4.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        AvaloniaXamlLoader.Load(this);
    }
}
