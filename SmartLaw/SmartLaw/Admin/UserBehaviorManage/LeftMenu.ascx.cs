using System; 
using System.Web.UI; 
using Telerik.Web.UI;

namespace SmartLaw.Admin.UserBehaviorManage
{
    public partial class LeftMenu : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadPanelItem radPanelItem3 = new RadPanelItem("用户行为分析");
                radPanelItem3.Items.Add(new RadPanelItem("用户行为统计", "UserBehaviorStatistics.aspx")); 
                RadPanelBar1.Items.Add(radPanelItem3);
            }
        }
    }
}