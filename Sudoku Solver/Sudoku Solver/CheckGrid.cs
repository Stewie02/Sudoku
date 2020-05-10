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
        public static bool Check(TextBox[,] textBoxes)
        {
            if (!CheckRows(textBoxes))
                return false;
            if (!CheckColumns(textBoxes))
                return false;
            if (!CheckBlocks(textBoxes))
                return false;
            return true;
        }

        private static bool CheckRows(TextBox[,] textBoxes)
        {
            for (int i = 0; i < 9; i++)
            {
                List<int> numbers = new List<int>();
                for (int j = 0; j < 9; j++)
                {
                    if (textBoxes[i, j].Text != "")
                    {
                        int number = Convert.ToInt32(textBoxes[i, j].Text);
                        numbers.Add(number);
                    }
                }
                if (!SameNumberInList(numbers))
                    return false;
            }
            return true;
        }

        private static bool CheckColumns(TextBox[,] textBoxes)
        {
            for (int i = 0; i < 9; i++)
            {
                List<int> numbers = new List<int>();
                for (int j = 0; j < 9; j++)
                {
                    if (textBoxes[j, i].Text != "")
                    {
                        int number = Convert.ToInt32(textBoxes[j, i].Text);
                        numbers.Add(number);
                    }
                }
                if (!SameNumberInList(numbers))
                    return false;
            }
            return true;
        }

        private static bool CheckBlocks(TextBox[,] textBoxes)
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
                            if (textBoxes[row, column].Text != "")
                            {
                                numbers.Add(Convert.ToInt32(textBoxes[row, column].Text));
                            }
                            //try
                            //{
                            //    numbers.Add(Convert.ToInt32(textBoxes[row, column].Text));
                            //}
                            //catch (Exception e) { }
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
