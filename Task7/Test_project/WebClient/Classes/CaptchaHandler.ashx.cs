using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace WebClient.Classes
{
    /// <summary>
    /// Summary description for CaptchaHandler
    /// </summary>
    public class CaptchaHandler : IHttpHandler, IRequiresSessionState 
    {
        private const int _width = 50;
        private const int _height = 50;

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "image/png";
            Captcha captcha = Captcha.GetCaptcha(_width, _height);
            context.Session["Captcha"] = captcha.Value;
            captcha.Bmp.Save(response.OutputStream, ImageFormat.Png);
            response.Flush();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static bool CheckCaptcha(HttpSessionState session, string value)
        {
            return (string)session["Captcha"] == value;
        } 
        private class Captcha
        {
            public Bitmap Bmp { get; set; }
            public string Value { get; set; }
            private Random r = new Random();

            public Captcha(int width, int height)
            {
                Value = r.Next(100, 999).ToString();
                Bmp = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(Bmp))
                {
                    g.Clear(Color.GhostWhite);
                    g.DrawString(Value, new Font("Times New Roman", 12.0f), Brushes.Black, 10, 10);
                }
            }

           

            public static Captcha GetCaptcha(int width, int height)
            {
                return new Captcha(width, height);
            }
        }
        
    }
}