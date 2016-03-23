using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            int arraySize = 3000000;

            //int[] numbers = {3,1,4,1,345,509,90,3,4,675,2,12,6,5,3};
           var numbers = Helper.RandomNumbers(arraySize);
            MergeSort_Run(numbers);
            //QuickSort_Run(arraySize, numbers);

            Console.ReadLine();
        }

        private static void MergeSort_Run(int[] numbers)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            var result_serial = MergeSort_Serial.MergeSort_Recursive(numbers);
            sw.Stop();
            var serialTimeTaken = sw.ElapsedMilliseconds;

            sw.Restart();
            var result_parallel = MergeSort_Parallel.MergeSort_Recursive(numbers);
            sw.Stop();
            var parallelTimeTaken = sw.ElapsedMilliseconds;

            sw.Restart();
            var result_parallel_TH = MergeSort_Parallel_TH.MergeSort_Recursive(numbers, 10);
            sw.Stop();
            var parallelTHTimeTaken = sw.ElapsedMilliseconds;

            Console.WriteLine("MergeSort: Serial time taken: {0}, Parallel time taken: {1}, Parallel_TH time taken: {2}", serialTimeTaken,
                parallelTimeTaken, parallelTHTimeTaken);
        }

        private static void QuickSort_Run(int arraySize, int[] numbers)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var numbers_serial = new int[arraySize];
            var numbers_parallel = new int[arraySize];
            var numbers_parallel_TH = new int[arraySize];

            Array.Copy(numbers, numbers_serial, arraySize);
            Array.Copy(numbers, numbers_parallel, arraySize);
            Array.Copy(numbers, numbers_parallel_TH, arraySize);

            sw.Stop();
            Console.WriteLine("Randomizer done lenght: {0}, time taken: {1}", numbers.Length, sw.ElapsedMilliseconds);

            sw.Restart();
            QuickSort_Serial.QuickSort_Recursive(numbers_serial, 0, numbers_serial.Length - 1);
            sw.Stop();
            var serialTimeTaken = sw.ElapsedMilliseconds;

            sw.Restart();
            Task.Run(
                async () => { await QuickSort_Parallel.QuickSort_Recursive(numbers_parallel, 0, numbers_parallel.Length - 1); })
                .Wait();
            sw.Stop();
            var parallelTimeTaken = sw.ElapsedMilliseconds;

            sw.Restart();
            Task.Run(
                async () =>
                {
                    await QuickSort_Parallel_TH.QuickSort_Recursive(numbers_parallel_TH, 0, numbers_parallel_TH.Length - 1, 1);
                })
                .Wait();
            sw.Stop();
            var parallelTHTimeTaken = sw.ElapsedMilliseconds;

            Console.WriteLine("Quicksort: Serial time taken: {0}, Parallel time taken: {1}, Parallel_TH time taken: {2}", serialTimeTaken,
                parallelTimeTaken, parallelTHTimeTaken);
        }
    }
}
