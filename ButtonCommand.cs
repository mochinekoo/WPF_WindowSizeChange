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
                if (MainWindow.GetWindowRect(hwnd, out Rect windowRect) && MainWindow.GetClientRect(hwnd, out Rect clientRect)) {
                    int width = int.Parse(mainViewModel_.MainWindow.WidthBox.Text);
                    int height = int.Parse(mainViewModel_.MainWindow.HeightBox.Text);
                    int borderWidth = (windowRect.Right - windowRect.Left) - (clientRect.Right - clientRect.Left);
                    int borderHeight = (windowRect.Bottom - windowRect.Top) - (clientRect.Bottom - clientRect.Top);

                    width = width + borderWidth;
                    height = height + borderHeight;

                    bool result = MainWindow.MoveWindow(hwnd, windowRect.Left, windowRect.Top, width, height, false);
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
