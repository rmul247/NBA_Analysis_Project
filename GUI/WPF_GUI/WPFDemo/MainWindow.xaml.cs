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
            // *************************************
            // p_cmd for debugging purposes only
            // *****************************************
            {
                string argsForCMD = getProcessArgs(analysisType, "debug");

                Process p_cmd = runScript(argsForCMD, "debug");
                //Process p_cmd = new Process();
                //p_cmd.StartInfo = new ProcessStartInfo("CMD.exe", argsForCMD)
                //{
                //    RedirectStandardOutput = true,
                //    UseShellExecute = false//,
                //    //CreateNoWindow = true
                //};
                p_cmd.Start();
                //p_cmd.WaitForExit();
                
            }

            // ****************************************
            // p_pyt is the correct (hopefully) version
            // *****************************************

            string argsForPython = getProcessArgs(analysisType, "python");

            Process p_pyt = runScript(argsForPython, "python");
            p_pyt.Start();

            char[] spliter = { ';' };
            StreamReader sReader = p_pyt.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);

            processOutput(analysisType, output);

            //p_pyt.WaitForExit();

        }

        private string getProcessArgs(string analysisType, string mode) 
        {
            string processArgs = String.Empty;
            string analysisScriptPath = @"C:\Users\Andrew Mulcahy\Documents\Programming\Python\NBA2223-Analysis.py";
            if (mode == "debug")
                processArgs += "/k python ";

            if (analysisType == "RUN_ANALYSIS")
            {
                processArgs += "\"" + analysisScriptPath + "\" \"" + filePathText.Text + "\" " + analysisType + " " + headingsComboBox.Text + " " + analysisComboBox.Text + " " + tempTextBox.Text;
            }
            else
            {
                processArgs += "\"" + analysisScriptPath + "\"" + " " + "\"" + filePathText.Text + "\"" + " " + analysisType;
            }

            return processArgs;
        }

        private Process runScript(string argsForProcess, string mode)
        {
            string processPath = String.Empty;
            if (mode == "debug")
                processPath = "CMD.exe";
            else
                processPath = @"C:\Users\Andrew Mulcahy\AppData\Local\Programs\Python\Python312\python.exe";

            Process myProcess = new Process();
            myProcess.StartInfo = new ProcessStartInfo(processPath, argsForProcess)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            return myProcess;
        }
        
        private void processOutput(string analysisType, string[] output)
        {
            if (analysisType == "GET_TEAMS")
            {
                string[] teamsArray = cleanPythonData(output);
                foreach (string team in teamsArray)
                    teamsList.Add(team);
                teamsComboBox.ItemsSource = teamsList;
            }

            if (analysisType == "GET_HEADINGS")
            {
                string[] headingsArray = cleanPythonData(output);
                foreach (string heading in headingsArray)
                {
                    //char[] trimChars = { ' ', '\'' };
                    string nakedHeadings = heading.Trim('\'');
                    headingsList.Add(nakedHeadings);

                }
                headingsComboBox.ItemsSource = headingsList;
            }

            if (analysisType == "RUN_ANALYSIS")
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                foreach (string s in output)
                {
                    //tempTextBox.Text += s + Environment.NewLine;
                    stringBuilder1.Append(s);
                }
                MessageBox.Show(stringBuilder1.ToString());
            }
        }

        private string[] cleanPythonData(string[] output)
        {
            char[] trimChars = { '[', ']' };
            string[] teamsArray = output[0].Trim().Trim(trimChars).Split(", ");
            return teamsArray;
        }

        private void updateAnalysisOperatorComboBox(string heading)
        {
            if ((heading == "'Pos'") || (heading == "'Tm'"))
                analysisComboBox.ItemsSource = new List<String>{ "==" };
            else
                analysisComboBox.ItemsSource = new List<String> { "==", ">", ">=", "<", "<=", "!="};
        }

        // Button click event handlers
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                filePathText.Text = openFileDialog.FileName;
        }

        private void runAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            run_cmd("RUN_ANALYSIS");
        }

        // File changed event handler
        private void filePathText_TextChanged(object sender, TextChangedEventArgs e)
        {
            run_cmd("GET_TEAMS");
            run_cmd("GET_HEADINGS");
        }

        // heading selected event
        private void headingsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string newText = (sender as ComboBox).SelectedItem as string;
            analysisHeadingTextBlock.Text = newText;
            updateAnalysisOperatorComboBox(newText);
        }
    }
}
