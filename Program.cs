using System;
using System.Collections.Generic;
using System.Linq;

namespace eight_puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            IAgent agent = new GreedyAgent();
            var board = new Board();
            board.Print();
            var rng = new Random();
            while (!board.IsSolved())
            {
                board.Move(agent.GetMove(board));
            }
            System.Console.WriteLine("====SOLVED====");
            board.Print();
        }
    }
}
