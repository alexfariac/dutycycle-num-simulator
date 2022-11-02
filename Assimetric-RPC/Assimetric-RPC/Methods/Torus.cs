namespace Assimetric_RPC.Methods
{
    public class Torus : ScheduleMethod
    {
        public override string Name { get; }

        public override List<bool> Schedule { get; }

        public override double DutyCyclePerc { get; }

        public override int ScheduleSize { get; }

        public override int ActiveSlotsCount { get; }

        public Torus(int n, int m)
        {
            Name = nameof(Torus);
            Schedule = new List<bool>();
            ScheduleSize = n * m;

            int activeSlots = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    bool r = j == 0 || i == j && i < m / 2 + 1;
                    Schedule.Add(r);
                    ActiveSlotsCount++;
                }
            }

            ActiveSlotsCount = activeSlots;
            DutyCyclePerc = 100.0 * ActiveSlotsCount / ScheduleSize;
        }
    }
}
