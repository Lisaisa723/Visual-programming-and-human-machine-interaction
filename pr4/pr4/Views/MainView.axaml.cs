using Avalonia.Controls;
using pr4.ViewModels;

namespace pr4.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    public void DoubleClickHandler(object sender, Avalonia.Input.TappedEventArgs e)
    {
        (DataContext as MainViewModel)!.HandleDoubleClicked((ListBox)sender, e);
    }
}
