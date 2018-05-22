namespace OntoSeman
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_load = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_reasoner = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_textfile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_export = new System.Windows.Forms.ToolStripButton();
            this.textBox_query = new System.Windows.Forms.TextBox();
            this.dataGridView_results = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_individuals = new System.Windows.Forms.Label();
            this.label_classes = new System.Windows.Forms.Label();
            this.button_runQuery = new System.Windows.Forms.Button();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_find = new System.Windows.Forms.Button();
            this.bindingSource_result = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel_reasoner = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button_analize_sentence = new System.Windows.Forms.Button();
            this.progressBar_reasoner = new System.Windows.Forms.ProgressBar();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_results)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_result)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_load,
            this.toolStripButton_reasoner,
            this.toolStripButton_textfile,
            this.toolStripButton_export});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(722, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_load
            // 
            this.toolStripButton_load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_load.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_load.Image")));
            this.toolStripButton_load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_load.Name = "toolStripButton_load";
            this.toolStripButton_load.Size = new System.Drawing.Size(37, 22);
            this.toolStripButton_load.Text = "Load";
            this.toolStripButton_load.ToolTipText = "Load";
            this.toolStripButton_load.Click += new System.EventHandler(this.toolStripButton_load_Click);
            // 
            // toolStripButton_reasoner
            // 
            this.toolStripButton_reasoner.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_reasoner.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_reasoner.Image")));
            this.toolStripButton_reasoner.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_reasoner.Name = "toolStripButton_reasoner";
            this.toolStripButton_reasoner.Size = new System.Drawing.Size(93, 22);
            this.toolStripButton_reasoner.Text = "Apply Reasoner";
            this.toolStripButton_reasoner.Click += new System.EventHandler(this.toolStripButton_reasoner_Click);
            // 
            // toolStripButton_textfile
            // 
            this.toolStripButton_textfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_textfile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_textfile.Image")));
            this.toolStripButton_textfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_textfile.Name = "toolStripButton_textfile";
            this.toolStripButton_textfile.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton_textfile.Text = "Process Text File";
            this.toolStripButton_textfile.Click += new System.EventHandler(this.toolStripButton_textfile_Click);
            // 
            // toolStripButton_export
            // 
            this.toolStripButton_export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_export.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_export.Image")));
            this.toolStripButton_export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_export.Name = "toolStripButton_export";
            this.toolStripButton_export.Size = new System.Drawing.Size(101, 22);
            this.toolStripButton_export.Text = "Export Inferences";
            this.toolStripButton_export.Click += new System.EventHandler(this.toolStripButton_export_Click);
            // 
            // textBox_query
            // 
            this.textBox_query.Location = new System.Drawing.Point(53, 256);
            this.textBox_query.Multiline = true;
            this.textBox_query.Name = "textBox_query";
            this.textBox_query.Size = new System.Drawing.Size(603, 82);
            this.textBox_query.TabIndex = 1;
            // 
            // dataGridView_results
            // 
            this.dataGridView_results.AllowUserToAddRows = false;
            this.dataGridView_results.AllowUserToDeleteRows = false;
            this.dataGridView_results.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_results.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_results.Location = new System.Drawing.Point(53, 374);
            this.dataGridView_results.Name = "dataGridView_results";
            this.dataGridView_results.ReadOnly = true;
            this.dataGridView_results.Size = new System.Drawing.Size(603, 150);
            this.dataGridView_results.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Individuals:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Classes:";
            // 
            // label_individuals
            // 
            this.label_individuals.AutoSize = true;
            this.label_individuals.Location = new System.Drawing.Point(83, 107);
            this.label_individuals.Name = "label_individuals";
            this.label_individuals.Size = new System.Drawing.Size(0, 13);
            this.label_individuals.TabIndex = 7;
            // 
            // label_classes
            // 
            this.label_classes.AutoSize = true;
            this.label_classes.Location = new System.Drawing.Point(65, 130);
            this.label_classes.Name = "label_classes";
            this.label_classes.Size = new System.Drawing.Size(0, 13);
            this.label_classes.TabIndex = 8;
            // 
            // button_runQuery
            // 
            this.button_runQuery.Location = new System.Drawing.Point(536, 344);
            this.button_runQuery.Name = "button_runQuery";
            this.button_runQuery.Size = new System.Drawing.Size(120, 23);
            this.button_runQuery.TabIndex = 11;
            this.button_runQuery.Text = "Run Query!";
            this.button_runQuery.UseVisualStyleBackColor = true;
            this.button_runQuery.Click += new System.EventHandler(this.button_runQuery_Click);
            // 
            // textBox_search
            // 
            this.textBox_search.Location = new System.Drawing.Point(536, 22);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(94, 20);
            this.textBox_search.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(489, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Search";
            // 
            // button_find
            // 
            this.button_find.Location = new System.Drawing.Point(636, 20);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(75, 23);
            this.button_find.TabIndex = 14;
            this.button_find.Text = "Find!";
            this.button_find.UseVisualStyleBackColor = true;
            this.button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // bindingSource_result
            // 
            this.bindingSource_result.DataSourceChanged += new System.EventHandler(this.bindingSource1_DataSourceChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Reasoned?";
            // 
            // panel_reasoner
            // 
            this.panel_reasoner.Location = new System.Drawing.Point(86, 155);
            this.panel_reasoner.Name = "panel_reasoner";
            this.panel_reasoner.Size = new System.Drawing.Size(63, 13);
            this.panel_reasoner.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Query/Sentence analize";
            // 
            // button_analize_sentence
            // 
            this.button_analize_sentence.Location = new System.Drawing.Point(432, 344);
            this.button_analize_sentence.Name = "button_analize_sentence";
            this.button_analize_sentence.Size = new System.Drawing.Size(98, 24);
            this.button_analize_sentence.TabIndex = 18;
            this.button_analize_sentence.Text = "Analize";
            this.button_analize_sentence.UseVisualStyleBackColor = true;
            this.button_analize_sentence.Click += new System.EventHandler(this.button_analize_sentence_Click);
            // 
            // progressBar_reasoner
            // 
            this.progressBar_reasoner.Enabled = false;
            this.progressBar_reasoner.Location = new System.Drawing.Point(86, 155);
            this.progressBar_reasoner.Name = "progressBar_reasoner";
            this.progressBar_reasoner.Size = new System.Drawing.Size(471, 13);
            this.progressBar_reasoner.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_reasoner.TabIndex = 19;
            this.progressBar_reasoner.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 557);
            this.Controls.Add(this.button_analize_sentence);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel_reasoner);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_find);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_search);
            this.Controls.Add(this.button_runQuery);
            this.Controls.Add(this.label_classes);
            this.Controls.Add(this.label_individuals);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_results);
            this.Controls.Add(this.textBox_query);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.progressBar_reasoner);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_results)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_result)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolStripButton_load;
        private System.Windows.Forms.ToolStripButton toolStripButton_reasoner;
        private System.Windows.Forms.TextBox textBox_query;
        private System.Windows.Forms.DataGridView dataGridView_results;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_individuals;
        private System.Windows.Forms.Label label_classes;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button button_runQuery;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.BindingSource bindingSource_result;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_reasoner;
        private System.Windows.Forms.ToolStripButton toolStripButton_textfile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_analize_sentence;
        private System.Windows.Forms.ToolStripButton toolStripButton_export;
        private System.Windows.Forms.ProgressBar progressBar_reasoner;
    }
}

