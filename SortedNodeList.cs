using System;
using System.Collections.Generic;
using System.Linq;

namespace eight_puzzle
{
    class SortedNodeList
    {
        private LinkedList<GraphNode> _list = new LinkedList<GraphNode>();
        private HashSet<Board> _set = new HashSet<Board>();
        public int Count
        {
            get
            {
                return _set.Count;
            }
        }

        public HashSet<Board> GetSet()
        {
            return _set;
        }
        public bool TryPop(out GraphNode node)
        {
            if (_list.Count < 1)
            {
                node = null;
                return false;
            }
            node = _list.First.Value;
            _list.RemoveFirst();
            return true;
        }

        public void Push(GraphNode node)
        {
            if (_set.Contains(node.Board))
            {
                return;
            }
            _set.Add(node.Board);

            var current = _list.First;
            while (current != null)
            {
                if (node.GetHeuristic() < current.Value.GetHeuristic())
                {
                    _list.AddBefore(current, node);
                    return;
                }
                current = current.Next;
            }

            // only reachable if worse heuristic than entire list
            _list.AddLast(node);
        }
    }
}