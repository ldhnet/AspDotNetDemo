using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test202201
{
    public class MathProxy : IMath
    {
        private readonly IMath math;
        public MathProxy(IMath _math)
        {
            math=_math;
        }
        //Math math=new Math();
        public double Add(double x, double y)
        {
            return math.Add(x, y);
        }

        public double Dev(double x, double y)
        {
            return math.Sub(x, y);
        }

        public double Mul(double x, double y)
        {
            return math.Mul(x, y);
        }

        public double Sub(double x, double y)
        {
            return math.Dev(x, y);
        }
    }
}
