using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SmartLaw.Admin.NoticesManage
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadPanelItem radPanelItem3 = new RadPanelItem("公告管理");
                radPanelItem3.Items.Add(new RadPanelItem("公告添加", "NoticeEdit.aspx"));
                radPanelItem3.Items.Add(new RadPanelItem("公告列表", "NoticeList.aspx")); 
                RadPanelBar1.Items.Add(radPanelItem3);
            }
        }
    }
}