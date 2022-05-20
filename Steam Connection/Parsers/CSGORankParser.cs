using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            try
            {
                using (Process csgoStatsParser = new Process())
                {
                    csgoStatsParser.StartInfo.UseShellExecute = false;
                    csgoStatsParser.StartInfo.RedirectStandardOutput = true;
                    csgoStatsParser.StartInfo.CreateNoWindow = true;
                    csgoStatsParser.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\external\\CsgoStatsParser\\CsgoStatsParser.exe";
                    csgoStatsParser.StartInfo.Arguments = steamId64;
                    csgoStatsParser.Start();
                    string skillgroup = csgoStatsParser.StandardOutput.ReadToEnd();
                    if (skillgroup.Contains("error"))
                        cSRank = new CSRank();
                    else
                        cSRank = new CSRank(skillgroup);
                    csgoStatsParser.WaitForExit();
                }
            }
            catch
            {
                cSRank = new CSRank();
            }
        }
        public CSRank getCSRank()
        {
            return this.cSRank;
        }
    }
}
