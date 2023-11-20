using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Windows.Controls;

namespace AutoK.AutoKMain
{
    internal class ProcessesWatcher
    {
        public List<String> processNames = new List<String>() { "\"Select Process\"" };
        public ComboBox ctrl_comboBox;
        private Thread _thread;
        
        public void start()
        {
            this._thread = new Thread(new ThreadStart(watch));
            this._thread.Start();
        }

        public void stop()
        {
            this._thread.Abort();
            this._thread = null;
        }

        private void watch()
        {
            while (true)
            {
                List<Process> processes = Process.GetProcesses().ToList();
                List<String> names = new List<String>();
                processes.ForEach(p => {
                    if (p.MainWindowTitle == String.Empty) return;
                    int existingIndex = processNames.FindIndex(n => n.Contains(p.Id.ToString()));
                    if (existingIndex == -1)
                    {
                        names.Add(p.MainWindowTitle + " - #" + p.Id);
                        return;
                    }
                    processNames[existingIndex] = p.MainWindowTitle + " - PID:" + p.Id;
                });
                this.processNames = this.processNames.Union(names).ToList();
                if (this.ctrl_comboBox != null)
                {
                    
                    ctrl_comboBox.Dispatcher.Invoke(new Action(() =>
                    {
                        int ctrl_selectedIndex = ctrl_comboBox.SelectedIndex;
                        ctrl_comboBox.ItemsSource = this.processNames;
                        ctrl_comboBox.SelectedIndex = ctrl_selectedIndex;
                    }));
                }
                Thread.Sleep(1000);
            }
        }

        
    }
}
