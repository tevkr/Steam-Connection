using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steam_Connection.MVVM.Model;

namespace Steam_Connection.Parsers
{
    class CSGORankParser
    {
        private string steamId64;
        private CSRank cSRank;
        public CSGORankParser(string steamId64)
        {
            this.steamId64 = steamId64;
        }
        public void parseCSGORank()
        {
            string rank = ExecuteCommand(steamId64);
            if (rank.Contains("skillgroup"))
                cSRank = new CSRank(rank);
            else
                cSRank = new CSRank();
        }
        public CSRank getCSGORank()
        {
            return this.cSRank;
        }
        public static string ExecuteCommand(string steamId64)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Environment.CurrentDirectory + "\\python-3.7.7",
                    FileName = "python.exe",
                    Arguments = "CSGO_rank_parser.py " + steamId64,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            string output = "";
            while (!proc.StandardOutput.EndOfStream)
            {
                output = proc.StandardOutput.ReadLine();
            }
            return output;
        }
    }
}
