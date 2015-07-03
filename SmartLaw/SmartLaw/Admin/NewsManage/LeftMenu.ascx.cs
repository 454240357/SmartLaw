using System; 
using Telerik.Web.UI;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.NewsManage
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                _user = SessionUser.GetSession();
                RadPanelItem radPanelItem3 = new RadPanelItem("条目管理");
               
                if (_user.hasAuthority("Auth_News_Retrieve"))
                {
                    radPanelItem3.Items.Add(new RadPanelItem("检索条目", "NewsList.aspx"));
                }
                if (_user.hasAuthority("Auth_News_Add"))
                {
                    radPanelItem3.Items.Add(new RadPanelItem("添加条目", "NewsEdit.aspx"));
                } 
                if (_user.hasAuthority("Auth_News_Examine"))
                {
                    radPanelItem3.Items.Add(new RadPanelItem("条目审核", "NewsCheck.aspx"));
                } 
                RadPanelBar1.Items.Add(radPanelItem3); 
            }
        }
    }
}