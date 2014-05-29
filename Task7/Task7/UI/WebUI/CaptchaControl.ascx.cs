using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;

namespace WebUI
{
    public partial class CaptchaControl : System.Web.UI.UserControl
    {
        public static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        protected string PreviousCaptcha
        {
            get;
            private set;
        }

        public bool RightInput
        {
            get { return (PreviousCaptcha == txtInput.Text); }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            if (HttpContext.Current.Session["Captcha"] != null)
            {
                PreviousCaptcha = HttpContext.Current.Session["Captcha"].ToString();
            }

            const string symbols = "0123456789";
            var rnd = new byte[6];
            var chars = new char[6];
            Rand.GetBytes(rnd);

            for (int i = 0; i < 6; i++)
                chars[i] = symbols[rnd[i] % 10];

            HttpContext.Current.Session.Add("Captcha", new String(chars));
        }

        public void Clear()
        {
            txtInput.Text = "";
        }

        /*public void LoadCaptcha()
        {
            if (HttpContext.Current.Session["Captcha"] != null)
            {
                PreviousCaptcha = HttpContext.Current.Session["Captcha"].ToString();
            }

            const string symbols = "0123456789";
            var rnd = new byte[6];
            var chars = new char[6];
            Rand.GetBytes(rnd);

            for (int i = 0; i < 6; i++)
                chars[i] = symbols[rnd[i] % 10];

            HttpContext.Current.Session.Add("Captcha", new String(chars));
        }*/
    }
}