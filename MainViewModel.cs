using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;

using Rect = WPF_WindowSizeChange.MainWindow.Rect;

namespace WPF_WindowSizeChange {
    internal class MainViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        private MainWindow MainWindow { get; set; }

        public MainViewModel(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
        }

        public string TaskComboBox { get; 
            set {
                foreach (Process process in MainWindow.ProcessList)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        if (process.ProcessName == value)
                        {

                            if (MainWindow.GetWindowRect(process.MainWindowHandle, out Rect rect))
                            {
                                MainWindow.WidthBox.Text = (rect.Right - rect.Left).ToString();
                                MainWindow.HeightBox.Text = (rect.Bottom - rect.Top).ToString();
                            }

                            
                        }
                    }
                }
                        
            } 
        }

    }
}
