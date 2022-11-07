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

            int lcm = MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize);

            for (int relativeOffset = 0; relativeOffset < lcm; relativeOffset++)
            {
                int discoveryTime = DiscoveryTime(schedule1, schedule2, relativeOffset, lcm);
                if(discoveryTime == -1)
                {
                    Console.WriteLine($"S1: {schedule1.Name}, S2: {schedule2.Name} DC1: {schedule1.DutyCyclePerc}, DC2: {schedule2.DutyCyclePerc}, MIN-NDT: inf, AVG-NDT: inf, MAX-NDT: inf");
                    return;
                }

                discoveryTimeSum += discoveryTime;
                maxDiscoveryTime = Math.Max(maxDiscoveryTime, discoveryTime);
                minDiscoveryTime = Math.Min(minDiscoveryTime, discoveryTime);
            }

            var averageNdt = discoveryTimeSum / lcm;
            Console.WriteLine($"S1: {schedule1.Name}, S2: {schedule2.Name} DC1: {schedule1.DutyCyclePerc}, DC2: {schedule2.DutyCyclePerc}, MIN-NDT: {minDiscoveryTime}, AVG-NDT: {averageNdt}, MAX-NDT: {maxDiscoveryTime}");
        }

        public static int DiscoveryTime(ScheduleMethod schedule1, ScheduleMethod schedule2, int relativeOffset = 0, int lcm = -1)
        {
            int schedule_lcm = lcm == -1 ? MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize) : lcm;
            for (int i = 0; i < schedule_lcm; i++)
            {
                int index1 = i % schedule1.ScheduleSize;
                int index2 = (i + relativeOffset) % schedule2.ScheduleSize;

                if(schedule1.Schedule[index1] && schedule2.Schedule[index2])
                {
                    return i;
                }
            }

            return -1;
        }

    }
}
