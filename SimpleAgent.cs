using System;
using System.Linq;

namespace eight_puzzle
{
    public class SimpleAgent : IAgent
    {
        Random rng = new Random();
        public int GetMove(Board board)
        {
            return board.GetLegalMoves().OrderBy(_ => rng.Next()).First();
        }
    }
}