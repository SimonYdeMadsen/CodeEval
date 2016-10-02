using System;
using System.IO;


namespace DecodeNumbers
{
    /*
    Example Input: 1319
    Example Output: 4
    1: 1-3-1-9
    2: 1-3-19
    3: 13-1-9
    4: 13-19
*/



    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;


                    Boolean errorOccured = false;
                    int decodeCount = 1;
                    Boolean firstWasValid = false;

                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        int substringValue = Int32.Parse(line.Substring(i, 2));
                        if (substringValue < 0 || substringValue > 99)
                        {
                            errorOccured = true;
                            break;
                        }

                        if (substringValue >= 11 && substringValue <= 26)
                        {
                            decodeCount++;
                            if (i == 0) firstWasValid = true;
                            if (i == 2 && firstWasValid) decodeCount++;
                        }
                        
                    }
                    if (!errorOccured) Console.WriteLine(decodeCount);

                }
        }
    }
}


