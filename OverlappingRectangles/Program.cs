using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace OverlappingRectangles
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

                    string[] lineCoords = line.Split(',');

                    Tuple<int, int>[] coordsArray = new Tuple<int, int>[lineCoords.Length / 2]; //may need +1

                    for (int i = 0; i < lineCoords.Length; i = i + 2)
                    {
                        Tuple<int, int> currentCoords = new Tuple<int, int>(int.Parse(lineCoords[i]), int.Parse(lineCoords[i + 1]));

                        coordsArray[i / 2] = currentCoords;
                    }




                    var xRangeOfFirstBox = Enumerable.Range(coordsArray[0].Item1+1, Math.Abs(coordsArray[0].Item1 - coordsArray[1].Item1));
                    var yRangeOfFirstBox = Enumerable.Range(coordsArray[0].Item2+1, Math.Abs(coordsArray[0].Item2 - coordsArray[1].Item2));




                    var xRangeOfSecondBox = Enumerable.Range(coordsArray[2].Item1+1, Math.Abs(coordsArray[2].Item1-coordsArray[3].Item1)) ;

                    var yRangeOfSecondBox = Enumerable.Range(coordsArray[2].Item2+1, Math.Abs(coordsArray[2].Item2 - coordsArray[3].Item2));

                    Console.WriteLine(xRangeOfFirstBox.Intersect(xRangeOfSecondBox).Count());



                    /*
                      -3,3,-1,1    
                      coordsArray[2]: -2,4,    coordsArray[3]: 2,2


                        r1: range(-3, -1) & 
                        r2: range(1, 3)

                        if range(-2, 2) intersects with r1 & range(2, 4) intersects with r2 then overlapping;
                      */







                }
        }


        public void Intersection() {

        }


    }
        
}
