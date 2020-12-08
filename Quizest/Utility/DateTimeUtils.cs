using System;

namespace Utility
{
    public static class DateTimeUtils
    {
        public static string ToDuration(int? duration)
        {
            return duration == null
                    ? string.Empty 
                    : TimeSpan.FromSeconds(duration.Value).Hours == 0
                      ? TimeSpan.FromSeconds(duration.Value).ToString(@"mm\:ss")
                      : TimeSpan.FromSeconds(duration.Value).ToString(@"hh\:mm\:ss");
        }
    }
}
