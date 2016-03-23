using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class MergeSort_Serial : AMergeSort
    {
        public static int[] MergeSort_Recursive(int[] numbers)
        {
            if (numbers.Length <= 1)
            {
                return numbers;
            }
                int lengthLeft = numbers.Length/2;
                int lengthRight = numbers.Length - lengthLeft;
                int[] left = new int[lengthLeft];
                int[] right = new int[lengthRight];

                Array.Copy(numbers, left, lengthLeft);
                left = MergeSort_Recursive(left);

                Array.Copy(numbers, lengthLeft, right, 0, lengthRight);
                right = MergeSort_Recursive(right);

                return Merge(left, right);
        }
    }
}
