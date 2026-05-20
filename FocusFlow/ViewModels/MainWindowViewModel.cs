using FocusFlow.Helpers;
using FocusFlow.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFlow.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MainWindow mainWindow;

        private ObservableCollection<Tasks> _tasksData;
        public ObservableCollection<Tasks> TasksData
        {
            get { return _tasksData; }

            set 
            {
                _tasksData = value; 
                OnPropertyChanged();   
            }
        }

        private TimeSpan _timer;
        public TimeSpan Timer
        {
            get { return _timer; }
            set
            {
                _timer = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(MainWindow mainWindow) { 
            this.mainWindow = mainWindow;
        }
    }
}
