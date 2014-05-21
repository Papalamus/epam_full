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
    public partial class ArticleInput : System.Web.UI.Page
    {

        private Controller _ctrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpSessionState ss = HttpContext.Current.Session;
            _ctrl = (Controller)HttpContext.Current.Session["ctrl"];
            if (_ctrl == null)
            {
                _ctrl = new Controller();
            }

            HttpContext.Current.Session["ctrl"] = _ctrl;
        }
        public void CreatePerson(object sender, EventArgs e)
        {
            int articleCode = int.Parse(TextBox1.Text);
            int value = int.Parse(TextBox1.Text);
            string title = TextBox3.Text.Trim();
            Article a = new Article() { ArticleCode = articleCode ,Value = value,Title = title};
            _ctrl.Insert(a);
            _ctrl.IsRefreshNeed=true;
            Response.Redirect("WebForm1.aspx");
        }
    }
}