namespace Assimetric_RPC.Methods
{
    public class Grid : ScheduleMethod
    {
        public override string Name { get; }

        public override List<bool> Schedule { get; }

        public override double DutyCyclePerc { get; }

        public override int ScheduleSize { get; }

        public override int ActiveSlotsCount { get; }

        public override List<int> ActiveSlots { get; }

        public override List<int> NextActiveSlots { get; }

        public Grid(int n, int m)
        {
            Name = $"{nameof(Grid)}({n},{m})";
            Schedule = new ();
            ActiveSlots = new ();
            ScheduleSize = n * m;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        Schedule.Add(true);
                        ActiveSlots.Add(i * n + j);
                        ActiveSlotsCount++;
                    }
                    else
                    {
                        Schedule.Add(false);
                    }

                }
            }

            NextActiveSlots = new();
            for (int i = 0; i < ScheduleSize; i++)
            {
                int nextActiveSlot = FindNextGreatestActiveSlot(i);
                int slotsToNext = nextActiveSlot - i < 0 ? ScheduleSize + nextActiveSlot - i : nextActiveSlot - i;
                NextActiveSlots.Add(slotsToNext);
            }

            DutyCyclePerc = 100.0 * ActiveSlotsCount / ScheduleSize;
        }
    }
}
