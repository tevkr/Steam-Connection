using FuzzySharp;
using Steam_Connection.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steam_Connection.Core.Config
{
    [Serializable]
    class Config
    {
        private static Config config;
        private Config()
        {
            this.accounts = new List<Account>();
            supportedLanguages = new List<CultureInfo>();
            supportedLanguages.Add(new CultureInfo("ru-RU"));
            supportedLanguages.Add(new CultureInfo("en-US"));
            d2RanksMode = cSRanksMode = nonConfirmationMode = themeMode = vacMode = closeMode = pinMode = false;
            pinCode = "";
            steamDir = "";
        }
        public static Config getInstance()
        {
            if (config == null)
            {
                if (File.Exists("config.dat"))
                {
                    config = deserialize();
                    if (config.language != null)
                        config.language = config.language;
                }
                else
                    config = new Config();
            }
            return config;
        }
        public enum Languages
        {
            Russian = 0,
            English = 1
        }
        public List<CultureInfo> supportedLanguages { get; set; }
        private CultureInfo _language;
        public CultureInfo language
        {
            get
            {
                return _language;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("Languages/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "en-US":
                        dict.Source = new Uri(String.Format("Languages/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Languages/lang.en-US.xaml", UriKind.Relative);
                        break;
                }
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                    where d.Source != null && d.Source.OriginalString.StartsWith("Languages/lang.")
                    select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
                _language = System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
        }
        public List<Account> accounts { get; }
        public string steamDir { get; set; }
        //public Theme theme { get; set; }
        //public Language language { get; set; }
        public string pinCode { get; set; }
        public bool d2RanksMode { get; set; }
        public bool cSRanksMode { get; set; }
        public bool nonConfirmationMode { get; set; }
        public bool themeMode { get; set; }
        public bool vacMode { get; set; }
        public bool closeMode { get; set; }
        public bool pinMode { get; set; }
        /*public async void updateAccInfo()
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                accounts[i] = new Account(accounts[i].steamId64, accounts[i].login, accounts[i].password);
            }
        }*/
        public void saveChanges()
        {
            serialize(config);
        }

        public void clear()
        {
            config = new Config();
            saveChanges();
        }

        public List<int> searchByNickname(string nickname = "")
        {
            var foundAccountsIndexes = new List<int>();
            if (nickname != "")
            {
                for (int i = 0; i < accounts.Count; i++)
                {
                    if (accounts[i].nickname.ToLower().StartsWith(nickname) ||
                        Fuzz.Ratio(nickname.ToLower(), accounts[i].nickname.ToLower()) > 40)
                    {
                        foundAccountsIndexes.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < accounts.Count; i++) 
                    foundAccountsIndexes.Add(i);
            }
            return foundAccountsIndexes;
            /*if (nickname != "")
            {
                var foundAccounts = new List<Account>(); ;
                foreach (var account in accounts)
                    if (account.nickname.ToLower().StartsWith(nickname) ||
                        Fuzz.Ratio(nickname.ToLower(),
                                   account.nickname.ToLower()
                                   ) > 50)
                    {
                        foundAccounts.Add(account);
                    }
                return foundAccounts;
            }
            else
            {
                return accounts;
            }*/
        }
        private const string cryptoKey =
            "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";

        private const int keySize = 256;
        private const int ivSize = 16; // block size is 128-bit
        private static void writeObjectToStream(Stream outputStream, Object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(outputStream, obj);
        }
        private static object readObjectFromStream(Stream inputStream)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            object obj = binForm.Deserialize(inputStream);
            return obj;
        }
        private static CryptoStream createEncryptionStream(byte[] key, Stream outputStream)
        {
            byte[] iv = new byte[ivSize];

            using (var rng = new RNGCryptoServiceProvider())
            {
                // Using a cryptographic random number generator
                rng.GetNonZeroBytes(iv);
            }

            // Write IV to the start of the stream
            outputStream.Write(iv, 0, iv.Length);

            Rijndael rijndael = new RijndaelManaged();
            rijndael.KeySize = keySize;

            CryptoStream encryptor = new CryptoStream(
                outputStream,
                rijndael.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);
            return encryptor;
        }
        private static CryptoStream createDecryptionStream(byte[] key, Stream inputStream)
        {
            byte[] iv = new byte[ivSize];

            if (inputStream.Read(iv, 0, iv.Length) != iv.Length)
            {
                throw new ApplicationException("Failed to read IV from stream.");
            }

            Rijndael rijndael = new RijndaelManaged();
            rijndael.KeySize = keySize;

            CryptoStream decryptor = new CryptoStream(
                inputStream,
                rijndael.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);
            return decryptor;
        }
        private static void serialize(Config config)
        {
            byte[] key = Convert.FromBase64String(cryptoKey);

            using (FileStream file = new FileStream(Environment.CurrentDirectory + @"\config.dat", FileMode.Create))
            {
                using (CryptoStream cryptoStream = createEncryptionStream(key, file))
                {
                    writeObjectToStream(cryptoStream, config);
                }
            }
        }
        private static Config deserialize()
        {
            byte[] key = Convert.FromBase64String(cryptoKey);

            using (FileStream file = new FileStream(Environment.CurrentDirectory + @"\config.dat", FileMode.Open))
            using (CryptoStream cryptoStream = createDecryptionStream(key, file))
            {
                return (Config)readObjectFromStream(cryptoStream);
            }
        }
    }
}
