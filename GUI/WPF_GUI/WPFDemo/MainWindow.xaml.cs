using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;

namespace WPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<String> headingsList = new List<String>();
        public List<String> teamsList = new List<String>();

        private string _filePath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Functionality not implemented yet!");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                filePathText.Text = openFileDialog.FileName;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            _filePath = filePathText.Text;
            run_cmd(_filePath, "SUMMARISE_TEAM");
        }

        private void run_cmd(string _filePath, string analysisType)
        {
            //string analysisFilePath = @"C:\Users\Andrew Mulcahy\Documents\Programming\Data\NBAPlayerStats2223\2022-2023 NBA Player Stats - Regular.csv";
            string pythonPath = @"C:\Users\Andrew Mulcahy\AppData\Local\Programs\Python\Python312\python.exe";
            string analysisScriptPath = @"C:\Users\Andrew Mulcahy\Documents\Programming\Projects\NBAData_WPF_Py\Py_Analysis\NBA2223-Analysis.py";
            string argsForPython = String.Empty;

            string additionalSelection = "";
            if (analysisType == "SUMMARISE_TEAM")
            {
                additionalSelection = teamsComboBox.Text;
                MessageBox.Show($"additionalSelection: {additionalSelection}");
                argsForPython += "\"" + analysisScriptPath + "\"" + " " + "\"" + _filePath + "\"" + " " + analysisType + " " + additionalSelection;
            }
            else
            {
                argsForPython += "\"" + analysisScriptPath + "\"" + " " + "\"" + _filePath + "\"" + " " + analysisType;
            }

            Process p_pyt = new Process();
            p_pyt.StartInfo = new ProcessStartInfo(pythonPath, argsForPython)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p_pyt.Start();

            char[] spliter = { ';' };
            StreamReader sReader = p_pyt.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);
            MessageBox.Show(output[0]);

            if (analysisType == "GET_TEAMS")
            {
                char[] trimChars = { '[', ']' };
                string[] teamsArray = output[0].Trim().Trim(trimChars).Split(", ");
                foreach (string team in teamsArray)
                {
                    teamsList.Add(team);
                }
                teamsComboBox.ItemsSource = teamsList;
            }

            if (analysisType == "SUMMARISE_TEAM")
            {

                foreach (string s in output)
                {
                    tempTextBox.Text += s + Environment.NewLine;
                }
            }
                
            //p_pyt.WaitForExit();

        }

        private void getTeamsButton_Click(object sender, RoutedEventArgs e)
        {
            _filePath = filePathText.Text;
            run_cmd(_filePath, "GET_TEAMS");
        }

        private void OnScroll(Object sender, EventArgs e)
        {
            MessageBox.Show($"Scrolling! {e}");
        }
    }
}
