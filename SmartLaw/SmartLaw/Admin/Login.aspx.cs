using System; 
using System.Web; 
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Text;
using SmartLaw.BLL;
using System.Configuration;

namespace SmartLaw.Admin
{
	public partial class Login : System.Web.UI.Page
	{
	    readonly Log _log = new Log();
        public static string Titlestr = ConfigurationManager.AppSettings["Title"];
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                try
                {
                    if (Session["SkinId"] == null)
                    {
                        RadAjaxLoadingPanel1.Skin = GetSkin();
                        RadWindowManager1.Skin = GetSkin();
                    }
                    else
                    {
                        RadAjaxLoadingPanel1.Skin = Session["SkinId"].ToString();
                        RadWindowManager1.Skin = Session["SkinId"].ToString();
                    }
                }
                catch
                {
                    RadAjaxLoadingPanel1.Skin = "Vista";
                    RadWindowManager1.Skin = "Vista";
                }
                ValidatePwd();
            }
		}

        public string GetSkin()
        {
            HttpCookie cookie = Request.Cookies["CustomSkin"];
            if (cookie == null)
                return "Vista";
            return cookie.Values["Skin"];
        }
        private void SetSkin(string value)
        {
            HttpCookie cookie = new HttpCookie("CustomSkin");
            cookie.Values["Skin"] = value;
            //设置过期时间为一个月
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
        }

        private void StoragePwd(string loginId, string password)
        {
            HttpCookie cookie = new HttpCookie("Login");
            cookie.Values["loginId"] = loginId;
            cookie.Values["pwd"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            //设置过期时间为一年
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
        }

        private void ValidatePwd()
        {
            HttpCookie cookie = Request.Cookies["Login"];
            if (cookie == null)
                return;
            string loginId = cookie.Values["loginId"];
            string password;
            try
            {
                string psw = cookie.Values["pwd"].Replace(' ', '+'); 
                password = Encoding.UTF8.GetString(Convert.FromBase64String(psw));
            }
            catch
            {
                return;
            }

            SessionUser user = new SessionUser();
            user.SetSession(loginId, password, Session); 
            if (SessionUser.IsLogined())
            {
                string skinValue = "Windows7";
                Session["SkinId"] = skinValue;
                Model.Log logM = new Model.Log();
                logM.OperationItem = "操作员登录";
                logM.Operator = loginId;
                logM.OperationTime = DateTime.Now;
                logM .OperationDetail = "ip:【" + Request.UserHostAddress + "】";
                _log.Add(logM);
                if (Request.QueryString["returnurl"] == null)
                    Response.Redirect("NewsManage/NewsList.aspx");
                else
                    Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["returnurl"].ToString()));
            }
            TB_LoginId.Text = loginId;
        }

        protected void Bt_Login_Click(object sender, EventArgs e)
        {
            string loginId = TB_LoginId.Text.Trim();
            string password = TB_Password.Text.Trim();
            if (CB_Storage.Checked)
                StoragePwd(loginId, password);
            SessionUser user = new SessionUser();
            user.SetSession(loginId, password, Session); 
            if (SessionUser.IsLogined())
            {
                Model.Log logM = new Model.Log();
                logM.OperationItem = "操作员登录";
                logM.Operator = loginId;
                logM.OperationTime = DateTime.Now;
                logM.OperationDetail = "ip:【" + Request.UserHostAddress + "】";
                _log.Add(logM);
                if (Request.QueryString["returnurl"] == null)
                    Response.Redirect("NewsManage/NewsList.aspx");
                else
                    Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["returnurl"].ToString()));
            }
            else
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('用户名不存在或密码错误！');", true);
            }
        }
	}
}