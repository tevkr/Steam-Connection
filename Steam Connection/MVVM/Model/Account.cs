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
        public string steamLink { get; set; }
        public string steamPicture { get; set; }
        public int vacCount { get; set; }
        public D2Rank d2Rank { get; set; }
        public CSRank cSRank { get; set; }
        public Account(string nickname,
            string login,
            string password,
            string steamLink,
            string steamPicture,
            int vacCount,
            int d2rank, int d2star, int d2leaderRank,
            int csrank)
        {
            this.nickname = nickname;
            this.login = login;
            this.password = password;
            this.steamLink = steamLink;
            this.steamPicture = steamPicture;
            this.vacCount = vacCount;
            this.d2Rank = new D2Rank(d2rank, d2star, d2leaderRank);
            this.cSRank = new CSRank(); //TODO
        }
        public Account(string steamLink, string login, string password)
        {
            this.login = login;
            this.password = password;
            this.steamLink = steamLink;
            SteamParser steamParser = new SteamParser(steamLink);
            this.nickname = steamParser.nickname;
            this.steamPicture = steamParser.steamPicture;
            //TODO vacCount d2Rank
            //this.vacCount = parser.vacCount;
            //this.d2Rank = new D2Rank(parser.rank, parser.star, parser.leaderRank);
        }
    }
}
