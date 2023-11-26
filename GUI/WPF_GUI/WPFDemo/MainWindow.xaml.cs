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

        public MainWindow()
        {
            InitializeComponent();
        }


        private void run_cmd(string analysisType)
        {
            /*
            // *************************************
            // p_cmd for debugging purposes only
            // *****************************************
            string argsForCMD = "/k python " + "\"" + analysisScriptPath + "\"" + " " + "\"" + filePathText.Text + "\"" + " " + analysisType;
            Console.WriteLine($"Args for cmd: {argsForCMD}");
            
            
            Process p_cmd = new Process();
            p_cmd.StartInfo = new ProcessStartInfo("CMD.exe", argsForCMD)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false//,
                //CreateNoWindow = true
            };
            //p_cmd.Start();
            */




            // ****************************************
            // p_pyt is the correct (hopefully) version
            // *****************************************


            string argsForPython = getProcessArgs(analysisType);

            Process p_pyt = callPythonScript(argsForPython);
            p_pyt.Start();

            char[] spliter = { ';' };
            StreamReader sReader = p_pyt.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);

            processOutput(analysisType, output);

            //p_pyt.WaitForExit();

        }

        private string getProcessArgs(string analysisType) 
        {
            string argsForPython = String.Empty;
            string analysisScriptPath = @"C:\Users\Andrew Mulcahy\Documents\Programming\Python\NBA2223-Analysis.py";

            string additionalSelection = "";
            if (analysisType == "SUMMARISE_TEAM")
            {
                additionalSelection = teamsComboBox.Text;
                argsForPython += "\"" + analysisScriptPath + "\"" + " " + "\"" + filePathText.Text + "\"" + " " + analysisType + " " + additionalSelection;
            }
            else
            {
                argsForPython += "\"" + analysisScriptPath + "\"" + " " + "\"" + filePathText.Text + "\"" + " " + analysisType;
            }

            return argsForPython;
        }

        private void processOutput(string analysisType, string[] output)
        {
            if (analysisType == "GET_TEAMS")
            {
                //char[] trimChars = { '[', ']' };
                //string[] teamsArray = output[0].Trim().Trim(trimChars).Split(", ");
                string[] teamsArray = cleanPythonData(output);
                foreach (string team in teamsArray)
                {
                    teamsList.Add(team);
                }
                teamsComboBox.ItemsSource = teamsList;
            }


            if (analysisType == "GET_HEADINGS")
            {
                string[] headingsArray = cleanPythonData(output);
                foreach (string team in headingsArray)
                {
                    headingsList.Add(team);
                }
                headingsComboBox.ItemsSource = headingsList;
            }


            if (analysisType == "SUMMARISE_TEAM")
            {

                foreach (string s in output)
                {
                    tempTextBox.Text += s + Environment.NewLine;
                }
            }
        }

        private string[] cleanPythonData(string[] output)
        {
            char[] trimChars = { '[', ']' };
            string[] teamsArray = output[0].Trim().Trim(trimChars).Split(", ");
            return teamsArray;
        }

        private Process callPythonScript(string argsForPython)
        {
            string pythonPath = @"C:\Users\Andrew Mulcahy\AppData\Local\Programs\Python\Python312\python.exe";

            Process p_pyt = new Process();
            p_pyt.StartInfo = new ProcessStartInfo(pythonPath, argsForPython)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            return p_pyt;
        }

        // Button click event handlers
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                filePathText.Text = openFileDialog.FileName;
        }

        private void getTeamsButton_Click(object sender, RoutedEventArgs e)
        {
            run_cmd("GET_TEAMS");
        }

        private void getHeadingsButton_Click(object sender, RoutedEventArgs e)
        {
            run_cmd("GET_HEADINGS");
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            run_cmd("SUMMARISE_TEAM");
        }

    }
}
