using System;
using System.IO;


namespace NumberOfOnes
{
    /*
     Example Input: 987

        remainder = 1; number/2 = 493;
        remainder = 1; number/2 = 246;
        remainder = 0; number/2 = 123;
        remainder = 1; number/2 = 61;
        remainder = 1; ... 30;
        remainder = 0; ... 15;
        remainder = 1; ... 7;
        remainder = 1; ... 3;
        remainder = 1; ... 1;
        remainder = 1; ... 0;
        
        Backwards: 1111011011
        Number of 1's: 8

    Example Output: 8
         
         */


    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;
                    // do something with line

                    int number = Int32.Parse(line);

                    int numberOfOnes = 0;

                    while (number > 0)
                    {
                        int remainder = number % 2;
                        if (remainder == 1) numberOfOnes++;
                        number = number / 2;
                    }


                    Console.WriteLine(numberOfOnes);
                    
                }
        }
    }
}
