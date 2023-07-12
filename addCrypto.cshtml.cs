using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace CryptoWallet.Pages
{
    public class addCryptoModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cryptoHelper;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"INSERT INTO {Models.User.login}(cryptoName,cryptoCount,cryptoPrice) VALUES ('{Request.Form["nameAddCr"]}','{Int32.Parse(Request.Form["numberAddCr"])}','{double.Parse(Request.Form["sumAddCr"])}')";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            Response.Redirect("https://localhost:44300/workspace");
        }
    }
}
