using System;
using Xamarin.Forms;

namespace MeditationCounter.Models
{
    public class Countdown : BindableObject
    {
        public event Action Completed;
        public event Action Ticked;

        public int Minutes { get; set; }

        private bool stopTimer = false;
        TimeSpan _displayTime;
        public TimeSpan DisplayTime
        {
            get { return _displayTime; }

            private set
            {
                _displayTime = value;
                OnPropertyChanged();
            }
        }

        public void Stop()
        {
            stopTimer = true;
        }

        public void Start(int seconds = 1)
        {
            stopTimer = false;
            DisplayTime = new TimeSpan(0, Minutes, 0);
            // Invoke timer every second
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                if (stopTimer) return false;
                DisplayTime = DisplayTime.Subtract(new TimeSpan(0, 0, seconds));
                var ticked = DisplayTime.TotalSeconds > 1;

                if (ticked)
                {
                    Ticked?.Invoke();
                }
                else
                {
                    Completed?.Invoke();
                }

                return ticked; 
            });
        }
    }
}