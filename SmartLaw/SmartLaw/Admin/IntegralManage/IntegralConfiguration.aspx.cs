using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Web.Script.Serialization;
using System.Data;
using System.Text;

namespace SmartLaw.Admin.IntegralManage
{
    public partial class IntegralConfiguration : BasePage
    {
        SmartLaw.BLL.Log log = new BLL.Log();
        SmartLaw.BLL.Integral ig = new BLL.Integral();
        BLL.SysCodeDetail scd = new BLL.SysCodeDetail(); 
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            RadListBox1.Transferred += new RadListBoxTransferredEventHandler(RadListBox1_Transferred); 
            if (!IsPostBack)
            { 
                List<Model.SysCodeDetail> scdList1 = scd.GetModelList(0, "UserBehavior", -1, -1, false);
                scdList1.RemoveAll(at => at.IsValid == false);
                RadListBox1.DataTextField = "Memo";
                RadListBox1.DataValueField = "SYSCodeDetialID"; 
                RadListBox1.DataSource = scdList1;
                RadListBox1.DataBind();
                
                List<Model.SysCodeDetail> scdList0 = scd.GetModelList(0, "UserBehavior", -1, -1, false);
                scdList0.RemoveAll(at => at.IsValid == true);
                RadListBoxDestination.DataTextField = "Memo";
                RadListBoxDestination.DataValueField = "SYSCodeDetialID"; 
                RadListBoxDestination.DataSource = scdList0 ;
                RadListBoxDestination.DataBind();

            }
        }
         

        void RadListBox1_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            foreach (RadListBoxItem item in e.Items)
            { 
                item.DataBind(); 
            }     
        }

        protected void RbtnSubmit_Click(object sender, EventArgs e)
        {
            Model.Log logM = new Model.Log();
            logM.OperationItem = "积分配置";
            logM.OperationTime = DateTime.Now;
            logM.OperationDetail = "";
            logM.Operator = _user.UserInfo.UserID;
            logM.Memo = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                List<Model.SysCodeDetail> scdList = scd.GetModelList(0, "UserBehavior", -1, -1, false);
                foreach (RadListBoxItem rlbi in RadListBox1.Items)
                { 
                    RadTextBox rtbp = rlbi.FindControl("IgPoint") as RadTextBox;
                    int point;
                    if (int.TryParse(rtbp.Text, out point) == false)
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，有效积分项必须配置积分值！');", true);
                        return;
                    }

                }
                foreach (RadListBoxItem rlbi in RadListBox1.Items)
                {   
                    bool uP = false; 
                    RadTextBox rtbp = rlbi.FindControl("IgPoint") as RadTextBox; 
                    string IgID = rlbi.Value;
                    Model.SysCodeDetail scdModel = scd.GetModel(IgID);
                    if (scdModel.IsValid == false || !scdModel.SYSCodeDetialContext.Equals(IgID))
                    {
                        uP = true;
                    }
                    scdModel.IsValid = true;
                    scdModel.SYSCodeDetialContext = rtbp.Text;
                    if (uP)
                    {
                        scd.Update(scdModel);
                        sb.Append("[" + IgID + "-" + "1-" + rtbp.Text + "]  ");
                        scdList.RemoveAll(st => st.SYSCodeDetialID.Equals(IgID));
                    }
                }

                foreach (Model.SysCodeDetail scdm in scdList)
                {
                    if (scdm.IsValid)
                    {
                        scdm.IsValid = false;
                        scd.Update(scdm);
                        sb.Append("[" + scdm.SYSCodeDetialID + "-0- ]");
                    }
                }
                logM.OperationDetail = sb.ToString();
            }
            catch (Exception ex)
            {
                logM.Memo += ex.Message;
            }
            finally
            {
                log.Add(logM);
                if (logM.Memo.Equals(""))
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('恭喜，积分配置修改成功');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，积分配置修改失败');", true);
                }
            }

        }
        
         
         

    }
}