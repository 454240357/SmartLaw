using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using System.Data;

namespace SmartLaw.Admin.IntegralManage
{
    public partial class GiftList : BasePage
    {
        SmartLaw.BLL.Prize pr = new BLL.Prize();
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                RGrid_GiftList.Visible = false;
            }
        }

        protected void RbtnSubmit_Click(object sender, EventArgs e)
        {
            int stype = RCB_SearchType.SelectedValue.Equals("1") ? 1 : 3; 
            DataSet prList = new DataSet();
            if (!TB_Content.Text.Trim().Equals(""))
            {
                prList = pr.GetList(stype, TB_Content.Text.Trim(), -1, 7, true);
            }
            else
            {
                prList = pr.GetList(-1, "", -1, 7, true);
            } 
            RGrid_GiftList.DataSource = prList.Tables[0];
            ViewState["prList"] = prList.Tables[0];
            RGrid_GiftList.DataBind();
            RGrid_GiftList.Visible = true;

        }

        protected void RGrid_GiftList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGrid_GiftList.DataSource = (DataTable)ViewState["prList"];
        }

        public string GetTime(object time)
        {

            DateTime dt = DateTime.Parse(time.ToString());
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string GetValid(object stock)
        {

            long st = long.Parse(stock.ToString());
            return st > 0 ? "有效" : "无效";
        }

    }
}