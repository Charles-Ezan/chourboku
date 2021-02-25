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
        /*        private void Start_Game(object sender, EventArgs e)
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
                            Case new_case = new Case(grid[column, row]);
                            cases[column, row] = new_case;
                        }
                    }
                }*/
        private void Start_Game(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphic = grid.CreateGraphics();
            Pen pen = new Pen(Brushes.Black, 1);
            Font font = new Font("Arial",10);


            int line_number = 10;
            float x = 0f;
            float y = 0f;

            // Taille des case
            float size = 20f;

            // lignes verticales
            for (int i = 0; i < line_number; i++)
            {
                graphic.DrawLine(pen, x, 0, x, line_number*size-size);
                x += size;
            }

            // lignes horizontales
            for (int i = 0; i < line_number; i++)
            {
                graphic.DrawLine(pen, 0, y, line_number*size-size, y);
                y += size;
            }
        }
    }
}
