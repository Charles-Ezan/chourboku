using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public partial class Form1 : Form
    {
        Cell[,] cases = new Cell[10, 10];
        public Form1()
        {
            InitializeComponent();
        }

        //Creation de la grille
        private void Start_Game(object sender, EventArgs e)
        {
            for(int i=0; i<10; i++)
            {
                grid.Rows.Add();
            }

            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Width = 80;
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Height = 80;
            }

            for(int column=0; column < grid.Columns.Count; column++)
            {
                for (int row = 0; row < grid.Rows.Count; row++)
                {
                    Cell new_case = new Cell(grid[column, row]);
                    cases[column, row] = new_case;
                }
            }
        }

    }
}
