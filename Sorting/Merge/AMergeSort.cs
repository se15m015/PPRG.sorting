using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public abstract class AMergeSort
    {
        public static int[] Merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];
            int l =0, r = 0;

            while (true)
            {
                if (left[l] < right[r])
                {
                    result[l + r] = left[l];
                    l++;
                    if (l == left.Length)
                    {
                       Array.Copy(right, r, result, l+r, result.Length-(l+r));
                       break;
                    }
                }
                else
                {
                    result[l + r] = right[r];
                    r++;
                    if (r == right.Length)
                    {
                        Array.Copy(left, l, result, l + r, result.Length - (l + r));
                        break;
                    }

                }
            }
            return result;
        }
    }
}
