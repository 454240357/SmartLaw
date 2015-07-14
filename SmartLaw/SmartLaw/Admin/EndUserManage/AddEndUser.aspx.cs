using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.BLL;
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Text;
using SmartLaw.MongoDBService;
namespace SmartLaw.Admin.EndUserManage
{
    public partial class AddEndUser : BasePage
    {
        SysCodeDetail scd = new SysCodeDetail();
        Log log = new Log();
        private SessionUser user;
        SmartLaw.BLL.EndUser eu = new BLL.EndUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_EndUser_Add");
            if (!IsPostBack)
            {
                List<Model.SysCodeDetail> identityList = scd.GetModelList(0, "Identity", -1, 1, false);
                identityList.RemoveAll(rt => rt.IsValid == false); 
                RCB_Identity.DataTextField = "SYSCodeDetialContext";
                RCB_Identity.DataValueField = "SYSCodeDetialID";
                RCB_Identity.DataSource = identityList;
                RCB_Identity.DataBind();
                BindRegionTree();
            } 
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


        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (RCB_Identity.CheckedItems.Count == 0)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('身份选择不能为空！');", true);
                return;
            }
            RadTreeView regionTreeView = RadDropDownTree1.Controls[0] as RadTreeView;
            if (regionTreeView.SelectedNodes.Count==0)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('区域选择不能为空！');", true);
                return;
            }
            if (RTB_Name.Text.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('用户姓名不能为空！');", true);
                return;
            }
            if (RTB_SIM.Text.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('STB号不能为空！');", true);
                return;
            } 
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "添加终端用户";
            logModel.OperationTime = DateTime.Now;
            logModel.Operator = user.UserInfo.UserID;
            logModel.Memo = "";
            Model.EndUser euModel = new Model.EndUser();
            euModel.EndUserName = RTB_Name.Text;
            euModel.SimCardNo = RTB_SIM.Text;
            euModel.LastModifyTime = DateTime.Now;
            euModel.IsValid = true;
            StringBuilder sb = new StringBuilder();
            foreach (RadComboBoxItem rcbi in RCB_Identity.CheckedItems)
            {
                sb.Append("|"+rcbi.Value + "|");
            }
            sb.Append("|" + regionTreeView.SelectedValue + "|");
            string identityStr = sb.ToString();
            logModel.OperationDetail = "姓名："+RTB_Name.Text+"SIM："+RTB_SIM.Text+"身份&区域："+identityStr;
            euModel.Identities = identityStr; 
            long autoid = 0;
            try
            {
                autoid = eu.Add(euModel);
                if (autoid > 0)
                {
                    using (MongoDBServiceSoapClient mg = new MongoDBServiceSoapClient())
                    {
                        MongoDBService.EndUser dc = new MongoDBService.EndUser();
                        bool IsAddMg = true;
                        string rtStr = "";
                        if (mg.SelectEnduserBySimCardNo(euModel.SimCardNo, out rtStr) == null)
                        {
                            IsAddMg = false;
                            dc.AutoID = autoid.ToString();
                            dc.EnduserName = euModel.EndUserName;
                            string[] identityArr = euModel.Identities.Split('|');
                            ArrayOfString aof = new ArrayOfString();
                            foreach (string id in identityArr)
                            {
                                if (id.Trim().Equals(""))
                                {
                                    continue;
                                }
                                aof.Add(id);
                            }
                            dc.Identities = aof;
                            dc.IsValid = true;
                            dc.LastModifyTime = euModel.LastModifyTime;
                            dc.SimCardNo = euModel.SimCardNo;
                            IsAddMg = mg.InsertEnduser(dc, out rtStr);
                        }
                        if (!IsAddMg)
                        {
                            eu.Delete(autoid);
                            autoid = 0;
                        }
                        logModel.Memo += rtStr;
                    }
                }
            }
            catch (Exception ex)
            {
                autoid = 0;
                logModel.Memo += ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (autoid > 0)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('恭喜！用户添加成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('抱歉！用户添加失败！');", true);
                }
            }
        }


    }
}