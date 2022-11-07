using Assimetric_RPC.Methods;
using Assimetric_RPC.Utils;

namespace Assimetric_RPC
{
    public static class HeterogenousRPC
    {
        public static void NDTMetrics(ScheduleMethod schedule1, ScheduleMethod schedule2)
        {
            int discoveryTimeSum = 0;
            int maxDiscoveryTime = 0;
            int minDiscoveryTime = int.MaxValue;
            int iterations = 0;

            int lcm = MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize);

            for(int startIndex1 = 0; startIndex1 < lcm; startIndex1++)
            {
                for(int startIndex2 = startIndex1; startIndex2 < lcm; startIndex2++)
                {
                    int discoveryTime = DiscoveryTime(schedule1, schedule2, startIndex1, startIndex2, lcm);
                    if (discoveryTime == -1)
                    {
                        Console.WriteLine($"S1: {schedule1.Name}, S2: {schedule2.Name} DC1: {schedule1.DutyCyclePerc}, DC2: {schedule2.DutyCyclePerc}, MIN-NDT: inf, AVG-NDT: inf, MAX-NDT: inf");
                        return;
                    }

                    discoveryTimeSum += discoveryTime;
                    maxDiscoveryTime = Math.Max(maxDiscoveryTime, discoveryTime);
                    minDiscoveryTime = Math.Min(minDiscoveryTime, discoveryTime);
                    iterations++;
                }
            }

            var averageNdt = discoveryTimeSum / iterations;
            Console.WriteLine($"S1: {schedule1.Name}, S2: {schedule2.Name} DC1: {schedule1.DutyCyclePerc}, DC2: {schedule2.DutyCyclePerc}, MIN-NDT: {minDiscoveryTime}, AVG-NDT: {averageNdt}, MAX-NDT: {maxDiscoveryTime}");
        }

        public static int DiscoveryTime(ScheduleMethod schedule1, ScheduleMethod schedule2, int startIndex1 = 0, int startIndex2 = 0, int lcm = -1)
        {
            int schedule_lcm = lcm == -1 ? MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize) : lcm;
            for (int i = 0; i < schedule_lcm; i++)
            {
                int index1 = (i + startIndex1) % schedule1.ScheduleSize;
                int index2 = (i + startIndex2) % schedule2.ScheduleSize;

                if (schedule1.Schedule[index1] && schedule2.Schedule[index2])
                {
                    return i;
                }
            }

            return -1;
        }

        public static List<int> CommonActiveSlots(ScheduleMethod s1, ScheduleMethod s2, int s1InitialSlot = 0, int s2InitialSlot = 0, int lcm = -1)
        {
            int schedule_lcm = lcm == -1 ? MathUtils.Lcm(s1.ScheduleSize, s2.ScheduleSize) : lcm;

            List<int> intersectionSlots = new List<int>();

            for(int i = 0; i < schedule_lcm; i++)
            {
                int index1 = (i + s1InitialSlot) % s1.ScheduleSize;
                int index2 = (i + s2InitialSlot) % s2.ScheduleSize;

                if(s1.Schedule[index1] && s2.Schedule[index2])
                {
                    intersectionSlots.Add(1);
                }
                else
                {
                    intersectionSlots.Add(0);
                }
            }

            return intersectionSlots;
        }

    }
}
