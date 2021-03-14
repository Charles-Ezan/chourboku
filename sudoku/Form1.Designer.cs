
using System.Windows.Forms;

namespace sudoku
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.grid = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.launcher_resolution = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sudoku_resolved_value = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.recursive_call_value = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.resolution_time_value = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ac3_value = new System.Windows.Forms.CheckBox();
            this.mrv_value = new System.Windows.Forms.CheckBox();
            this.degree_heuristic_value = new System.Windows.Forms.CheckBox();
            this.least_constraining_value = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Location = new System.Drawing.Point(29, 21);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(515, 317);
            this.grid.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(117, 361);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(220, 361);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // launcher_resolution
            // 
            this.launcher_resolution.Location = new System.Drawing.Point(332, 361);
            this.launcher_resolution.Name = "launcher_resolution";
            this.launcher_resolution.Size = new System.Drawing.Size(150, 23);
            this.launcher_resolution.TabIndex = 3;
            this.launcher_resolution.Text = "Launch Resolution";
            this.launcher_resolution.UseVisualStyleBackColor = true;
            this.launcher_resolution.Click += new System.EventHandler(this.launcher_resolution_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resolution_time_value);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sudoku_resolved_value);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.recursive_call_value);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(560, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 163);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mesure de performance";
            // 
            // sudoku_resolved_value
            // 
            this.sudoku_resolved_value.AutoSize = true;
            this.sudoku_resolved_value.Location = new System.Drawing.Point(130, 31);
            this.sudoku_resolved_value.Name = "sudoku_resolved_value";
            this.sudoku_resolved_value.Size = new System.Drawing.Size(13, 13);
            this.sudoku_resolved_value.TabIndex = 3;
            this.sudoku_resolved_value.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sudoku résolu ?";
            // 
            // recursive_call_value
            // 
            this.recursive_call_value.AutoSize = true;
            this.recursive_call_value.Location = new System.Drawing.Point(130, 59);
            this.recursive_call_value.Name = "recursive_call_value";
            this.recursive_call_value.Size = new System.Drawing.Size(13, 13);
            this.recursive_call_value.TabIndex = 1;
            this.recursive_call_value.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre d\'appel récursif";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Temps de résolution";
            // 
            // resolution_time_value
            // 
            this.resolution_time_value.AutoSize = true;
            this.resolution_time_value.Location = new System.Drawing.Point(130, 84);
            this.resolution_time_value.Name = "resolution_time_value";
            this.resolution_time_value.Size = new System.Drawing.Size(13, 13);
            this.resolution_time_value.TabIndex = 5;
            this.resolution_time_value.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.least_constraining_value);
            this.groupBox2.Controls.Add(this.degree_heuristic_value);
            this.groupBox2.Controls.Add(this.mrv_value);
            this.groupBox2.Controls.Add(this.ac3_value);
            this.groupBox2.Location = new System.Drawing.Point(560, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 163);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optimisations";
            // 
            // ac3_value
            // 
            this.ac3_value.AutoSize = true;
            this.ac3_value.Location = new System.Drawing.Point(25, 42);
            this.ac3_value.Name = "ac3_value";
            this.ac3_value.Size = new System.Drawing.Size(48, 17);
            this.ac3_value.TabIndex = 0;
            this.ac3_value.Text = "Ac-3";
            this.ac3_value.UseVisualStyleBackColor = true;
            // 
            // mrv_value
            // 
            this.mrv_value.AutoSize = true;
            this.mrv_value.Location = new System.Drawing.Point(25, 65);
            this.mrv_value.Name = "mrv_value";
            this.mrv_value.Size = new System.Drawing.Size(50, 17);
            this.mrv_value.TabIndex = 1;
            this.mrv_value.Text = "MRV";
            this.mrv_value.UseVisualStyleBackColor = true;
            // 
            // degree_heuristic_value
            // 
            this.degree_heuristic_value.AutoSize = true;
            this.degree_heuristic_value.Location = new System.Drawing.Point(25, 88);
            this.degree_heuristic_value.Name = "degree_heuristic_value";
            this.degree_heuristic_value.Size = new System.Drawing.Size(105, 17);
            this.degree_heuristic_value.TabIndex = 2;
            this.degree_heuristic_value.Text = "Degree Heuristic";
            this.degree_heuristic_value.UseVisualStyleBackColor = true;
            // 
            // least_constraining_value
            // 
            this.least_constraining_value.AutoSize = true;
            this.least_constraining_value.Location = new System.Drawing.Point(25, 111);
            this.least_constraining_value.Name = "least_constraining_value";
            this.least_constraining_value.Size = new System.Drawing.Size(143, 17);
            this.least_constraining_value.TabIndex = 3;
            this.least_constraining_value.Text = "Least Constraining Value";
            this.least_constraining_value.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.launcher_resolution);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        private Panel grid;
        private Button button1;
        private Button button2;
        private Button launcher_resolution;
        private GroupBox groupBox1;
        private Label recursive_call_value;
        private Label label1;
        private Label sudoku_resolved_value;
        private Label label2;
        private Label resolution_time_value;
        private Label label3;
        private GroupBox groupBox2;
        private CheckBox least_constraining_value;
        private CheckBox degree_heuristic_value;
        private CheckBox mrv_value;
        private CheckBox ac3_value;

        #endregion
        /*        private System.Windows.Forms.DataGridView grid;
                private DataGridView dataGridView1;*/
    }
}

