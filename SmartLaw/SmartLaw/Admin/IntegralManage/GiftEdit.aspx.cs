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
    public partial class GiftEdit : BasePage
    {
        SmartLaw.BLL.Prize pr = new BLL.Prize();
        BLL.Log log = new BLL.Log();
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            if (!IsPostBack)
            { 
                
            }
        }

        protected void RbtnSubmit_Click(object sender, EventArgs e)
        {
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "定义礼品";
            logModel.OperationTime = DateTime.Now;
            logModel.Operator = _user.UserInfo.UserID;
            logModel.OperationDetail = "";
            logModel.Memo = "";
            long addId = 0;
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

            try
            {
                Model.Prize prModel = new Model.Prize();
                prModel.Points = int.Parse(GPointsn.Text);
                prModel.PrizeName = GName.Text;
                prModel.PrizeUnit = GUnit.Text;
                prModel.Registrant = _user.UserInfo.UserID;
                prModel.RegTime = DateTime.Now;
                prModel.Remarks = GMemo.Text;
                prModel.Stock = long.Parse(GStockn.Text);
                addId =pr.Add(prModel);
                logModel.OperationDetail = prModel.PrizeName + "-" + prModel.PrizeUnit + "-" + prModel.Stock;
            }
            catch (Exception ex)
            {
                logModel.Memo = ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (addId > 0)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('恭喜，礼品定义成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，礼品定义失败！');", true);
                }
            }
        }
    }
}