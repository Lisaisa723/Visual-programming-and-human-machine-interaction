using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Drawing;

namespace pr2.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        var btn = (Button)sender;
        rectangle.Fill = btn.Background;
    }
}
