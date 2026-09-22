using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_WindowSizeChange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window {

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect rect);

        public static List<Process> ProcessList { get; private set; } = new List<Process>();

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(this);

            List<string> processeList = new List<string>();
            foreach (Process process in Process.GetProcesses())
            {
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    //Console.WriteLine($"Process: {process.ProcessName}, Handle: {process.MainWindowHandle}");

                    processeList.Add(process.ProcessName);
                    ProcessList.Add(process);
                }
            }

            TaskComboBox.ItemsSource = processeList;
        }
    }
}