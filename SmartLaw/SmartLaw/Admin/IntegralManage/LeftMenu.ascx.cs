using System; 
using System.Web.UI; 
using Telerik.Web.UI;

namespace SmartLaw.Admin.IntegralManage
{
    public partial class LeftMenu : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadPanelItem radPanelItem3 = new RadPanelItem("积分管理");
                radPanelItem3.Items.Add(new RadPanelItem("积分历史", "IntegralList.aspx"));
                radPanelItem3.Items.Add(new RadPanelItem("积分配置", "IntegralConfiguration.aspx"));
                radPanelItem3.Items.Add(new RadPanelItem("礼品查询", "GiftList.aspx"));
                radPanelItem3.Items.Add(new RadPanelItem("礼品定义", "GiftEdit.aspx"));
                radPanelItem3.Items.Add(new RadPanelItem("积分兑换记录", "IntegralExchangeRecords.aspx")); 
                RadPanelBar1.Items.Add(radPanelItem3);
            }
        }
    }
}