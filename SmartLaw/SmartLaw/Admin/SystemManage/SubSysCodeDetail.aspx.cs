using System; 
using SmartLaw.BLL;
using Telerik.Web.UI;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.SystemManage
{
    public partial class SubSysCodeDetail : BasePage
	{
        public string subSysCodeId;
        SysCode sc = new SysCode();
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
            string SubCodeName = subSysCodeId;  
            BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
            Model.SysCodeDetail scdModel = new Model.SysCodeDetail();
            scdModel = scd.GetModel(SubCodeName);
            if (scdModel == null)
                return false;
            Lb_SubCodeName.Text = scdModel.SYSCodeDetialID;
            TB_SubCodeContent.Text = scdModel.SYSCodeDetialContext;
            RCB_Enable.SelectedValue = scdModel.IsValid ? "1" : "0";
            TB_SubCodeMemo.Text = scdModel.Memo;
            Model.SysCode scModel =  sc.GetModel(scdModel.SYSCodeID);
            if (scModel != null)
            {
                HL_SysCodeName.Text = scModel.SYSCodeContext;
                HL_SysCodeName.NavigateUrl = "SubSysCodeList.aspx?SysCodeId=" + scModel.SYSCodeID.Trim();
            }
            HL_SubSysCodeName.Text = scdModel.SYSCodeDetialContext;
            HL_SubSysCodeName.NavigateUrl = "SubSysCodeDetail.aspx?subSysCodeId=" + scdModel.SYSCodeDetialID.Trim();

            //if (syscodeType.codeId == "SysUser")
           //     Response.Redirect("SysUserDetail.aspx?LoginID=" + SubCodeName);
            return true; 
        }

        protected void Bt_Modify_Click(object sender, EventArgs e)
        { 
            string SubCodeName = subSysCodeId;
            string SubCodeContent = TB_SubCodeContent.Text.Trim();
            bool SubCodeEnable = RCB_Enable.SelectedItem.Value == "1" ? true : false;
            string SubCodeMemo = TB_SubCodeMemo.Text.Trim(); 
            if (SubCodeContent == "")
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c1", "OpenAlert('代码内容必须为1-50个字符！');", true);
                return;
            }
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改小类信息";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
            Model.SysCodeDetail scdModel = scd.GetModel(SubCodeName);
            scdModel.SYSCodeDetialID = SubCodeName;
            scdModel.SYSCodeDetialContext = SubCodeContent;
            scdModel.LastModifyTime = DateTime.Now;
            scdModel.Memo = SubCodeMemo;
            scdModel.IsValid = SubCodeEnable;
            bool isUpdate = false; 
            logModel.OperationDetail = "小类编号："+SubCodeName + " - 父类编号：" +scdModel.SYSCodeID+ " - 小类名称："+SubCodeContent + " - 状态："+SubCodeEnable;
            try
            {
                isUpdate = scd.Update(scdModel);
                if (isUpdate)
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
                if (isUpdate)
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！小类\"" + SubCodeName + "\"修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！小类\"" + SubCodeName + "\"修改失败！');", true);
                }
            }
        }
	}
}