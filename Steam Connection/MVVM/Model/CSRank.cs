using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Connection.MVVM.Model
{
    [Serializable]
    class CSRank
    {
        private string rank;
        public CSRank()
        {
            rank = "skillgroup_none";
        }
        public CSRank(string rank)
        {
            this.rank = rank;
        }
        public string getRank()
        {
            return "/Images/Ranks/CSGO/" + rank + ".png";
        }
    }
}
