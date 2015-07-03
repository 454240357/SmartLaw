using System; 
using System.Web; 

namespace SmartLaw.App_Code
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            try
            {
                if (Session["SkinId"] == null)
                {
                    Page.Theme = GetSkin();
                }
                else
                {
                    Page.Theme = Session["SkinId"].ToString();
                }
            }
            catch
            {
                Page.Theme = "Windows7";
            }
            base.OnPreInit(e);
        }

        public string GetSkin()
        {
            HttpCookie cookie = Request.Cookies["CustomSkin"];
            if (cookie == null)
                return "Windows7";
            return cookie.Values["Skin"];
        }
    }
}