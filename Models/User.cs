using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWallet.Models
{
    public static class User
    {
        public static string login { get; set; }
        public static string password { get; set; }
        public static void SetLog(string login)
        {
            User.login = login;
        }
        public static void SetPass(string pass)
        {
            User.password = pass;
        }
    }
}
