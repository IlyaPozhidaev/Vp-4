using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Notepad.Models;
using Notepad.ViewModels.Pages;
using System;
using System.Reactive;

namespace Notepad.Views.Pages
{
    public partial class FileListView : UserControl
    { 

        public FileListView()
        {
            InitializeComponent();
        }

        public void DoubleClickedEvent(object? sender, RoutedEventArgs args)
        {
            if (DataContext is FileListViewModel fileListViewModel)
            {
                fileListViewModel.OpenCommand.Execute().Subscribe();
            }
        }
    }
}
