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

        //function to check if a given number is prime
        public static bool isPrime(int n)
        {
            //since 0 and 1 is not prime return false.
            if (n == 1 || n == 0) return false;

            //Run a loop from 2 to square root of n.
            for (int i = 2; i * i <= n; i++)
            {
                // if the number is divisible by i, then n is not a prime number.
                if (n % i == 0) return false;
            }
            //otherwise, n is prime number.
            return true;
        }

        public static List<int> getPrimes(int n)
        {
            List<int> primes = new ();
            
            int prime = 0;

            for (int i = 0; i < n; i++)
            {
                while (!isPrime(prime))
                {
                    prime++;
                }
                primes.Add(prime);
                prime++;
            }

            return primes;
        }

    }
}
