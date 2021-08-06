using Steam_Connection.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Connection.MVVM.Model
{
    [Serializable]
    class Account
    {
        public string nickname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string steamId64 { get; set; }
        public string steamPicture { get; set; }
        public int vacCount { get; set; }
        public D2Rank d2Rank { get; set; }
        public CSRank cSRank { get; set; }
        public Account(){}
        public Account(string nickname,
            string login,
            string password,
            string steamId64,
            string steamPicture,
            int vacCount,
            int d2rank, int d2star, int d2leaderRank,
            int csrank)
        {
            this.nickname = nickname;
            this.login = login;
            this.password = password;
            this.steamId64 = steamId64;
            this.steamPicture = steamPicture;
            this.vacCount = vacCount;
            this.d2Rank = new D2Rank(d2rank, d2star, d2leaderRank);
            this.cSRank = new CSRank(); //TODO
        }
        public Account(string steamId64, string login, string password)
        {
            this.login = login;
            this.password = password;
            this.steamId64 = steamId64;
            SteamParser steamParser = new SteamParser(steamId64);
            steamParser.parseAccInfo();
            steamParser.parseVacs();
            this.nickname = steamParser.getNickname();
            this.steamPicture = steamParser.getSteamPicture();
            this.vacCount = steamParser.getVacCount();
            Dota2RankParser d2RankParser = new Dota2RankParser(steamId64);
            d2RankParser.parseDota2Rank();
            this.d2Rank = d2RankParser.getD2Rank();
            //TODO vacCount d2Rank
            //this.vacCount = parser.vacCount;
            //this.d2Rank = new D2Rank(parser.rank, parser.star, parser.leaderRank);
        }
    }
}
