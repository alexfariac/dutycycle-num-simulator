namespace Assimetric_RPC
{
    public class Grid
    {
        public List<int> Schedule { get; }

        public double DutyCyclePerc { get; }

        public Grid(int n, int m)
        {
            List<int> s = new ();

            int activeSlots = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        s.Add(1);
                        activeSlots++;
                    }
                    else
                    {
                        s.Add(0);
                    }
                    
                }
            }

            Schedule = s;
            DutyCyclePerc = 100 * (double)activeSlots/(n*m);
        }
    }
}
