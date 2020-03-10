﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Nodes;

namespace Assets.Pathfinding
{
    public class Pathfinding // Actual algorithm for pathing
    {
        public event EventHandler Updated;
        private void OnUpdated()
        {
            Updated?.Invoke(null, EventArgs.Empty);
        }
        public Map Map { get; set; }
        public Node Start { get; set; }
        public Node End { get; set; }
        public int NodeVisits { get; private set; }
        public double ShortestPathLength { get; set; }
        public double ShortestPathCost { get; private set; }

        public Pathfinding(Map map)
        {
            Map = map;
            End = map.EndNode;
            Start = map.StartNode;
        }

        // Djikstra search as contingency

        /*
        public List<Node> GetShortestPathDijkstra()
        {
            DijkstraSearch();
            var shortestPath = new List<Node>();
            shortestPath.Add(End);
            BuildShortestPath(shortestPath, End);
            shortestPath.Reverse();
            return shortestPath;
        }
       
        private void DijkstraSearch()
        {
            NodeVisits = 0;
            Start.MinCostToStart = 0;
            var prioQueue = new List<Node>();
            prioQueue.Add(Start);
            do
            {
                NodeVisits++;
                prioQueue = prioQueue.OrderBy(x => x.MinCostToStart.Value).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);
                foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
                {
                    var childNode = cnn.ConnectedNode;
                    if (childNode.Visited)
                        continue;
                    if (childNode.MinCostToStart == null || node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                        childNode.NearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == End)
                    return;
            } while (prioQueue.Any());
        }
        */

        private void BuildShortestPath(List<Node> list, Node node)
        {
            if (node.NearestToStart == null)
                return;
            list.Add(node.NearestToStart);
            ShortestPathLength += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Length;
            ShortestPathCost += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Cost;
            BuildShortestPath(list, node.NearestToStart);
        }

        public List<Node> AStarShortPath() // Call this one to return the path
        {
            foreach (var node in Map.Nodes)
                node.StraightLineDistanceToEnd = node.StraightLineDistanceTo(End);
            AStarSearch();
            var ShortestPath = new List<Node>();
            ShortestPath.Add(End);
            BuildShortestPath(ShortestPath, End);
            ShortestPath.Reverse();
            return ShortestPath;
        }

        private void AStarSearch()
        {
            NodeVisits = 0;
            Start.MinCostToStart = 0;
            var PriorityQueue = new List<Node>();
            PriorityQueue.Add(Start);
            do
            {
                PriorityQueue = PriorityQueue.OrderBy(x => x.MinCostToStart + x.StraightLineDistanceToEnd).ToList();
                var SelectedNode = PriorityQueue.First();
                PriorityQueue.Remove(SelectedNode);
                NodeVisits++;
                foreach (var cnn in SelectedNode.Connections.OrderBy(x => x.Cost))
                {
                    var ChildNode = cnn.ConnectedNode;
                    if (ChildNode.Visited)
                        continue;
                    if (ChildNode.MinCostToStart == null || SelectedNode.MinCostToStart + cnn.Cost < ChildNode.MinCostToStart)
                    {
                        ChildNode.MinCostToStart = SelectedNode.MinCostToStart + cnn.Cost;
                        ChildNode.NearestToStart = SelectedNode;
                        if (!PriorityQueue.Contains(ChildNode))
                            PriorityQueue.Add(ChildNode);
                    }
                }
                SelectedNode.Visited = true;
                if (SelectedNode == End)
                    return;
            } while (PriorityQueue.Any());
        }
    }
}
