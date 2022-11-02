namespace Assimetric_RPC.Methods
{
    public abstract class ScheduleMethod
    {
        public abstract string Name { get; }

        public abstract List<bool> Schedule { get; }

        public abstract double DutyCyclePerc { get; }

        public abstract int ScheduleSize { get; }

        public abstract int ActiveSlotsCount { get; }
    }
}
