namespace Assimetric_RPC.Utils
{
    public static class MathUtils
    {
        public static int gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static int Lcm(int a, int b)
        {
            return a / gcf(a, b) * b;
        }

    }
}
