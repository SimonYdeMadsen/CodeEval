using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistinctSubsequences
{
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

                    string[] lineArr = line.Split(',');

                    string sequence = lineArr[0];
                    string subsequence = lineArr[1];


                    
                    FindSubsequences(sequence, subsequence, 0, 0, "");

                    Console.WriteLine("count: "+ count);
                }
        }


        static List<string> resultSet = new List<string>();
        static int count = 0;

        private static void FindSubsequences(string sequence, string subsequence, int seqIndex, int subIndex, string temp)
        {
            //TO-DO: make it work with duplicate letters

            for (int i = seqIndex; i < sequence.Length; i++)
            {
                if (sequence[i].Equals(subsequence[subIndex]))
                {
                    temp += i;
                    if (subsequence[subIndex] == subsequence[subsequence.Length - 1])
                    {
                        Console.WriteLine("temp: "+ temp);
                        if (!resultSet.Contains(temp)) {
                            count++;
                            resultSet.Add(temp);
                        }
                    }
                    else if (subIndex < subsequence.Length)
                        FindSubsequences(sequence, subsequence, i+1, subIndex+1, temp);
                }
            }


        }





    }
}
