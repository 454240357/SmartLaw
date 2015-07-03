using System; 
using Telerik.Web.UI;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.EndUserManage
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                _user = SessionUser.GetSession();
                RadPanelItem radPanelItem3 = new RadPanelItem("终端用户管理");
               
                if (_user.hasAuthority("Auth_EndUser_Retrieve"))
                {
                    radPanelItem3.Items.Add(new RadPanelItem("检索终端用户", "EndUserList.aspx"));
                }
                if (_user.hasAuthority("Auth_EndUser_Add"))
                {
                    radPanelItem3.Items.Add(new RadPanelItem("添加终端用户", "AddEndUser.aspx"));
                }  
                RadPanelBar1.Items.Add(radPanelItem3); 
            }
        }
    }
}