using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku_Solver
{
    /// <summary>
    /// This class handles all interaction with the user
    /// It also makes sure the given sudoku is in the right form
    /// </summary>
    public partial class MainWindow : Window
    {
        private int size = Settings.size;
        private TextBox[,] textBoxGrid;
        private Solver solver;
        private bool validSudoku;

        /// <summary>
        /// This function is called when the application starts
        /// </summary>
        public MainWindow()
        {
            textBoxGrid = new TextBox[size, size];
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// This function adds some inputboxes and a some rows/column to the sudokuGrid
        /// </summary>
        public void Initialize()
        {
            int fontSize;
            if (Settings.size > 9)
                fontSize = 30;
            else
                fontSize = 60;

            for (int i = 0; i < size; i++)
            {
                sudokuGrid.RowDefinitions.Add(new RowDefinition());
                sudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Text = "";
                    textBox.Width = 700 / size;
                    textBox.Height = 700 / size;
                    textBox.TextAlignment = TextAlignment.Center;
                    textBox.FontSize = fontSize;
                    Grid.SetColumn(textBox, i);
                    Grid.SetRow(textBox, j);
                    textBoxGrid[i, j] = textBox;
                    sudokuGrid.Children.Add(textBox);
                }
            }
        }

        /// <summary>
        /// This function takes in a sudoku and checks if it's valid and returns a boolean based on the result
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns>true if the sudoku is valid</returns>
        private bool IsValidGrid(int[,] sudoku)
        {
            if(!CheckGrid.Check(sudoku))
            {
                MessageBox.Show("De sudoku is niet oplosbaar");
                return false;
            }
            return true;
        }

        /// <summary>
        /// This function is called when the user presses the button to solve the sudoku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Solve(object sender, RoutedEventArgs e)
        {
            // Get the sudoku array
            int[,] sudoku = GetSudokuArray(textBoxGrid);

            // Check if the sudoku didn't contains strange input, if not go on
            if (validSudoku)
            {
                if (IsValidGrid(sudoku))
                {
                    solver = new Solver();
                    // If the solver solved the sudoku show the solution and display it in a messagebox
                    if (solver.Solve(sudoku))
                    {
                        ShowSolution();
                        MessageBox.Show("De sudoku is opgelost!");
                    }
                    else
                    {
                        MessageBox.Show("Niet oplosbaar");
                    }
                }
            }
        }

        /// <summary>
        /// This function gets the solution from the solver class and iterates over the array and fills the textboxes
        /// </summary>
        private void ShowSolution()
        {
            int[,] solution = solver.GetSolution();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // If the sudoku only contained numbers...
                    if((bool)isNumeric.IsChecked)
                        textBoxGrid[i, j].Text = solution[i, j].ToString();
                    // If it contained characters calculate the character with help of the ascii table
                    else
                        textBoxGrid[i, j].Text = ((char)(solution[i, j] + 64)).ToString();
                }
            }
        }

        /// <summary>
        /// This function takes in the textboxgrid and returns a valid multidimensional sudoku array which the Solver class needs
        /// </summary>
        /// <param name="textBoxGrid"></param>
        /// <returns></returns>
        private int[,] GetSudokuArray(TextBox[,] textBoxGrid)
        {
            int[,] sudoku = new int[size, size];
            validSudoku = true;

            // Iterate over all textboxes
            for (int i = 0; i < size && validSudoku; i++)
            {
                for (int j = 0; j < size && validSudoku; j++)
                {
                    // If the sudoku should only contain numbers...
                    if ((bool)isNumeric.IsChecked)
                    {
                        try
                        {
                            // If the text property of the textbox is empty the value will be -1
                            if (textBoxGrid[i, j].Text == "")
                                sudoku[i, j] = -1;
                            // Check if the number is in the range for the sudoku size
                            else if (Convert.ToInt32(textBoxGrid[i, j].Text) > 0 && Convert.ToInt32(textBoxGrid[i, j].Text) < Settings.size + 1)
                                sudoku[i, j] = Convert.ToInt32(textBoxGrid[i, j].Text);
                            else
                            {
                                // If the code is here the sudoku is not valid for sure
                                validSudoku = false;
                                MessageBox.Show("De getallen moeten tussen de 0 en de " + (Settings.size + 1) + " vallen");
                            }
                        }
                        catch (FormatException e)
                        {
                            // When there is an error caught you know the user filled in another character then a number
                            validSudoku = false;
                            MessageBox.Show("De sudoku mag alleen cijfers bevaten");
                        }
                    }
                    // The sudoku should contain letters
                    else
                    {
                        if (textBoxGrid[i, j].Text == "")
                            sudoku[i, j] = -1;
                        else if ((int)textBoxGrid[i, j].Text.ToUpper()[0] > 64 && (int)textBoxGrid[i, j].Text.ToUpper()[0] < 65 + Settings.size)
                            sudoku[i, j] = (int)textBoxGrid[i, j].Text.ToUpper()[0] - 64;
                        else
                        {
                            validSudoku = false;
                            MessageBox.Show("De letters moeten tussen de A en de " + (char)(65 + Settings.size) + " vallen");
                        }
                    }
                    

                }
            }
            return sudoku;
        }
    }
}