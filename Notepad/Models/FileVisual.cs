using Avalonia.Animation;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad.Models
{
    public class FileVisual
    {
        private string name;
        private string image;
        private bool isFile = false;

        public string Image { get => image; set => image = value; }
        public string Name { get => name; set => name = value; }
        public bool IsFile { get => isFile; set => isFile = value; }
    }
}
