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
    public class loginModel : PageModel
    {
        
        public void getOut()
        {
            Models.User.login = null;
            Response.Redirect("https://localhost:44300/");
        }
       
        public static bool checker(string login, string password)
        {
            bool check = false;
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cryptoHelper;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"SELECT * FROM users WHERE login ='{login}' AND password ='{password}'";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                check = true;
            }
            return check;
        }
        public static bool registred = true;
        public static bool corpsw = true;
        public void OnGet()
        {   
        }
        public void OnPost()
        {
            if (checker(Request.Form["login"], Request.Form["psw"]))
            {
                registred = true;
                corpsw = true;
                Models.User.SetLog(Request.Form["login"]);
                Response.Redirect("https://localhost:44300/");
            }
            else
            {
                registred = false;
                corpsw = false;
            }
        }
    }
}
