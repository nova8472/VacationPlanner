using FluentAssertions;
using VacationPlanner.Core;
using Xunit;

namespace VacationPlanner.Test
{
	public class PlannerTest
	{
		public static IEnumerable<object[]> TestData()
		{
			//
			// FeesVersion=1
			// 
			yield return new object[]
			{
				PlanningStrategy.SmallestGapFirst, 1, GetCalendarScenario1(), GetExpectedResultScenario1()
			};
		}

		private static object GetExpectedResultScenario1()
		{
			throw new NotImplementedException();
		}

		private static object GetCalendarScenario1()
		{
			Dictionary<DateTime, bool> calendar = new()
			{
				{ new DateTime(2020, 1, 1), false }, 
				// TODO Add test scenarios
			};

			return calendar;
		}

		[Theory]
		[MemberData(nameof(TestData))]
		public void Plan_WhenCalled_ReturnsCorrectPlan(
			PlanningStrategy planningStrategy,
			ushort vacationDays,
			Dictionary<DateTime, bool> calendar,
			List<DateTime> expectedResult)
		{
			// ARRANGE
			Planner planner = new(planningStrategy, vacationDays);

			// ACT
			List<DateTime> vacationDates = planner.Plan(calendar);

			// ASSERT
			vacationDates.Should().BeEquivalentTo(expectedResult);
		}
	}
}
