using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoutingProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            string route;
            int lineNumber = 0;
            List<List<uint>> listOfHosts = new List<List<uint>>();
            List<Node> vertexSet = new List<Node>();
            using (StreamReader reader = File.OpenText(args[0]))

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    line = line.Trim();
                    if (line == null) continue;
                    lineNumber++;


                    //operate on the host interfaces
                    if (lineNumber == 1)
                    {
                        route = line.Substring(1, line.Length - 2);

                        string[] routeMapping = Regex.Split(route, "],");

                        Console.WriteLine(string.Join("\n", routeMapping));



                        foreach (string hostMapping in routeMapping)
                        {

                            List<uint> listOfHostInterfaces = new List<uint>();

                            Node hostNode = new Node();

                            foreach (string networkInterface in hostMapping.Split(','))
                            {

                                string currentInterface = networkInterface.Split('\'')[1];



                                string[] currentCIDR = currentInterface.Split('/');
                                string ipAddress = currentCIDR[0];
                                int subnetMask = int.Parse(currentCIDR[1]);

                                var addressBytes = ipAddress.Split('.');

                                string addressBitRepresentation = "";
                                for (int i = 0; i < addressBytes.Length; i++)
                                {
                                    addressBitRepresentation += Convert.ToString(int.Parse(addressBytes[i]), 2).PadLeft(8, '0');
                                }

                                // (subnetMask) 1's followed by (32-subnetMask) 0's
                                string subnetBitRepresentation = "".PadLeft(subnetMask, '1').PadRight(32, '0');

                                //bitwise AND
                                uint subnetInterface = (Convert.ToUInt32(addressBitRepresentation, 2) & Convert.ToUInt32(subnetBitRepresentation, 2));

                                listOfHostInterfaces.Add(subnetInterface);


                                hostNode.Data.Add(subnetInterface);


                            }
                            listOfHosts.Add(listOfHostInterfaces);


                            vertexSet.Add(hostNode);


                        }
                        Console.WriteLine("number of hosts: " + listOfHosts.Count);






                    } // end line 1
                    else
                    {
                        Console.WriteLine("host 5: ");
                        PrintInterfacesOfHost(listOfHosts, 5);
                        Console.WriteLine("host 1: ");
                        PrintInterfacesOfHost(listOfHosts, 1);
                        Console.WriteLine("host 3: ");
                        PrintInterfacesOfHost(listOfHosts, 3);
                        Console.WriteLine("host 4: ");
                        PrintInterfacesOfHost(listOfHosts, 4);
                        Console.WriteLine("host 8: ");
                        PrintInterfacesOfHost(listOfHosts, 8);


                        if (line.Length < 5)
                            Console.WriteLine(line);

                        string[] idArray = line.Split(' ');

                        int start_ID = int.Parse(idArray[0]);
                        int end_ID = int.Parse(idArray[1]);


                        //dijkstra's

                        Tuple<int[], List<int>[]> dijkstraLists = Dijkstras(vertexSet, start_ID);

                        string result = "" + end_ID;
                        var prev = dijkstraLists.Item2;
                        int u = end_ID;

                        Console.WriteLine("prev.Count: " + prev.Length);
                        Console.WriteLine("prev[{0}].Count: {1}", u, prev[u].Count);


                        while (prev[u].Count > 0)
                        {
                            u = prev[u][0];
                            result = u+", "+result;
                        }

                        if (! (int.Parse("" + result[0]) == start_ID))
                        {
                            Console.WriteLine("No connection.");
                        }
                        else
                        {
                            Console.WriteLine('[' + result + ']');
                        }

                        //TO-DO: add support for more than one shortest path (sort them)




                    } // end else








                } //end while
        }

        private static Tuple<int[], List<int>[]> Dijkstras(List<Node> vertexSet, int source)
        {
            int lengthOfQ = vertexSet.Count;
            Node[] Q = new Node[lengthOfQ];
            
            int[] dist = new int[vertexSet.Count];
            List<int>[] prev = new List<int>[vertexSet.Count];
            for (int i = 0; i < vertexSet.Count; i++)
            {
                dist[i] = 10000;
                prev[i] = new List<int>();
                Q[i] = vertexSet[i];
            }
            dist[source] = 0;

            while (lengthOfQ > 0)
            {


                int minDistance = 10000;
                int sourceIndex = -1;
                List<int> neighborIndices = new List<int>();


                //select a source node
                for (int i = 0; i < dist.Length; i++)
                {
                    if (Q[i] != null && dist[i] <= minDistance)
                    {
                        minDistance = dist[i];
                        sourceIndex = i;
                    }
                    

                }


                Console.WriteLine("minDistance: "+ minDistance);
                Console.WriteLine("sourceIndex: "+ sourceIndex);
                Console.WriteLine("length of Q: "+ lengthOfQ);
                if (sourceIndex == -1)
                {
                    throw new IndexOutOfRangeException("Custom: Source index not set.");
                }


                Node currentSource = Q[sourceIndex];


                //find neighbors of current source node
                for (int i = 0; i < Q.Length; i++)
                {
                    if (i == sourceIndex) continue;

                    foreach (uint sourceData in currentSource.Data)
                    {
                        if (Q[i] != null && Q[i].Data.Contains(sourceData))
                        {
                            neighborIndices.Add(i);
                            Console.WriteLine("neighbor: " + i);
                            break;
                        }
                    }
                }

                //remove source from Q
                Q[sourceIndex] = null;
                lengthOfQ--;
                

                //find alternative paths
                for (int i = 0; i < neighborIndices.Count; i++)
                {
                    int alternativeRoute = minDistance + 1;
                    if (alternativeRoute < dist[neighborIndices[i]])
                    {
                        //a shorter path has been found

                        dist[neighborIndices[i]] = alternativeRoute;
                        prev[neighborIndices[i]].Add(sourceIndex);

                        Console.WriteLine("updated prev[{0}]",neighborIndices[i]);

                        //Console.WriteLine("altered ({0}, {1}) to distance value {2}", sourceIndex, neighborIndices[i], alternativeRoute);

                    }
                }
                Console.WriteLine();

                

            }
            return new Tuple< int[],List<int>[] >(dist, prev);
        }












        private static void PrintInterfacesOfHost(List<List<uint>> listOfMappings, int index)
        {

            
            Console.WriteLine(string.Join("\n", listOfMappings[index]));
            Console.WriteLine();
        }
    }


    class Node
    {
        List<uint> data = new List<uint>();
        int distance = int.MaxValue;

        List<Node> previousNodes = new List<Node>(); 

        public Node()
        {
        }

        public List<uint> Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public int Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
            }
        }

        internal List<Node> PreviousNodes
        {
            get
            {
                return previousNodes;
            }

            set
            {
                previousNodes = value;
            }
        }
    }

}


