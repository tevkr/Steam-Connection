using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.Themes.CustomMessageBox;

namespace Steam_Connection.Validations
{
    class SteamLinkValidation
    {
        private static string APIKey = System.Environment.GetEnvironmentVariable("STEAM_API_KEY");
        public enum steamLinkTypes
        {
            unknown,
            steamId64Link,
            steamId64,
            customIdLink,
            customId,
            errorType
        }
        private const int maxSteamId64Length = 17;
        private string steamLink;
        private string steamId64;
        private steamLinkTypes steamLinkType = steamLinkTypes.unknown;
        public steamLinkTypes getSteamLinkType()
        {
            return steamLinkType;
        }
        public SteamLinkValidation(string steamLink)
        {
            this.steamLink = steamLink;
            try
            {
                checkSteamLinkType();
                convertSteamLinkToSteamId64();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        private bool isSteamId64Correct(string steamId64)
        {
            string apiString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + APIKey + "&steamids=" + steamId64;
            var json = new WebClient().DownloadString(apiString);
            return (steamId64.Length == maxSteamId64Length && steamId64.All(char.IsDigit) && json != "{\"response\":{\"players\":[]}}");
        }
        private bool isCustomIdCorrect(string customId)
        {
            string apiString = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + APIKey + "&vanityurl=" + customId;
            var json = new WebClient().DownloadString(apiString);
            var list = JsonConvert.DeserializeObject<Root>(json);
            if (list.response.success == 1) return true;
            else return false;
        }
        private string getSteamId64FromCustomId(string customId)
        {
            string apiString = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + APIKey + "&vanityurl=" + customId;
            var json = new WebClient().DownloadString(apiString);
            var list = JsonConvert.DeserializeObject<Root>(json);
            return list.response.steamid;
        }
        private void checkSteamLinkType()
        {
            if (Uri.IsWellFormedUriString(steamLink, UriKind.RelativeOrAbsolute)
                && (steamLink.Contains("https://steamcommunity.com/") || steamLink.Contains("steamcommunity.com/"))
                && (steamLink.Contains("/id/") || steamLink.Contains("/profiles/")))
            {
                string[] steamLinkArr = steamLink.Split('/');
                string steamId = steamLinkArr.Last(l => { return l != ""; });
                if (steamLink.Contains("/profiles/"))
                {
                    if (isSteamId64Correct(steamId))
                        steamLinkType = steamLinkTypes.steamId64Link;
                }
                else
                {
                    if (isCustomIdCorrect(steamId))
                        steamLinkType = steamLinkTypes.customIdLink;
                }
            }
            else
            {
                string steamId = steamLink;
                if (steamId.All(char.IsDigit))
                {
                    if (isSteamId64Correct(steamId))
                        steamLinkType = steamLinkTypes.steamId64;
                }
                else
                {
                    if (isCustomIdCorrect(steamId))
                        steamLinkType = steamLinkTypes.customId;
                }
            }
            if (steamLinkType == steamLinkTypes.unknown)
                steamLinkType = steamLinkTypes.errorType;
        }
        private void convertSteamLinkToSteamId64()
        {
            if (steamLinkType != steamLinkTypes.unknown && steamLinkType != steamLinkTypes.errorType)
            {
                if (steamLinkType == steamLinkTypes.steamId64Link)
                {
                    string[] steamLinkArr = steamLink.Split('/');
                    string steamId = steamLinkArr.Last(l => { return l != ""; });
                    steamId64 = steamId;
                }
                else if (steamLinkType == steamLinkTypes.steamId64)
                {
                    steamId64 = steamLink;
                }
                else if (steamLinkType == steamLinkTypes.customIdLink)
                {
                    string[] steamLinkArr = steamLink.Split('/');
                    string steamId = steamLinkArr.Last(l => { return l != ""; });
                    steamId64 = getSteamId64FromCustomId(steamId);
                }
                else if (steamLinkType == steamLinkTypes.customId)
                {
                    steamId64 = getSteamId64FromCustomId(steamLink);
                }
            }
            else
            {
                steamId64 = "";
            }
        }
        public string getSteamId64()
        {
            return steamId64;
        }
        private class Response
        {
            public string steamid { get; set; }
            public int success { get; set; }
        }
        private class Root
        {
            public Response response { get; set; }
        }
    }
}
