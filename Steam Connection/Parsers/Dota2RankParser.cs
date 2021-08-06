using Newtonsoft.Json;
using Steam_Connection.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Connection.Parsers
{
    class Dota2RankParser
    {
        private string steamId64;
        private D2Rank d2Rank;
        public Dota2RankParser(string steamId64)
        {
            this.steamId64 = steamId64;
        }
        public void parseDota2Rank()
        {
            int rankTier = 0;
            int leaderboardRank = 0;
            long lSteamid64 = Convert.ToInt64(steamId64);
            long lSteamid32 = lSteamid64 - 76561197960265728;
            string steamId32 = Convert.ToString(lSteamid32);
            string APIString = "https://api.opendota.com/api/players/" + steamId32;
            var json = new WebClient().DownloadString(APIString);
            var list = JsonConvert.DeserializeObject<RootObject>(json);
            if (list.rank_tier != "null")
            {
                if (Convert.ToInt32(list.rank_tier) < 80)
                {
                    rankTier = Convert.ToInt32(list.rank_tier);
                }
                else
                {
                    leaderboardRank = Convert.ToInt32(list.leaderboard_rank);
                    if (leaderboardRank > 100)
                    {
                        rankTier = 80;
                    }
                    else if (leaderboardRank <= 100 && leaderboardRank > 10)
                    {
                        rankTier = 81;
                    }
                    else if (leaderboardRank <= 10)
                    {
                        rankTier = 82;
                    }
                }
                d2Rank = new D2Rank(rankTier / 10, rankTier % 10, leaderboardRank);
            }
            else
                d2Rank = new D2Rank();
        }
        public async void parseDota2RankAsync()
        {
            int rankTier = 0;
            int leaderboardRank = 0;
            long lSteamid64 = Convert.ToInt64(steamId64);
            long lSteamid32 = lSteamid64 - 76561197960265728;
            string steamId32 = Convert.ToString(lSteamid32);
            string APIString = "https://api.opendota.com/api/players/" + steamId32;
            var webClient = new WebClient();
            var json = await webClient.DownloadStringTaskAsync(APIString);
            var list = JsonConvert.DeserializeObject<RootObject>(json);
            if (list.rank_tier != "null")
            {
                if (Convert.ToInt32(list.rank_tier) < 80)
                {
                    rankTier = Convert.ToInt32(list.rank_tier);
                }
                else
                {
                    leaderboardRank = Convert.ToInt32(list.leaderboard_rank);
                    if (leaderboardRank > 100)
                    {
                        rankTier = 80;
                    }
                    else if (leaderboardRank <= 100 && leaderboardRank > 10)
                    {
                        rankTier = 81;
                    }
                    else if (leaderboardRank <= 10)
                    {
                        rankTier = 82;
                    }
                }
                d2Rank = new D2Rank(rankTier / 10, rankTier % 10, leaderboardRank);
            }
            else
                d2Rank = new D2Rank();
        }
        public D2Rank getD2Rank()
        {
            return this.d2Rank;
        }
        private class RootObject
        {
            public string rank_tier { get; set; }
            public string leaderboard_rank { get; set; }
        }
    }
}
