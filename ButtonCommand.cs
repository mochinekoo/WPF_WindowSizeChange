using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

using Rect = WPF_WindowSizeChange.MainWindow.Rect;

namespace WPF_WindowSizeChange {
    public class ButtonCommand {

        private readonly MainViewModel mainViewModel_;

        public ICommand ChangeCommand { get; private set; }

        public ButtonCommand(MainViewModel mainViewModel) {
            mainViewModel_ = mainViewModel;
            ChangeCommand = new BaseCommand(RunChangeCommand);

        }

        private void RunChangeCommand() {
            var processes = Process.GetProcessesByName(mainViewModel_.TaskComboBox);
            if (processes != null) {
                var process = processes[0];
                var hwnd = process.MainWindowHandle;
                if (MainWindow.GetWindowRect(hwnd, out Rect rect)) {
                    bool result = MainWindow.MoveWindow(hwnd, rect.Left, rect.Top, int.Parse(mainViewModel_.MainWindow.WidthBox.Text), int.Parse(mainViewModel_.MainWindow.HeightBox.Text), false);
                    if (!result) {
                        MessageBox.Show("失敗");
                    }
                }
                else {
                    MessageBox.Show("失敗");
                }
                
            }
            else {
                MessageBox.Show("失敗");
            }
        }

    }
}
