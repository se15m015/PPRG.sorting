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
            //int arraySize = 3000000;
            int arraySize = 150000;

            //int[] numbers = {3,1,4,1,345,509,90,3,4,675,2,12,6,5,3};
            var numbers = Helper.RandomNumbers(arraySize);
            //QuickSort_Run(numbers, 5, 10, 15);
            //Console.ReadLine();
            //MergeSort_Run(numbers, 5, 10, 15);
            MergeSort_Run(numbers, 1000, 3000, 6000);

            Console.Beep();
            Console.ReadLine();
        }

        private static void QuickSort_Run(int[] numbers, int th1, int th2, int th3)
        { 
            int arraySize = numbers.Length;
            Stopwatch sw_serial = new Stopwatch();
            sw_serial.Start();
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

            sw_serial.Stop();
            Console.WriteLine("Randomizer done lenght: {0}, time taken: {1}", numbers.Length, sw_serial.ElapsedMilliseconds);

            sw_serial.Restart();
            QuickSort_Serial.QuickSort_Recursive(numbers_serial, 0, numbers_serial.Length - 1);
            sw_serial.Stop();
            var serialTimeTaken = sw_serial.ElapsedMilliseconds;


            Task[] tasks = new Task[4];


            Stopwatch sw_parallel= new Stopwatch();
            sw_parallel.Start();
            long parallelTimeTaken = 0;
            tasks[0] = Task.Run(
                async () => { await QuickSort_Parallel.QuickSort_Recursive(numbers_parallel, 0, numbers_parallel.Length - 1); })
                .ContinueWith(x => {
                    sw_parallel.Stop();
                    parallelTimeTaken = sw_parallel.ElapsedMilliseconds;
                });

            Stopwatch sw_th1= new Stopwatch();
            sw_th1.Start();
            long parallelTH1TimeTaken = 0;

            tasks[1] = Task.Run(
                async () =>
                {
                    await QuickSort_Parallel_TH.QuickSort_Recursive(numbers_parallel_TH1, 0, numbers_parallel_TH1.Length - 1, th1);
                })
                .ContinueWith(x =>
                {
                    sw_th1.Stop();
                    parallelTH1TimeTaken = sw_th1.ElapsedMilliseconds;
                });

            Stopwatch sw_th2 = new Stopwatch();
            sw_th2.Start();
            long parallelTH2TimeTaken = 0;

            tasks[2] = Task.Run(
                async () =>
                {
                    await QuickSort_Parallel_TH.QuickSort_Recursive(numbers_parallel_TH2, 0, numbers_parallel_TH2.Length - 1, th2);
                })
                .ContinueWith(x =>
                {
                    sw_th2.Stop();
                    parallelTH2TimeTaken = sw_th2.ElapsedMilliseconds;
                });


            Stopwatch sw_th3 = new Stopwatch();
            sw_th3.Start();
            long parallelTH3TimeTaken =0;

            tasks[3] = Task.Run(
                async () =>
                {
                    await QuickSort_Parallel_TH.QuickSort_Recursive(numbers_parallel_TH3, 0, numbers_parallel_TH3.Length - 1, th3);
                })
                .ContinueWith(x =>
                {
                    sw_th3.Stop();
                    parallelTH3TimeTaken = sw_th3.ElapsedMilliseconds;
                });


            Task.WaitAll(tasks);

            Console.WriteLine("QuickSort:\n\t Serial time taken: {0},\n\t Parallel time taken: {1},\n\t Parallel_TH-{2}: time taken: {3},\n\t Parallel_TH-{4}: time taken: {5},\n\t Parallel_TH-{6}: time taken: {7}", serialTimeTaken,
                parallelTimeTaken, th1, parallelTH1TimeTaken, th2, parallelTH2TimeTaken, th3, parallelTH3TimeTaken);
        }

        private static void MergeSort_Run(int[] numbers, int th1, int th2, int th3)
        {
            Stopwatch sw_serial = new Stopwatch();
            sw_serial.Start();
            var result_serial = MergeSort_Serial.MergeSort_Recursive(numbers);
            sw_serial.Stop();
            var serialTimeTaken = sw_serial.ElapsedMilliseconds;

            Task[] tasks = new Task[4];


            Stopwatch sw_parallel= new Stopwatch();
            sw_parallel.Start();
            int[] result_parallel;
            long parallelTimeTaken = 0;
            tasks[0] = MergeSort_Parallel.MergeSort_Recursive(numbers).ContinueWith(x =>
                {
                    result_parallel = x.Result;
                    sw_parallel.Stop();
                    parallelTimeTaken = sw_parallel.ElapsedMilliseconds;
                });


            Stopwatch sw_par_th1 = new Stopwatch();
            sw_par_th1.Start();
            int[] result_parallel_TH1;
            long parallelTH1TimeTaken = 0;
            tasks[1] = MergeSort_Parallel_TH.MergeSort_Recursive(numbers, th1).ContinueWith(x =>
                {
                    result_parallel_TH1 = x.Result;
                    sw_par_th1.Stop();
                    parallelTH1TimeTaken = sw_par_th1.ElapsedMilliseconds;
                });

            Stopwatch sw_par_th2 = new Stopwatch();
            sw_par_th2.Start();
            int[] result_parallel_TH2;
            long parallelTH2TimeTaken = 0;
            tasks[2] = MergeSort_Parallel_TH.MergeSort_Recursive(numbers, th2).ContinueWith(x =>
                {
                    result_parallel_TH2 = x.Result;
                    sw_par_th2.Stop();
                    parallelTH2TimeTaken = sw_par_th2.ElapsedMilliseconds;
                });

            Stopwatch sw_par_th3 = new Stopwatch();
            sw_par_th3.Start();
            int[] result_parallel_TH3;
            long parallelTH3TimeTaken = 0;
            tasks[3] = MergeSort_Parallel_TH.MergeSort_Recursive(numbers, th3).ContinueWith(x =>
                {
                    result_parallel_TH3 = x.Result;
                    sw_par_th3.Stop();
                    parallelTH3TimeTaken = sw_par_th3.ElapsedMilliseconds;
                });

            Task.WaitAll(tasks);

            Console.WriteLine("MergeSort:\n\t Serial time taken: {0},\n\t Parallel time taken: {1},\n\t Parallel_TH-{2}: time taken: {3},\n\t Parallel_TH-{4}: time taken: {5},\n\t Parallel_TH-{6}: time taken: {7}", serialTimeTaken,
                parallelTimeTaken, th1, parallelTH1TimeTaken, th2, parallelTH2TimeTaken, th3, parallelTH3TimeTaken);
        }
    }
}
