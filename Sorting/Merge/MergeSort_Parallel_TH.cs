using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class MergeSort_Parallel_TH : AMergeSort
    {
        public static async Task<int[]> MergeSort_Recursive(int[] numbers, int threshold)
        {
            if (numbers.Length <= 1)
            {
                return numbers;
            }
            int lengthLeft = numbers.Length/2;
            int lengthRight = numbers.Length - lengthLeft;
            int[] leftResult;
            int[] rightResult;

            if (lengthLeft < threshold)
            {
                int[] left = new int[lengthLeft];
                Array.Copy(numbers, left, lengthLeft);
                leftResult = MergeSort_Serial.MergeSort_Recursive(left);
            }
            else
            {
                leftResult = await Task<int[]>.Run(async () =>
                    {
                        int[] left = new int[lengthLeft];
                        Array.Copy(numbers, left, lengthLeft);
                        return await MergeSort_Recursive(left, threshold);
                    });
            }


            if (lengthRight < threshold)
            {
                int[] right = new int[lengthRight];
                Array.Copy(numbers, lengthLeft, right, 0, lengthRight);
                rightResult = MergeSort_Serial.MergeSort_Recursive(right);
            }
            else
            {
                rightResult = await Task<int[]>.Run(async () =>
                {
                    int[] right = new int[lengthRight];
                    Array.Copy(numbers, lengthLeft, right, 0, lengthRight);
                    return await MergeSort_Recursive(right, threshold);
                });
            }

            return Merge(leftResult, rightResult);
        }
    }
}
