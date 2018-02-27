using System;
using System.Collections.Generic;
using System.Linq;

namespace eight_puzzle
{
    public class GreedyAgent : IAgent
    {
        private Stack<int> plan;

        public GreedyAgent()
        {
            plan = new Stack<int>();
        }

        public int GetMove(Board board)
        {
            int move;
            if (board.IsSolved())
            {
                return 0;
            }
            else if (plan.TryPop(out move))
            {
                return move;
            }
            else
            {
                BuildPlan(board);
            }
            return plan.Pop();
        }

        private void BuildPlan(Board board)
        {
            plan.Clear();
            GraphNode node = Search(board);
            if (node != null)
            {
                while (node.Parent != null)
                {
                    plan.Push(node.Move);
                    node = node.Parent;
                }
            }
        }

        private GraphNode Search(Board board)
        {
            var frontier = new SortedNodeList();
            frontier.Push(new GraphNode(board, 0, null));
            GraphNode node;
            while (frontier.TryPop(out node))
            {
                if (node.Board.IsSolved())
                {
                    Console.WriteLine("Found solution with " + frontier.Count + " nodes explored");
                    return node;
                }
                foreach (var newNode in Explore(node))
                {
                    frontier.Push(newNode);
                }
            }
            return null;
        }

        private IEnumerable<GraphNode> Explore(GraphNode node)
        {
            var newNodes = new List<GraphNode>();
            foreach (int move in node.Board.GetLegalMoves())
            {
                var newBoard = node.Board.Copy();
                // transition
                newBoard.Move(move);
                newNodes.Add(new GraphNode(newBoard, move, node));
            }
            return newNodes;
        }
    }
}