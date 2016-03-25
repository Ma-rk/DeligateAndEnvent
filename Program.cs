using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeligateAndEnvent
{
    class PrimeCallbackArg : EventArgs
    {
        public int Prime;
        public PrimeCallbackArg(int prime)
        {
            this.Prime = prime;
        }
    }

    class PrimeGenerator
    {
        public delegate void PrimDelegate(object sender, EventArgs arg);

        //PrimDelegate callbacks;

        //public void AddDelegate(PrimDelegate callback)
        //{
        //    callbacks = Delegate.Combine(callbacks, callback) as PrimDelegate;
        //}

        //public void RemoveDelegate(PrimDelegate callback)
        //{
        //    callbacks = Delegate.Remove(callbacks, callback) as PrimDelegate;
        //}

        public event EventHandler PrimeGenerated;

        public void Run(int limit)
        {
            for (int i = 2; i < limit; i++)
                if (IsPrime(i) == true && PrimeGenerated != null)
                    PrimeGenerated(this, new PrimeCallbackArg(i));
        }

        private bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0)
                return candidate == 2;
            for (int i = 3; (i * i) <= candidate; i += 2)
                if ((candidate % i) == 0)
                    return false;
            return candidate != 1; ;
        }
    }

    class Program
    {
        static void PrintPrime(object sender, EventArgs arg)
        {
            Console.Write((arg as PrimeCallbackArg).Prime + ", ");
        }

        static int Sum;
        static void SumPrime(object sender, EventArgs arg)
        {
            Sum += (arg as PrimeCallbackArg).Prime;
        }

        static void Main(string[] args)
        {
            PrimeGenerator gen = new PrimeGenerator();

            //PrimeGenerator.PrimDelegate callprint = PrintPrime;
            //gen.AddDelegate(callprint);
            gen.PrimeGenerated += PrintPrime;

            //PrimeGenerator.PrimDelegate callsum = SumPrime;
            //gen.AddDelegate(callsum);
            gen.PrimeGenerated += SumPrime;

            gen.Run(10);
            Console.WriteLine();
            Console.WriteLine(Sum);

            Sum = 0;
            gen.PrimeGenerated -= SumPrime;
            gen.Run(15);
            Console.WriteLine();
            Console.WriteLine(Sum);
        }
    }
}
