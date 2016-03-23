using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class MergeSort_Parallel : AMergeSort
    {
        public static async Task<int[]> MergeSort_Recursive(int[] numbers)
        {
            if (numbers.Length <= 1)
            {
                return numbers;
            }
            int lengthLeft = numbers.Length/2;
            int lengthRight = numbers.Length - lengthLeft;

            int[] leftResult = await Task<int[]>.Run(async () =>
            {
                int[] left = new int[lengthLeft];
                Array.Copy(numbers, left, lengthLeft);
                return await MergeSort_Recursive(left);
            });

            int[] rightResult = await Task<int[]>.Run(async () =>
            {
                int[] right = new int[lengthRight];
                Array.Copy(numbers, lengthLeft, right, 0, lengthRight);
                return await MergeSort_Recursive(right);
            });

            return Merge(leftResult, rightResult);
        }
    }
}
