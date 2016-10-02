using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotMovements
{
    class Program
    {
        static void Main(string[] args)
        {


            Node node = new Node();

            node.Populate(node, 4);

            node.PrintTree(node);


            


        }

        
        


    }
}
