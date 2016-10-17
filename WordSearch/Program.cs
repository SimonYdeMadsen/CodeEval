using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    class Program
    {
        /*
         Given a hard coded board, determine if any path spells the input word:
        ASADB=False; ABCCED=True; ABCF=False
        */

        static bool result = false;

        static void Main(string[] args)
        {
            char[,] board = new char[,] {
                        { 'A', 'B', 'C', 'E' },
                        { 'S', 'F', 'C', 'S' },
                        { 'A', 'D', 'E', 'E' } };

            int boardX = 3;
            int boardY = 4;


            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;


                    result = false;

                    for (int i = 0; i < boardX; i++)
                    {
                        if (result) break;

                        for (int j = 0; j < boardY; j++)
                        {
                            if (board[i, j] == line[0])
                            {
                                SearchForWord(board, line.Substring(1), i, j);
                                
                            }
                        }
                    }


                    Console.WriteLine(result);
                }

        }

        

        private static void SearchForWord(char[,] board, string line, int i, int j)
        {
            if (line.Length <= 0)
            {
                result = true;
                return;
            }
            

            if (i - 1 >= 0 && board[i - 1, j] == line[0])
                SearchForWord(board, line.Substring(1), i - 1, j);

            if(i + 1 < board.GetLength(0) && board[i + 1, j] == line[0])
                SearchForWord(board, line.Substring(1), i + 1, j);

            if(j - 1 >= 0 && board[i, j - 1] == line[0])
                SearchForWord(board, line.Substring(1), i, j - 1);

            if(j + 1 < board.GetLength(1) && board[i, j + 1] == line[0])
                SearchForWord(board, line.Substring(1), i, j + 1);

        }
    }
}
