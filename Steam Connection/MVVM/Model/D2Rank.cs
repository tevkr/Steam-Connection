using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Connection.MVVM.Model
{
    [Serializable]
    class D2Rank
    {
        private int rank;
        private int star;
        private int leaderRank;
        public D2Rank()
        {
            rank = star = leaderRank = 0;
        }
        public D2Rank(int rank, int star, int leaderRank)
        {
            this.rank = rank;
            this.star = star;
            this.leaderRank = leaderRank;
        }
        public string getRank()
        {
            return "/Images/Ranks/Dota2/SeasonalRank" + rank + "-" + star + ".png";
        }
        public string getLeaderRank()
        {
            return leaderRank.ToString();
        }
    }
}
