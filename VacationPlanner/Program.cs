using VacationPlanner;
using static VacationPlanner.Enums;

// INPUT
const ushort VACATION_DAYS = 20;
const PlanningStrategy PLANNING_STRATEGY = PlanningStrategy.SmallestGapFirst;

Dictionary<DateTime, bool> calendar = Helpers.InitializeYear();
Helpers.AddPublicHolidays(calendar);

// CALCULATE
Planner planner = new(PLANNING_STRATEGY, VACATION_DAYS);
List<DateTime> vacationDates = planner.Plan(calendar);

// OUTPUT
Console.WriteLine($"To use {VACATION_DAYS} vacation days most efficiently combined with public holidays and weekends, take the following dates off: \n");
vacationDates.Sort();
int counter = 0;
foreach (DateTime date in vacationDates)
{
	counter++;
	Console.WriteLine($"{counter}: {date}");
}


