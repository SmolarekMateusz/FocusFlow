using FocusFlow.Command;
using FocusFlow.Helpers;
using FocusFlow.Models;
using FocusFlow.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace FocusFlow.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private AddTaskView addTaskView;

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

        private ObservableCollection<FocusSession> _focusSessionData;
        public ObservableCollection<FocusSession> FocusSessionData
        {
            get { return _focusSessionData; }

            set
            {
                _focusSessionData = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _timeLeft;

        public TimeSpan TimeLeft
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FormattedTime));
            }
        }
        private Tasks _selectedTask;

        public Tasks SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }
        private bool _isSessionActive;

        public bool IsSessionActive
        {
            get => _isSessionActive;
            set
            {
                _isSessionActive = value;
                OnPropertyChanged();
            }
        }
        public string FormattedTime =>
            TimeLeft.ToString(@"mm\:ss");

        private DispatcherTimer _timer;
        private int _setedTime;
        public ICommand AddTaskCommand { get; }
        public ICommand StartTimerCommand { get; }
        public ICommand StopTimerCommand { get; }
        public ICommand ResetTimerCommand { get; }
        public ICommand TurnUpTimerCommand { get; }
        public ICommand TurnDownTimerCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand SelectTaskCommand { get;}

        public MainWindowViewModel(MainWindow mainWindow) { 
            this.mainWindow = mainWindow;
            
            _setedTime = 25;

            SetupTimer();

            AddTaskCommand = new RelayCommand(AddTask);
            StartTimerCommand = new RelayCommand(StartTimer);
            StopTimerCommand = new RelayCommand(StopTimer);
            ResetTimerCommand = new RelayCommand(ResetTimer);
            TurnUpTimerCommand = new RelayCommand(TurnUpTimer);
            TurnDownTimerCommand = new RelayCommand(TurnDownTimer);
            RemoveTaskCommand = new RelayCommand(RemoveTask);
            SelectTaskCommand = new RelayCommand(SelectTask);

            TasksData = new ObservableCollection<Tasks>();
            FocusSessionData = new ObservableCollection<FocusSession>();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            if (TimeLeft.TotalSeconds > 0)
            {
                TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                _timer.Stop();
                SelectedTask.SpentMinutes += _setedTime;
                SelectedTask.IsCompleted();
                IsSessionActive = false;
                SetupTimer();

            }
        }
        private void SelectTask(object obj)
        {
            if (obj is Tasks task)
            {
                if (!_timer.IsEnabled)
                {
                    SelectedTask = task;
                }
            }
        }
        private void AddTask(object obj) {
            addTaskView = new AddTaskView();
            addTaskView.ShowDialog();
            if (addTaskView.taskTitle.Text.Length > 0 && addTaskView.taskTime.Value > 0 && addTaskView.taskTitle.Text != null && addTaskView.taskTime.Value != null)
            {
                Tasks task = new Tasks();
                task.Title = addTaskView.taskTitle.Text;
                task.EstimatedMinutes = (int) addTaskView.taskTime.Value;
                TasksData.Add(task);
            }
        }
        private void StartTimer(object obj) {
            if (SelectedTask != null && !_timer.IsEnabled)
            {
                _timer.Start();
                if (!_isSessionActive)
                {
                    IsSessionActive = true;
                    FocusSession session = new FocusSession();
                    session.StartedAt = DateTime.Now;
                    session.DurationMinutes = _setedTime;
                    session.RelatedTask = SelectedTask;
                }
            }
        }
        private void StopTimer(object obj) {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
        }
        private void ResetTimer(object obj) {
            _timer.Stop();
            SelectedTask.SpentMinutes += _setedTime - _timeLeft.Minutes;
            SelectedTask.IsCompleted();
            SetupTimer();
            IsSessionActive = false;
        }
        private void TurnUpTimer(object obj) {
            if (!_timer.IsEnabled)
            {
                _setedTime += 1;
                SetupTimer();
            }
        }
        private void TurnDownTimer(object obj) {
            if (!_timer.IsEnabled && _setedTime > 0)
            {
                _setedTime -= 1;
                SetupTimer();
            }
        }
        private void RemoveTask(object parameter) {
            if (parameter is Tasks task)
            {
                TasksData.Remove(task);
                if (task == SelectedTask)
                {
                    SelectedTask = null;
                }
            }
        }

        private void SetupTimer()
        {
            TimeLeft = TimeSpan.FromMinutes(_setedTime);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTick;
        }
    }
}
