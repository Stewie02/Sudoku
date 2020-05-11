using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku_Solver
{
    /// <summary>
    /// This static class contains a few methods to check if the sudoku is still valid and one to check if it's solved
    /// </summary>
    public static class CheckGrid
    {

        private static int size = Settings.size;

        /// <summary>
        /// This function checks if the sudoku is solved or not.
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns>true if the sudoku is solved, else false</returns>
        public static bool IsSolved(int[,] sudoku)
        {
            // Check if the sudoku array contains any -1's. If so it isn't solved
            foreach (int number in sudoku)
            {
                if (number == -1)
                    return false;
            }
            // Return if the sudoku has any errors
            return Check(sudoku);
        }

        /// <summary>
        /// This function calls a few funtions to check if the sudoku is valid until now
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns>true if the sudoku is valid until now</returns>
        public static bool Check(int[,] sudoku)
        {
            if (!CheckRowsColumns(sudoku))
                return false;
            if (!CheckBlocks(sudoku))
                return false;
            return true;
        }

        /// <summary>
        /// This function goes through all rows and columns and checks if those doesn't contains a value twice 
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns>true if there isn't a value twice</returns>
        private static bool CheckRowsColumns(int[,] sudoku)
        {
            for (int i = 0; i < size; i++)
            {
                List<int> rowNumbers = new List<int>();
                List<int> columnNumbers = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    if (sudoku[i, j] != -1)
                    {
                        int rowNumber = sudoku[i, j];
                        rowNumbers.Add(rowNumber);
                    }
                    if (sudoku[j, i] != -1)
                    {
                        int columnNumber = sudoku[j, i];
                        columnNumbers.Add(columnNumber);
                    }
                }
                if (SameNumberInList(rowNumbers) || SameNumberInList(columnNumbers))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// This function checks if the sudoku has a number twice in a block
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns>true if there isn't a value twice</returns>
        private static bool CheckBlocks(int[,] sudoku)
        {
            int squareRoot = (int)Math.Sqrt(size);
            // Go through all blocks and put the values in a list and check if occurs more than once
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

                    if (SameNumberInList(numbers))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This function takes in a list and checks if it contains a number twice
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>true if a number occurs more than once</returns>
        private static bool SameNumberInList(List<int> numbers)
        {
            numbers.Sort();
            int previous = -1;
            foreach (int number in numbers)
            {
                if (previous == number)
                    return true;
                previous = number;
            }
            return false;
        }
    }
}
