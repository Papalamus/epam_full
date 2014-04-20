using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataObjects;
using DataObjects.DataBase.Interface;
using DataObjects.Entities;

namespace WebClient
{
    public partial class WebForm1 : System.Web.UI.Page
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
            if (ctrl.IsRefreshNeed)
            {
                GridView1.DataSource = ctrl.GetAll();
                GridView1.DataBind();
            }
            
            HttpContext.Current.Session["ctrl"] = ctrl;
        }

        public void ChangeEntity(object sender, EventArgs e)
        {
            HtmlAnchor anchor = sender as HtmlAnchor;
            switch (anchor.Name)
            {
                case "Person":
                    ctrl.ChosenEntity = EntityType.Person;
                    break;
                case "Article":
                    ctrl.ChosenEntity = EntityType.Article;
                    break;
            }
            HttpContext.Current.Session["ctrl"] = ctrl;
            RefreshPage();
        }

        public void ChangeConnecter(object sender, EventArgs e)
        {
            HtmlAnchor anchor = sender as HtmlAnchor;
            switch (anchor.Name)
            {
                case "Ado":
                    ctrl.ChosenConnecter = ConnecterType.ADO;
                    break;
                case "MyORM":
                    ctrl.ChosenConnecter = ConnecterType.MyORM;
                    break;
            }
            HttpContext.Current.Session["ctrl"] = ctrl;
            RefreshPage();
        }

        public void GetAll(object sender, EventArgs e)
        {
            GridView1.DataSource = ctrl.GetAll();
            GridView1.DataBind();
        }

        public void RedirectOnInput(object sender, EventArgs e)
        {
            switch (ctrl.ChosenEntity)
            {
               case EntityType.Article:
                    HttpContext.Current.Response.Redirect("ArticleInput.aspx");
                    break;
               case EntityType.Person:
                    HttpContext.Current.Response.Redirect("PersonInput.aspx");
                    break;
            }
        }

        private void RefreshPage()
        {
            ctrl.IsRefreshNeed = true;
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }
    }
}