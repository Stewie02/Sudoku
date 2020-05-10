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
            for (int i = 0; i < 9; i++)
            {
                List<int> numbers = new List<int>();
                for (int j = 0; j < 9; j++)
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
            for (int i = 0; i < 9; i++)
            {
                List<int> numbers = new List<int>();
                for (int j = 0; j < 9; j++)
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
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    List<int> numbers = new List<int>();
                    for (int row = i * 3; row < i * 3 + 3; row++)
                    {
                        for (int column = j * 3; column < j * 3 + 3; column++)
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
