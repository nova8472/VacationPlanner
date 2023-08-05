namespace VacationPlanner.Test
{
	public class PlannerTest
	{
		public static IEnumerable<object[]> TestData()
		{
			yield return new object[]
			{
				PlanningStrategy.SmallestGapFirst, 1, GetCalendar1(), GetExpectedResultSmallestGapFirstOneDayCalendar1()
			};

			yield return new object[]
			{
				PlanningStrategy.SmallestGapFirst, 3, GetCalendar1(), GetExpectedResultSmallestGapFirstThreeDaysCalendar1()
			};

			yield return new object[]
			{
				PlanningStrategy.SmallestGapFirst, 5, GetCalendar1(), GetExpectedResultSmallestGapFirstThreeDaysCalendar1()
			};
		}

		#region Expected results
		private static object GetExpectedResultSmallestGapFirstOneDayCalendar1()
		{
			List<DateTime> result = new()
			{
				new DateTime(2020, 1, 5)
			};

			return result;
		}

		private static object GetExpectedResultSmallestGapFirstThreeDaysCalendar1()
		{
			List<DateTime> result = new()
			{
				{ new DateTime(2020, 1, 2) },
				{ new DateTime(2020, 1, 3) },
				{ new DateTime(2020, 1, 5) },
			};

			return result;
		}

		private static object GetExpectedResultSmallestGapFirstFiveDaysCalendar1()
		{
			List<DateTime> result = new()
			{
				{ new DateTime(2020, 1, 2) },
				{ new DateTime(2020, 1, 3) },
				{ new DateTime(2020, 1, 5) },
			};

			return result;
		}


		#endregion Expected results

		#region Calendars
		private static object GetCalendar1()
		{
			Dictionary<DateTime, bool> calendar = new()
			{
				{ new DateTime(2020, 1, 1), true },
				{ new DateTime(2020, 1, 2), false },
				{ new DateTime(2020, 1, 3), false },
				{ new DateTime(2020, 1, 4), true },
				{ new DateTime(2020, 1, 5), false },
			};

			return calendar;
		}
		#endregion Calendars

		[Theory]
		[MemberData(nameof(TestData))]
		public void Plan_WhenCalled_ReturnsCorrectPlan(
			PlanningStrategy planningStrategy,
			ushort vacationDays,
			Dictionary<DateTime, bool> calendar,
			List<DateTime> expectedResult)
		{
			// Arrange
			Planner planner = new(planningStrategy, vacationDays);

			// Act
			List<DateTime> vacationDates = planner.Plan(calendar);

			// Assert
			vacationDates.Should().BeEquivalentTo(expectedResult);
		}
	}
}
