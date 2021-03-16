using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Ajout
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sudoku
{
    public partial class Form1 : Form
    {
        private Agent agent_sudoku = new Agent();

        public Form1()
        {
            InitializeComponent();
        }


        public void Launch_first_sudoku()
        {
            int[,] second_sudoku = new int[,]
            {

                { 0, 0, 8, 0, 0, 0, 5, 0, 0},

                { 6, 0, 0, 7, 0, 5, 0, 0, 3 },

                { 0, 9, 0, 8, 3, 2, 0, 0, 0 },

                { 0, 0, 4, 0, 1, 0, 0, 0, 0 },

                { 3, 8, 0, 4, 0, 7, 0, 5, 1 },

                { 0, 0, 0, 0, 8, 0, 2, 0, 0 },

                { 0, 0, 0, 1, 5, 9, 0, 7, 0 },

                { 8, 0, 0, 3, 0, 4, 0, 0, 5 },

                { 0, 0, 9, 0, 0, 0, 1, 0, 0 }
            };


            int[,] third_sudoku = new int[,]
{

                { 0,0,0,0,0,4,6,7,8},

                { 0,0,0,9,0,0,0,0,4},

                { 0,0,7,0,0,6,1,9,0},

                { 0,9,8,7,6,0,0,0,2},

                { 0,0,0,0,0,0,0,0,0},

                { 6,0,0,0,3,2,9,1,0},

                { 0,8,2,6,0,0,7,0,0 },

                { 7,0,0,0,0,3,0,0,0 },

                { 9,5,6,4,0,0,0,0,0}
};

            agent_sudoku.Initialize_assignement(third_sudoku);
        }

        public void Create_grid()
        {
            // Clear the grid
            grid.Refresh();

            // Création de la grille visuelle
            Graphics graphic = grid.CreateGraphics();
            Pen effective_pen = new Pen(Brushes.Black, 1);
            Pen pen = new Pen(Brushes.Black, 1);
            Pen grass_pen = new Pen(Brushes.Black, 3);
            Font font = new Font("Arial", 10);


            int line_number = 10;
            float x = 0f;
            float y = 0f;

            // Taille des case
            float size = 20f;

            // lignes verticales
            for (int i = 0; i < line_number; i++)
            {
                if (i % 3 == 0)
                    effective_pen = grass_pen;

                else
                    effective_pen = pen;
                graphic.DrawLine(effective_pen, x, 0, x, line_number * size - size);
                x += size;
            }

            // lignes horizontales
            for (int i = 0; i < line_number; i++)
            {

                if (i % 3 == 0)
                    effective_pen = grass_pen;

                else
                    effective_pen = pen;


                graphic.DrawLine(effective_pen, 0, y, line_number * size - size, y);
                y += size;
            }

            x = 0f;
            y = 0f;

            // Charger un sudoku dans le tableau
            for (int k = 0; k < line_number - 1; k++)
            {
                for (int n = 0; n < line_number - 1; n++)
                {
                    graphic.DrawString(agent_sudoku.Get_asssignement().sudoku[k, n].ToString(), font, Brushes.Black, x, y);
                    x += size;
                }
                x = 0f;
                y += size;
            }
        }

        // Lancer le sudoku
        private void button1_Click(object sender, EventArgs e)
        {
            Launch_first_sudoku();
            Create_grid();
            Console.WriteLine("Grille créée !");
        }

        private void launcher_resolution_Click(object sender, EventArgs e)
        {
            // Optimisations utilisées
            agent_sudoku.optimisation_used[0] = ac3_value.Checked;
            agent_sudoku.optimisation_used[1] = mrv_value.Checked;
            agent_sudoku.optimisation_used[2] = degree_heuristic_value.Checked;
            agent_sudoku.optimisation_used[3] = least_constraining_value.Checked;

            var watch = System.Diagnostics.Stopwatch.StartNew(); // timer

            bool solved_sudoku = agent_sudoku.BacktrackingSearch();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            sudoku_resolved_value.Text = solved_sudoku.ToString();


            recursive_call_value.Text = agent_sudoku.performance_measure.ToString();

            resolution_time_value.Text = elapsedMs.ToString() + " ms";

            Create_grid();
        }

        // Fonction pour Drag and Drop un sudoku au format ".ss"
        private void grid_DragEnter(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (Is_SS_Filename(file) == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private bool Is_SS_Filename(string[] file)
        {
            if ((file.Length == 1) && (Path.GetExtension(file[0]) == ".ss"))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Wrong format or multiple files");
                return false;
            }
        }

        public int[,] SS_File_Converter(string Sudoku_SS)
        {
            List<char> sudoku_SS = Sudoku_SS.ToList();

            Console.WriteLine(sudoku_SS);

            for (var i = 0; i < sudoku_SS.Count; i++)
            {
                if (sudoku_SS[i] == '.')
                {
                    sudoku_SS[i] = '0';
                }

                if (sudoku_SS[i] == '-' || sudoku_SS[i] == '!')
                {
                    sudoku_SS[i] = ' ';
                }
            }

            sudoku_SS.RemoveAll(item => item == ' ');
            sudoku_SS.RemoveAll(item => item == '\n');

            int rows = (int)Math.Sqrt(sudoku_SS.Count);

            int[,] sudokuArray = new int[rows, rows];

            int k = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    sudokuArray[i, j] = (int)sudoku_SS[k] - 48;
                    k++;
                }
            }

            return sudokuArray;
        }

        public void grid_DragDrop(object sender, DragEventArgs e)
        {
            var file = e.Data.GetData(DataFormats.FileDrop);
            string[] filePath = file as string[];

            string path = File.ReadAllText(filePath[0]); //drop sudoku

            int[,] grid_sudoku = SS_File_Converter(path); //Conversion int[,]

            agent_sudoku.Initialize_assignement(grid_sudoku);

            Create_grid();
        }
    }
}
