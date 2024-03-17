using System.Collections.ObjectModel;
using System.IO;
using ReactiveUI;
using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using System.Runtime.CompilerServices;

namespace pr4.ViewModels;
public class MainViewModel : ViewModelBase
{
    private List<string> path;
    private ObservableCollection<DirEntity> _files;
    private string _selectedItem;

    public MainViewModel()
    {
        path = new List<string>();
        OpenDirectory("..");
    }

    public ObservableCollection<DirEntity> Files
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
    public Bitmap? _image;
    public Bitmap? ImageToView
    {
        get => _image;
        set { this.RaiseAndSetIfChanged(ref _image, value); }
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

        var _directories = new ObservableCollection<DirEntity>();
        if (currentPath == string.Empty) 
        {
            foreach (var d in DriveInfo.GetDrives())
            {
                _directories.Add(new Drive(d.Name, d.Name));
            }
        } else
        {
            _directories = new ObservableCollection<DirEntity>();
            _directories.Add(new Folder("..", currentPath.Substring(0, currentPath.LastIndexOf('\\'))));
            foreach (var item in Directory.GetDirectories(currentPath))
            {
                _directories.Add(new Folder(item.Substring(item.LastIndexOf('\\') + 1) + "\\", item));
            }
            foreach (var item in Directory.GetFiles(currentPath))
            {
                _directories.Add(new File(item.Substring(item.LastIndexOf('\\') + 1), item));
            }
        }
        Files = _directories;
    }

    public void HandleDoubleClicked(ListBox sender, Avalonia.Input.TappedEventArgs e)
    {
        if (sender == null || sender.SelectedItem == null)
            return;
        var item = ((DirEntity)sender.SelectedItem).Name;
        OpenDirectory(item);

        string extension = Path.GetExtension(item);
        bool isImage = false;
        switch (extension)
        {
            case ".jpg":
                isImage = true;
                break;
            case ".jpeg":
                isImage = true;
                break;
            case ".png":
                isImage = true;
                break;
            default:
                break;
        }
        if (isImage)
        {
            ImageToView = new Bitmap(((DirEntity)sender.SelectedItem).PathToEntity);
        }
    }

}

