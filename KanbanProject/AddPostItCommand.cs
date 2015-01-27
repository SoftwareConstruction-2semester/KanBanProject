using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace KanbanProject
{
    public class AddPostItCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ObservableCollection<PostIt> todolist = (ObservableCollection<PostIt>) parameter;
            todolist.Add(new PostIt("djg", 4, "Ebbe", new SolidColorBrush(Colors.Bisque)));
        }

        public event EventHandler CanExecuteChanged;
    }
}