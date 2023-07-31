namespace VacationPlanner.Core
{
    /// <summary>
    /// A class representing an interval during which one must work.
    /// </summary>
    internal class WorkdayGap
    {
        public WorkdayGap(DateTime start, DateTime end, ushort size)
        {
            Start = start;
            End = end;
            Size = size;
        }

        internal DateTime Start { get; set; }
        internal DateTime End { get; set; }
        internal ushort Size { get; set; }
    }

    internal class WorkdayGapComparer : IComparer<WorkdayGap>
    {
        private readonly bool _isDescending;
        public WorkdayGapComparer()
        {
            _isDescending = false;
        }

        public WorkdayGapComparer(bool isDescending)
        {
            _isDescending = isDescending;
        }

        public int Compare(WorkdayGap? x, WorkdayGap? y)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y == null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            if (_isDescending)
            {
                return y.Size.CompareTo(x.Size);
            }
            else
            {
                return x.Size.CompareTo(y.Size);
            }
        }
    }
}
