﻿using System;
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

        private TextBox[,] textBoxGrid = new TextBox[9,9];
        private Solver solver;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < 9; i++)
            {
                sudokuGrid.RowDefinitions.Add(new RowDefinition());
                sudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Text = "";
                    textBox.Width = 700 / 9;
                    textBox.Height = 700 / 9;
                    textBox.TextAlignment = TextAlignment.Center;
                    textBox.FontSize = 60;
                    Grid.SetColumn(textBox, i);
                    Grid.SetRow(textBox, j);
                    textBoxGrid[i, j] = textBox;
                    sudokuGrid.Children.Add(textBox);
                }
            }
        }

        private bool IsValidGrid()
        {
            foreach(TextBox textBox in textBoxGrid)
            {
                if(textBox.Text != "")
                {
                    try
                    {
                        int number = Convert.ToInt32(textBox.Text);
                        if(number < 1 || number > 9)
                        {
                            MessageBox.Show("Het getal moet tussen de 0 en 10 zitten.");
                            return false;
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("De sudoku mag alleen cijfers bevaten");
                        return false;
                    }
                }
            }
            if(!CheckGrid.Check(textBoxGrid))
            {
                MessageBox.Show("De sudoku is niet oplosbaar");
                return false;
            }
            return true;
        }

        private void Solve(object sender, RoutedEventArgs e)
        {
            if (IsValidGrid())
            {
                solver = new Solver(textBoxGrid);
                if (solver.Solve(textBoxGrid))
                {
                    MessageBox.Show("Solvable");
                }
                else
                {
                    MessageBox.Show("Niet oplosbaar");
                }
            }
        }
    }
}