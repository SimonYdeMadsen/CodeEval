using System;
using System.IO;


namespace TimeToEat
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;
                    line = line.Trim();

                    string[] lineArray = line.Split(' ');


                    //string op1 = "00:00:00";
                    //string op2 = "00:00:01";
                    //Console.WriteLine("check operator: "+ op1 +" LEQ "+ op2 + ": "+ IsLessThanOrEqual(op1, op2));
                    

                    Quicksort(lineArray, 0, lineArray.Length-1);
                    //Console.WriteLine(string.Join(", ", lineArray));

                    for (int i = lineArray.Length-1; i >= 0; i--)
                    {
                        if (i < lineArray.Length - 1) Console.Write(" ");
                        Console.Write(lineArray[i]);
                    }
                    Console.WriteLine();
                }
        }

        public static void Quicksort(string[] input, int start, int end)
        {
            if (start < end)
            {
                int pivot = Partition(input, start, end);
                Quicksort(input, start, pivot - 1);
                Quicksort(input, pivot+1, end);
            }
        }

        private static int Partition(string[] input, int start, int end)
        {
            string pivot = input[end], tempForSwap;


            int i = start;

            for (int j = start; j < end; j++) 
            {
                if (IsLessThanOrEqual( input[j], pivot))
                {
                    tempForSwap = input[i];
                    input[i] = input[j];
                    input[j] = tempForSwap;
                    i++;
                }
            }
            tempForSwap = input[i];
            input[i] = input[end];
            input[end] = tempForSwap;
            return i;
        }

        
        public static bool IsLessThanOrEqual(string timeFirst, string timeSecond)
        {
            int firstHour = int.Parse(timeFirst.Substring(0, 2));
            int secondHour = int.Parse(timeSecond.Substring(0, 2));
            if ( firstHour > secondHour)
            {
                return false;
            }
            else if (firstHour < secondHour) return true;

            int firstMinute = int.Parse(timeFirst.Substring(3, 2));
            int secondMinute = int.Parse(timeSecond.Substring(3, 2));

            if (firstMinute > secondMinute)
            {
                return false;
            }
            else if (firstMinute < secondMinute) return true;

            //check for equality here
            if (int.Parse(timeFirst.Substring(6, 2)) > int.Parse(timeSecond.Substring(6, 2)))
            {
                return false;
            }

            return true;
        }
    }
}
