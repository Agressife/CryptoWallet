using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoWallet.Pages
{
    public class signOutModel : PageModel
    {
        public void OnGet()
        {
            Models.User.login = null;
            reginModel.taken = false;
            Response.Redirect("https://localhost:44300/");
        }
    }
}
