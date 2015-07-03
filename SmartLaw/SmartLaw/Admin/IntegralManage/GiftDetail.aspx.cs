using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using Telerik.Web.UI;

namespace SmartLaw.Admin.IntegralManage
{
    public partial class GiftDetail : BasePage
    {
        BLL.Prize pr = new BLL.Prize();
        public string AutoID;
        private SessionUser _user;
        BLL.Log log = new BLL.Log();
        protected void Page_Load(object sender, EventArgs e)
        {
            AutoID = Request.QueryString["AutoID"];
            _user = SessionUser.GetSession();
            if (!IsPostBack)
                if (!ReadValue())
                    Panel1.Visible = false;
        }

        private bool ReadValue()
        {
            if (AutoID == null)
            {
                return false;
            }
            Model.Prize prModel = pr.GetModel(long.Parse(AutoID));
            if (prModel == null)
            {
                return false;
            }
            GName.Text = prModel.PrizeName;
            GMemo.Text = prModel.Remarks;
            GUnit.Text = prModel.PrizeUnit; 
            if (prModel.Stock <= 0)
            {
                RCB_Enable.SelectedValue = "0";
                GStockn.Enabled = false;
            }
            else
            {
                GStockn.Text = prModel.Stock.ToString();
            }
            GPointsn.Text = prModel.Points.ToString();
            if (true)
            {
                Bt_Modify.Visible = true;
            }
            return true;
        }

        protected void Bt_Modify_Click(object sender, EventArgs e)
        {
            if (GName.Text.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，礼品名称不能为空！');", true);
                return;
            }
            if (GUnit.Text.Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，单位不能为空！');", true);
                return;
            }
            if (RCB_Enable.SelectedValue.Equals("1"))
            {
                if (GStockn.Text.Equals(""))
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，库存不能为空！');", true);
                    return;
                }
                else if (int.Parse(GStockn.Text) <= 0)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，库存值必须大于零！');", true);
                    return;
                }
            }
            if (GPointsn.Text.Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，兑换分值不能为空！');", true);
                return;
            }
            else if (int.Parse(GPointsn.Text) <= 0)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，兑换分值必须大于零！');", true);
                return;
            }

            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改礼品";
            logModel.Operator = _user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            logModel.OperationDetail = "";
            logModel.Memo = "";
            bool isUpdate = false;
            try
            {
                Model.Prize prModel = pr.GetModel(long.Parse(AutoID));
                if (prModel == null)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，该礼品已不存在！');", true);
                    return;
                }
                prModel.Points = int.Parse(GPointsn.Text);
                prModel.PrizeName = GName.Text;
                prModel.PrizeUnit = GUnit.Text;
                prModel.Registrant = _user.UserInfo.UserID;
                prModel.RegTime = DateTime.Now;
                prModel.Remarks = GMemo.Text;
                prModel.Stock = RCB_Enable.SelectedValue.Equals("1") ? long.Parse(GStockn.Text) : 0;
                isUpdate = pr.Update(prModel);
                logModel.OperationDetail = prModel.PrizeName + "-" + prModel.PrizeUnit + "-" + prModel.Stock;
            }
            catch (Exception ex)
            {
                logModel.Memo = ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isUpdate)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('恭喜，礼品修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，礼品修改失败！');", true);
                }
            }
        }

        protected void RCB_Enable_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value.Equals("1"))
            {
                GStockn.Enabled = true;
            }
            if (e.Value.Equals("0"))
            {
                GStockn.Enabled = false;
            }
        }

    }
}