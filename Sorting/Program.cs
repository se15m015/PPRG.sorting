using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            int arraySize = 200000;
            int[] numbers = Helper.RandomNumbers(arraySize);

            string path = "numbers.store";
            if (!File.Exists(path))
            {
                WriteNumbersToFile(numbers, path);
            }
            numbers = LoadNumbersFromFile(path);

            arraySize = numbers.Length;


            int th1 = 20000, th2 = 24000, th3 = 28000;

            QuickSort_Run(numbers, th1, th2, th3);
            //Console.ReadLine();
            Console.WriteLine();
            //MergeSort_Run(numbers, 5, 10, 15);
            MergeSort_Run(numbers, th1, th2, th3);

            Console.Beep();
            Console.ReadLine();
        }

        private static int[] LoadNumbersFromFile(string path)
        {
            int[] numbers;
            FileStream fs = File.OpenRead(path);
            StreamReader sr = new StreamReader(fs);
            List<int> numbersReader = new List<int>();
            while (sr.Peek() != -1)
            {
                numbersReader.Add(Int32.Parse(sr.ReadLine()));
            }
            sr.Close();
            fs.Close();
            numbers = numbersReader.ToArray();
            return numbers;
        }

        private static void WriteNumbersToFile(int[] numbers,string path)
        {
            FileStream fs = File.OpenWrite(path);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var number in numbers)
            {
                sw.WriteLine(number);
            }

            sw.Flush();
            sw.Close();
            fs.Close();
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

            Task.WaitAll(tasks[0]);

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

            Task.WaitAll(tasks[1]);

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

            Task.WaitAll(tasks[2]);

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


            Task.WaitAll(tasks[3]);

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

            Task.WaitAll(tasks[0]);

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

            Task.WaitAll(tasks[1]);

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


            Task.WaitAll(tasks[2]);

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

            Task.WaitAll(tasks[3]);

            Console.WriteLine("MergeSort:\n\t Serial time taken: {0},\n\t Parallel time taken: {1},\n\t Parallel_TH-{2}: time taken: {3},\n\t Parallel_TH-{4}: time taken: {5},\n\t Parallel_TH-{6}: time taken: {7}", serialTimeTaken,
                parallelTimeTaken, th1, parallelTH1TimeTaken, th2, parallelTH2TimeTaken, th3, parallelTH3TimeTaken);
        }
    }
}
