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
    public abstract class AQuickSort
    {
        public static int Partition(int[] numbers, int first, int last)
        {
            int pivot = numbers[first];
            int indexFirstOpen = first+1;
            int indexLastClosed = first;

            for (int i = first+1; i <= last; i++)
            {
                if(numbers[i] < pivot)
                {
                    Swap(numbers, indexFirstOpen, i);
                    indexLastClosed = indexFirstOpen;
                    indexFirstOpen++;
                }
            }

            Swap(numbers, first, indexLastClosed);

            return indexLastClosed;
        }

        private static void Swap(int[] numbers, int indexFirstOpen, int i)
        {
            int tmp = numbers[indexFirstOpen];
            numbers[indexFirstOpen] = numbers[i];
            numbers[i] = tmp;
        }
    }
}
