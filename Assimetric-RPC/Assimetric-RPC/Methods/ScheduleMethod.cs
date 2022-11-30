namespace Assimetric_RPC.Methods
{
    public abstract class ScheduleMethod
    {
        public abstract string Name { get; }

        public abstract List<bool> Schedule { get; }

        public abstract double DutyCyclePerc { get; }

        public abstract int ScheduleSize { get; }

        public abstract int ActiveSlotsCount { get; }

        public abstract List<int> ActiveSlots { get; }

        public abstract List<int> NextActiveSlots { get;  }

        public override string ToString()
        {
            return $"{Name}: {DutyCyclePerc}";
        }

        public int FindNextGreatestActiveSlot(int slotIndex)
        {
            return ActiveSlots.Where(item => item > slotIndex).FirstOrDefault(ActiveSlots.First());
        }
    }
}
