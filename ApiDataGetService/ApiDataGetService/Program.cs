using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ApiDataGetService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<TransactionsPart>(s =>
                {
                    s.ConstructUsing(transaction => new TransactionsPart());
                    s.WhenStarted(transaction => transaction.Start());
                    s.WhenStopped(transaction => transaction.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("WeApiDataService");
                x.SetDisplayName("Web Api DataService");
                x.SetDescription("Veritabanından alınan verileri txt dosyasına dakikada bir yazdırır. ");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode()); //exitcode inte dönüştürüyoruz
            Environment.ExitCode = exitCodeValue;
        }
    }
}
