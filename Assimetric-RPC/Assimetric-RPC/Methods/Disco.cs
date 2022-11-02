namespace Assimetric_RPC.Methods
{
    public class Disco : ScheduleMethod
    {
        public override string Name { get; }

        public override List<bool> Schedule { get; }

        public override double DutyCyclePerc { get; }

        public override int ScheduleSize { get; }

        public override int ActiveSlotsCount { get; }

        public Disco(int p, int q)
        {
            Name = nameof(Disco);
            Schedule = new ();
            ScheduleSize = p * q;

            for (int i = 0; i < ScheduleSize; i++)
            {
                if (i % p == 0 || i % q == 0)
                {
                    Schedule.Add(true);
                    ActiveSlotsCount++;
                }
                else
                {
                    Schedule.Add(false);
                }
            }

            DutyCyclePerc = 100.0 * ActiveSlotsCount / ScheduleSize;
        }
    }
}
