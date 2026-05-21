using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFlow.Models
{
    public class FocusSession
    {
        public DateTime StartedAt { get; set; }
        public int DurationMinutes { get; set; }
        public Tasks RelatedTask { get; set; }
    }
}
