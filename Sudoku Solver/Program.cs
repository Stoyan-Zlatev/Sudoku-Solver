using System;
using System.Linq;
using System.Collections.Generic;

namespace Sudoku_Solver
{
    class Program
    {
        public static int[,] board = new int[9, 9];
        public static Dictionary<Tuple<int, int>, List<int>> possibilites = new Dictionary<Tuple<int, int>, List<int>>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 9; i++)
            {
                var input = Console.ReadLine();
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = int.Parse(input[j].ToString());
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    possibilites.Add(new Tuple<int, int>(i, j), new List<int>());
                    if (board[i, j] == 0)
                    {
                        FindPossibilites(i, j, numbers);
                    }
                }
            }

            Solve(0, 0);
        }

        public static bool IsPossible(int row, int col, int index)
        {
            for (int i = 0; i < 9; i++)
            {
                //row
                if (board[row, i] == possibilites[new Tuple<int, int>(row, col)][index])
                {
                    return false;
                }
                //col
                if (board[i, col] == possibilites[new Tuple<int, int>(row, col)][index])
                {
                    return false;
                }
            }

            //Current Square
            int row1 = row / 3 * 3;
            int col1 = col / 3 * 3;
            for (int i = row1; i < row1 + 3; i++)
            {
                for (int j = col1; j < col1 + 3; j++)
                {
                    if (board[i, j] == possibilites[new Tuple<int, int>(row, col)][index])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static void PrintBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void Solve(int row, int col)
        {
            if (col == 9)
            {
                Solve(row + 1, 0);
                return;
            }

            if (row == 9)
            {
                PrintBoard();
                Console.WriteLine();
                return;
            }

            if (board[row, col] == 0)
            {
                for (int i = 0; i < possibilites[new Tuple<int, int>(row, col)].Count(); i++)
                {
                    if (IsPossible(row, col, i))
                    {
                        board[row, col] = possibilites[new Tuple<int, int>(row, col)][i];
                        Solve(row, col + 1);
                        board[row, col] = 0;
                    }
                }
                return;
            }
            else
            {
                Solve(row, col + 1);
            }
        }

        public static void FindPossibilites(int row, int col, List<int> numbers)
        {
            for (int i = 0; i < 9; i++)
            {
                //row
                if (numbers.Contains(board[row, i]))
                {
                    numbers.Remove(board[row, i]);
                }
                //col
                if (numbers.Contains(board[i, col]))
                {
                    numbers.Remove(board[i, col]);
                }
            }

            //Current Square
            int row1 = row / 3 * 3;
            int col1 = col / 3 * 3;
            for (int i = row1; i < row1 + 3; i++)
            {
                for (int j = col1; j < col1 + 3; j++)
                {
                    if (numbers.Contains(board[i, j]))
                    {
                        numbers.Remove(board[i, j]);
                    }
                }
            }

            possibilites[new Tuple<int, int>(row, col)] = numbers.ToList();
        }
    }
}
