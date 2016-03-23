using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class Helper
    {
        public static int[] RandomNumbers(int length)
        {
            int[] rng = new int[length];
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                rng[i] = rand.Next(0,9999);
            }
            return rng;
        }
    }
}
