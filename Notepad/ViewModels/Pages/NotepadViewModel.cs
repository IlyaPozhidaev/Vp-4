using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Notepad.ViewModels.Pages
{
    public class NotepadViewModel : ViewModelBase
    {
        private string text;

        public NotepadViewModel()
        {
            OpenCommand = ReactiveCommand.Create<Unit, bool>(_ => false);
            SaveCommand = ReactiveCommand.Create<Unit, bool>(_ => true);
        }

        public string Text
        {
            get => text;
            set => this.RaiseAndSetIfChanged(ref text, value);
        }

        public ReactiveCommand<Unit, bool> OpenCommand { get; }
        public ReactiveCommand<Unit, bool> SaveCommand { get; }
    }
}
