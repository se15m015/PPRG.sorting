using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//reference:
//http://me.dt.in.th/page/Quicksort/

namespace Sorting
{
    public class QuickSort_Serial
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

        public static void QuickSort_Recursive(int[] numbers, int left, int right)
        {
            if ((right - left)  < 1)
            {
                return;
            }

            int pivot = Partition(numbers, left, right);
            QuickSort_Recursive(numbers, left, pivot - 1);
            QuickSort_Recursive(numbers, pivot+1, right);
        }
    }
}
