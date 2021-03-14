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
            /*Launch_first_sudoku();
            Create_grid();*/
            Console.WriteLine("Grille créée !");
        }
        /*
        // Lire un sudoku dans un fichier json
        private void button2_Click(object sender, EventArgs e)
        {
            string json_path = $"C:\\Users\\riwan\\source\\repos\\chourboku\\sudoku\\sudokus.json";
            *//*var reader = new StreamReader(json_path);*/
        /*         StreamReader file = new StreamReader(json_path);*/
        /*string jsonString = */

        /*            using (StreamReader file = File.OpenText(@"C:\Users\riwan\source\repos\chourboku\sudoku"))
                    {
                        string jsonFromFile = StreamReader.ReadToEnd();
                    }
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);
                    }*//*
        string jsonFromFile;
        using (var reader = new StreamReader(json_path))
        {
            jsonFromFile = reader.ReadToEnd(); 
        }
        *//*Console.WriteLine(jsonFromFile);*//*
        var j_object = (JObject)JsonConvert.DeserializeObject(jsonFromFile);
        Console.WriteLine(j_object);

        var a_sudoku = j_object["RawSudoku"].First;
        List<string> string_sudoku = new List<string>();

*//*            Console.WriteLine(a_sudoku.GetType());
            Console.WriteLine(a_sudoku);*//*

        }*/

        private void launcher_resolution_Click(object sender, EventArgs e)
        {
            bool solved_sudoku = agent_sudoku.BacktrackingSearch();

            Console.WriteLine("Sudoku résolu ? " + solved_sudoku + " mesure de performance : " + agent_sudoku.performance_measure);
            Create_grid();



            /*            int[,] first_sudoku = new int[,] { {0,9,0,8,6,5,2,0,0},
                                                                                                      {0,0,5,0,1,2,0,6,8},
                                                                                                      {0,0,0,0,0,0,0,4,0},
                                                                                                      {0,0,0,0,0,8,0,5,6},
                                                                                                      {0,0,8,0,0,0,4,0,0},
                                                                                                      {4,5,0,9,0,0,0,0,0},
                                                                                                      {0,8,0,0,0,0,0,0,0},
                                                                                                      {2,4,0,1,7,0,5,0,0},
                                                                                                      {0,0,7,2,8,3,0,9,0}};
                        CSP csp = new CSP(first_sudoku);

                        csp.Display_csp_element();*/


            /*csp.Get_a_list_of_all_binary_constraints();*/
            /*csp.Binary_initial_constraint_maker(Tuple.Create(1, 1), first_sudoku);*/
            /*csp.Test_mini_grid_constraints(1, 0, 0, first_sudoku);*/


            /*         Agent an_agent = new Agent();
                     an_agent.Initialize_assignement(first_sudoku);*/

            /*            CSP csp = new CSP(first_sudoku);
                        Console.WriteLine("csp : " + csp.variables[8].domain.Count);
                        CSP another_csp = new CSP();
                        another_csp = csp;
                        Console.WriteLine("another_csp : " + another_csp.variables[8].domain.Count);*/

            /*            an_agent.Ac_3();*/

        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
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

            int rows = (int) Math.Sqrt(sudoku_SS.Count);

            int[,] sudokuArray = new int[rows, rows];

            int k = 0;

            for (int i=0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    sudokuArray[i, j] = (int)sudoku_SS[k] - 48;
                    k++;
                }
            }

            return sudokuArray;  
        }

        public void panel1_DragDrop(object sender, DragEventArgs e)
        {
            var file = e.Data.GetData(DataFormats.FileDrop);
            string[] filePath = file as string[];

            string path = File.ReadAllText(filePath[0]); //drop sudoku

            int[,] grid_sudoku = SS_File_Converter(path); //Conversion int[,]

            agent_sudoku.Initialize_assignement(grid_sudoku);

            Create_grid();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
