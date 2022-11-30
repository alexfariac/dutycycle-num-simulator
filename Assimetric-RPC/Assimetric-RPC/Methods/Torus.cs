namespace Assimetric_RPC.Methods
{
    public class Torus : ScheduleMethod
    {
        public override string Name { get; }

        public override List<bool> Schedule { get; }

        public override double DutyCyclePerc { get; }

        public override int ScheduleSize { get; }

        public override int ActiveSlotsCount { get; }

        public override List<int> ActiveSlots { get; }

        public override List<int> NextActiveSlots { get; }

        public Torus(int n, int m)
        {
            Name = $"{nameof(Torus)}({n},{m})";
            Schedule = new List<bool>();
            ActiveSlots = new();
            ScheduleSize = n * m;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    bool r = j == 0 || i == j && i < m / 2 + 1;
                    Schedule.Add(r);
                    if (r)
                    {
                        ActiveSlots.Add(i*n+j);
                        ActiveSlotsCount++;
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
