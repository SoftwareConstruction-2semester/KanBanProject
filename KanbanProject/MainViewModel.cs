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

namespace KanbanProject
{
    class MainViewModel : INotifyPropertyChanged, IDropTarget
    {
        
        
        private string _inputStickerName;
        private ObservableCollection<ObservableCollection<PostIt>> _categoryCollection;
        ObservableCollection<CategoryViewModel> todoCategory = new ObservableCollection<CategoryViewModel>();
        ObservableCollection<CategoryViewModel> doingCategory = new ObservableCollection<CategoryViewModel>();
        ObservableCollection<CategoryViewModel> doneCategory = new ObservableCollection<CategoryViewModel>();
        private ObservableCollection<PostIt> _toDoList;
        private ObservableCollection<PostIt> _doingList;
        private ObservableCollection<PostIt> _doneList;
        private CategoryViewModel toDoHandler;
        private CategoryViewModel doingHandler;
        private CategoryViewModel doneHandler;
        private ICommand _addSticker;

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

        public ObservableCollection<PostIt> ToDoList
        {
            get { return _toDoList; }
            set { _toDoList = value; }
        }

        public ObservableCollection<PostIt> DoingList
        {
            get { return _doingList; }
            set { _doingList = value; }
        }

        public ObservableCollection<PostIt> DoneList
        {
            get { return _doneList; }
            set { _doneList = value; }
        }

        public ICommand AddSticker
        {
            get { return _addSticker; }
        }

        //constructor
        public MainViewModel()
        {
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
