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
    public class reginModel : PageModel
    {
        public static bool taken=false;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if (checkerReg(Request.Form["login"]))
            {
                taken = false;
                CryptoWallet.Models.User.SetLog(Request.Form["login"]);
                Models.User.SetPass(Request.Form["psw"]);
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cryptoHelper;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlExpression = $"INSERT INTO users(login,password) VALUES ('{Models.User.login}','{Models.User.password}')";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                sqlExpression = $"CREATE TABLE {Models.User.login.ToString()} (id INT IDENTITY(1,1), cryptoName TEXT, cryptoCount FLOAT, cryptoPrice DECIMAL)";
                command = new SqlCommand(sqlExpression, connection);
                reader = command.ExecuteReader();
                Response.Redirect("https://localhost:44300/");
            }
            else taken = true;

        }
        public static bool checkerReg(string reg)
        {
            bool check = true;
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cryptoHelper;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"SELECT * FROM users WHERE login ='{reg}'";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                check = false;
            }
            connection.Close();
            return check;
        }
    }
}
