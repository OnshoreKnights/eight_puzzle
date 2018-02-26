using System;
using System.Collections.Generic;
using System.Linq;

namespace eight_puzzle
{
    class Board
    {
        private int[] _board;
        private int _width;
        internal int moves;

        internal Board(int tileCount = 9)
        {
            if (tileCount <= 0)
            {
                throw new ArgumentOutOfRangeException("dim", "Parameter must be postive");
            }
            else if (Math.Sqrt(tileCount) % 1 != 0)
            {
                throw new ArgumentException("Parameter must be a perfect square", "dim");
            }
            _width = (int)Math.Round(Math.Sqrt(tileCount));
            var rng = new Random();
            _board = Enumerable.Range(0, tileCount).OrderBy(_ => rng.Next()).ToArray();
        }

        internal void Move(int move)
        {
            if (!IsLegalMove(move))
            {
                throw new ApplicationException("Illegal move!");
            }
            moves++;
            int zeroIdx = GetEmptySquare();
            // could eliminate temp and use 0 literal, 
            // but this leaves flexibility of setting "empty" to something else
            int temp = _board[zeroIdx];
            _board[zeroIdx] = _board[move];
            _board[move] = temp;
        }

        internal bool IsSolved()
        {
            int last = -1;
            foreach (int tile in _board)
            {
                if (tile != 0)
                {
                    if (tile < last)
                    {
                        return false;
                    }
                    last = tile;
                }
            }
            return true;
        }

        private bool IsLegalMove(int move)
        {
            return GetLegalMoves().Contains(move);
        }

        internal void Print()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (i % _width == 0)
                {
                    if (i != 0)
                    {
                        Console.WriteLine('|');
                    }
                    for (int _ = 0; _ < (_width * 2 + 1); _++)
                    {
                        Console.Write('-');
                    }
                    System.Console.WriteLine();
                }
                Console.Write('|');
                if (_board[i] != 0)
                {
                    Console.Write(_board[i]);
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine('|');
            for (int _ = 0; _ < (_width * 2 + 1); _++)
            {
                Console.Write('-');
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Legal Moves: { " + String.Join(", ", GetLegalMoves()) + " }");
            System.Console.WriteLine();
        }

        private int GetEmptySquare()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i] == 0)
                {
                    return i;
                }
            }
            throw new ApplicationException("Invalid board");
        }

        internal IEnumerable<int> GetLegalMoves()
        {
            var moves = new List<int>();
            int emptyIdx = GetEmptySquare();
            // above
            if (emptyIdx - _width >= 0)
            {
                moves.Add(emptyIdx - _width);
            }
            // below
            if (emptyIdx + _width < _board.Length)
            {
                moves.Add(emptyIdx + _width);
            }
            // left
            if ((emptyIdx - 1) / _width == emptyIdx / _width && emptyIdx != 0)
            {
                moves.Add(emptyIdx - 1);
            }
            // right
            if ((emptyIdx + 1) / _width == emptyIdx / _width)
            {
                moves.Add(emptyIdx + 1);
            }
            return moves;
        }
    }
}