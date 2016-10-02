using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace APileOfBricks
{
    /*
     * input: 
     * [4,3] [3,-3]|(1 [10,9,4] [9,4,2])
     * [-1,-5] [5,-2]|(1 [4,7,8] [2,9,0]);(2 [0,7,1] [5,9,8])
     * [-4,-5] [-5,-3]|(1 [4,8,6] [0,9,2]);(2 [8,-1,3] [0,5,4])
     * output: 
     * 1
     * 1,2
     * -
    */
    class APileOfBricks
    {
        public static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line) continue;


                    line = line.Replace("[", "").Replace("]", "");

                    string[] temp = line.Split('|');

                    string hole = temp[0];
                    string[] bricks = temp[1].Split(';');



                    int space_index = hole.IndexOf(' ');

                    string[] holeFirst = hole.Substring(0, space_index).Split(',');
                    string[] holeSecond = hole.Substring(space_index).Trim().Split(',');




                    int holeX = abs_diff(holeFirst[0], holeSecond[0]);
                    int holeY = abs_diff(holeFirst[1], holeSecond[1]);

                    //store x and y values of hole
                    int[] hole_arr = new int[] { holeX, holeY };





                    bool has_output = false;

                    //there can be several bricks
                    for (int i = 0; i < bricks.Length; i++)
                    {
                        //remove the id, brackets, and whitespace
                        bricks[i] = bricks[i].Trim().Replace("(", "").Replace(")", "").Split(new string[] {" "}, 2, StringSplitOptions.None)[1];

                        

                        string[] brick_arr = bricks[i].Split(' ');


                        string[] brickFirst = brick_arr[0].Split(',');
                        string[] brickSecond = brick_arr[1].Split(',');

                        
                        //Console.WriteLine("brickfirst: " + string.Join(",", brickFirst));
                        //Console.WriteLine("brickseond: " + string.Join(",", brickSecond));

                        int brickX = abs_diff(brickFirst[0], brickSecond[0]);

                        int brickY = abs_diff(brickFirst[1], brickSecond[1]);

                        int brickZ = abs_diff(brickFirst[2], brickSecond[2]);

                        //Console.WriteLine("brickX : " + brickX + " and brickY: " + brickY + " and brickZ: " + brickZ);




                        int[] brick = new int[3] { brickX, brickY, brickZ };
                        int[] brick_relevant = new int[2];
                        int brick_smallest = brick.Min();
                        brick_relevant[0] = brick_smallest;

                        int smallest_int = Array.IndexOf(brick, brick_smallest);
                        brick[smallest_int] = Int32.MaxValue;
                        brick_smallest = brick.Min();
                        brick_relevant[1] = brick_smallest;

                        
                        //if brick LEQ hole
                        if (brick_relevant[0] <= hole_arr.Min() && brick_relevant[1] <= hole_arr.Max())
                        {
                            if (has_output) Console.Write(",");
                            has_output = true;
                            Console.Write(i + 1);
                        }




                    } //endfor

                    if (!has_output) Console.Write('-');
                    Console.Write("\n");
                }//endwhile
            }
        }

        public static int abs_diff(string val1, string val2)
        {
            return Math.Abs((Int32.Parse(val1)) - (Int32.Parse(val2)));
        }

    }
}
