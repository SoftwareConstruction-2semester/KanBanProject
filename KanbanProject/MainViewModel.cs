using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GongSolutions.Wpf.DragDrop;
using Newtonsoft.Json;

namespace KanbanProject
{
    class MainViewModel : INotifyPropertyChanged, IDropTarget
    {
        private string _inputStickerName;
        private ObservableCollection<ObservableCollection<PostIt>> _categoryCollection;
        ObservableCollection<CategoryViewModel> todoCategory = new ObservableCollection<CategoryViewModel>();
        ObservableCollection<CategoryViewModel> doingCategory = new ObservableCollection<CategoryViewModel>();
        ObservableCollection<CategoryViewModel> doneCategory = new ObservableCollection<CategoryViewModel>();
        private static ObservableCollection<PostIt> _toDoList;
        private static ObservableCollection<PostIt> _doingList;
        private static ObservableCollection<PostIt> _doneList;
        private CategoryViewModel toDoHandler;
        private CategoryViewModel doingHandler;
        private CategoryViewModel doneHandler;
        private ICommand _addSticker;
        private ICommand _saveCommand;
        private ICommand _loadCommand;

        public ICollectionView ToDoCategory { get; private set; }
        public ICollectionView DoingCategory { get; private set; }
        public ICollectionView DoneCategory { get; private set; }
        public ObservableCollection<ObservableCollection<PostIt>> CategoryCollection
        {
            get { return _categoryCollection; }
            set { _categoryCollection = value; }
        }

        public string InputStickerName
        {
            get { return _inputStickerName; }
            set { _inputStickerName = value;
            OnPropertyChanged("InputStickerName");}
        }

        public static ObservableCollection<PostIt> ToDoList
        {
            get { return _toDoList; }
            set
            {
                _toDoList = value;
               // OnPropertyChanged("ToDoList");
            }
        }

        public static ObservableCollection<PostIt> DoingList
        {
            get { return _doingList; }
            set
            {
                _doingList = value;
            }
        }

        public static ObservableCollection<PostIt> DoneList
        {
            get { return _doneList; }
            set { _doneList = value; }
        }

        public ICommand AddSticker
        {
            get { return _addSticker; }
        }

        public ICommand Save
        {
            get { return _saveCommand;}
        }

        public ICommand Load
        {
            get { return _loadCommand; }
        }

        //constructor
        public MainViewModel()
        {
            _loadCommand = new LoadCommand();
            _saveCommand = new SaveCommand();
            _categoryCollection = new ObservableCollection<ObservableCollection<PostIt>>();
            
            CategoryViewModel toDoHandler = new CategoryViewModel("ToDo");
            todoCategory.Add(toDoHandler);
            ToDoList = toDoHandler.PostIts;
            
            CategoryViewModel doingHandler = new CategoryViewModel("Doing");
            doingCategory.Add(doingHandler);
            DoingList = doingHandler.PostIts;

            CategoryViewModel doneHandler = new CategoryViewModel("Done");
            doneCategory.Add(doneHandler);
            DoneList = doneHandler.PostIts;

            ToDoCategory = CollectionViewSource.GetDefaultView(todoCategory);
            DoingCategory = CollectionViewSource.GetDefaultView(doingCategory);
            DoneCategory = CollectionViewSource.GetDefaultView(doneCategory);

            //create postit's
            toDoHandler.PostIts.Add(new PostIt("Create Domain Model", 1, "Someone else?", new SolidColorBrush(Colors.Chartreuse)));
            toDoHandler.PostIts.Add(new PostIt("Copy ViewModel from Larus", 2, "You!", new SolidColorBrush(Colors.CadetBlue)));
            toDoHandler.PostIts.Add(new PostIt("Copy Paste XAML", 1, "You!", new SolidColorBrush(Colors.Chocolate)));

            //add postit command
            _addSticker = new AddPostItCommand();
        }

        internal class SaveCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                PersistenceHandler.Save(ToDoList, DoingList, DoneList);
            }

            public event EventHandler CanExecuteChanged;
        }

        internal class LoadCommand   : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                ToDoList.Clear();
                List<PostIt> toDoPostits = PersistenceHandler.LoadTodoList();
                foreach (var postIt in toDoPostits)
                {
                    ToDoList.Add(postIt);
                }

                DoingList.Clear();
                List<PostIt> doingPostits = PersistenceHandler.LoadDoingList();
                foreach (var postIt in doingPostits)
                {
                    DoingList.Add(postIt);
                }

                DoneList.Clear();
                List<PostIt> donePostits = PersistenceHandler.loadDoneList();
                foreach (var postIt in donePostits)
                {
                    DoneList.Add(postIt);
                }
            }

            public event EventHandler CanExecuteChanged;
        }
        public void DragOver(IDropInfo dropInfo)
        {
           // InputStickerName = dropInfo.TargetItem.ToString();
            if (dropInfo.Data is PostIt && dropInfo.TargetItem is CategoryViewModel)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            CategoryViewModel fieldCollection = (CategoryViewModel)dropInfo.TargetItem;
            PostIt stick = (PostIt)dropInfo.Data;
            fieldCollection.PostIts.Add(stick);
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(stick);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
