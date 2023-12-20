using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Fortnite.Utils;

namespace Fortnite
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Welcome to Fortnite v{Fortnite.Version}!");

            var noDump = args.Length > 0 && args[0] == "nodump";
            var runDir = Directory.GetCurrentDirectory();

            if (!noDump)
            {
                // run offset dumper
                Console.WriteLine(" Dumping Offsets");
                Console.WriteLine("     (Add the nodump arg to skip this step.)");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(runDir + "/offset_dumper/cs2-dumper.exe")
                {
                    WorkingDirectory = runDir + "/offset_dumper",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                p.Start();

                // wait for dump to finish
                while (true)
                {
                    Thread.Sleep(100);
                    if (p.HasExited) break;
                }
            }

            // read offsets
            Console.WriteLine(" Reading Offsets");
            Offsets.ReadOffsets(runDir);

            // start cheat
            Console.WriteLine(" Starting Cheat");
            new Fortnite().Run();
        }
    }
}