using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FocusFlow.Models
{
    public class Tasks
    {
        public String Title { get; set; }

        public int EstimatedMinutes { get; set; }

        public int SpentMinutes { get; set; } = 0;
        public bool IsComplete { get; set; } = false;

        public void IsCompleted()
        {
            if (SpentMinutes >= EstimatedMinutes)
            {
                IsComplete = true;
            }
        }
    }

}
