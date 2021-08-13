using System;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
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
            var target = new Uri($"https://csgo-stats.net/player/{steamId64}/");
            var handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2");
            try
            {
                var html = client.GetStringAsync(target).Result;
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var imageRank =
                    htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/div/div[2]/div/div/div[1]/div[1]/a/div");
                var imageRankStyle = imageRank.Attributes["style"].Value;
                var rank = imageRankStyle.Split('/').Last().Trim(')').Split('.').First();
                if (int.TryParse(rank, out _))
                    cSRank = new CSRank($"skillgroup{rank}");
                else
                    cSRank = new CSRank();
            }
            catch { }
            handler.Dispose();
            client.Dispose();
        }
        public CSRank getCSRank()
        {
            return this.cSRank;
        }
    }
}
