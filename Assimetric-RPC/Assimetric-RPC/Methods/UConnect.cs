namespace Assimetric_RPC.Methods
{
    internal class UConnect : ScheduleMethod
    {
        /* U-Connect 
         * 1, if:  t mod p = 0 or 0 <= t mod p^2 < (p+1)/2
         * 0, otherwise
         */
        public override string Name { get; }

        public override List<bool> Schedule { get; }

        public override double DutyCyclePerc { get; }

        public override int ScheduleSize { get; }

        public override int ActiveSlotsCount { get; }

        public override List<int> ActiveSlots { get; }

        public UConnect(int p)
        {
            Name = $"{nameof(UConnect)}({p})";
            Schedule = new ();
            ActiveSlots = new ();
            ScheduleSize = p * p;

            for (int i = 0; i < ScheduleSize; i++)
            {
                int v = i % (ScheduleSize);
                if (i % p == 0 || 0 <= v && v < (p + 1) / 2.0)
                {
                    Schedule.Add(true);
                    ActiveSlots.Add(i);
                    ActiveSlotsCount++;
                }
                else
                {
                    Schedule.Add(false);
                }
            }

            DutyCyclePerc = 100.0 * ActiveSlotsCount / (ScheduleSize);
        }
    }
}
