using DataModel;
using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lords.DataModel;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Dicom;

namespace PacsOnCloud
{
    class Program
    {
        async Task WaitAysnc()
        {
            // await会捕获当前上下文...
            await Task.FromResult(0);
            // ...这里会试图用上面捕获的上下文继续执行
        }

        void Deadlock()
        {
            // 开始延迟
            Task task = WaitAysnc();

            // 同步程序块，正在等待异步方法完成
            task.Wait();
        }

        static async Task<int> GetResult()
        {
            int result = 0;
            // wait 1 second
            await Task.Delay(1000); 
            return result;
        }

        static int Add(int a, int b)
        {
            return a + b;
        }

        static async Task Process()
        {
            // Step 1
            Console.WriteLine($"Logic B {Thread.CurrentThread.ManagedThreadId}");
            await GetResult().ConfigureAwait(false);
            // This function returns when "await"
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
                // Step 3
                Console.WriteLine($"Logic C {Thread.CurrentThread.ManagedThreadId}");
            });
            // Step 4, after the task completed.
            Console.WriteLine($"Logic D {Thread.CurrentThread.ManagedThreadId}");
            // This function end.
        }

        static void Main(string[] args)
        {
            LogManager.SetImplementation(ConsoleLogManager.Instance);
            Logger logger = LogManager.GetLogger("");
            try
            {
                //var x = GetResult();
                //Console.WriteLine(x.Result);
                string ipAddress = "127.0.0.1";
                int port = 801;

                //MyDicomServer server = new MyDicomServer(port, logger);
                //var server = MyDicomServer.CreateCFindServer(port, logger);

                IHelper helper = new Helper();

                helper.Handler += (sender, status) =>
                {
                    if (status == RegisterStatus.Ok)
                    {

                    }

                };

                IDisposable disposable = helper as IDisposable;
                Console.WriteLine($"Dispose == null ? {disposable == null}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine($"Press any key to exit... {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
        }
    }
}
