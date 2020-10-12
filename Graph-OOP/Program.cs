using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;

namespace Graph_OOP
{
    class Node
    {
        private string name;
        private Dictionary<Node, float> connections;
        public List<Node> allNodes = new List<Node>(); 

        //Constructor
        public Node(string newName)
        {
            connections = new Dictionary<Node, float>();
            this.name = newName;

        }

        //Name getter
        public string getName()
        {
            return this.name;
        }

        //Adds a connection to the Node's dictionary
        public void AddConnection(Node newConnection, float weight)
        {
            this.connections.Add(newConnection, weight);
        }

        //Cleanly print the connections from any Node passed
        public void printConnection(Node node)
        {
            Console.WriteLine("----- Nodes connected to " + node.getName() + " -----");
            foreach (KeyValuePair<Node, float> c in connections)
            {
                Console.WriteLine(c.Key.name + ": " + c.Value);
            }
        }

        //Prints connections of all Nodes in a list
        public void printConections(List<Node> nodes)
        {
            foreach (Node n in nodes)
            {
                Console.WriteLine("\n----- Nodes connected to " + n.getName() + " -----");
                foreach (KeyValuePair<Node, float> c in n.connections)
                {
                    Console.WriteLine(c.Key.name + ": " + c.Value);
                }
            }
        }

        //A function to start a check connections dialogue for a user to type a node name and print all connections
        public bool checkConnected(List<Node> allNodes)
        {
            Console.WriteLine("\n\n-----Check Connected-----\nenter 'from' Node:");
            string from = Console.ReadLine();
            Console.WriteLine("enter 'to' node:");
            string to = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("-----Check Connected-----");

            foreach (KeyValuePair<Node, float> o in StringToNode(from, allNodes).connections)
            {
                if (o.Key.name.ToString() == StringToNode(to, allNodes).getName().ToLower())
                {
                    Console.WriteLine(from + " is connected to " + to + " with a value of: " + o.Value);
                    return true;
                }
            }
            Console.WriteLine("There is no connection from " + from + " to " + to);
            return false;
        }

        //Linear search to translate a string to return its Node object
        private Node StringToNode(string input, List<Node> allNodes)
        {
            foreach (Node n in allNodes)
            {
                if (n.getName().ToLower() == input.ToLower())
                {
                    return n;
                }
            }
            return allNodes[0];
        }

        //Depth first Traversal function to manage the use of the recursive function
        public void DepthFirstTraversal(List<Node> Nodes, Node StartNode)
        {
            List<Node> Visited = new List<Node>();
            List<Node> Output = dfs(StartNode, Visited);

            Console.WriteLine("\n\n ----- Depth First Search -----\n");
            foreach (Node n in Output)
            {
                Console.Write(n.name + " - ");
            }
        }

        //Depth First Search handeling function to recursivly search the graph
        //Pseudocode often shows passing a graph (or list of nodes) List<Node> allNodes to the function, however,
        //this is not required as each Node object has a dictionary of connections
        private List<Node> dfs(Node CurrentVertex, List<Node> Visited)
        {
            Visited.Add(CurrentVertex);
            //Key: Node object
            //value: weight
            foreach (KeyValuePair<Node, float> o in CurrentVertex.connections)
            {
                if (!Visited.Contains(o.Key)) //Does not contain
                {
                    dfs(o.Key, Visited);
                }
            }

            return Visited;
        }
        
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            //Make nodes
            Node Invalid = new Node("InvalidNode");
            Node London = new Node("London");
            Node HaywardsHeath = new Node("Haywards-Heath");
            Node BurgessHill = new Node("Burgess-Hill");
            Node Brighton = new Node("Brighton");
            Node Worthing = new Node("Worthing");
            Node Littlehampton = new Node("Littlehampton");
            Node Chichester = new Node("Chichester");
            Node Southampton = new Node("Southampton");
            Node Bristol = new Node("Bristol");
            Node Reading = new Node("Reading");
            Node Redhill = new Node("Redhill");
            
            //Add connections to the Nodes
            London.AddConnection(HaywardsHeath, 40f);
            London.AddConnection(Redhill, 20f);
            London.AddConnection(Reading, 25f);
            London.AddConnection(Chichester, 70f);
            
            HaywardsHeath.AddConnection(London, 40f);
            HaywardsHeath.AddConnection(Redhill, 14f);
            HaywardsHeath.AddConnection(BurgessHill, 7f);
            
            BurgessHill.AddConnection(HaywardsHeath, 7f);
            BurgessHill.AddConnection(Brighton, 10f);
            BurgessHill.AddConnection(Worthing, 19f);
            
            Brighton.AddConnection(BurgessHill, 10f);
            Brighton.AddConnection(Worthing, 15f);
            
            Worthing.AddConnection(Brighton, 15f);
            Worthing.AddConnection(BurgessHill, 19f);
            Worthing.AddConnection(Littlehampton, 5f);
            Worthing.AddConnection(Chichester, 22f);
            
            Littlehampton.AddConnection(Worthing, 5f);
            
            Chichester.AddConnection(Worthing, 22f);
            Chichester.AddConnection(London, 70f);
            Chichester.AddConnection(Southampton, 17f);
            
            Southampton.AddConnection(Chichester, 17f);
            Southampton.AddConnection(Reading, 50f);
            Southampton.AddConnection(Bristol, 70f);
            
            Bristol.AddConnection(Southampton, 70f);
            Bristol.AddConnection(Reading, 55f);
            
            Reading.AddConnection(Bristol, 55f);
            Reading.AddConnection(Redhill, 21f);
            Reading.AddConnection(Southampton, 50f);
            Reading.AddConnection(London, 25f);
            
            Redhill.AddConnection(Reading, 21f);
            Redhill.AddConnection(London, 20f);
            Redhill.AddConnection(HaywardsHeath, 14f);
            
            //Add all the Nodes to an All Nodes list
            List<Node> allNodes = new List<Node>()
            {
                London,
                HaywardsHeath,
                BurgessHill,
                Brighton,
                Worthing,
                Littlehampton,
                Chichester,
                Southampton,
                Bristol,
                Reading,
                Redhill
            };
           
            London.printConections(allNodes);

            //London.checkConnected(allNodes);
            
            London.DepthFirstTraversal(allNodes, London);
        }
    }
}
