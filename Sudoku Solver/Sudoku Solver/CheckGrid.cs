using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku_Solver
{
    public static class CheckGrid
    {

        private static int size = 9;

        public static bool IsSolved(int[,] sudoku)
        {
            foreach (int number in sudoku)
            {
                if (number == -1)
                    return false;
            }
            return true;
        }

        public static bool Check(int[,] sudoku)
        {
            if (!CheckRows(sudoku))
                return false;
            if (!CheckColumns(sudoku))
                return false;
            if (!CheckBlocks(sudoku))
                return false;
            return true;
        }

        private static bool CheckRows(int[,] sudoku)
        {
            for (int i = 0; i < size; i++)
            {
                List<int> numbers = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    if (sudoku[i, j] != -1)
                    {
                        int number = Convert.ToInt32(sudoku[i, j]);
                        numbers.Add(number);
                    }
                }
                if (!SameNumberInList(numbers))
                    return false;
            }
            return true;
        }

        private static bool CheckColumns(int[,] sudoku)
        {
            int random = new Random().Next(1,1000000);
            for (int i = 0; i < size; i++)
            {
                List<int> numbers = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    if (sudoku[j, i] != -1)
                    {
                        int number = sudoku[j, i];
                        numbers.Add(number);
                    }
                }
                if (!SameNumberInList(numbers))
                    return false;
            }
            return true;
        }

        private static bool CheckBlocks(int[,] sudoku)
        {
            int squareRoot = (int)Math.Sqrt(size);
            for (int i = 0; i < squareRoot; i++)
            {
                for (int j = 0; j < squareRoot; j++)
                {
                    List<int> numbers = new List<int>();
                    for (int row = i * squareRoot; row < i * squareRoot + squareRoot; row++)
                    {
                        for (int column = j * squareRoot; column < j * squareRoot + squareRoot; column++)
                        {
                            if (sudoku[row, column] != -1)
                            {
                                numbers.Add(sudoku[row, column]);
                            }
                        }
                    }

                    if (!SameNumberInList(numbers))
                        return false;
                }
            }
            return true;
        }

        private static bool SameNumberInList(List<int> numbers)
        {
            numbers.Sort();
            int previous = -1;
            foreach (int number in numbers)
            {
                if (previous == number)
                    return false;
                previous = number;
            }
            return true;
        }
    }
}
