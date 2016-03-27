using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//reference:
//http://me.dt.in.th/page/Quicksort/

namespace Sorting
{
    public class QuickSort_Parallel : AQuickSort
    {
        public static async Task QuickSort_Recursive(int[] numbers, int left, int right)
        {
            if ((right - left)  < 1)
            {
               // Console.WriteLine("Parallel Thread count: {0}", Process.GetCurrentProcess().Threads.Count);
                return;
            }

            int pivot = Partition(numbers, left, right);
            await Task.Run(async () => { 
                await QuickSort_Recursive(numbers, left, pivot - 1);
            });

            await QuickSort_Recursive(numbers, pivot + 1, right);
        }
    }
}
