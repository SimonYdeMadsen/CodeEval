using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheCubes
{

    enum Field { Blank, Hedge, Hole }

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


                    string[] lineArray = line.Split(';');

                    int dimension = int.Parse(lineArray[0]);

                    if (lineArray[1].Length % dimension != 0)
                    {
                        Console.WriteLine("You screwed up the input.");
                        throw new FormatException();
                    }

                    //string[] levels = Regex.Split(lineArray[1], "(?<=^(.{"+ (dimension * dimension) + "})+)");
                    //string[] currentLevel = Regex.Split(level, "(?<=^(.{" + dimension + "})+)");


                    int mazeLength = 0;
                    bool completedSuccessfully = true;

                    Field[,] levelMatrix = new Field[dimension,dimension];

                    int dimensionSquared = dimension * dimension;
                    int numberOfLevels = lineArray[1].Length / dimensionSquared;

                    
                    for (int levelNumber = 0; levelNumber < numberOfLevels; levelNumber++) {

                        string level = lineArray[1].Substring(dimensionSquared * levelNumber, dimensionSquared);
                        Tuple<int, int> exitCoordinates = null;
                        List<Tuple<int, int>> holeCoordinates = new List<Tuple<int, int>>();



                        //Console.WriteLine("level#: "+ levelNumber);

                        for (int i = 0; i < dimension; i++)
                        {

                            string currentRow = level.Substring(i * dimension, dimension);

                            //Console.WriteLine(currentRow);

                            for (int j = 0; j < currentRow.Length; j++)
                            {
                                if (currentRow[j].Equals(' '))
                                {
                                    levelMatrix[i, j] = Field.Blank;
                                }
                                else if (currentRow[j].Equals('*'))
                                {
                                    levelMatrix[i, j] = Field.Hedge;
                                }
                                else
                                {
                                    levelMatrix[i, j] = Field.Hole;
                                    holeCoordinates.Add(new Tuple<int, int>(i, j));
                                }
                            }

                            

                            //check if one of the end rows has a single hole
                            if ((i == 0 || i == dimension - 1) && Regex.Matches(currentRow, @"[\s]").Count == 1)
                            {
                                exitCoordinates = new Tuple<int, int>(i, dimension / 2);
                            }



                        }//end current level
                        int shortestPath = FindShortestPathOfLevel(levelMatrix, exitCoordinates, holeCoordinates);
                        if (shortestPath > 0)
                            mazeLength += shortestPath;
                        else completedSuccessfully = false;

                    }//end all levels
                    int result = mazeLength + numberOfLevels;
                    if (completedSuccessfully)
                        Console.WriteLine(result);
                    else Console.WriteLine("0");



                }//end io
        }//end main


        //static List<int> distances;
        static int minDistance;

        private static int FindShortestPathOfLevel(Field[,] matrix, Tuple<int, int> exit, List<Tuple<int,int>> holes)
        {
            
            minDistance = int.MaxValue; 
            var startPos = new Tuple<int,int>(1,1);
            List<Tuple<int, int>> goalPosList = new List<Tuple<int, int>>();


            if (exit != null)
            {
                goalPosList.Add(exit);
            }
            else if (holes.Count > 0)
            {
                foreach (Tuple<int, int> hole in holes)
                { 
                    goalPosList.Add(hole);
                }
            }
            else
            {
                return 0;
            }
            FindAllPaths(matrix, startPos, goalPosList, 0);


            if (minDistance < int.MaxValue)
                return minDistance;
            else return 0;
        }



        private static void FindAllPaths(Field[,] matrix, Tuple<int, int> startPos, List<Tuple<int, int>> goalPosList, int distance)
        {
            //Console.WriteLine("Currently at: " + startPos + " distance: "+ distance);


            if (goalPosList.Contains(startPos))
            {
                Console.WriteLine("found goal with distance: "+ distance);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    
                    Console.WriteLine("minDistance updated to: "+ minDistance);
                }
                
            }
            //Don't need to search further. 
            else
            {
                

                matrix[startPos.Item1, startPos.Item2] = Field.Hedge;

                int rowLength = (int)Math.Sqrt(matrix.Length);


                if (startPos.Item1 + 1 < rowLength && matrix[startPos.Item1 + 1, startPos.Item2] != Field.Hedge)
                    FindAllPaths(matrix, new Tuple<int, int>(startPos.Item1 + 1, startPos.Item2), goalPosList, ++distance);

                if (startPos.Item1 - 1 >= 0 && matrix[startPos.Item1 - 1, startPos.Item2] != Field.Hedge)
                    FindAllPaths(matrix, new Tuple<int, int>(startPos.Item1 - 1, startPos.Item2), goalPosList, ++distance);


                if (startPos.Item2 + 1 < rowLength && matrix[startPos.Item1, startPos.Item2 + 1] != Field.Hedge)
                    FindAllPaths(matrix, new Tuple<int, int>(startPos.Item1, startPos.Item2 + 1), goalPosList, ++distance);

                if (startPos.Item2 - 1 >= 0 && matrix[startPos.Item1, startPos.Item2 - 1] != Field.Hedge)
                    FindAllPaths(matrix, new Tuple<int, int>(startPos.Item1, startPos.Item2 - 1), goalPosList, ++distance);


            }
        }
    }

    
}
