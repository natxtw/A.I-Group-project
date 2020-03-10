using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Nodes
{
    public class Map // Node functionality for pathfinding
    {
        public static Map Randomize(int NodeCount, int Branching, int Seed, bool RandomWeights)
        {
            var rnd = new Random(Seed);
            var map = new Map();

            for(int i = 0; i < NodeCount; i++)
            {
                var newNode = Node.GetRandom(rnd, i.ToString());
                if (!newNode.ToCloseToAny(map.Nodes))
                    map.Nodes.Add(newNode);
            }

            foreach (var node in map.Nodes)
                node.ConnectClosestNodes(map.Nodes, Branching, rnd, RandomWeights);
            map.EndNode = map.Nodes[rnd.Next(map.Nodes.Count - 1)];
            map.StartNode = map.Nodes[rnd.Next(map.Nodes.Count - 1)];

            return map;
        }

        public List<Node> Nodes { get; set; } = new List<Node>(); // World map
        public Node StartNode { get; set;  } // Current location
        public Node EndNode { get; set;  } // Destination from decision making
        public List<Node> ShortestPath { get; set; } = new List<Node>();
    }

    public class Node // Map tile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Point { get; set; }
        public List<Edge> Connections { get; set; }
        public double? MinCostToStart { get; set; }
        public Node NearestToStart { get; set; }
        public bool Visited { get; set; }
        public double StraightLineDistanceToEnd { get; set; }

        internal static Node GetRandom(Random rnd, string name)
        {
            return new Node
            {
                Point = new Point
                {
                    X = rnd.NextDouble(),
                    Y = rnd.NextDouble()
                },
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        internal void ConnectClosestNodes(List<Node> nodes, int Branching, Random rnd, bool RandomWeight)
        {
            var connections = new List<Edge>();
            foreach(var node in nodes)
            {
                if (node.Id == this.Id)
                    continue;
                var Distance = Math.Sqrt(Math.Pow(Point.X - node.Point.X, 2) + Math.Pow(Point.Y - node.Point.Y, 2));
                connections.Add(new Edge
                {
                    ConnectedNode = node,
                    Length = Distance,
                    Cost = RandomWeight ? rnd.NextDouble() : Distance,
                });
            }
            connections = connections.OrderBy(x => x.Length).ToList();
            var count = 0;
            foreach (var cnn in connections)
            {
                if (!Connections.Any(c => c.ConnectedNode == cnn.ConnectedNode))
                    Connections.Add(cnn);
                count++;

                if(!cnn.ConnectedNode.Connections.Any(cc => cc.ConnectedNode == this))
                {
                    var backConnection = new Edge { ConnectedNode = this, Length = cnn.Length };
                    cnn.ConnectedNode.Connections.Add(backConnection);
                }
                if (count == Branching)
                    return;
            }
        }

        public double StraightLineDistanceTo(Node end)
        {
            return Math.Sqrt(Math.Pow(Point.X - end.Point.X, 2) + Math.Pow(Point.Y - end.Point.Y, 2));
        }

        internal bool ToCloseToAny(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                var d = Math.Sqrt(Math.Pow(Point.X - node.Point.X, 2) + Math.Pow(Point.Y - node.Point.Y, 2));
                if (d < 0.01)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Edge
    {
            public double Length { get; set; }
            public double Cost { get; set; } // 1 for path, 100 for grass to ensure guests move on the path
            public Node ConnectedNode { get; set; }
        public override string ToString()
        {
            return "-> " + ConnectedNode.ToString();
        }
    }

    public class Point
    {
            public double X { get; set; }
            public double Y { get; set; }
    }
}
