using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            //int[] numbers = {3,1,4,1,345,509,90,3,4,675,2,12,6,5,3};
            sw.Start();
            var numbers = Helper.RandomNumbers(1000);
            sw.Stop();
            Console.WriteLine("Randomizer done lenght: {0}, time taken: {1}", numbers.Length, sw.ElapsedMilliseconds);
            sw.Restart();
            QuickSort_Serial.QuickSort_Recursive(numbers, 0, numbers.Length - 1);

            List<int> checkList = new List<int>();
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
                if (checkList.Any(x => i < x))
                {
                    Console.WriteLine("Booooooeese");
                }
                checkList.Add(i);
            }
            sw.Stop();
            Console.WriteLine("Sorting time taken: {0}", sw.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
