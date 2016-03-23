using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//reference:
//http://me.dt.in.th/page/Quicksort/

namespace Sorting
{
    public class QuickSort_Serial : AQuickSort
    {
        public static void QuickSort_Recursive(int[] numbers, int left, int right)
        {
            if ((right - left)  < 1)
            {
               // Console.WriteLine("Serial Thread count: {0}",Process.GetCurrentProcess().Threads.Count);
                return;
            }

            int pivot = Partition(numbers, left, right);
            QuickSort_Recursive(numbers, left, pivot - 1);
            QuickSort_Recursive(numbers, pivot+1, right);
        }
    }
}
