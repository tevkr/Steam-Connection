using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Connection.Validations
{
    class SteamDirValidation
    {
        public static bool isSteamDirCorrect(string steamDir)
        {
            return steamDir.ToLower().Split('\\').Last().Equals("steam.exe") || steamDir.ToLower().Split('/').Last().Equals("steam.exe");
        }
    }
}
