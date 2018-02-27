namespace eight_puzzle
{
    public class GraphNode
    {
        public Board Board {get;set;}
        public GraphNode Parent{get;set;}
        public int Move {get;set;}
        public GraphNode(Board board, int move, GraphNode parent)
        {
            this.Board = board;
            this.Parent = parent;
            this.Move = move;
        }

        public int GetHeuristic()
        {
            return Board.GetManhattanDistance();
        }
    }
}