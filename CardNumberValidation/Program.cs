using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardNumberValidation
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
                    line = line.Trim().Replace(" ", "");

                    
                    bool evenLength = false;
                    if (line.Length % 2 == 0) evenLength = true;

                    int digitSum = 0, offset = 0;

                    //compensates for starting in the beginning of the string
                    if (evenLength) offset = 1;

                    for (int i = 0; i < line.Length; i++)
                    {

                        if ((i+offset) % 2 == 1)
                        {
                            int twiceOfDigit = int.Parse(line[i] + "") * 2;
                            if (twiceOfDigit > 9)
                            {
                                digitSum += int.Parse(twiceOfDigit.ToString()[0] + "");
                                digitSum += int.Parse(twiceOfDigit.ToString()[1] + "");
                            }
                            else digitSum += twiceOfDigit;

                        }
                        else digitSum += int.Parse(line[i]+"");

                    }

                    if (digitSum % 10 == 0)
                    {
                        Console.WriteLine(1);
                    }
                    else Console.WriteLine(0);

                }

        }
    }
}
