namespace Assimetric_RPC.Methods
{
    public class Searchlight : ScheduleMethod
    {
        public override string Name { get; }

        public override List<bool> Schedule { get; }

        public override double DutyCyclePerc { get; }

        public override int ScheduleSize { get; }

        public override int ActiveSlotsCount { get; }

        public override List<int> ActiveSlots { get; }

        public override List<int> NextActiveSlots { get; }

        public Searchlight(int t)
        {
            Name = $"{nameof(Searchlight)}({t})";
            Schedule = new ();
            ActiveSlots = new ();
            ActiveSlotsCount = t;
            ScheduleSize = t * (t / 2);


            for (int i = 0; i < t / 2; i++)
            {
                for (int j = 0; j < t; j++)
                {
                    bool active = (j == 0 || j == i + 1);
                    Schedule.Add(active);
                    if(active) ActiveSlots.Add(i*t+j);
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
