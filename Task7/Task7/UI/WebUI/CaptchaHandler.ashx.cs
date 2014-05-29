using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Drawing.Imaging;

namespace WebUI
{
    public class CaptchaHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            const int width = 75;
            const int height = 25;
            var backgroundColor = Color.DarkGray;
            var foregroundBrush = Brushes.Red;
            const int fontSize = 16;

            if (context.Session["Captcha"] == null)
                return;

            var captchaTxt = context.Session["Captcha"].ToString();
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.Clear(backgroundColor);

            var font = new Font("Calibri", fontSize, FontStyle.Bold);

            graphics.DrawString(captchaTxt, font, foregroundBrush, 0, 0);
            font.Dispose();

            var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);

            graphics.Dispose();

            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(stream.GetBuffer());
            context.Response.Flush();
    
            stream.Close();
            bitmap.Dispose();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}