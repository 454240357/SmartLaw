using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SmartLaw.Admin.QuestionnaireManage
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadPanelItem radPanelItem3 = new RadPanelItem("问卷管理");
                radPanelItem3.Items.Add(new RadPanelItem("添加问卷", "QuestionnaireEdit.aspx"));
                radPanelItem3.Items.Add(new RadPanelItem("问卷列表", "QuestionnaireList.aspx")); 
                RadPanelBar1.Items.Add(radPanelItem3);
            }
        }
    }
}