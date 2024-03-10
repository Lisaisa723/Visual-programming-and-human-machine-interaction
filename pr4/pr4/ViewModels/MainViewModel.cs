using System.Collections.ObjectModel;
using System.IO;
using ReactiveUI;
using Avalonia.Controls;
using System.Collections.Generic;

namespace pr4.ViewModels;
public class MainViewModel : ViewModelBase
{
    private List<string> path;
    private ObservableCollection<string> _files;
    private string _selectedItem;

    public MainViewModel()
    {
        path = new List<string>();
        OpenDirectory("..");
    }

    public ObservableCollection<string> Files
    {
        get => _files;
        set => this.RaiseAndSetIfChanged(ref _files, value);
    }

    public string SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    private string PathToString()
    {
        string res = string.Empty;
        for (var i = 0; i < path.Count; ++i)
        {
            res += path[i];
        }
        return res;
    }

    public void OpenDirectory(string directory)
    {
        if (directory == null || directory != ".." && !Directory.Exists(PathToString() + directory))
            return;
        
        if (directory == "..")
        {
            if (path.Count > 0)
                path.RemoveAt(path.Count - 1);
        }
        else
        {
            path.Add(directory);
        }

        var currentPath = PathToString();

        var _directories = new ObservableCollection<string>();
        if (currentPath == string.Empty) 
        {
            foreach (var d in DriveInfo.GetDrives())
            {
                _directories.Add(d.Name);
            }
        } else
        {
            _directories = new ObservableCollection<string>();
            _directories.Add("..");
            foreach (var item in Directory.GetDirectories(currentPath))
            {
                _directories.Add(item.Substring(item.LastIndexOf('\\') + 1) + "\\");
            }
            foreach (var item in Directory.GetFiles(currentPath))
            {
                _directories.Add(item.Substring(item.LastIndexOf('\\') + 1));
            }
        }
        Files = _directories;
    }

    public void HandleDoubleClicked(ListBox sender, Avalonia.Input.TappedEventArgs e)
    {
        if (sender == null || sender.SelectedItem == null)
            return;
        var item = sender.SelectedItem.ToString();
        OpenDirectory(item);
    }
}

