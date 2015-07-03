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
    public partial class IntegralDetail : BasePage
    {
        public string SimID;
        private SessionUser _user;
        SmartLaw.BLL.Integral ig = new BLL.Integral();
        SmartLaw.BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
        SmartLaw.BLL.EndUser eu = new BLL.EndUser();
        SmartLaw.BLL.Log log = new BLL.Log();
        protected void Page_Load(object sender, EventArgs e)
        {
            SimID = Request.QueryString["SimID"];
            _user = SessionUser.GetSession();
            if (!IsPostBack)
                if (!ReadValue())
                    Panel1.Visible = false;
        }
        private bool ReadValue()
        {
            if (SimID == null)
            {
                return false;
            }
            List<SmartLaw.Model.Integral> igList = ig.GetModelList(1, SimID, -1, 5, true);
            TB_TotalIntegral.Text = igList[0].TotalIntegral.ToString(); ;
            RGrid_IntegralList.DataSource = igList;
            ViewState["RGrid_IntegralList"] = igList;
            RGrid_IntegralList.DataBind();
            RGrid_IntegralList.Visible = true; 
            return true;
        }
        public string GetItemsName(object kind)
        {
            SmartLaw.Model.SysCodeDetail scdM = scd.GetModel(kind.ToString());
            if (scdM == null)
            {
                return kind.ToString();
            }
            return scdM.Memo;
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

        protected void RGrid_IntegralList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_IntegralList.DataSource = (List<SmartLaw.Model.Integral>)ViewState["RGrid_IntegralList"];
        }

        protected void Bt_Modify_Click(object sender, EventArgs e)
        {
            Model.Integral igM = ig.GetModelList(1, SimID, 1, 5, true)[0];
            Model.Log logM = new Model.Log();
            logM.OperationItem = "修改积分";
            logM.OperationDetail = "Sim:"+SimID+";原积分："+igM.TotalIntegral+";当前积分："+TB_TotalIntegral.Text;
            logM.OperationTime = DateTime.Now;
            logM.Operator = _user.UserInfo.UserID;
            logM.Memo = "";
            int added = int.Parse((long.Parse(TB_TotalIntegral.Text) - igM.TotalIntegral).ToString());
            bool isSet = false;
            try
            {
                Model.Integral newIgm = new Model.Integral();
                newIgm.IntegralAdded = added;
                newIgm.Items = "手动设置-"+_user.UserInfo.UserName+"["+_user.UserInfo.UserID+"]";
                newIgm.LastModifyTime = DateTime.Now;
                newIgm.SimCardNo = SimID;
                newIgm.TotalIntegral = long.Parse(TB_TotalIntegral.Text); 
                isSet = ig.Add(newIgm); 
            }
            catch (Exception ex)
            {
                logM.Memo += ex.Message;
            }
            finally
            {
                log.Add(logM);
                if (isSet)
                {
                    ReadValue();
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('恭喜，积分手动设置成功！');", true);
                }
                else
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c5", "OpenAlert('抱歉，积分手动设置失败！');", true);
            }
        }
    }
}