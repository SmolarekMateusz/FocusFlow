using FocusFlow.Command;
using FocusFlow.Helpers;
using FocusFlow.Models;
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
        
        }
        private void TimerTick(object sender, EventArgs e)
        {
            if (TimeLeft.TotalSeconds > 0)
            {
                TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            }
        }

        private void AddTask(object obj) { }
        private void StartTimer(object obj) {
            _timer.Start();
        }
        private void StopTimer(object obj) {
            _timer.Stop();
        }
        private void ResetTimer(object obj) {
            _timer.Stop();
            SetupTimer();
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
        private void RemoveTask(object obj) { }

        private void SetupTimer()
        {
            TimeLeft = TimeSpan.FromMinutes(_setedTime);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTick;
        }
    }
}
