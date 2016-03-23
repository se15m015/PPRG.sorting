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
            QuickSort_Run(numbers, 5, 10, 15);
            Console.ReadLine();
            MergeSort_Run(numbers, 5, 10, 15);

            Console.ReadLine();
        }

        private static void QuickSort_Run(int[] numbers, int th1, int th2, int th3)
        { 
            int arraySize = numbers.Length;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var numbers_serial = new int[arraySize];
            var numbers_parallel = new int[arraySize];
            var numbers_parallel_TH1 = new int[arraySize];
            var numbers_parallel_TH2 = new int[arraySize];
            var numbers_parallel_TH3 = new int[arraySize];

            Array.Copy(numbers, numbers_serial, arraySize);
            Array.Copy(numbers, numbers_parallel, arraySize);
            Array.Copy(numbers, numbers_parallel_TH1, arraySize);
            Array.Copy(numbers, numbers_parallel_TH2, arraySize);
            Array.Copy(numbers, numbers_parallel_TH3, arraySize);

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
                    await QuickSort_Parallel_TH.QuickSort_Recursive(numbers_parallel_TH1, 0, numbers_parallel_TH1.Length - 1, th1);
                })
                .Wait();
            sw.Stop();
            var parallelTH1TimeTaken = sw.ElapsedMilliseconds;

            sw.Restart();
            Task.Run(
                async () =>
                {
                    await QuickSort_Parallel_TH.QuickSort_Recursive(numbers_parallel_TH2, 0, numbers_parallel_TH2.Length - 1, th2);
                })
                .Wait();
            sw.Stop();
            var parallelTH2TimeTaken = sw.ElapsedMilliseconds;
            
            sw.Restart();
            Task.Run(
                async () =>
                {
                    await QuickSort_Parallel_TH.QuickSort_Recursive(numbers_parallel_TH3, 0, numbers_parallel_TH3.Length - 1, th3);
                })
                .Wait();
            sw.Stop();
            var parallelTH3TimeTaken = sw.ElapsedMilliseconds;

            Console.WriteLine("QuickSort: Serial time taken: {0}, Parallel time taken: {1}, Parallel_TH-{2}: time taken: {3},Parallel_TH-{4}: time taken: {5}, Parallel_TH-{6}: time taken: {7}", serialTimeTaken,
                parallelTimeTaken, th1, parallelTH1TimeTaken, th2, parallelTH2TimeTaken, th3, parallelTH3TimeTaken);
        }

        private static void MergeSort_Run(int[] numbers, int th1, int th2, int th3)
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
            var result_parallel_TH1 = MergeSort_Parallel_TH.MergeSort_Recursive(numbers, th1);
            sw.Stop();
            var parallelTH1TimeTaken = sw.ElapsedMilliseconds;

            sw.Restart();
            var result_parallel_TH2 = MergeSort_Parallel_TH.MergeSort_Recursive(numbers, th2);
            sw.Stop();
            var parallelTH2TimeTaken = sw.ElapsedMilliseconds;

            sw.Restart();
            var result_parallel_TH3 = MergeSort_Parallel_TH.MergeSort_Recursive(numbers, th3);
            sw.Stop();
            var parallelTH3TimeTaken = sw.ElapsedMilliseconds;

            Console.WriteLine("MergeSort: Serial time taken: {0}, Parallel time taken: {1}, Parallel_TH-{2}: time taken: {3},Parallel_TH-{4}: time taken: {5}, Parallel_TH-{6}: time taken: {7}", serialTimeTaken,
                parallelTimeTaken, th1, parallelTH1TimeTaken, th2, parallelTH2TimeTaken, th3, parallelTH3TimeTaken);
        }
    }
}
