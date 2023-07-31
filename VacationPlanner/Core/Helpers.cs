namespace VacationPlanner.Core
{
    internal class Helpers
    {
        public static void AddPublicHolidays(Dictionary<DateTime, bool> vacationDates)
        {
            List<DateTime> publicHolidays = new()
        {
            new DateTime(DateTime.Now.Year, 1, 1),
            new DateTime(DateTime.Now.Year, 1, 2),
            new DateTime(DateTime.Now.Year, 3, 3),
            new DateTime(DateTime.Now.Year, 5, 1),
            new DateTime(DateTime.Now.Year, 5, 6),
            new DateTime(DateTime.Now.Year, 5, 24),
            new DateTime(DateTime.Now.Year, 12, 31),
            new DateTime(DateTime.Now.Year, 12, 24),
            new DateTime(DateTime.Now.Year, 12, 25),
            new DateTime(DateTime.Now.Year, 12, 27)
        };

            foreach (DateTime publicHoliday in publicHolidays)
            {
                vacationDates[publicHoliday] = true;
            }
        }

        public static Dictionary<DateTime, bool> InitializeYear()
        {
            Dictionary<DateTime, bool> calendarWithWeekends = new();
            DateTime startDate = new(DateTime.Now.Year, 1, 1);
            DateTime endDate = new(DateTime.Now.Year, 12, 31);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                bool isWeekend = IsWeekend(date);
                calendarWithWeekends.Add(date, isWeekend);
            }

            return calendarWithWeekends;
        }

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }
    }
}
