﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//reference:
//http://me.dt.in.th/page/Quicksort/

namespace Sorting
{
    public class QuickSort_Parallel_TH : AQuickSort
    {
        public static async Task QuickSort_Recursive(int[] numbers, int left, int right, int threshold)
        {
            int partSize = right - left;
            if (partSize < 1)
            {
                // Console.WriteLine("Parallel Thread count: {0}", Process.GetCurrentProcess().Threads.Count);
                return;
            }

            int pivot = Partition(numbers, left, right);

            if (partSize <= threshold)
            {
                Task tleft = QuickSort_Recursive(numbers, left, pivot - 1, threshold);
                Task.WaitAll(tleft);
                Task tright = QuickSort_Recursive(numbers, pivot + 1, right, threshold);
                Task.WaitAll(tright);
            }
            else
            {
                await Task.Run(async () =>
                {
                    await QuickSort_Recursive(numbers, left, pivot - 1, threshold);
                });

                await QuickSort_Recursive(numbers, pivot + 1, right, threshold);
            }

        }
    }
}
