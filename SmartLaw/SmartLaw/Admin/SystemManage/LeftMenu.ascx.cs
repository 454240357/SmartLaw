using System; 
using System.Web.UI; 
using Telerik.Web.UI;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.SystemManage
{
    public partial class LeftMenu : UserControl
    {
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                RadPanelItem radPanelItem3 = new RadPanelItem("系统代码管理");
                radPanelItem3.Items.Add(new RadPanelItem("查看系统代码", "SysCodeList.aspx"));
                radPanelItem3.Items.Add(new RadPanelItem("新建系统代码", "AddSysCode.aspx"));
                if (_user.hasAuthority("Auth_Code_CRUD"))
                {
                    RadPanelBar1.Items.Add(radPanelItem3);
                }

                RadPanelItem radPanelItem2 = new RadPanelItem("管理操作员");
                radPanelItem2.Items.Add(new RadPanelItem("查询操作员", "SysUserList.aspx"));
                if (_user.hasAuthority("Auth_SysUser_Add"))
                {
                    radPanelItem2.Items.Add(new RadPanelItem("添加操作员", "AddSysUser.aspx"));
                }
                RadPanelBar1.Items.Add(radPanelItem2);
            }
        }
    }
}