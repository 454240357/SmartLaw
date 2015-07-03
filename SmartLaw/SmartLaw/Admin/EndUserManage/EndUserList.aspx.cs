using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using SmartLaw.BLL;
using Telerik.Web.UI;
using System.Data;
using SmartLaw.MongoDBService;
using System.Text;

namespace SmartLaw.Admin.EndUserManage
{
    public partial class EndUserList : BasePage
    {
        private SessionUser user;
        SysCodeDetail scd = new SysCodeDetail();
        SmartLaw.BLL.EndUser eu = new BLL.EndUser();
        Log log = new Log();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_EndUser_Retrieve");
            if (!IsPostBack)
            {
                List<Model.SysCodeDetail> identityList = scd.GetModelList(0, "Identity", -1, 1, false);
                identityList.RemoveAll(rt => rt.IsValid == false);
                RCB_Identity.DataTextField = "SYSCodeDetialContext";
                RCB_Identity.DataValueField = "SYSCodeDetialID";
                RCB_Identity.DataSource = identityList;
                RCB_Identity.DataBind();
                RGrid_EndUserList.Visible = false;
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

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            List<Model.EndUser> euList = new List<Model.EndUser>();
            bool tag = false;
            if (!RTB_SIM.Text.Trim().Equals(""))
            {
                euList = eu.GetModelList(2, RTB_SIM.Text, -1, -1, false);
                tag = true;
            }
            if (!RTB_Name.Text.Trim().Equals(""))
            {
                if (tag==false)
                {
                    euList = eu.GetModelList(1, RTB_Name.Text, -1, -1, false);
                    tag = true;
                }
                else
                {
                    euList.RemoveAll(rt => !rt.EndUserName.Contains(RTB_Name.Text));
                }
            }
            if (tag == false)
            { 
                euList = eu.DataTableToList(eu.GetAllList().Tables[0]); 
            } 
            RadTreeView regionTreeView = RadDropDownTree1.Controls[0] as RadTreeView;
            List<string> identityList = new List<string>();
            List<string> rgList = new List<string>();
            foreach (RadComboBoxItem rcbi in RCB_Identity.CheckedItems)
            {
                identityList.Add(rcbi.Value);
            }
            foreach (RadTreeNode rtn in regionTreeView.CheckedNodes)
            {
                rgList.Add("|" + rtn.Value + "|");
            }
            if (identityList.Count > 0)
            {
                euList.RemoveAll(rt => !identityList.Exists(st => rt.Identities.Contains(st)));
            }
            if (rgList.Count > 0)
            {
                euList.RemoveAll(rt => !rgList.Exists(st => rt.Identities.Contains(st)));
            }
            RGrid_EndUserList.DataSource = euList;
            RGrid_EndUserList.DataBind();
            ViewState["EndUserList"] = euList;
            RGrid_EndUserList.Visible = true;
            if (euList.Count > 0)
            {
                Button1.Visible = true; 
            }
            
        }

        public bool deleteAble(object auid)
        { 
            return true;
        }

        public bool syncAble(object auid)
        {
            return true;
        }


        protected void RGrid_EndUserList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_EndUserList.DataSource = (List<Model.EndUser>)ViewState["EndUserList"];
        }
        protected void RGrid_EndUserList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string AutoID = Convert.ToString(e.CommandArgument);
                bool isDelete = false;
                Model.Log logModel = new Model.Log();
                logModel.OperationItem = "删除终端用户";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                Model.EndUser euModel = eu.GetModel(long.Parse(AutoID));
                try
                {
                    if (euModel != null)
                    {
                        logModel.OperationDetail = "用户姓名：" + euModel.EndUserName + " - STB号：" + euModel.SimCardNo + " - 身份：" + euModel.Identities;

                        using (MongoDBServiceSoapClient mg = new MongoDBServiceSoapClient())
                        {
                            string rtStr = "";
                            bool isDeleteMg = true;
                            if (mg.SelectEnduserBySimCardNo(euModel.SimCardNo, out rtStr) != null)
                            {
                                isDeleteMg = false;
                                isDeleteMg = mg.DeleteEnduser(euModel.SimCardNo, out rtStr);
                            }
                            if (isDeleteMg)
                            {
                                isDelete = eu.Delete(long.Parse(AutoID));
                            }

                            logModel.Memo += rtStr;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logModel.Memo = "异常：" + ex.Message;
                }
                finally
                {
                    if (euModel != null)
                    {
                        log.Add(logModel);
                        if (isDelete)
                        {
                            List<Model.EndUser> endUserList = (List<Model.EndUser>)ViewState["EndUserList"];
                            endUserList.RemoveAll(rt => rt.AutoID == long.Parse(AutoID));
                            RGrid_EndUserList.DataSource = endUserList;
                            RGrid_EndUserList.Rebind(); 
                            RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc1", "OpenAlert('恭喜用户删除成功。');", true);
                        }
                        else
                        {
                            RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc2", "OpenAlert('抱歉！用户删除失败。');", true);
                        }
                    }
                    else
                    {
                        List<Model.EndUser> endUserList = (List<Model.EndUser>)ViewState["EndUserList"];
                        endUserList.RemoveAll(rt => rt.AutoID == long.Parse(AutoID));
                        RGrid_EndUserList.DataSource = endUserList;
                        RGrid_EndUserList.DataBind();
                        ViewState["EndUserList"] = endUserList;
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc2", "OpenAlert('抱歉！用户删除失败。该用户已不存在！！');", true);
                    }
                }
            }
            if (e.CommandName == "Sync")
            {
                string AutoID = Convert.ToString(e.CommandArgument);
                bool isSyncMg = false;
                Model.Log logModel = new Model.Log();
                logModel.OperationItem = "同步终端用户";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                Model.EndUser euModel = eu.GetModel(long.Parse(AutoID));
                try
                {
                    if (euModel != null)
                    {
                        logModel.OperationDetail = "用户姓名：" + euModel.EndUserName + " - STB号：" + euModel.SimCardNo + " - 身份：" + euModel.Identities;
                        MongoDBService.EndUser dc = new MongoDBService.EndUser();
                        {
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
                            using (MongoDBServiceSoapClient mg = new MongoDBServiceSoapClient())
                            {
                                string rtStr = "";
                                if (mg.SelectEnduserBySimCardNo(euModel.SimCardNo, out rtStr) != null)
                                {
                                    isSyncMg = mg.UpdateEnduser(dc, out rtStr);
                                }
                                else
                                {
                                    isSyncMg = mg.InsertEnduser(dc, out rtStr);
                                }
                                logModel.Memo += rtStr;

                            }
                        }
                    }
                } 
                catch (Exception ex)
                {
                    logModel.Memo = "异常：" + ex.Message;
                }
                finally
                {
                    if (euModel != null)
                    {
                        log.Add(logModel);
                        if (isSyncMg)
                        { 
                            RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc1", "OpenAlert('恭喜用户同步成功。');", true);
                        }
                        else
                        {
                            RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc2", "OpenAlert('抱歉！用户同步失败。');", true);
                        }
                    }
                    else
                    { 
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc2", "OpenAlert('抱歉！用户同步失败。该用户本地已不存在！！');", true);
                    }
                }
            }
        }

