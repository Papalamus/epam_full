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
using WebClient.Classes;

namespace WebClient
{
    public partial class WebForm1 : System.Web.UI.Page
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
            if (_ctrl.IsRefreshNeed)
            {
                TableMaker.MakeTable(_ctrl.GetAll(), Table1);
                Table1.Visible = true;
            }

            


            HttpContext.Current.Session["_ctrl"] = _ctrl;
        }

        public void ChangeEntity(object sender, EventArgs e)
        {
            HtmlAnchor anchor = sender as HtmlAnchor;
            switch (anchor.Name)
            {
                case "Person":
                    _ctrl.ChosenEntity = EntityType.Person;
                    break;
                case "Article":
                    _ctrl.ChosenEntity = EntityType.Article;
                    break;
            }
            HttpContext.Current.Session["_ctrl"] = _ctrl;
            RefreshPage();
        }

        public void ChangeConnecter(object sender, EventArgs e)
        {
            HtmlAnchor anchor = sender as HtmlAnchor;
            switch (anchor.Name)
            {
                case "Ado":
                    _ctrl.ChosenConnecter = ConnecterType.ADO;
                    break;
                case "MyORM":
                    _ctrl.ChosenConnecter = ConnecterType.MyORM;
                    break;
            }
            HttpContext.Current.Session["_ctrl"] = _ctrl;
            RefreshPage();
        }

        public void GetAll(object sender, EventArgs e)
        {
            //GridView1.DataSource = _ctrl.GetAll();
            TableMaker.MakeTable(_ctrl.GetAll(), Table1);
            Table1.Visible = true;
            //GridView1.DataSource = resultTable;
            //GridView1.DataBind();
        }

        public void RedirectOnInput(object sender, EventArgs e)
        {
            if (!CaptchaHandler.CheckCaptcha(Context.Session, CaptchaValue.Text))
            {
                return;
            }
            switch (_ctrl.ChosenEntity)
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
            _ctrl.IsRefreshNeed = true;
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }
    }
}