using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using System.Data;
using Telerik.Web.UI;

namespace SmartLaw.Admin.IntegralManage
{
    public partial class IntegralList : BasePage
    {
        SmartLaw.BLL.EndUser eu = new BLL.EndUser();
        SmartLaw.BLL.Integral ig = new BLL.Integral();
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                RGrid_IntegralList.Visible = false;
            }
        }

        public string GetName(object kind)
        {
            List<SmartLaw.Model.EndUser> euModelList = eu.GetModelList(2, kind.ToString(), -1, -1, false);
            if (euModelList.Count > 0)
            {
                return euModelList[0].EndUserName;
            }
            return "";
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            List<SmartLaw.Model.Integral> igList = new List<Model.Integral>();
            if (TB_CodeContent.Text.Trim() == "")
            {
                igList = ig.GetModelListView();
            }
            else
            {
                string value = TB_CodeContent.Text.Trim();
                int searchType = int.Parse(RCB_SearchType.SelectedItem.Value);
                if (searchType == 2)
                {
                    List<SmartLaw.Model.EndUser> euModelList = eu.GetModelList(1, value, -1, -1, false);
                    foreach (SmartLaw.Model.EndUser euM in euModelList)
                    {
                        igList.AddRange(ig.GetModelList(1, euM.SimCardNo, 1, 5, true));
                    }
                }
                else
                {
                    igList.AddRange(ig.GetModelList(1,value, 1, 5, true));
                }
            }
            RGrid_IntegralList.DataSource = igList;
            ViewState["RGrid_IntegralList"] = igList;
            RGrid_IntegralList.DataBind();
            RGrid_IntegralList.Visible = true; 
            
        }

        protected void RGrid_IntegralList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_IntegralList.DataSource = (List<SmartLaw.Model.Integral>)ViewState["RGrid_IntegralList"];
        }
    }
}