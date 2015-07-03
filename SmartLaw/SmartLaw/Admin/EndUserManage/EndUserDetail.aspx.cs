using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using SmartLaw.BLL;
using Telerik.Web.UI;
using System.Text;
using SmartLaw.MongoDBService;
using System.Data;
namespace SmartLaw.Admin.EndUserManage
{
    public partial class EndUserDetail : BasePage
    {
        public long AutoID;
        private SessionUser user;
        SmartLaw.BLL.EndUser eu = new BLL.EndUser();
        SysCodeDetail scd = new SysCodeDetail();
        Log log = new Log();
        List<string> sbIlist = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_EndUser_Edit");
            AutoID = long.Parse(Request.QueryString["AutoID"]);
            if (!IsPostBack)
            {
                if (!ReadValue())
                {
                    Panel1.Visible = false;
                }
            }
        }

        private bool ReadValue()
        {
            Model.EndUser euMOdel = eu.GetModel(AutoID);
            if (euMOdel == null)
            {
                return false;
            } 
            Lb_AutoID.Text = AutoID.ToString();
            RTB_Name.Text = euMOdel.EndUserName;
            RTB_SIM.Text = euMOdel.SimCardNo; 
            RCB_Enable.SelectedValue = euMOdel.IsValid ? "1" : "0";
            List<Model.SysCodeDetail> identityList = scd.GetModelList(0, "Identity", -1, 1, false);
            identityList.RemoveAll(rt => rt.IsValid == false);
            RCB_Identity.DataTextField = "SYSCodeDetialContext";
            RCB_Identity.DataValueField = "SYSCodeDetialID";
            RCB_Identity.DataSource = identityList;
            RCB_Identity.DataBind(); 
            string[] identityArray = euMOdel.Identities.Split('|');
            foreach (string id in identityArray)
            {
                if (id.Trim().Equals(""))
                {
                    continue;
                }
                sbIlist.Add(id);
            } 
            foreach (RadComboBoxItem rcbi in RCB_Identity.Items)
            {
                if (identityArray.Contains(rcbi.Value))
                {
                    rcbi.Checked = true;
                }
            }
            RadDropDownTree1.NodeDataBound += RadDropDownTree1_NodeDataBound;
            BindRegionTree(); 
            return true;
        }

        protected void BindRegionTree()
        {
            //初始化区域选择下拉树
            List<Model.SysCodeDetail> rgList = scd.GetModelList(0, "Region", -1, 1, false);
            rgList.RemoveAll(rt => rt.IsValid == false);
            rgList.Find(rt => rt.SYSCodeDetialID.Equals("Area_Jfs")).Memo = null;
            RadDropDownTree1.DataFieldID = "SYSCodeDetialID";
            RadDropDownTree1.DataFieldParentID = "Memo";
            RadDropDownTree1.DataValueField = "SYSCodeDetialID";
            RadDropDownTree1.DataTextField = "SYSCodeDetialContext";
            RadDropDownTree1.DataSource = rgList;
            RadDropDownTree1.DataBind();
            RadTreeView treeView = RadDropDownTree1.Controls[0] as RadTreeView;
            treeView.Nodes[0].Expanded = true;
            treeView.ShowLineImages = false;
        }


        void RadDropDownTree1_NodeDataBound(object sender, Telerik.Web.UI.DropDownTreeNodeDataBoundEventArguments e)
        {
            if (sbIlist != null && sbIlist.Contains(e.DropDownTreeNode.Value))
            {
                e.DropDownTreeNode.CreateEntry();
            }
        }

        protected void Bt_Modify_Click(object sender, EventArgs e)
        {
            if (RCB_Identity.CheckedItems.Count == 0)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('身份选择不能为空！');", true);
                return;
            }
            RadTreeView regionTreeView = RadDropDownTree1.Controls[0] as RadTreeView;
            if (regionTreeView.SelectedNodes.Count == 0)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('区域选择不能为空！');", true);
                return;
            }
            if (RTB_Name.Text.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('用户姓名不能为空！');", true);
                return;
            } 
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改终端用户";
            logModel.OperationTime = DateTime.Now;
            logModel.Operator = user.UserInfo.UserID;
            logModel.Memo = "";
            Model.EndUser euModel = eu.GetModel(AutoID);
            if (euModel == null)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('抱歉！该用户已不存在！');", true);
                return;
            } 
            euModel.EndUserName = RTB_Name.Text;
            euModel.SimCardNo = RTB_SIM.Text;
            euModel.LastModifyTime = DateTime.Now;
            euModel.IsValid = RCB_Enable.SelectedValue=="1" ? true : false;
            StringBuilder sb = new StringBuilder();
            foreach (RadComboBoxItem rcbi in RCB_Identity.CheckedItems)
            {
                sb.Append("|"+rcbi.Value + "|");
            }
            sb.Append("|" + regionTreeView.SelectedValue + "|");
            string identityStr = sb.ToString();
            logModel.OperationDetail = "姓名：" + RTB_Name.Text + "SIM：" + RTB_SIM.Text + "身份&区域：" + identityStr;
            euModel.Identities = identityStr;
            bool isUpdate = false;
            try
            {
                using (MongoDBServiceSoapClient mg = new MongoDBServiceSoapClient())
                {
                    MongoDBService.EndUser dc = new MongoDBService.EndUser();
                    dc.AutoID = euModel.AutoID.ToString();
                    dc.EnduserName = euModel.EndUserName;
                    string[] identityArr = euModel.Identities.Split('|');
                    ArrayOfString aof = new ArrayOfString();
                    foreach (string id in identityArr)
                    {
                        if (id.Equals(""))
                        {
                            continue;
                        }
                        aof.Add(id);
                    }
                    dc.Identities = aof;
                    dc.IsValid = euModel.IsValid;
                    dc.LastModifyTime = euModel.LastModifyTime;
                    dc.SimCardNo = euModel.SimCardNo;
                    string rtStr = "";
                    if (mg.UpdateEnduser(dc, out rtStr))
                    {
                        isUpdate = eu.Update(euModel);
                    }
                }
            }
            catch (Exception ex)
            {
                logModel.Memo += ex.Message;
            }
            finally
            {
                if (euModel != null)
                {
                    log.Add(logModel);
                    if (isUpdate)
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('恭喜！用户修改成功！');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('抱歉！用户修改失败！');", true);
                    }
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('抱歉！用户修改失败,已不存在该用户！');", true);
                }
            }
        
        }

    }
}