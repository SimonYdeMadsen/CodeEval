using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatsChallenge
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

                    int lengthOfWire = int.Parse(lineArray[0]);
                    lengthOfWire -= 2 * 6;

                    int proximityRequirement = int.Parse(lineArray[1]);

                    int currentNumberOfBats = int.Parse(lineArray[2]);


                    int additionalBats;

                    if (currentNumberOfBats == 0) additionalBats = 1;
                    else additionalBats = 0;


                    //Console.WriteLine((lengthOfWire - proximityRequirement * currentNumberOfBats) / proximityRequirement + additionalBats);

                    if (lineArray.Length <= 3)
                    {
                        while (lengthOfWire - proximityRequirement * currentNumberOfBats >= proximityRequirement)
                        {
                            additionalBats++;
                            lengthOfWire -= proximityRequirement;
                        }
                    }
                    else
                    {
                        int firstBatPos = int.Parse(lineArray[3])-6;

                        //handles positions before the other bats
                        while (proximityRequirement <= firstBatPos)
                        {
                            additionalBats++;
                            //Console.WriteLine("one additinal bat before");
                            firstBatPos -= proximityRequirement;
                        }

                        //check if more bats can sit in between the current bats
                        if (currentNumberOfBats > 1)
                        {
                            for (int i = 0; i < currentNumberOfBats-1; i++)
                            {
                                
                                firstBatPos = int.Parse(lineArray[3+i]);
                                
                                int secondBatPos = int.Parse(lineArray[3+i+1]);

                                //Console.WriteLine("bat1: " + firstBatPos + " bat2: " + secondBatPos);

                                while (secondBatPos - firstBatPos >= 2 * proximityRequirement)
                                {
                                    //Console.WriteLine("bat can fit between bats: " + secondBatPos + " and " + firstBatPos);
                                    additionalBats++;
                                    secondBatPos -= 2 * proximityRequirement;   
                                }
                            }
                        }
                        /*6+88*3 +|| +88.. 445 + 88 .. 606 +88 ... 751   =831
                            270 |161| 445|      161   |606   ... done
                               3      1          1          1           = 6 additional bats iff 1*proximityRequirement
                               3        0        0          0           = 3 if 1*proximityRequirement
                        */

                        //update length to where the last bat was sitting
                        lengthOfWire-= int.Parse(lineArray[lineArray.Length-1])-6;

                        

                        
                        //add the additional bats
                        additionalBats += (lengthOfWire / proximityRequirement);
                    }


                    Console.WriteLine(additionalBats/* + " on input " + line*/);
                    
                }
        }
    }
}
