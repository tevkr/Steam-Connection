using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Steam_Connection.Parsers
{
    class SteamParser
    {
        private HtmlDocument SteamIdXyz;
        public SteamParser(string profileUrl)
        {
            try
            {
                var htmlPage = GetSteamIdXyzHtmlPage(profileUrl).Result;
                SteamIdXyz = new HtmlDocument();
                SteamIdXyz.LoadHtml(htmlPage);
            }
            catch (Exception ex)
            {
                SteamIdXyz = null;
            }
        }
        public bool IsSteamProfileUrlValid()
        {
            if (SteamIdXyz == null) return default;
            try
            {
                return SteamIdXyz.DocumentNode.SelectSingleNode("//div[@class='h3 center'][1]") == null;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GetProfilePermalink()
        {
            if (SteamIdXyz == null) return default;
            try
            {
                return SteamIdXyz.DocumentNode.SelectSingleNode("//input[7]").Attributes["value"].Value;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public string GetSteamId64()
        {
            if (SteamIdXyz == null) return default;
            try
            {
                return SteamIdXyz.DocumentNode.SelectSingleNode("//input[4]").Attributes["value"].Value;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public string GetAvatarLink()
        {
            if (SteamIdXyz == null) return default;
            try
            {
                return SteamIdXyz.DocumentNode.SelectSingleNode("//img[@class='avatar'][1]").Attributes["src"].Value;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public string GetNick()
        {
            if (SteamIdXyz == null) return default;
            try
            {
                return SteamIdXyz.DocumentNode.SelectSingleNode("//input[5]").Attributes["value"].Value;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public int GetVacsCount()
        {
            if (SteamIdXyz == null) return default;
            try
            {
                string vacsCountStr = SteamIdXyz.DocumentNode.SelectSingleNode("//em[1]").Attributes["title"].Value;
                Regex regex = new Regex(@"Total VAC bans: (\d+)");
                MatchCollection matches = regex.Matches(vacsCountStr);
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    vacsCountStr = groups[1].Value;
                }
                return int.Parse(vacsCountStr);
            }
            catch (Exception)
            {
                return default;
            }
        }
        private static async Task<string> GetSteamIdXyzHtmlPage(string id)
        {

            using (HttpClient httpClient = new HttpClient())
            using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Safari/537.36");
                formDataContent.Add(new StringContent(id), "id");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("https://steamid.xyz/q", formDataContent);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await httpResponseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    return default;
                }
            }
        }
    }
}
