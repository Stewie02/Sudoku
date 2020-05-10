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

        //private TextBox[,] textBoxes;
        //private Stack<TextBox[,]> previousTextBoxes = new Stack<TextBox[,]>();

        public Solver(TextBox[,] textBoxes)
        {
            //this.textBoxes = textBoxes;
        }

        public bool Solve(TextBox[,] _textBoxes)
        {
            //previousTextBoxes.Push((TextBox[,]) textBoxes.Clone());
            //TextBox[,] textBoxes = new TextBox[9, 9];
            //textBoxes = (TextBox[,]) _textBoxes.Clone();

            TextBox[,] textBoxes = new TextBox[9, 9];
            int minX = Math.Min(_textBoxes.GetLength(0), textBoxes.GetLength(0));
            int minY = Math.Min(_textBoxes.GetLength(1), textBoxes.GetLength(1));

            for (int a = 0; a < minX; ++a)
                Array.Copy(_textBoxes, a * _textBoxes.GetLength(1), textBoxes, a * textBoxes.GetLength(1), minY);


            int row, column = 0;

            string number = "-1";
            for (row = 0; number != "" && row < 9; ++row)
            {
                for (column = 0; number != "" && column < 9; ++column)
                {
                    number = textBoxes[row, column].Text;
                    Console.WriteLine("Row: " + row + " Col: " + column + " Value: " + number);
                }
            }
            column--;
            row--;
            int i = 1;
            while (i < 10)
            {
                textBoxes[row, column].Text = Convert.ToString(i);
                if (CheckGrid.Check(textBoxes))
                {
                    Console.WriteLine("Hij is tot zo ver oplosbaar");
                    if (Solve(textBoxes))
                        return true;
                }

                i++;
            }
            return false;
        }

    }
}