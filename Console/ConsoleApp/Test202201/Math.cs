using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test202201
{
    public class MathExtension:IMath
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Sub(double x, double y)
        {
            return x - y;
        }

        public double Mul(double x, double y)
        {
            return x * y;
        }

        public double Dev(double x, double y)
        {
            return x / y;
        }
    }

    public class MathExtension2 : IMath
    {
        public double Add(double x, double y)
        {
            return x + y + 10;
        }

        public double Sub(double x, double y)
        {
            return x - y;
        }

        public double Mul(double x, double y)
        {
            return x * y;
        }

        public double Dev(double x, double y)
        {
            return x / y;
        }
    }
}
