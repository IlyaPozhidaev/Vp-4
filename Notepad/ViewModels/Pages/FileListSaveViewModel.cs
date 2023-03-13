using Notepad.Models;
using ReactiveUI;
using System.IO;
using System.Reactive;

namespace Notepad.ViewModels.Pages
{
    public class FileListSaveViewModel : FileListViewModel
    {
        private string fileText;

        public FileListSaveViewModel()
        {
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
                        using (StreamWriter writer = new StreamWriter(path+ @"\" + textBoxText, false))
                        {
                            writer.WriteLine(fileText);
                        }
                        status = true;
                        return "Saved!";
                    }
                }
                using (StreamWriter writer = new StreamWriter(path+ @"\" + textBoxText, false))
                {
                    writer.WriteLine(fileText);
                }
                status = true;
                return "Saved!";
            });
        }
        public override FileVisual File
        {
            get => file;
            set
            {
                this.RaiseAndSetIfChanged(ref file, value);
                if (file is not null) TextBoxText = file.IsFile == true ? file.Name : "";
                if (file is not null) ButtonContent = file.IsFile == true ? "Save" : "Open";
            }
        }
        public string FileText
        {
            get => fileText;
            set => fileText = value;
        }

        public override string TextBoxText
        {
            get => textBoxText;
            set
            {
                this.RaiseAndSetIfChanged(ref textBoxText, value);
                if (textBoxText!="") ButtonContent = "Save";
                else ButtonContent = "Open";
            }
        }

        public override ReactiveCommand<Unit, string> OpenCommand { get; }
    }
}
