using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EveryTestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Action action = async () => await RunAsync().ConfigureAwait(false);
            action();
        }

        public async Task RunAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            await (this.MainWindow as MainWindow).BackWorkAsync();
        }
    }
}
