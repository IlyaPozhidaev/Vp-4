using Notepad.ViewModels.Pages;
using Notepad.Views.Pages;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Notepad.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _content;
        private NotepadViewModel _notepadView;
        private FileListOpenViewModel _fileListOpenView;
        private FileListSaveViewModel _fileListSaveView;

        public MainWindowViewModel()
        {
            _notepadView = new NotepadViewModel();
            _notepadView.Text = "";
            _fileListOpenView = new FileListOpenViewModel();
            _fileListSaveView = new FileListSaveViewModel();
            Content = _notepadView;
            Observable.Merge(
                _notepadView.OpenCommand,
                _notepadView.SaveCommand
            ).Subscribe(flag =>
            {
                if (flag == true)
                {
                    Content = _fileListSaveView;
                    _fileListSaveView.FileText = _notepadView.Text;
                }
                else
                {
                    Content = _fileListOpenView;
                }
            });
            _fileListOpenView.OpenCommand.Subscribe(str =>
            {
                if (_fileListOpenView.Status == true)
                {
                    _notepadView.Text = str;
                    _fileListOpenView.Status = false;
                    Content = _notepadView;
                }
            });
            _fileListOpenView.CancelCommand.Subscribe(unit =>
            {
                Content = _notepadView;
            });
            _fileListSaveView.OpenCommand.Subscribe(str =>
            {
                if (_fileListSaveView.Status == true)
                {
                    _fileListOpenView.DirectoriesAndFiles();
                    _fileListSaveView.DirectoriesAndFiles();
                    _fileListSaveView.Status = false;
                    Content = _notepadView;
                }
            });
            _fileListSaveView.CancelCommand.Subscribe(unit =>
            {
                Content = _notepadView;
            });
        }

        public object Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }
    }
}
