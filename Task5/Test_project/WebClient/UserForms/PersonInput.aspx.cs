using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects.Entities;

namespace WebClient.UserForms
{
    public partial class PersonInput : System.Web.UI.Page
    {
        private Controller ctrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            HttpSessionState ss = HttpContext.Current.Session;
            ctrl = (Controller)HttpContext.Current.Session["ctrl"];
            if (ctrl == null)
            {
                ctrl = new Controller();
            }
            
            HttpContext.Current.Session["ctrl"] = ctrl;
        }

        public void CreatePerson(object sender, EventArgs e)
        {
            int inn = int.Parse(TextBox1.Text);
            string name = TextBox2.Text.Trim();
            string surname = TextBox3.Text.Trim();
            Person p = new Person(){INN = inn,Name= name,Surname = surname};
            ctrl.Insert(p);
            ctrl.IsRefreshNeed = true;
            Response.Redirect("WebForm1.aspx");
        }
    }
}