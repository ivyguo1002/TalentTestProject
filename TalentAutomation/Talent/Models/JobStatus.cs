using System;
using System.Collections.Generic;
using System.Text;

namespace Talent.Models
{
    public class JobStatus
    {
        public string JobStatusText { get; set; }
        public System.Drawing.Color JobStatusColour { get; set; }

        public bool Equals(JobStatus jobStatus)
        {
            if (JobStatusText == jobStatus.JobStatusText && JobStatusColour.ToArgb() == jobStatus.JobStatusColour.ToArgb())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
