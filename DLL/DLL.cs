using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class DLL
    {
        public static double SaleCost(int countBook,double Cost)
        {
            double Sale = 0;


            if (countBook >= 3)
            {
                Sale = 0.05;
            }
            if (countBook >= 5)
            {
                Sale = 0.10;
            }
            if (countBook >= 10)
            {
                Sale = 0.15;
            }
            if (Cost / 500 >= 1)
            {
                double a= Convert.ToDouble(Math.Floor((Cost / 500)) / 100);
                Sale = Sale + a;
            }
            return Sale;
        }
    }
}
