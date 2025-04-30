using System;

namespace IncaTechnologies.Recurrence
{
    internal static class DefaultOccurrences
    {
        internal static Secondly Secondly => new Secondly { Second = 0 };
        internal static Minutely Minutely
        {
            get
            {
                var minutely = new Minutely { Minute = 0 };
                var secondly = Secondly;
                minutely.Secondly = secondly;
                secondly.Minutely = minutely;

                return minutely;
            }
        }
        internal static Hourly Hourly
        {
            get
            {
                var hourly = new Hourly { Hour = 0 };
                var minutely = Minutely;
                var secondly = minutely.Secondly;
                hourly.Minutely = minutely;
                minutely.Hourly = hourly;
                minutely.Secondly = secondly;
                secondly.Minutely = minutely;

                return hourly;
            }
        }
        internal static Daily Daily 
        { 
            get 
            {
                var daily = new Daily { DayOfWeek = DayOfWeek.Sunday, DayOfMonth = 1 };
                var hourly = Hourly;
                hourly.Daily = daily;
                daily.At.Add(hourly);

                return daily;
            }
        }
        internal static Weekly Weekly
        {
            get
            {
                var weekly = new Weekly();
                var daily = Daily;
                daily.Weekly = weekly;
                weekly.On.Add(daily);

                return weekly;
            }
        }
        internal static Monthly Monthly
        {
            get
            {
                var monthly = new Monthly { Month = 1 };
                var daily = Daily;

                daily.Monthly = monthly;
                monthly.TheDay.Add(daily);

                return monthly;
            }
        }
        internal static Yearly Yearly
        {
            get
            {
                var yearly = new Yearly();
                var monthly = Monthly;
                monthly.Yearly = yearly;
                yearly.In.Add(monthly);

                return yearly;
            }
        }
    }
}