using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.X11;
using Notepad.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;

namespace Notepad.ViewModels.Pages
{
    public class FileListViewModel : ViewModelBase
    {
        protected string path = Directory.GetCurrentDirectory();
        protected ObservableCollection<FileVisual> _directories;
        protected DirectoryInfo dr;
        protected FileVisual file;
        protected string textBoxText;
        protected string buttonContent;
        protected bool status;


        public FileListViewModel()
        {
            Status = false;
            DirectoriesAndFiles();

            OpenCommand = ReactiveCommand.Create<Unit, string>(_ =>
            {
                if (file is not null)
                {
                    if (!file.IsFile)
                    {
                        if (file.Image == "Assets/up.jpg")
                        {
                            if (Directory.GetParent(path) is not null)
                            {
                                path = Directory.GetParent(path).FullName;
                                DirectoriesAndFiles();
                            }

                        }
                        else
                        {
                            path += @"\" + file.Name;
                            DirectoriesAndFiles();
                        }
                        return "";
                    }
                    else
                    {
                        string str;
                        using (StreamReader reader = new StreamReader(path + @"\" + textBoxText))
                        {
                            str = reader.ReadToEnd();
                        }
                        status = true;
                        return str;
                    }
                }
                if (textBoxText != "")
                {
                    if (System.IO.File.Exists(path + @"\" + textBoxText))
                    {
                        string str;
                        using (StreamReader reader = new StreamReader(path + @"\" + textBoxText))
                        {
                            str = reader.ReadToEnd();
                        }
                        status = true;
                        return str;
                    }
                    else { status = true; TextBoxText = ""; return ""; }
                }
                return "";
            });

            CancelCommand = ReactiveCommand.Create(() => { });
            ButtonContent = "Open";
        }

        public void DirectoriesAndFiles()
        {
            dr = new DirectoryInfo(path);
            Directories = new ObservableCollection<FileVisual>();
            if (Directory.GetParent(path) is not null) Directories.Add(new FileVisual { Name = "..", IsFile = false, Image = "Assets/up.png" });
            foreach (var d in dr.GetDirectories())
            {
                Directories.Add(new FileVisual { Name = d.Name, IsFile = false, Image = "Assets/directory.png" });
            }
            foreach (var d in dr.GetFiles())
            {
                Directories.Add(new FileVisual { Name = d.Name, IsFile = true, Image = "Assets/file.png" }); ;
            }
        }

        public ObservableCollection<FileVisual> Directories
        {
            get => _directories;
            set
            {
                this.RaiseAndSetIfChanged(ref _directories, value);
            }
        }


        public virtual FileVisual File
        {
            get => file;
            set 
            { 
                this.RaiseAndSetIfChanged(ref file, value);
                if (file is not null) TextBoxText = file.IsFile == true ? file.Name : "";
            }
        }

        public virtual string TextBoxText
        {
            get => textBoxText;
            set => this.RaiseAndSetIfChanged(ref textBoxText, value);
        }

        public string ButtonContent 
        {
            get => buttonContent;
            set => this.RaiseAndSetIfChanged(ref buttonContent, value);
        }

        public bool Status
        {
            get => status;
            set => status = value;
        }

        public virtual ReactiveCommand<Unit, string> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }
    }
}
