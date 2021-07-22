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
        public int rank { get; set; }
        public int star { get; set; }
        public int leaderRank { get; set; }
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
            return "";//ranks[rank, star]
        }
        public string getLeaderRank()
        {
            return leaderRank.ToString();
        }
        private static string[,] ranks = new string[9, 7]
        {
            {//Uncalibrated
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/e/e7/SeasonalRank0-0.png/230px-SeasonalRank0-0.png", "", "",
                "", "", "", ""
            },
            {//Herald
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/8/85/SeasonalRank1-1.png/140px-SeasonalRank1-1.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/e/ee/SeasonalRank1-2.png/140px-SeasonalRank1-2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/0/05/SeasonalRank1-3.png/140px-SeasonalRank1-3.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/6/6d/SeasonalRank1-4.png/140px-SeasonalRank1-4.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/2/2b/SeasonalRank1-5.png/140px-SeasonalRank1-5.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/9/94/SeasonalRank1-6.png/140px-SeasonalRank1-6.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/1/12/SeasonalRank1-7.png/140px-SeasonalRank1-7.png"
            },
            {//Guardian
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/c/c7/SeasonalRank2-1.png/140px-SeasonalRank2-1.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/2/2c/SeasonalRank2-2.png/140px-SeasonalRank2-2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/f/f5/SeasonalRank2-3.png/140px-SeasonalRank2-3.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/b/b4/SeasonalRank2-4.png/140px-SeasonalRank2-4.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/3/32/SeasonalRank2-5.png/140px-SeasonalRank2-5.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/7/72/SeasonalRank2-6.png/140px-SeasonalRank2-6.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/c/c6/SeasonalRank2-7.png/140px-SeasonalRank2-7.png"
            },
            {//Crusader
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/8/82/SeasonalRank3-1.png/140px-SeasonalRank3-1.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/6/6e/SeasonalRank3-2.png/140px-SeasonalRank3-2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/6/67/SeasonalRank3-3.png/140px-SeasonalRank3-3.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/8/87/SeasonalRank3-4.png/140px-SeasonalRank3-4.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/b/b1/SeasonalRank3-5.png/140px-SeasonalRank3-5.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/3/33/SeasonalRank3-6.png/140px-SeasonalRank3-6.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/6/66/SeasonalRank3-7.png/140px-SeasonalRank3-7.png"
            },
            {//Archon
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/7/76/SeasonalRank4-1.png/140px-SeasonalRank4-1.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/8/87/SeasonalRank4-2.png/140px-SeasonalRank4-2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/6/60/SeasonalRank4-3.png/140px-SeasonalRank4-3.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/4/4a/SeasonalRank4-4.png/140px-SeasonalRank4-4.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/a/a3/SeasonalRank4-5.png/140px-SeasonalRank4-5.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/7/7e/SeasonalRank4-6.png/140px-SeasonalRank4-6.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/9/95/SeasonalRank4-7.png/140px-SeasonalRank4-7.png"
            },
            {//Legend
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/7/79/SeasonalRank5-1.png/140px-SeasonalRank5-1.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/5/52/SeasonalRank5-2.png/140px-SeasonalRank5-2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/8/88/SeasonalRank5-3.png/140px-SeasonalRank5-3.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/2/25/SeasonalRank5-4.png/140px-SeasonalRank5-4.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/8/8e/SeasonalRank5-5.png/140px-SeasonalRank5-5.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/2/2f/SeasonalRank5-6.png/140px-SeasonalRank5-6.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/c/c7/SeasonalRank5-7.png/140px-SeasonalRank5-7.png"
            },
            {//Ancient
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/e/e0/SeasonalRank6-1.png/140px-SeasonalRank6-1.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/1/1c/SeasonalRank6-2.png/140px-SeasonalRank6-2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/d/da/SeasonalRank6-3.png/140px-SeasonalRank6-3.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/d/db/SeasonalRank6-4.png/140px-SeasonalRank6-4.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/4/47/SeasonalRank6-5.png/140px-SeasonalRank6-5.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/b/bd/SeasonalRank6-6.png/140px-SeasonalRank6-6.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/b/b8/SeasonalRank6-7.png/140px-SeasonalRank6-7.png"
            },
            {//Divine
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/b/b7/SeasonalRank7-1.png/140px-SeasonalRank7-1.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/8/8f/SeasonalRank7-2.png/140px-SeasonalRank7-2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/f/fd/SeasonalRank7-3.png/140px-SeasonalRank7-3.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/1/13/SeasonalRank7-4.png/140px-SeasonalRank7-4.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/3/33/SeasonalRank7-5.png/140px-SeasonalRank7-5.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/a/a1/SeasonalRank7-6.png/140px-SeasonalRank7-6.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/c/c1/SeasonalRank7-7.png/140px-SeasonalRank7-7.png"
            },
            {//Immortal
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/f/f2/SeasonalRankTop0.png/140px-SeasonalRankTop0.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/a/ad/SeasonalRankTop2.png/140px-SeasonalRankTop2.png",
                "https://gamepedia.cursecdn.com/dota2_gamepedia/thumb/4/46/SeasonalRankTop4.png/140px-SeasonalRankTop4.png",
                "",
                "", "",""
            }
        };
    }
}
