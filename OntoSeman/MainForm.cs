using OntoSeman.Crosscutting;
using OntoSeman.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VDS.RDF;

namespace OntoSeman
{
    public partial class MainForm : Form
    {
        private OWLOntology OwlOntology { get; set; }
        private OWLManager OwlManager { get; set; }

        private bool Locked { get; set; }

        private string fromOntologyPath;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OwlManager = new OWLManager();
            OwlOntology = new OWLOntology();
            panel_reasoner.BackColor = Color.Red;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (fromOntologyPath != null && fromOntologyPath.Count() > 0)
                File.Delete(getTempFilePath());
        }

        private void initializeOntology(string path)
        {
            OwlOntology.Ontology = OwlManager.LoadOntology(path);
            OwlOntology.ObjectProperties = OwlManager.GetObjectProperties();
            OwlOntology.Individuals = OwlManager.GetIndividuals();
            OwlOntology.Classes = OwlManager.GetClasses();
            panel_reasoner.BackColor = Color.Red;
        }

        private void initializeLabels()
        {
            label_classes.Text = "" + OwlOntology.Classes.Count;
            label_individuals.Text = "" + OwlOntology.Individuals.Count;
        }

        private void toolStripButton_load_Click(object sender, EventArgs e)
        {
            if (Locked)
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Ontology Files|*.owl;*.rdf;*.rdfs";
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                OwlManager = new OWLManager();
                OwlOntology = new OWLOntology();
                initializeOntology(dialog.FileName);
                fromOntologyPath = dialog.FileName;
                clearLabels();
                clearQuery();
                initializeLabels();
            }
        }


        private void clearLabels()
        {
            label_classes.Text = "";
            label_individuals.Text = "";

            panel_reasoner.BackColor = Color.Red;
        }

        private void clearQuery()
        {
            textBox_query.Text = "";
            textBox_search.Text = "";

            panel_reasoner.BackColor = Color.Red;
        }

        private string getTempFilePath()
        {
            return fromOntologyPath.Substring(0, fromOntologyPath.LastIndexOf(".")) + "-temp.rdf";
        }

        private void applyReasoner()
        {
            if (panel_reasoner.BackColor == Color.Green)
            {
                MessageBox.Show("You already appllied the reasoner", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            launchReasonerProcess2(fromOntologyPath, getTempFilePath());
        }

        private void toolStripButton_reasoner_Click(object sender, EventArgs e)
        {
            if (Locked)
                return;

            if (fromOntologyPath == null || fromOntologyPath.Count() == 0)
            {
                MessageBox.Show("You need to load an ontology", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            progressBar_reasoner.Visible = true;
            progressBar_reasoner.BringToFront();

            applyReasoner();

            progressBar_reasoner.Visible = false;
            progressBar_reasoner.SendToBack();

            panel_reasoner.BackColor = Color.Green;

            initializeLabels();
        }

        private void button_find_Click(object sender, EventArgs e)
        {
            if (Locked)
                return;

            if (panel_reasoner.BackColor == Color.Red)
            {
                MessageBox.Show("You can do the search only after applying the reasoner", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (textBox_search.Text.Count() == 0)
            {
                MessageBox.Show("Can you write a real word?", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var query = "SELECT Distinct ?type WHERE {  <" + OwlOntology.Ontology.BaseUri + "#" + textBox_search.Text + "> rdf:type ?type.}";
            var queryResylt = OwlManager.RunQueryDataTable(query);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Word");
            dataTable.Columns.Add("Type");

            foreach (var match in queryResylt.Results)
            {
                var test = getLastId(match.ToString());

                if (isApproved(getLastId(match.ToString())))
                {
                    var row = dataTable.NewRow();
                    row["Word"] = textBox_search.Text;
                    row["Type"] = getLastId(match.ToString());
                    dataTable.Rows.Add(row);
                }
            }

            bindingSource_result.DataSource = dataTable;
            dataGridView_results.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView_results.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_results.AutoResizeColumns();
        }

        private void button_runQuery_Click(object sender, EventArgs e)
        {
            if (Locked)
                return;

            if (panel_reasoner.BackColor == Color.Red)
            {
                MessageBox.Show("You can do the search only after applying the reasoner", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (textBox_search.Text.Count() == 0)
            {
                MessageBox.Show("Can you write a real word?", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
                bindingSource_result.DataSource = OwlManager.RunQueryDataTable(textBox_query.Text);
        }

        private void bindingSource1_DataSourceChanged(object sender, EventArgs e)
        {
            dataGridView_results.DataSource = bindingSource_result;
            dataGridView_results.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView_results.Refresh();
            dataGridView_results.AutoResizeColumns();
        }

        private ICollection<string> readTextFile(string filename)
        {
            ICollection<string> words = new List<string>();

            // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(filename))
            {
                // Read the stream to a string, and write the string to the console.
                var line = sr.ReadToEnd().Trim().Split(';');

                foreach (string element in line)
                    words.Add(element);
            }

            return words;
        }

        private void toolStripButton_textfile_Click(object sender, EventArgs e)
        {
            if (Locked)
                return;

            if (panel_reasoner.BackColor == Color.Red)
            {
                MessageBox.Show("You can do the search only after applying the reasoner", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var words = readTextFile(dialog.FileName);

                if (panel_reasoner.BackColor == Color.Red)
                {
                    applyReasoner();
                    panel_reasoner.BackColor = Color.Green;
                    initializeLabels();
                }

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Word");
                dataTable.Columns.Add("Type");

                foreach (string word in words)
                {
                    var result = (from individual in OwlOntology.Individuals
                                  where individual.Contains(word)
                                  select individual).FirstOrDefault();

                    if (result != null)
                    {
                        var listOfIndividualTypes = OwlManager.RunQuery("SELECT Distinct ?type WHERE {  <" + result + "> rdf:type ?type.}");

                        foreach (string type in listOfIndividualTypes)
                        {
                            var test = getLastId(type);
                            if (getLastId(type).Equals("NamedIndividual") == false)
                            {
                                var row = dataTable.NewRow();
                                row["Word"] = word;
                                row["Type"] = getLastId(type);
                                dataTable.Rows.Add(row);
                            }
                        }
                    }
                }

                bindingSource_result.DataSource = dataTable;
                dataGridView_results.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_results.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_results.AutoResizeColumns();
            }
        }

        private string getLastId(string owlIRI)
        {
            return owlIRI.Substring(owlIRI.LastIndexOf('#') + 1);
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void launchReasonerProcess2(string ontologyFromPath, string ontologyTempPath)
        {
            Locked = true;
            progressBar_reasoner.Visible = true;
            progressBar_reasoner.BringToFront();

            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += (o, e) =>
            {
                File.WriteAllBytes("executer.jar", resources.executer);

                string executer = "executer.jar";

                var processInfo = new ProcessStartInfo("javaw", string.Format("-jar \"{0}\" \"{1}\" \"{2}\"", executer.Replace("\\", @"\"), ontologyFromPath, ontologyTempPath))
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    ErrorDialog = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    //WorkingDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath),
                };

                Process process = System.Diagnostics.Process.Start(processInfo);

                process.WaitForExit();
                int exitCode = process.ExitCode;
                process.Close();

                File.SetAttributes(ontologyTempPath, File.GetAttributes(ontologyTempPath) | FileAttributes.Hidden);
            };

            worker.RunWorkerCompleted += (o, e) =>
            {
                progressBar_reasoner.Visible = false;
                progressBar_reasoner.SendToBack();
                Locked = false;

                OwlManager.LoadOntology(getTempFilePath());

                OwlOntology.Ontology = OwlManager.Ontology;
                OwlOntology.ObjectProperties = OwlManager.GetObjectProperties();
                // OwlOntology.Individuals = new List<string>(OwlManager.GetIndividuals());
                OwlOntology.Classes = OwlManager.GetClasses();
            };

            worker.RunWorkerAsync();
        }


        private void launchReasonerProcess(string ontologyFromPath, string ontologyTempPath)
        {
            string executer = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\executer.jar";

            var processInfo = new ProcessStartInfo("javaw", string.Format("-jar \"{0}\" \"{1}\" \"{2}\"", executer.Replace("\\", @"\"), ontologyFromPath, ontologyTempPath))
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                ErrorDialog = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                //WorkingDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath),
            };

            Process process = System.Diagnostics.Process.Start(processInfo);

            process.WaitForExit();
            int exitCode = process.ExitCode;
            process.Close();

            File.SetAttributes(ontologyTempPath, File.GetAttributes(ontologyTempPath) | FileAttributes.Hidden);
        }

        private void button_analize_sentence_Click(object sender, EventArgs e)
        {
            if (Locked)
                return;

            if (panel_reasoner.BackColor == Color.Red)
            {
                MessageBox.Show("You can do the search only after applying the reasoner", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (textBox_query.Text.Count() == 0)
            {
                MessageBox.Show("Can you write a real word?", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Match match = Regex.Match(textBox_query.Text, @"(?i)^[a-z]+");

            var bagOfWords = textBox_query.Text.Split(' ');

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Word");
            dataTable.Columns.Add("Type");

            foreach (string word in bagOfWords)
            {
                var result = (from individual in OwlOntology.Individuals
                              where individual.Contains(word)
                              select individual).FirstOrDefault();

                if (result != null)
                {
                    var listOfIndividualTypes = OwlManager.RunQuery("SELECT Distinct ?type WHERE {  <" + result + "> rdf:type ?type.}");

                    foreach (string type in listOfIndividualTypes)
                    {
                        var test = getLastId(type);
                        if (isApproved(getLastId(type)))
                        {
                            var row = dataTable.NewRow();
                            row["Word"] = word;
                            row["Type"] = getLastId(type);
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }

            bindingSource_result.DataSource = dataTable;
            dataGridView_results.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView_results.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_results.AutoResizeColumns();
        }

        private bool isApproved(string word)
        {
            if (word.Equals("ClasseGramatical")
                || word.Equals("Polaridade")
                || word.Equals("Quadrante")
                || word.Equals("NamedIndividual")
                || word.Equals("Thing"))

                return false;
            else
                return true;
        }

        private void toolStripButton_export_Click(object sender, EventArgs e)
        {
            if (Locked)
                return;

            if (panel_reasoner.BackColor == Color.Red)
            {
                MessageBox.Show("You can do the search only after applying the reasoner", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save";
            dialog.Filter = "Ontology File|*.owl";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                OwlManager.SaveOntology(dialog.FileName);
            }
        }
    }
}
