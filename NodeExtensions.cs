using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greedy
{
    static class NodeExtensions
    {
        public static IEnumerable<Node> BreadthSearch (this Node startNode)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(startNode);
            var visited = new HashSet<Node>();
            while (queue.Count!=0)
            {
                var node = queue.Dequeue();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return node;
                foreach (var nextNode in node.IncidentNodes)
                    queue.Enqueue(nextNode);
            }
        }

        public static IEnumerable<Node> DepthSearch(this Node startNode)
        {
            var stack = new Stack<Node>();
            stack.Push(startNode);
            var visited = new HashSet<Node>();
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return node;
                foreach (var nextNode in node.IncidentNodes)
                    stack.Push(nextNode);
            }
        }
    }
}
