using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimetric_RPC
{
    internal class UConnect
    {
        public List<int> Schedule { get; }

        public double DutyCyclePerc { get; }


        /* U-Connect 
         * 1, if:  t mod p = 0 or 0 <= t mod p^2 < (p+1)/2
         * 0, otherwise
         */
        public UConnect(int p)
        {
            int activeSlots = 0;
            Schedule = new List<int>();

            for (int i = 0; i < p*p; i++)
            {
                int v = i % (p * p);
                if (i % p == 0 || (0 <= v && v < (p + 1) / 2.0))
                {
                    Schedule.Add(1);
                    activeSlots++;
                }
                else
                {
                    Schedule.Add(0);
                }
            }

            DutyCyclePerc = 100.0 * activeSlots / (p * p);
        }
    }
}
