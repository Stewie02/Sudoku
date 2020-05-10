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
        private int size = 4;
        //private TextBox[,] textBoxes;
        //private Stack<TextBox[,]> previousTextBoxes = new Stack<TextBox[,]>();

        public Solver()
        {
            //this.textBoxes = textBoxes;
        }

        public bool Solve(int[,] _sudoku)
        {
            //previousTextBoxes.Push((TextBox[,]) textBoxes.Clone());
            int[,] sudoku = new int[size, size];
            sudoku = (int[,])_sudoku.Clone();

            int row, column = 0;

            int number = 0;
            for (row = 0; number != -1 && row < size; ++row)
            {
                for (column = 0; number != -1 && column < size; ++column)
                {
                    number = sudoku[row, column];
                }
            }
            column--;
            row--;
            int i = 1;
            while (i < size + 1)
            {
                sudoku[row, column] = i;
                if (CheckGrid.Check(sudoku))
                {
                    if (Solve(sudoku))
                        return true;
                }

                i++;
            }
            return false;
        }

    }
}