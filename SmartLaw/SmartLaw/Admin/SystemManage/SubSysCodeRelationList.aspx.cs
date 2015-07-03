using System;
using System.Collections.Generic; 
using System.Web.UI.WebControls;
using System.Data;
using SmartLaw.BLL;
using Telerik.Web.UI;
using SmartLaw.App_Code;
using System.Text;

namespace SmartLaw.Admin.SystemManage
{
    public partial class SubSysCodeRelationList : BasePage
    {
        public string subSysCodeId;
        SysCode sc = new SysCode();
        SysCodeRelation scr = new SysCodeRelation();
        BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
        Log log = new Log(); 
        private SessionUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            subSysCodeId = Request.QueryString["SubSysCodeId"];
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Code_CRUD");
            if (!IsPostBack)
                if (!ReadValue())
                    Panel1.Visible = false;
        }

        private bool ReadValue()
        {
            Model.SysCodeDetail scdModel = scd.GetModel(subSysCodeId);
            if (scdModel == null)
                return false;
            Bind_DDL_SysCodeList();
            Model.SysCode scModel = sc.GetModel(scdModel.SYSCodeID);
            if (scModel != null)
            {
                HL_SysCodeName.Text = scModel.SYSCodeContext;
                HL_SysCodeName.NavigateUrl = "SubSysCodeList.aspx?SysCodeId=" + scModel.SYSCodeID.Trim();
            }
            if (scdModel != null)
            {
                HL_SubSysCodeName.Text = scdModel.SYSCodeDetialContext;
                HL_SubSysCodeName.NavigateUrl = "SubSysCodeDetail.aspx?subSysCodeId=" + scdModel.SYSCodeDetialID.Trim();
            } 
            return true;
        }

        private void Bind_DDL_SysCodeList()
        {
            Model.SysCodeDetail scdModel = new Model.SysCodeDetail();
            scdModel = scd.GetModel(subSysCodeId);
            DataSet ds = new DataSet();
            ds = sc.GetAllList();
            if (ds == null)
                return;
            RCB_SysCodeList.Items.Clear();
            string sysCodeID = scdModel.SYSCodeID;
            DataTable sysCodeList = ds.Tables[0];
            for (int i = 0; i < sysCodeList.Rows.Count; i++)
            {
                if (sysCodeList.Rows[i][0].ToString().Equals(sysCodeID))
                    continue;       //当前小类所属大类不在列表范围之中
                RCB_SysCodeList.Items.Add(new RadComboBoxItem(sysCodeList.Rows[i][1].ToString(), sysCodeList.Rows[i][0].ToString()));
            }
            RCB_SysCodeList.Items.Insert(0, new RadComboBoxItem("不限", "-1"));
        }

