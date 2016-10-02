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

                        PrintInterfacesOfHost(listOfHosts, 5);
                        PrintInterfacesOfHost(listOfHosts, 1);
                        PrintInterfacesOfHost(listOfHosts, 8);

                        if (line.Length < 5)
                            Console.WriteLine(line);

                        string[] idArray = line.Split(' ');

                        int start_ID = int.Parse(idArray[0]);
                        int end_ID = int.Parse(idArray[1]);


                        //dijkstra's

                        Tuple<int[], List<int>[]> result = Dijkstras(vertexSet, start_ID);

                        var prev = result.Item2;
                        int u = end_ID;


                        
                        while (prev[u].Count > 0)
                        {
                            Console.WriteLine("u: "+ u);
                            u = prev[u][0];
                            
                        }

                    }








                } //end else
                }

        private static Tuple<int[],List<int>[]> Dijkstras(List<Node> vertexSet, int source)
        {
            List<Node> Q = new List<Node>();

            int[] dist = new int[vertexSet.Count];
            List<int>[] prev = new List<int>[vertexSet.Count];
            for (int i = 0; i < vertexSet.Count; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = new List<int>();
                Q.Add(vertexSet[i]);
            }
            dist[source] = 0;

            while (Q.Count != 0)
            {
                int minDistance = int.MaxValue;
                int sourceIndex = 0;
                List<Node> sourceNeighbors = new List<Node>();
                for (int i = 0; i < dist.Length; i++)
                {
                    //select a source node
                    try
                    {
                        if (dist[i] < minDistance && Q[i] != null)
                        {
                            minDistance = Q[i].Distance;
                            sourceIndex = i;
                        }
                    }
                    catch (ArgumentOutOfRangeException) {}
                }

                Node currentSource = Q[sourceIndex];
                Q.RemoveAt(sourceIndex);
                for (int i = 0; i < Q.Count; i++)
                {
                    //find neighbors of source node
                    foreach (uint sourceData in currentSource.Data)
                        if (Q[i].Data.Contains(sourceData)) sourceNeighbors.Add(Q[i]);
                }



                for (int i = 0; i < sourceNeighbors.Count; i++)
                {
                    int alternativeRoute = minDistance + 1;
                    if (alternativeRoute < sourceNeighbors[i].Distance)
                    {
                        //a shorter path has been found
                        dist[i] = alternativeRoute;
                        prev[i].Add(sourceIndex);
                    }
                }

                

            }
            return new Tuple<int[],List<int>[]>(dist, prev);
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


