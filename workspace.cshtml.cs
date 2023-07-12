using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using HtmlAgilityPack;
namespace CryptoWallet.Pages
{
    public class workspaceModel : PageModel
    {
        public static List<Models.Crypto> cryptos = new List<Models.Crypto>();
        public static Models.Crypto ? crypto { get; set; }
        public const string StartPageLink = @"https://www.google.com/search?q=%D0%BA%D1%83%D1%80%D1%81+%D0%B4%D0%BE%D0%BB%D0%BB%D0%B0%D1%80%D0%B0&oq=%D0%BA%D1%83%D1%80%D1%81+%D0%B4%D0%BE%D0%BB%D0%BB%D0%B0%D1%80%D0%B0&aqs=chrome..69i57j0i10i512j0i10i131i433j0i512j0i10i131i433j0i10j69i61l2.1878j0j7&sourceid=chrome&ie=UTF-8";
        public void GetCurrs()
        {
            var htmlDoc = new HtmlWeb().Load(StartPageLink);
            var rows = htmlDoc.DocumentNode.SelectNodes("//div[@class='dDoNo ikb4Bb gsrt']//span[@class='DFlfde SwHCTb']");
            foreach (var cell in rows)
            {
                Models.CurrContainer.UsdCurr = cell.InnerText + " ₽";
                break;
            }
            var htmlBitcoin = new HtmlWeb().Load(@"https://www.google.com/search?q=%D0%BE%D1%82%D0%BD%D0%BE%D1%88%D0%B5%D0%BD%D0%B8%D0%B5+%D0%B1%D0%B8%D1%82%D0%BA%D0%BE%D0%B8%D0%BD%D0%B0+%D0%BA+%D0%B4%D0%BE%D0%BB%D0%BB%D0%B0%D1%80%D1%83&oq=%D0%BE%D1%82%D0%BD%D0%BE%D1%88%D0%B5%D0%BD%D0%B8%D0%B5+%D0%B1%D0%B8%D1%82%D0%BA%D0%BE%D0%B8%D0%BD%D0%B0+%D0%BA+%D0%B4%D0%BE%D0%BB%D0%BB%D0%B0%D1%80%D1%83&aqs=chrome.0.0i512j69i57.4474j1j7&sourceid=chrome&ie=UTF-8");
            var what = htmlBitcoin.DocumentNode.SelectNodes("//span[@class='pclqee']");
            foreach (var cell in what)
            {
                Models.CurrContainer.BitcoinCurr = cell.InnerText + " $";
                break;
            }
        }

        public void OnGet()
        {
            GetCurrs();
            cryptos.Clear();            
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cryptoHelper;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"SELECT cryptoName, cryptoCount, cryptoPrice FROM {Models.User.login}";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                var crypto = new Models.Crypto();
                crypto.setName(reader.GetValue(0).ToString());
                crypto.allTokens = Convert.ToDouble(reader.GetValue(1));
                crypto.usdForTokenBought = (decimal)reader.GetValue(2);
                crypto.setTicket();
                crypto.setUsdFTN();
                crypto.setUsdFAT();
                crypto.setUsdFATN();
                crypto.setUsdIncome();
                crypto.setPercentOI();
                cryptos.Add(crypto);
            }
        }
        public void OnPost()
        {
            int q = Int32.Parse(Request.Form["nameOfCrypto"]);
            crypto = cryptos[q];
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cryptoHelper;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlExpression = $"DELETE FROM {Models.User.login} WHERE cryptoName LIKE '{crypto.name}'";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            cryptos.Remove(crypto);
            crypto = new Models.Crypto();
            connection.Close();
            RedirectPermanent("https://localhost:44300/workspace");
        }
    }
}