        //同步
        protected void Button1_Click(object sender, EventArgs e)
        {
            int selectCount = RGrid_EndUserList.SelectedItems.Count;
            if (selectCount == 0)
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！您未选择任何项！！');", true);
                return;
            } 
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "批量同步终端用户";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            int totCount = selectCount; 
            StringBuilder notExist = new StringBuilder("");
            bool isSyncMgUp = false;
            bool isSyncMgIn = false;
            try
            {
                StringBuilder sb = new StringBuilder("【");
                StringBuilder sbU = new StringBuilder("【");
                StringBuilder sbI = new StringBuilder("【"); 
                List<MongoDBService.EndUser> euUpList = new List<MongoDBService.EndUser>();
                List<MongoDBService.EndUser> euInList = new List<MongoDBService.EndUser>();
                using (MongoDBServiceSoapClient mg = new MongoDBServiceSoapClient())
                {
                    foreach (GridDataItem item in RGrid_EndUserList.MasterTableView.Items)
                    {
                        if ((item["CheckboxSelectColumn"].Controls[0] as CheckBox).Checked)
                        {
                            String autoid = item["AutoID"].Text;
                            sb.Append(autoid + " ");
                            if (!eu.Exists(long.Parse(autoid)))
                            {
                                continue;
                            }
                            Model.EndUser euModel = eu.GetModel(long.Parse(autoid));
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
                            string rtStrx = "";
                            if (mg.SelectEnduserBySimCardNo(euModel.SimCardNo, out rtStrx) != null)
                            {
                                sbU.Append(autoid + " ");
                                euUpList.Add(dc);
                            }
                            else
                            {
                                sbI.Append(autoid + " ");
                                euInList.Add(dc);
                            }
                        }
                    }

                    string info0 = "";
                    string info1 = "";
                    isSyncMgUp = true;
                    if (euUpList.Count > 0)
                    {
                        string rtStr = "";
                        isSyncMgUp = false;
                        isSyncMgUp = mg.UpdateEndusers(euUpList.ToArray(), out rtStr);
                        if (isSyncMgUp)
                        {
                            info0 = "同步成功(更新)列表[" + euUpList.Count + "]:" + sbU.ToString();
                        }
                        else
                        {
                            info0 = "同步成功(更新)列表[" + euUpList.Count + "]:无";
                        }
                        logModel.Memo += "同步更新返回:" + rtStr;
                    }
                    isSyncMgIn = true;
                    if (euInList.Count > 0)
                    {
                        string rtStr = "";
                        isSyncMgIn = false;
                        isSyncMgIn = mg.InsertEndusers(euInList.ToArray(), out rtStr);
                        if (isSyncMgIn)
                        {
                            info0 = "同步成功(新增)列表[" + euUpList.Count + "]:" + sbU.ToString();
                        }
                        else
                        {
                            info0 = "同步成功(新增)列表[" + euUpList.Count + "]:无";
                        }
                        logModel.Memo += "同步新增返回:" + rtStr; ;
                    }
                    sb.Append("】");
                    sbU.Append("】");
                    sbI.Append("】");
                    logModel.OperationDetail = "同步请求列表[" + totCount + "]：" + sb.ToString() + info0 + info1;
                }
            }
            catch (Exception ex)
            {
                logModel.Memo = "异常：" + ex.Message;
            }
            finally
            {
                if (isSyncMgUp && isSyncMgIn)
                {
                    logModel.Memo = "成功";
                    log.Add(logModel);
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！同步成功！');", true);
                }
                else
                {
                    logModel.Memo += "失败";
                    log.Add(logModel);
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！同步未完全成功！');", true);
                }
            }
        }
        /*//删除
        protected void Button2_Click(object sender, EventArgs e)
        {


        }*/

    }
}