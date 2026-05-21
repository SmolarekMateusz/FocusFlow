using FocusFlow.Command;
using FocusFlow.Helpers;
using FocusFlow.Models;
using FocusFlow.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FocusFlow.ViewModels
{
    public class AddTaskViewModel : ViewModelBase
    {
        private AddTaskView addTaskView;

        private string _taskTitle;

        public string TaskTitle
        {
            get { return _taskTitle; }
            set 
            {
                _taskTitle = value; 
                OnPropertyChanged();
            }
        }

        public ICommand CreateTaskCommand { get; }

        public AddTaskViewModel(AddTaskView addTaskView)
        {
            this.addTaskView = addTaskView;

            _taskTitle = "";

            CreateTaskCommand = new RelayCommand(CreateTask);
        }

        private void CreateTask(object obj)
        {
            if (_taskTitle != null && _taskTitle.Length > 0)
            {
                addTaskView.Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Title is null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
