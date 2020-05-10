using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku_Solver
{
    class Solver
    {
        private int size = 9;
        private int[,] solution;

        public Solver()
        {
            solution = new int[size, size];
;        }

        public bool Solve(int[,] _sudoku, int counter)
        {
            int[,] sudoku = new int[size, size];
            sudoku = (int[,])_sudoku.Clone();

            Console.WriteLine(counter);

            int row, column = 0;

            int number = 0;
            for (row = 0; number != -1 && row < size; row++)
            {
                for (column = 0; number != -1 && column < size; column++)
                {
                    number = sudoku[row, column];
                    Console.WriteLine("Row:" + row + " Col: " + column + " Value: " + number + "    Counter: " + counter);
                }
            }
            column--;
            row--;
            int i = 1;
            bool solved = false;

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
                        solved = Solve(sudoku, counter + 1);
                }

                i++;
            }
            return solved;
        }

        public int[,] GetSolution()
        {
            return solution;
        }

    }
}