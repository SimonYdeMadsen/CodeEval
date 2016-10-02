using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotMovements
{
    class Node
    {
        private Node left;
        private Node right;

        private bool seen = false;
        
        public Node()
        {
            left = null;
            right = null;
        }

        internal Node Left
        {
            get
            {
                return left;
            }

            set
            {
                left = value;
            }
        }

        internal Node Right
        {
            get
            {
                return right;
            }

            set
            {
                right = value;
            }
        }

        public void HasBeenSeen()
        {
            seen = true;
        }

        public void Populate(Node current, int iter)
        {
            if (iter <= 0) return;

            current.left = new Node();
            current.right = new Node();


            Populate(current.left, iter-1);
            Populate(current.right, iter-1);
        }

        public void PrintTree(Node node)
        {
            if (node == null) return;
            {
                Console.WriteLine("left");
                PrintTree(node.left);

                Console.WriteLine("right");
                PrintTree(node.right);
            }
        }
}
}
