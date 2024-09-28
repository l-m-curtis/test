using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestProject1")]
namespace InterviewTestMid
{

    internal class Program
    {

        public static void Main(string[] args)
        {
            IHost H = Host.CreateDefaultBuilder().ConfigureServices(Ss => { Ss.AddSingleton<InterviewTestMid.ILogger, InterviewTestMid.Logger>(); Ss.AddSingleton<InterviewTestMid.IDoWorkService, InterviewTestMid.DoWorkService>(); }).Build();
            InterviewTestMid.IDoWorkService App = H.Services.GetRequiredService<InterviewTestMid.IDoWorkService>();
            App.DoWork();
        }

    }

}