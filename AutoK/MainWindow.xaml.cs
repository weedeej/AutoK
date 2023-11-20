using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AdonisUI.Controls;
using AutoK.AutoKMain;

namespace AutoK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProcessesWatcher processWatcher = new ProcessesWatcher();
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += this.onExit; // Closing Event

            // Listen for Processes
            this.ctrl_processNames.ItemsSource = processWatcher.processNames;
            this.processWatcher.ctrl_comboBox = this.ctrl_processNames;
            this.processWatcher.start();
            this.ctrl_processNames.SelectedIndex = 0;
        }

        private void onExit(object sender, CancelEventArgs e)
        {
            this.processWatcher.stop();
        }
    }
}
