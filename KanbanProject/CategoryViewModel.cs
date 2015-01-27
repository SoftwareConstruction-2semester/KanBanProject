using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject
{
    class CategoryViewModel
    {
        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public ObservableCollection<PostIt> PostIts { get; set; }

        public CategoryViewModel(string CategoryName)
        {
            _categoryName = CategoryName;
            PostIts = new ObservableCollection<PostIt>();
        }

        public CategoryViewModel(string CategoryName, List<string> savedStickers)
        {
            _categoryName = CategoryName;
            PostIts = new ObservableCollection<PostIt>();
            foreach (string savedSticker in savedStickers)
            {
                PostIts.Add(new PostIt() { Name = savedSticker });
            }
        }
    }
}
