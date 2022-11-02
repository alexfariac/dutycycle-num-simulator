namespace Assimetric_RPC.Methods
{
    public class Grid : ScheduleMethod
    {
        public override string Name { get; }

        public override List<bool> Schedule { get; }

        public override double DutyCyclePerc { get; }

        public override int ScheduleSize { get; }

        public override int ActiveSlotsCount { get; }

        public Grid(int n, int m)
        {
            Name = nameof(Grid);
            Schedule = new ();
            ScheduleSize = n * m;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        Schedule.Add(true);
                        ActiveSlotsCount++;
                    }
                    else
                    {
                        Schedule.Add(false);
                    }

                }
            }

            DutyCyclePerc = 100.0 * ActiveSlotsCount / ScheduleSize;
        }
    }
}
