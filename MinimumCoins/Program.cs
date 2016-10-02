using System;
using System.IO;


namespace MinimumCoins
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

                    int sumToReach = int.Parse(line);
                    int coins = 0;

                    while (sumToReach >= 5)
                    {
                        sumToReach -= 5;
                        coins++;
                    }
                    while (sumToReach >= 3)
                    {
                        sumToReach -= 3;
                        coins++;
                    }
                    while (sumToReach >= 1)
                    {
                        sumToReach -= 1;
                        coins++;
                    }
                    Console.WriteLine(coins);


                }

        }
    }
}
