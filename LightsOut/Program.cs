using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut
{
    class Program
    {
        enum Field { Off, On}

        

        static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;
                    line = line.Trim();

                    string[] lineArr = line.Split(' ');
                    int n = int.Parse(lineArr[0]);
                    int m = int.Parse(lineArr[1]);

                    string[] boardInput = lineArr[2].Split('|');

                    Console.WriteLine(string.Join("\n", boardInput));

                    Field[,] grid = new Field[n, m];
                    Console.WriteLine("n : " + n + " getlength 0: " + grid.GetLength(0));
                    Console.WriteLine("m : " + m + " getlength 1: " + grid.GetLength(1));


                    Console.WriteLine("grid[col, row]");
                    InitializeGrid(n, m, boardInput, grid);
                    Console.WriteLine();
                    PrintGrid(grid);


                    

                    int numberOfMoves = 0;
                    numberOfMoves = ChaseDownLights(n, m, grid, numberOfMoves);

                    PrintGrid(grid);
                    Console.WriteLine("Moves so far: " + numberOfMoves);




                    //TO-DO: figure out the logic for the last row 

                    //fx if 1010101010
                    FlipAdjacentFields(grid, 0, 0);
                    numberOfMoves++;
                    numberOfMoves = ChaseDownLights(n, m, grid, numberOfMoves);
                    PrintGrid(grid);
                    Console.WriteLine("Moves so far: " + numberOfMoves);


                    //...

                }
        }

        private static int ChaseDownLights(int n, int m, Field[,] grid, int numberOfMoves)
        {
            //nullify all rows but the last
            for (int i = 0; i < n-1; i++)
            {

                for (int j = 0; j < m; j++)
                {

                    if (grid[i, j] == Field.On)
                    {
                        //Click the field below
                        FlipAdjacentFields(grid, i + 1, j);
                        numberOfMoves++;
                    }
                }
            }

            return numberOfMoves;
        }

        private static void InitializeGrid(int n, int m, string[] boardInput, Field[,] grid)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (boardInput[i][j].Equals('O')) grid[i, j] = Field.On;

                }

            }
        }

        private static void FlipAdjacentFields(Field[,] grid, int row, int col)
        {
            if (row - 1 >= 0)
                FlipField(grid, row - 1, col);
            if (col - 1 >= 0)
                FlipField(grid, row, col - 1);
            FlipField(grid, row, col);
            if (col + 1 < grid.GetLength(1))
                FlipField(grid, row, col + 1);
            if (row + 1 < grid.GetLength(0))
                FlipField(grid, row + 1, col);
        }

        private static void FlipField(Field[,] grid, int row, int col)
        {
            if (grid[row, col] == Field.On) grid[row, col] = Field.Off;
            else grid[row, col] = Field.On;
        }

        private static void PrintGrid(Field[,] grid)
        {
            Console.WriteLine();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write((int)grid[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
