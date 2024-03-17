using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr4.ViewModels
{
    abstract public class DirEntity
    {
        public string Name { get; set; } = "";
        public Bitmap? ImageSource { get; set; }
        public string PathToEntity { get; set; }

        abstract protected void SetSource();

        protected void SetName(string name)
        {
            Name = name;
        }
        public DirEntity(string name, string path)
        {
            SetName(name);
            SetSource();
            PathToEntity = path;
        }
    }

    public class Folder : DirEntity
    {
        public Folder(string name, string path) : base(name, path)
        {
            SetName(name);
            SetSource();
            PathToEntity = path;
        }

        override protected void SetSource()
        {
            ImageSource = new Bitmap(@"Assets\folder.png");
        }
    }

    public class Drive : DirEntity
    {
        public Drive(string name, string path) : base(name, path)
        {
            SetName(name);
            SetSource();
            PathToEntity = path;
        }

        override protected void SetSource()
        {
            ImageSource = new Bitmap(@"Assets\drive.png");
        }
    }

    public class File : DirEntity
    {
        public File(string name, string path) : base(name, path)
        {
            SetName(name);
            SetSource();
            PathToEntity = path;
        }

        override protected void SetSource()
        {
            ImageSource = new Bitmap(@"Assets\file.png");
        }
    }

}
