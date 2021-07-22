using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        private string steamId64Link;
        private steamLinkTypes steamLinkType = steamLinkTypes.unknown;
        public steamLinkTypes getSteamLinkType()
        {
            return steamLinkType;
        }
        public SteamLinkValidation(string steamLink)
        {
            this.steamLink = steamLink;
            checkSteamLinkType();
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
                    string apiString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + APIKey + "&steamids=" + steamId;
                    var json = new WebClient().DownloadString(apiString);
                    if (steamId.Length == maxSteamId64Length && steamId.All(char.IsDigit) && json != "{\"response\":{\"players\":[]}}")
                        steamLinkType = steamLinkTypes.steamId64Link;
                    else
                        steamLinkType = steamLinkTypes.errorType;
                }
                else
                {
                    string apiString = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + APIKey + "&vanityurl=" + steamId;
                    var json = new WebClient().DownloadString(apiString);
                    var list = JsonConvert.DeserializeObject<Root>(json);
                    if (list.response.success == 1)
                        steamLinkType = steamLinkTypes.customIdLink;
                    else
                        steamLinkType = steamLinkTypes.errorType;
                }
            }
            else
            {
                string steamId = steamLink;
                if (isDigitsOnly(steamId))
                {
                    string apiString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + APIKey + "&steamids=" + steamId;
                    var json = new WebClient().DownloadString(apiString);
                    if (steamId.Length == maxSteamId64Length && steamId.All(char.IsDigit) && json != "{\"response\":{\"players\":[]}}")
                        steamLinkType = steamLinkTypes.steamId64;
                    else
                        steamLinkType = steamLinkTypes.errorType;
                }
                else
                {
                    string apiString = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + APIKey + "&vanityurl=" + steamId;
                    var json = new WebClient().DownloadString(apiString);
                    var list = JsonConvert.DeserializeObject<Root>(json);
                    if (list.response.success == 1)
                        steamLinkType = steamLinkTypes.customId;
                    else
                        steamLinkType = steamLinkTypes.errorType;
                }
            }
        }
        public void convertSteamLinkToSteamId64Link()
        {
            if (steamLinkType != steamLinkTypes.unknown && steamLinkType != steamLinkTypes.errorType)
            {
                if (steamLinkType == steamLinkTypes.steamId64Link)
                {
                    steamId64Link = steamLink;
                }
                else if (steamLinkType == steamLinkTypes.steamId64)
                {
                    steamId64Link = "https://steamcommunity.com/profiles/" + steamLink;
                }
                else if (steamLinkType == steamLinkTypes.customIdLink)
                {
                    string[] steamLinkArr = steamLink.Split('/');
                    string steamId = steamLinkArr.Last(l => { return l != ""; });
                    string apiString = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + APIKey + "&vanityurl=" + steamId;
                    var json = new WebClient().DownloadString(apiString);
                    var list = JsonConvert.DeserializeObject<Root>(json);
                    steamId64Link = "https://steamcommunity.com/profiles/" + list.response.steamid;
                }
                else if (steamLinkType == steamLinkTypes.customId)
                {
                    string apiString = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + APIKey + "&vanityurl=" + steamLink;
                    var json = new WebClient().DownloadString(apiString);
                    var list = JsonConvert.DeserializeObject<Root>(json);
                    steamId64Link = "https://steamcommunity.com/profiles/" + list.response.steamid;
                }
            }
            else
            {
                steamId64Link = "";
            }
        }
        public string getSteamId64Link()
        {
            return steamId64Link;
        }
        private bool isDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        public class Response
        {
            public string steamid { get; set; }
            public int success { get; set; }
        }
        public class Root
        {
            public Response response { get; set; }
        }
    }
}
