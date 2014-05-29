using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects.Entities;
using WebClient.Classes;

namespace WebClient.UserForms
{
    public partial class PersonInput : System.Web.UI.Page
    {
        private Controller _ctrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            HttpSessionState ss = HttpContext.Current.Session;
            _ctrl = (Controller)HttpContext.Current.Session["_ctrl"];
            if (_ctrl == null)
            {
                _ctrl = new Controller();
            }
            
            HttpContext.Current.Session["_ctrl"] = _ctrl;
        }

        public void CreatePerson(object sender, EventArgs e)
        {
            CaptchaHandler.CheckCaptcha(Context.Session, CaptchaValue.Text);
            int inn = int.Parse(TextBox1.Text);
            string name = TextBox2.Text.Trim();
            string surname = TextBox3.Text.Trim();
            Person p = new Person(){INN = inn,Name= name,Surname = surname};
            _ctrl.Insert(p);
            _ctrl.IsRefreshNeed = true;
            Response.Redirect("WebForm1.aspx");
        }
    }
}