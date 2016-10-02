using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanAndArabic
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

                    int[] arabicDigits = new int[line.Length/2];
                    char[] romanNumerals = new char[line.Length/2];

                    Dictionary<char, int> romanDict = new Dictionary<char, int>();
                    romanDict.Add('I', 1);
                    romanDict.Add('V', 5);
                    romanDict.Add('X', 10);
                    romanDict.Add('L', 50);
                    romanDict.Add('C', 100);
                    romanDict.Add('D', 500);
                    romanDict.Add('M', 1000);



                    for (int i = 0; i < line.Length; i+=2)
                    {
                        int arabicNum = int.Parse(line[i] + "");
                        arabicDigits[i/2] = arabicNum;

                        char romanChar = line[i + 1];
                        romanNumerals[i/2] = romanChar;

                    }

                    int resultSum = 0;

                    for (int i = 0; i < arabicDigits.Length; i++)
                    {
                        int currentRoman = romanDict[romanNumerals[i]];
                        

                        if (i+1 < arabicDigits.Length && currentRoman < romanDict[romanNumerals[i + 1]] )
                        {
                            //subtract
                            resultSum -= arabicDigits[i] * currentRoman;
                        }
                        else
                        {
                            //add
                            resultSum += arabicDigits[i] * currentRoman;
                        }
                    }

                    Console.WriteLine(resultSum);


                }
        }
        





    }
}
