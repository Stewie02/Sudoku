using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Sudoku_Solver
{
    /// <summary>
    /// This class solves a sudoku
    /// </summary>
    class Solver
    {
        private int size = Settings.size;
        private int[,] solution;

        /// <summary>
        /// This constructor initializes the multidimenional array which will be filled with be sudoku solution
        /// </summary>
        public Solver()
        {
            solution = new int[size, size];
;        }

        /// <summary>
        /// This function takes in the sudoku as a parameter
        /// It will search for the first unfilled spot and fills it
        /// Then it will check if the solution is still valid. If so perform a recursive call
        /// Else try another number
        /// </summary>
        /// <param name="_sudoku"></param>
        /// <returns>A boolean based on the fact if the sudoku is solvable</returns>
        public bool Solve(int[,] _sudoku)
        {
            // Make a clone of the array and use it in this function
            int[,] sudoku = new int[size, size];
            sudoku = (int[,])_sudoku.Clone();

            // Initialize a few variables
            int row, column = 0;
            int number = 0;

            // Find the first empty box in the sudoku
            for (row = 0; number != -1 && row < size; row++)
            {
                for (column = 0; number != -1 && column < size; column++)
                {
                    number = sudoku[row, column];
                }
            }
            column--;
            row--;
            int i = 1;
            bool solved = false;

            // Try to solve the sudoku, if it tries to fill in a number greater than the size or it is already solved it stops.
            while (i < size + 1 && !solved)
            {
                sudoku[row, column] = i;
                if (CheckGrid.Check(sudoku))
                {
                    if (CheckGrid.IsSolved(sudoku))
                    {
                        solution = sudoku;
                        return true;
                    }
                    else
                        solved = Solve(sudoku);
                }

                i++;
            }
            // Return if the sudoku is solved or not
            return solved;
        }

        /// <summary>
        /// This function returns the solution of the sudoku
        /// Don't call this function when you didn't perform Solve()!!
        /// </summary>
        /// <returns></returns>
        public int[,] GetSolution()
        {
            return solution;
        }

    }
}