using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CryptoWallet.Models;
using System.Data.SqlClient;
namespace CryptoWallet.Pages
{
    public class changerModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {
            Crypto change = new Crypto();
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cryptoHelper;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            change.name = Request.Form["changerName"];
            change.usdForTokenBought = decimal.Parse(Request.Form["changerNewPrice"]);
            change.allTokens = double.Parse(Request.Form["changerNewNumber"]);
            change.setTicket();
            change.setUsdFTN();
            change.setUsdFAT();
            change.setUsdFATN();
            change.setUsdIncome();
            change.setPercentOI();
            string sqlExpression = $"UPDATE {Models.User.login} SET cryptoCount={change.allTokens}, cryptoPrice={change.usdForTokenBought} WHERE cryptoName LIKE '{Request.Form["changerName"]}'";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
            Response.Redirect("https://localhost:44300/workspace");
        }
    }
}
