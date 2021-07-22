using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Connection.Parsers
{
    class SteamParser
    {
        private static string APIKey = System.Environment.GetEnvironmentVariable("STEAM_API_KEY");
        private string steamLink { get; }
        public string steamId64 { get; private set; }
        public string nickname { get; private set; }
        public string steamPicture { get; private set; }
        public int vacCount { get; }
        public int rank { get; }
        public SteamParser(string steamLink)
        {
            this.steamLink = steamLink;
            getSteamId64();
            parseAccInfo();
        }
        private void getSteamId64()
        {
            string[] steamLinkArr = steamLink.Split('/');
            string steamId = steamLinkArr.Last(l => { return l != ""; });
            if (steamLink.Contains("/profiles/"))
            {
                this.steamId64 = steamId;
            }
            else
            {
                string apiString = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + APIKey + "&vanityurl=" + steamId;
                var json = new WebClient().DownloadString(apiString);
                var list = JsonConvert.DeserializeObject<RootObjectSteamId64>(json);
                steamId64 = list.response.steamid;
            }
            this.steamId64 = steamId64;
        }
        public void parseAccInfo()
        {
            string apiString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + APIKey + "&steamids=" + steamId64;
            var json = new WebClient { Encoding = System.Text.Encoding.UTF8 }.DownloadString(apiString);
            var list = JsonConvert.DeserializeObject<RootobjectAccInfo>(json);
            nickname = list.response.players[0].personaname;
            steamPicture = list.response.players[0].avatarfull;
        }
        public void parseVacs()
        {
            // TODO
        }
        public class ResponseSteamId64
        {
            public string steamid { get; set; }
        }
        public class RootObjectSteamId64
        {
            public ResponseSteamId64 response { get; set; }
        }
        public class RootobjectAccInfo
        {
            public ResponseAccInfo response { get; set; }
        }
        public class ResponseAccInfo
        {
            public Player[] players { get; set; }
        }
        public class Player
        {
            public string personaname { get; set; }
            public string avatarfull { get; set; }
        }
    }
}
