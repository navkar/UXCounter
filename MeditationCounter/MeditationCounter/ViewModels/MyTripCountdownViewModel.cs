using MeditationCounter.Models;
using MeditationCounter.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeditationCounter.ViewModels
{
    public class MeditationCounterViewModel : BaseViewModel
    {
        private Session _session;
        private Countdown _countdown;
        private int _days;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private double _progress;

        public MeditationCounterViewModel()
        {
            _countdown = new Countdown();
        }

        public Session MySession
        {
            get => _session;
            set => SetProperty(ref _session, value);
        }

        public int Days
        {
            get => _days;
            set => SetProperty(ref _days, value);
        }

        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }
        public int Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public ICommand RestartCommand => new Command(Restart);

        public override Task LoadAsync()
        {
            LoadTrip();

            _countdown.Minutes = MySession.Minutes;
            _countdown.Start();

            _countdown.Ticked += OnCountdownTicked;
            _countdown.Completed += OnCountdownCompleted;

            return base.LoadAsync();
        }

        public override Task UnloadAsync()
        {
            _countdown.Ticked -= OnCountdownTicked;
            _countdown.Completed -= OnCountdownCompleted;

            return base.UnloadAsync();
        }

        void OnCountdownTicked()
        {
            Days = _countdown.DisplayTime.Days;
            Hours = _countdown.DisplayTime.Hours;
            Minutes = _countdown.DisplayTime.Minutes;
            Seconds = _countdown.DisplayTime.Seconds;
                        
            var totalSeconds = new TimeSpan(0,MySession.Minutes,0).TotalSeconds;
            var remainSeconds = _countdown.DisplayTime.TotalSeconds;
            Progress = remainSeconds / totalSeconds;
        }

        void OnCountdownCompleted()
        {
            Days = 0;
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            Progress = 0;
        }

        void LoadTrip()
        {
            var medidate = new Session()
            {
                Picture = "trip",
                Minutes = 1,
                Creation = DateTime.Now
            };

            MySession = medidate;
        }

        async void Restart()
        {
            Debug.WriteLine("Restart");
            _countdown.Stop();
            await UnloadAsync();
            await LoadAsync();
        }
    }
}