using System;
using System.Collections.Generic; 
using SmartLaw.BLL; 
using Telerik.Web.UI;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.SystemManage
{
    public partial class SubSysCodeList : BasePage
    {
        public string sysCodeId;
        private SessionUser user;
        SysCode sc = new SysCode();
        BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
        Log log = new Log();
        protected void Page_Load(object sender, EventArgs e)
        {
            sysCodeId = Request.QueryString["SysCodeId"];
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Code_CRUD");
            if (!IsPostBack)
                if (!ReadValue())
                    Panel1.Visible = false;
        }

        private bool ReadValue()
        {
            string CodeName = sysCodeId; 
            Model.SysCode scModel = sc.GetModel(CodeName);
            if (scModel == null)
                return false;
            HL_SysCodeName.Text = scModel.SYSCodeContext.Trim();
            HL_SysCodeName.NavigateUrl = "SubSysCodeList.aspx?SysCodeId=" + scModel.SYSCodeID.Trim();
            RGrid_SubSysCode.Visible = false;
            return true;
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            int searchType = int.Parse(RCB_SearchType.SelectedItem.Value);
            string keyWords = TB_KeyWords.Text.Trim();
            BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
            List<Model.SysCodeDetail> scdModelList = scd.GetModelList(0, sysCodeId, -1, -1, false); 
            if (keyWords != "")
            {
                if (searchType == 1)
                {
                    scdModelList.RemoveAll(st => !st.SYSCodeDetialID.Contains(keyWords));
                }
                else{
                    scdModelList.RemoveAll(st => !st.SYSCodeDetialContext.Contains(keyWords));
                }
            }
            RGrid_SubSysCode.DataSource = scdModelList;
            ViewState["SubSysCodeList"] = scdModelList;
            RGrid_SubSysCode.DataBind();
            RGrid_SubSysCode.Visible = true;
            RadScriptManager.RegisterStartupScript(Page, GetType(), "c0", "changeWindowSize();", true);
        } 

        public string GetValid(object kind)
        {
            bool b = bool.Parse(kind.ToString());
            if (b)
            { return "有效"; }
            else
            { return "无效"; }
        }

        protected void RGrid_SubSysCode_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_SubSysCode.DataSource = (List<Model.SysCodeDetail>)ViewState["SubSysCodeList"];
        }

        protected void RGrid_SubSysCode_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string subSysCodeId = Convert.ToString(e.CommandArgument); 
                bool isDelete = false;
                Model.Log logModel = new Model.Log();
                logModel.OperationItem = "删除子类";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                try
                {
                    Model.SysCodeDetail scdModel = new Model.SysCodeDetail(); 
                    logModel.OperationDetail = "子类编号：" + scdModel.SYSCodeDetialID + " - 子类内容：" + scdModel.SYSCodeDetialContext;
                    isDelete = scd.Delete(subSysCodeId);
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
                        int delNo = -1;
                        List<Model.SysCodeDetail> sysCodeDetailList = (List<Model.SysCodeDetail>)ViewState["SubSysCodeList"];
                        for(int i =0;i<sysCodeDetailList.Count;i++)
                        {
                            string tstr = sysCodeDetailList[i].SYSCodeDetialID;
                            if (tstr.Equals(subSysCodeId))
                            {
                                delNo = i;
                                break;
                            }
                        }
                        if (delNo != -1)
                        {
                            sysCodeDetailList.RemoveAt(delNo);
                        }
                        RGrid_SubSysCode.DataSource = sysCodeDetailList;
                        RGrid_SubSysCode.DataBind();
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "c1", "OpenAlert('恭喜！删除成功！');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('抱歉！删除失败！<br />请检查该小类是否存在未移除的关联！');", true);
                    }
                }
            }
        }


    }
}