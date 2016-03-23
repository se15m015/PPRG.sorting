using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] numbers = { 3, 8, 7, 5, 2, 1, 9, 6, 4 };
            var numbers = Helper.RandomNumbers(1000);
            QuickSort.QuickSort_Recursive(numbers, 0, numbers.Length - 1);

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
