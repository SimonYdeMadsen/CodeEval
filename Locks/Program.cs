using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locks
{
    class Program
    {
        enum DoorState {Open=0 , Locked=1 };

        static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;
                    line = line.Trim();

                    string[] lineArr = line.Split(' ');

                    int numberOfDoors = int.Parse(lineArr[0]);
                    int numberOfIterations = int.Parse(lineArr[1]);


                    DoorState[] doors = new DoorState[numberOfDoors];

                    for (int iter = 0; iter < numberOfIterations; iter++)
                    {
                        if (iter == numberOfIterations - 1)
                        {
                            SwapDoorState(doors, numberOfDoors - 1);
                        }
                        else {
                            for (int i = 0; i < numberOfDoors; i++)
                            {
                                if ((i + 1) % 2 == 0) doors[i] = DoorState.Locked;

                                if ((i + 1) % 3 == 0)
                                {
                                    SwapDoorState(doors, i);
                                }
                            }
                        }
                    }

                    //Console.WriteLine(string.Join(", ", doors));

                    int unlockedDoors = 0;
                    foreach (int i in doors) if (i == 0) unlockedDoors++;


                    Console.WriteLine(unlockedDoors);


                }
        }

        private static void SwapDoorState(DoorState[] doors, int i)
        {
            if (doors[i] == DoorState.Locked) doors[i] = DoorState.Open;
            else doors[i] = DoorState.Locked;
        }
    }
}
