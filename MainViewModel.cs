using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;

using Rect = WPF_WindowSizeChange.MainWindow.Rect;

namespace WPF_WindowSizeChange {
    public class MainViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindow MainWindow { get; set; }
        private string TaskCombo_ = null;

        public ButtonCommand ButtonCommand { get; set; }

        public MainViewModel(MainWindow mainWindow) {
            this.MainWindow = mainWindow;
            this.ButtonCommand = new ButtonCommand(this);
        }

        public string TaskComboBox { get { return TaskCombo_; } 
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
                                TaskCombo_ = process.ProcessName;
                            }

                            
                        }
                    }
                }
                        
            } 
        }

    }
}
