using static VacationPlanner.Enums;

namespace VacationPlanner
{
	internal class Planner
	{
		private readonly PlanningStrategy _planningStrategy;
		private readonly ushort _vacationDays;
		public Planner()
		{

		}

		public Planner(PlanningStrategy planningStrategy, ushort vacationDays)
		{
			_planningStrategy = planningStrategy;
			_vacationDays = vacationDays;
		}

		public List<DateTime> Plan(Dictionary<DateTime, bool> calendar)
		{
			List<WorkdayGap> workdayGaps = GetWorkDayGaps(calendar);

			List<DateTime> vacationDates = new();

			switch (_planningStrategy)
			{
				case PlanningStrategy.SmallestGapFirst:
					{
						vacationDates = GetVacationDatesForSmallestGapFirstStrategy(workdayGaps, _vacationDays);
					}
					break;
				case PlanningStrategy.BiggestGapFirst:
					{
						vacationDates = GetVacationDatesForBiggestGapFirstStrategy(workdayGaps, _vacationDays);
					}
					break;
				case PlanningStrategy.Random:
					{
						// TODO
					}
					break;
				default:
					break;
			}

			return vacationDates;			
		}

		private List<WorkdayGap> GetWorkDayGaps(Dictionary<DateTime, bool> workdaysWithVacations)
		{
			List<WorkdayGap> workdayGaps = new();
			ushort gapSize = 0;
			DateTime gapStart = workdaysWithVacations.Keys.First();
			DateTime gapEnd = gapStart;
			foreach (KeyValuePair<DateTime, bool> day in workdaysWithVacations)
			{
				if (!day.Value) // workday
				{
					gapSize++;
					gapEnd = day.Key;
				}
				else
				{
					if (gapSize > 0)
					{
						WorkdayGap gap = new(gapStart, gapEnd, gapSize);

						workdayGaps.Add(gap);

						gapSize = 0;
					}

					gapStart = day.Key.AddDays(1);
					gapEnd = gapStart;
				}
			}

			return workdayGaps;
		}

		private List<DateTime> GetVacationDatesForSmallestGapFirstStrategy(List<WorkdayGap> workdayGaps, ushort vacationDays)
		{
			WorkdayGapComparer comparer = new(false);
			workdayGaps.Sort(comparer);
			

			var vacationDates = CalculateVacationDatesFromWorkdayGroups(vacationDays, workdayGaps);

			return vacationDates;
		}
		
		private List<DateTime> GetVacationDatesForBiggestGapFirstStrategy(List<WorkdayGap> workdayGaps, ushort vacationDays)
		{
			WorkdayGapComparer comparer = new(true);
			workdayGaps.Sort(comparer);
			
			var vacationDates = CalculateVacationDatesFromWorkdayGroups(vacationDays, workdayGaps);

			return vacationDates;
		}

		private static List<DateTime> CalculateVacationDatesFromWorkdayGroups(ushort vacationDays, List<WorkdayGap> workdayGaps)
		{
			IEnumerable<IGrouping<ushort, WorkdayGap>> workdayGapGroups = workdayGaps.GroupBy(wg => wg.Size);

			List<DateTime> vacationDates = new();
			foreach (IGrouping<ushort, WorkdayGap> gapGroup in workdayGapGroups)
			{
				foreach (WorkdayGap gap in gapGroup)
				{
					// TODO Offer to choose a group
					for (DateTime date = gap.Start; date <= gap.End; date = date.AddDays(1))
					{
						if (vacationDays > 0)
						{
							vacationDates.Add(date);
						}

						vacationDays--;

						if (vacationDays == 0)
						{
							return vacationDates;
						}
					}
				}
			}

			return vacationDates;
		}
	}
}
