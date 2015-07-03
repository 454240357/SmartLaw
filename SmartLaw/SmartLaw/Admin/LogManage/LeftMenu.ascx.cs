using System; 
using System.Web.UI; 
using Telerik.Web.UI;

namespace SmartLaw.Admin.LogManage
{
    public partial class LeftMenu : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadPanelItem radPanelItem3 = new RadPanelItem("日志管理");
                radPanelItem3.Items.Add(new RadPanelItem("日志查看", "Loglist.aspx"));
                RadPanelBar1.Items.Add(radPanelItem3);
            }
        }
    }
}