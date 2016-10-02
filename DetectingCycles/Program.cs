using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingCycles
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

                    string[] lineArray = line.Split(' ');

                    string tortoise = "";
                    string hare = "";

                    int i = 0, j = 1;
                    do
                    {
                        if (i+j >= lineArray.Length) break;
                        tortoise = lineArray[i];
                        hare = lineArray[i + j];
                        i++;
                        j++;

                    } while (!tortoise.Equals(hare));
                    
                    Console.WriteLine("equal.. i-ele: "+ lineArray[i-1] +" i+j-ele: "+ lineArray[i+j-2]);
                    Console.WriteLine("tort: "+ tortoise + " hare: " + hare);


                    int h = i + j - 2;
                    i = 0;
                    do {
                        if (i >= lineArray.Length || h >= lineArray.Length) break;
                        tortoise = lineArray[i];
                        hare = lineArray[h];
                        i++;
                        h++;

                    } while (!tortoise.Equals(hare));
                    i--;
                    h--;

                    Console.WriteLine("First repetition at i=" + i);

                    int repetitionIndex = i;
                    int length = 0;
                    do
                    {
                        if (i + 1 >= lineArray.Length) break;
                        hare = lineArray[i + 1];
                        i++;
                        length++;

                    } while (!tortoise.Equals(hare));

                    Console.WriteLine("length: "+ length);

                    
                    string result = "";
                    for (i = 0; i < length; i++)
                    {
                        if (i > 0) result += " ";
                        result += lineArray[repetitionIndex+i];
                    }
                    Console.WriteLine("result: "+ result + "\n---------------");

                    //TO-DO: debug the next test-case

                }

        }
    }
}
