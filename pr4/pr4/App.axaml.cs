﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using pr4.ViewModels;
using pr4.Views;

namespace pr4;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mvm = new MainViewModel();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mvm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
