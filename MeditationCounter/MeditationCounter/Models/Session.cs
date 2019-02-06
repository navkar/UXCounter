using System;

namespace MeditationCounter.Models
{
    public class Session
    {
        public string Picture { get; set; }
        public int Minutes { get; set; }
        public DateTime Creation { get; set; }
    }
}
