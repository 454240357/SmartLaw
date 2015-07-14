using System;
using System.Collections.Generic; 
using System.Web.UI.WebControls;
using SmartLaw.BLL;
using System.Data;
using Telerik.Web.UI;
using SmartLaw.App_Code;
using System.Text;

namespace SmartLaw.Admin.SystemManage
{
    public partial class AddSubSysCodeRelation : BasePage
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
            Model.SysCodeDetail scdModel= scd.GetModel(subSysCodeId);
            if (scdModel == null)
                return false;
            Bind_DDL_SysCodeList();
            Model.SysCode scModel = sc.GetModel(scdModel.SYSCodeID);
            if (scModel != null)
            {
                HL_SysCodeName.Text = scModel.SYSCodeContext;
                HL_SysCodeName.NavigateUrl = "SubSysCodeList.aspx?SysCodeId=" + scModel.SYSCodeID.Trim();
            }
            HL_SubSysCodeName.Text = scdModel.SYSCodeDetialContext;
            HL_SubSysCodeName.NavigateUrl = "SubSysCodeDetail.aspx?subSysCodeId=" + scdModel.SYSCodeDetialID.Trim(); 
            RGrid_SubSysCode.Visible = false;
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
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            string sysCodeID = RCB_SysCodeList.SelectedItem.Value.Trim();
            BLL.SysCodeDetail scd = new BLL.SysCodeDetail(); 
            List<Model.SysCodeDetail> SyscodeDetialList = new List<Model.SysCodeDetail>();
            string keywords = TB_SubCode.Text.ToLower().Trim();  
            SyscodeDetialList = scd.GetModelList(0,sysCodeID,-1,-1,false); 
            if (keywords != "")
            {
                SyscodeDetialList.RemoveAll(st => !st.SYSCodeDetialID.ToLower().Contains(keywords));
            }

            if (SyscodeDetialList == null || SyscodeDetialList.Count==0)
                return;

            /*筛选尚未关联的记录*/ 
            List<Model.SysCodeDetail> newSyscodeDetialTypeList = new List<Model.SysCodeDetail>();
            List<Model.SysCodeRelation> scrModelList = scr.GetModelList(1,subSysCodeId,-1,-1,false);
            foreach (Model.SysCodeDetail scdmodel in SyscodeDetialList)
            {
                if (scrModelList.Exists(st => (st.SysCodeDetialIDEx.Equals(scdmodel.SYSCodeDetialID) ||
                                                    st.SysCodeDetialIDEx.Equals(scdmodel.SYSCodeDetialID))))
                {
                    continue;
                }
                newSyscodeDetialTypeList.Add(scdmodel);
            }
            ViewState["SyscodeDetialTypeList"] = newSyscodeDetialTypeList;
            RGrid_SubSysCode.DataSource = newSyscodeDetialTypeList;
            RGrid_SubSysCode.DataBind();
            RGrid_SubSysCode.Visible = true;
            RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "changeWindowSize();", true);
        }

        public string GetValid(object kind)
        {
            bool b = bool.Parse(kind.ToString());
            return b ? "有效" : "无效";
        }

        protected void RGrid_SubSysCode_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_SubSysCode.DataSource = (List<SmartLaw.Model.SysCodeDetail>)ViewState["SyscodeDetialTypeList"];
        }

        protected void RGrid_SubSysCode_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "AddRelation")
            {
                string anotherSubCodeId = Convert.ToString(e.CommandArgument); 
                Model.SysCodeRelation scrModel = new Model.SysCodeRelation();
                scrModel.SysCodeDetialID = subSysCodeId;
                scrModel.SysCodeDetialIDEx = anotherSubCodeId;
                long result = 0;
                Model.Log logModel = new Model.Log();
                try
                {
                    logModel.OperationItem = "添加子类关联";
                    logModel.Operator = user.UserInfo.UserID;
                    logModel.OperationTime = DateTime.Now;
                    logModel.OperationDetail = "子类编号：" + subSysCodeId + " & 子类编号：" + anotherSubCodeId; 
                    result = scr.Add(scrModel);
                    if (result != 0)
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
                    if (result != 0)
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('恭喜！关联成功！');", true);
                        List<Model.SysCodeDetail> syscodeDetialList = (List<Model.SysCodeDetail>)ViewState["SyscodeDetialTypeList"];
                        syscodeDetialList.RemoveAll(item => item.SYSCodeDetialID.Equals(anotherSubCodeId));
                        RGrid_SubSysCode.DataSource = syscodeDetialList;
                        RGrid_SubSysCode.DataBind();
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！关联失败！');", true);
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!scd.Exists(subSysCodeId))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！该代码已不存在！！');", true);
                return;
            }
            int totalCount = RGrid_SubSysCode.SelectedItems.Count;
            StringBuilder notExists = new StringBuilder();
            StringBuilder sb2 = new StringBuilder("【"); 
            int sucCount = 0;
            int errCount = 0;
            if (totalCount == 0)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！您未选择关联项！！');", true);
                return;
            }
            Model.Log logM = new Model.Log();
            logM.OperationItem = "批量子类关联";
            logM.OperationTime = DateTime.Now;
            logM.Operator = user.UserInfo.UserID;
            try
            {
                StringBuilder sb = new StringBuilder("【"); 
                long isUpdate = 0;
                foreach (GridDataItem item in RGrid_SubSysCode.MasterTableView.Items)
                {
                    if ((item["CheckboxSelectColumn"].Controls[0] as CheckBox).Checked)
                    {
                        String sysCodeDetialID = item["SYSCodeDetialID"].Text;
                        if (!scd.Exists(sysCodeDetialID))
                        {
                            errCount++;
                            notExists.Append(sysCodeDetialID + " ");
                            continue;
                        }
                        sb.Append(sysCodeDetialID + " ");
                        Model.SysCodeRelation scrModel = new Model.SysCodeRelation();
                        scrModel.SysCodeDetialID = subSysCodeId;
                        scrModel.SysCodeDetialIDEx = sysCodeDetialID;
                        isUpdate = scr.Add(scrModel);
                        if (isUpdate != 0)
                        {
                            sucCount++;
                            sb2.Append(sysCodeDetialID + " ");
                        }
                    }

                }
                sb.Append("】");
                sb2.Append("】");
                logM.OperationDetail = "子类关联主项:"+subSysCodeId+"; 子类关联提交列表[" + totalCount + "]：" + sb.ToString() + "子类关联成功列表[" + sucCount + "]:" + sb2.ToString();
            }
            catch (Exception ex)
            {
                logM.Memo = "异常：" + ex.Message;
            }
            finally
            {
                if (totalCount == (sucCount + errCount) && totalCount > 0)
                {
                    logM.Memo = "成功"; 
                    if (errCount == 0)
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('恭喜！子类关联成功！');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('恭喜！子类关联成功！但是子类【" + notExists.ToString() + "】已不存在,未关联。');", true);
                    }
                    List<Model.SysCodeDetail> syscodeDetialList = (List<Model.SysCodeDetail>)ViewState["SyscodeDetialTypeList"];

                    syscodeDetialList.RemoveAll(item => sb2.ToString().Contains(item.SYSCodeDetialID));
                    RGrid_SubSysCode.DataSource = syscodeDetialList;
                    RGrid_SubSysCode.DataBind();
                }
                else
                {
                    logM.Memo = "失败";
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！子类关联未成功！（请查询关联查看结果！）');", true);
                }
                log.Add(logM);

            }
        } 
    }
}