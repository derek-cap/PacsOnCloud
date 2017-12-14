using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace EveryTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            //Action action = async () => await BackWorkAsync();
            //action();
        }

        public async Task BackWorkAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            var vm = this.DataContext as MainWindowViewModel;
            vm.CornerInfo.SetText($"{Thread.CurrentThread.ManagedThreadId} ABC");
        }
    }
}
