using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Web.Configuration;
using SmartLaw.App_Code;
using SmartLaw.BLL;
using System.Data; 
using System.Configuration;

namespace SmartLaw.Admin
{
    public partial class MasterPage2 : System.Web.UI.MasterPage
    {
        SessionUser user; 
        SysCodeDetail scd = new SysCodeDetail();
        public string LoginId;
        public static string titlestr = ConfigurationManager.AppSettings["Title"];
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            LoginId = user.UserInfo.UserID;
            if (!IsPostBack)
            {
                DataSet ds = scd.GetListBySysCode(user.UserInfo.UserID, "Role");
                string role = "";
                if (ds.Tables[0].Rows.Count != 0)
                {
                    role = ds.Tables[0].Rows[0]["SysCodeDetialContext"].ToString();
                }
                Ltr_OperatorInfo.Text = "(" + role + ")"; 
            }
        }

        protected void LB_Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            ClearPwdCookie();
            string loginUrl = WebConfigurationManager.AppSettings["LoginPageUrl"].Trim();
            Response.Redirect(loginUrl);
        }

        private void ClearPwdCookie()
        {
            HttpCookie cookie = Request.Cookies["Login"];
            if (cookie == null)
                return;
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
        }
        public string getUserName()
        {
            return user.UserInfo.UserName;
        }

        public string getUserInfo()
        {
            string targetUrl = Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.ToLower().IndexOf("/admin")) + "/Admin/SystemManage/SysUserDetail.aspx" + "?LoginID=" + user.UserInfo.UserID; 
            return targetUrl;
        } 
    }
}