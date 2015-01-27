using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KanbanProject
{
    class PostIt
    {
        private string _name;
        private int _estimatedTimeToCompleteTask;
        private string _responsibleName;
        private SolidColorBrush _colorBrush;

        public String ResponsibleName
        {
            get { return _responsibleName; }
            set { _responsibleName = value; }
        }

        public int EstimatedTimeToCompleteTask
        {
            get { return _estimatedTimeToCompleteTask; }
            set { _estimatedTimeToCompleteTask = value; }
        }

        public SolidColorBrush ColorBrush
        {
            get { return _colorBrush; }
            set { _colorBrush = value; }
        }

        public PostIt(string name, int estimatedTimeToCompleteTask, String responsibleName, SolidColorBrush colorBrush)
        {
            _name = name;
            _estimatedTimeToCompleteTask = estimatedTimeToCompleteTask;
            _responsibleName = responsibleName;
            _colorBrush = colorBrush;
        }

        public PostIt()
        {
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
