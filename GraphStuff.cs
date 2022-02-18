using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greedy
{

        public class Edge
        {
            public readonly Node First;
            public readonly Node Second;
            public Edge(Node first, Node second)
            {
                First = first;
                Second = second;
            }
            public bool IsIncident (Node node)
            {
                return node == First || node == Second;
            }
            public Node OtherNode(Node node)
            {
                if (First == node) return Second;
                else if (Second == node) return First;
                else throw new ArgumentException("Node have no other Node");
            }
        }
        public class Node
        {
            private readonly List<Edge> incidentsEdges = new List<Edge>();
            public readonly int Number;
            public Node (int number)
            {
                Number = number;
            }
            public override string ToString()
            {
                return Number.ToString();
            }
            public IEnumerable<Node> IncidentNodes
            {
                get
                {
                    return incidentsEdges.Select(x => x.OtherNode(this));
                }
            }
            public IEnumerable<Edge> IncidentEdges
            {
                get
                {
                    return incidentsEdges.Select(x => x);
                }
            }
            public void Connect (Node anotherNode)
            {
                var edge = new Edge(this, anotherNode);
                incidentsEdges.Add(edge);
                anotherNode.incidentsEdges.Add(edge);
            }
            public void Disconnect(Edge edge)
            {
                edge.First.incidentsEdges.Remove(edge);
                edge.Second.incidentsEdges.Remove(edge);
            }
        }
        public class Graph
        {
            private readonly Node[] nodes;
            public Graph(int nodesCount)
            {
                nodes = Enumerable
                    .Range(0, nodesCount)
                    .Select(z => new Node(z))
                    .ToArray();
            }
        public static Graph MakeGrapg (params int[] incidentNodes)
        {
            var graph = new Graph(incidentNodes.Max() + 1);
            for (int i = 0; i < incidentNodes.Length - 1; i += 2)
                graph.Connect(incidentNodes[i], incidentNodes[i + 1]);
            return graph;
        }
            public  Node this[int index]
            {
                get { return nodes[index]; }
            }
            public IEnumerable<Node> Nodes
            {
                get
                {
                    return nodes.Select(z => z);
                }
            }
            public IEnumerable<Edge> Edges
            {
                get
                {
                    return Nodes.SelectMany(z => z.IncidentEdges).Distinct();
                }
            }
            public void Connect (int v1, int v2)
            {
                nodes[v1].Connect(nodes[v2]);
            }
        }
    class GraphStuff
    {
        public static void Main()
        {
            var graph = new Graph(2);
            graph.Connect(0, 1);

            var flags = new Dictionary<Node, bool>();
            flags[graph[0]] = true;
            flags[graph[1]] = false;

            var weights = new Dictionary<Edge, int>();
            weights[graph[0].IncidentEdges.First()] = 10;
             
        }
    }
}