        public string GetValid(object kind)
        {
            Boolean b = (Boolean)kind;
            return b ? "有效" : "无效";
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            GetSearchResult();
            RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "changeWindowSize();", true);
        }

        private void GetSearchResult()
        {
            string bigSysCodeId = RCB_SysCodeList.SelectedItem == null ? string.Empty : RCB_SysCodeList.SelectedItem.Value;
            string keywords = TB_SubCode.Text.Trim();
            List<Model.SysCode> scList = new List<Model.SysCode>();
            if (bigSysCodeId == "-1")
            {
                scList = sc.DataTableToList(sc.GetAllList().Tables[0]);
            }
            else
            {
                scList = sc.GetModelList(0, bigSysCodeId, -1, -1, false); 
            }
            Repeater1.DataSource = scList;
            Repeater1.DataBind();
            List<Model.SysCodeRelation> syscodeRelationList = scr.GetModelList(1, subSysCodeId, -1, -1, false);
            syscodeRelationList.AddRange(scr.GetModelList(2, subSysCodeId, -1, -1, false));
            foreach (RepeaterItem ri in Repeater1.Items)
            {
                string bigCode = ((Label)ri.FindControl("BigCode")).Text;
                if (bigCode.Equals(scd.GetModel(subSysCodeId).SYSCodeID))
                {
                    ri.Visible = false;
                    continue;
                }
                List<Model.SysCodeDetail> scdlist = scd.GetModelList(0, bigCode, -1, -1, false); 
                scdlist.RemoveAll(st => !syscodeRelationList.Exists(rt => rt.SysCodeDetialID.Equals(st.SYSCodeDetialID) || rt.SysCodeDetialIDEx.Equals(st.SYSCodeDetialID)));
                if (keywords != "")
                {
                    scdlist.RemoveAll(st => !st.SYSCodeDetialID.ToLower().Contains((keywords))); 
                }
                if (scdlist.Count == 0)
                    ri.Visible = false;
                else
                {
                    ((RadGrid)ri.FindControl("RGrid_RelationSubSysCode")).DataSource = scdlist;
                    ((RadGrid)ri.FindControl("RGrid_RelationSubSysCode")).DataBind();
                }
            }
        }

        protected void RGrid_RelationSubSysCode_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "DelRelation")
            {
                string subsyscode = Convert.ToString(e.CommandArgument);
                bool isDelete = false;
                Model.Log logModel = new Model.Log();
                logModel.OperationItem = "删除子类关联";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                try
                {
                    Model.SysCodeRelation scrModel = scr.GetModel(subSysCodeId, subsyscode); 
                    logModel.OperationDetail = "子类编号：" + scrModel.SysCodeDetialID + " & 子类编号：" + scrModel.SysCodeDetialIDEx;
                    isDelete = scr.Delete(scrModel.AutoID);
                    if (isDelete)
                    {
                        logModel.Memo = "成功";
                    }
                    else
                    {
                        logModel.Memo = "失败！";
                    }
                }
                catch (Exception ex)
                {
                    logModel.Memo = "异常：" + ex.Message;
                }
                finally
                {
                    log.Add(logModel);
                    if (isDelete)
                    {
                        GetSearchResult();
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "c1", "OpenAlert('删除关联成功！');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('删除关联失败！);", true);

                    }
                    ReadValue();
                }
            }
        }
         
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!scd.Exists(subSysCodeId))
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！该代码已不存在！！');", true);
                return;
            }
            bool selected = false;
            int totalCount = 0;
            int sucCount = 0;
            int errCount = 0;
            Model.Log logM = new Model.Log(); 
            logM.OperationItem = "批量删除子类关联";
            logM.OperationTime = DateTime.Now;
            logM.Operator = user.UserInfo.UserID; 
            StringBuilder notExists = new StringBuilder();
            try
            { 
                foreach (RepeaterItem ri in Repeater1.Items)
                {
                    RadGrid rg = ri.FindControl("RGrid_RelationSubSysCode") as RadGrid;
                    totalCount += rg.SelectedItems.Count; 
                    StringBuilder sb2 = new StringBuilder("【"); 
                    if (totalCount == 0)
                    {
                        continue;
                    }
                    selected = true; 
                    StringBuilder sb = new StringBuilder("【");
                    bool isDelete = false;
                    foreach (GridDataItem item in rg.MasterTableView.Items)
                    {
                        if ((item["CheckboxSelectColumn"].Controls[0] as CheckBox).Checked)
                        {
                            String subsyscode = item["SYSCodeDetialID"].Text;
                            Model.SysCodeRelation scrModel = scr.GetModel(subSysCodeId, subsyscode);
                            if (scrModel==null)
                            {
                                errCount++;
                                notExists.Append(subsyscode + " ");
                                continue;
                            }
                            sb.Append(subsyscode + " ");
                            isDelete = scr.Delete(scrModel.AutoID);
                            if (isDelete)
                            {
                                sucCount++;
                                sb2.Append(subsyscode + " ");
                            }
                        }

                    }
                    sb.Append("】");
                    sb2.Append("】");
                    logM.OperationDetail = "子类关联主项:" + subSysCodeId + ";子类关联删除提交列表[" + totalCount + "]：" + sb.ToString() + "子类关联删除成功列表[" + sucCount + "]:" + sb2.ToString();
                } 
            }
            catch (Exception ex)
            {
                logM.Memo = "异常：" + ex.Message;
            }
            finally
            {
                if (!selected)
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！您未选择关联项！！');", true);
                }
                else
                {
                    if (totalCount == (sucCount + errCount) && totalCount > 0)
                    {
                        logM.Memo = "成功";
                        if (errCount == 0)
                        {
                            RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！子类关联删除成功！');", true);
                        }
                        else
                        {
                            RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！子类关联删除成功！但是子类【" + notExists.ToString() + "】已不存在,无须删除。');", true);
                        }
                        GetSearchResult();
                    }
                    else
                    {
                        logM.Memo = "失败";
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！子类关联删除未成功！（请查询关联查看结果！）');", true);
                    }
                    log.Add(logM);
                }

            }
        } 
    }
}