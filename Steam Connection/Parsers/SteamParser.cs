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
        private string steamId64;
        private string nickname;
        private string steamPicture;
        public int vacCount;
        public SteamParser(string steamId64)
        {
            this.steamId64 = steamId64;
            parseAccInfo();
            parseVacs();
        }
        private string getPlayerSummariesString(string steamId)
        {
            return "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" 
                + APIKey + "&steamids=" + steamId;
        }
        private string getPlayerBansString(string steamId)
        {
            return "http://api.steampowered.com/ISteamUser/GetPlayerBans/v1/?key=" 
                + APIKey + "&steamids=" + steamId;
        }
        private void parseAccInfo()
        {
            string apiString = getPlayerSummariesString(steamId64);
            var json = new WebClient { Encoding = System.Text.Encoding.UTF8 }.DownloadString(apiString);
            var list = JsonConvert.DeserializeObject<RootobjectAccInfo>(json);
            nickname = list.response.players[0].personaname;
            steamPicture = list.response.players[0].avatarfull;
        }
        private void parseVacs()
        {

            string apiString = getPlayerBansString(steamId64);
            var json = new WebClient { Encoding = System.Text.Encoding.UTF8 }.DownloadString(apiString);
            var list = JsonConvert.DeserializeObject<RootObjectVacInfo>(json);
            vacCount = Int32.Parse(list.players[0].NumberOfVACBans);
        }
        public string getNickname()
        {
            return this.nickname;
        }
        public string getSteamPicture()
        {
            return this.steamPicture;
        }
        public int getVacCount()
        {
            return this.vacCount;
        }
        // SteamId64 from custom URL
        private class ResponseSteamId64
        {
            public string steamid { get; set; }
        }
        private class RootObjectSteamId64
        {
            public ResponseSteamId64 response { get; set; }
        }
        // Account info from SteamId64
        private class RootobjectAccInfo
        {
            public ResponseAccInfo response { get; set; }
        }
        private class ResponseAccInfo
        {
            public Player[] players { get; set; }
        }
        private class Player
        {
            public string personaname { get; set; }
            public string avatarfull { get; set; }
        }

        // VAC from SteamId64
        private class RootObjectVacInfo
        {
            public PlayerVacInfo[] players { get; set; }
        }
        private class PlayerVacInfo
        {
            public string NumberOfVACBans { get; set; }
        }
        
    }
}
