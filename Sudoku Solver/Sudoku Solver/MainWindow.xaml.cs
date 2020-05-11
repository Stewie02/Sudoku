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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int size = Settings.size;
        private TextBox[,] textBoxGrid;
        private Solver solver;
        private bool validSudoku;

        public MainWindow()
        {
            textBoxGrid = new TextBox[size, size];
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
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
                    textBox.FontSize = 60;
                    Grid.SetColumn(textBox, i);
                    Grid.SetRow(textBox, j);
                    textBoxGrid[i, j] = textBox;
                    sudokuGrid.Children.Add(textBox);
                }
            }
        }

        private bool IsValidGrid(int[,] sudoku)
        {
            if(!CheckGrid.Check(sudoku))
            {
                MessageBox.Show("De sudoku is niet oplosbaar");
                return false;
            }
            return true;
        }

        private void Solve(object sender, RoutedEventArgs e)
        {
            int[,] sudoku = GetSudokuArray(textBoxGrid);
            if (validSudoku)
            {
                if (IsValidGrid(sudoku))
                {
                    solver = new Solver();
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

        private void ShowSolution()
        {
            int[,] solution = solver.GetSolution();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if((bool)isNumeric.IsChecked)
                        textBoxGrid[i, j].Text = solution[i, j].ToString();
                    else
                        textBoxGrid[i, j].Text = ((char)(solution[i, j] + 64)).ToString();
                }
            }
        }

        private int[,] GetSudokuArray(TextBox[,] textBoxGrid)
        {
            int[,] sudoku = new int[size, size];
            validSudoku = true;
            for (int i = 0; i < size && validSudoku; i++)
            {
                for (int j = 0; j < size && validSudoku; j++)
                {
                    if ((bool)isNumeric.IsChecked)
                    {
                        try
                        {
                            if (textBoxGrid[i, j].Text == "")
                                sudoku[i, j] = -1;
                            else if (Convert.ToInt32(textBoxGrid[i, j].Text) > 0 && Convert.ToInt32(textBoxGrid[i, j].Text) < Settings.size + 1)
                                sudoku[i, j] = Convert.ToInt32(textBoxGrid[i, j].Text);
                            else
                            {
                                validSudoku = false;
                                MessageBox.Show("De getallen moeten tussen de 0 en de " + (Settings.size + 1) + " vallen");
                            }
                        }
                        catch (FormatException e)
                        {
                            validSudoku = false;
                            MessageBox.Show("De sudoku mag alleen cijfers bevaten");
                        }
                    }
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