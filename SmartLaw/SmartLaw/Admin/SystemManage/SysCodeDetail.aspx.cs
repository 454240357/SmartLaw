using System; 
using SmartLaw.BLL; 
using Telerik.Web.UI;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.SystemManage
{
    public partial class SysCodeDetail : BasePage
    {
        public string sysCodeId;
        SysCode sc = new SysCode();
        Log log = new Log();
        private SessionUser user;
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
            Model.SysCode scModel = new Model.SysCode();
            scModel = sc.GetModel(CodeName);
            if (scModel == null)
                return false;
            Lb_CodeName.Text = scModel.SYSCodeID;
            TB_CodeContent.Text = scModel.SYSCodeContext;
            RCB_Enable.SelectedValue = scModel.ISValid ? "1" : "0";
            TB_CodeMemo.Text = scModel.Memo;
            HL_SysCodeName.Text = scModel.SYSCodeContext.Trim();
            HL_SysCodeName.NavigateUrl = "SubSysCodeList.aspx?SysCodeId=" + scModel.SYSCodeID.Trim();
            return true;
        }

        protected void Bt_Modify_Click(object sender, EventArgs e)
        {
            string CodeName = sysCodeId;
            string CodeContent = TB_CodeContent.Text.Trim();
            bool CodeEnable = RCB_Enable.SelectedItem.Value == "1";
            string CodeMemo = TB_CodeMemo.Text.Trim(); 
            if (CodeName == "")
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('代码内容必须为1-50个字符！');", true);
                return;
            }
            Model.SysCode scModel = new Model.SysCode();
            scModel.SYSCodeID = CodeName;
            scModel.SYSCodeContext = CodeContent;
            scModel.ISValid = CodeEnable;
            scModel.Memo = CodeMemo;
            scModel.LastModifytime = DateTime.Now;
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改大类信息";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            logModel.OperationDetail = "大类编号："+CodeName + " - 大类名称：" +CodeContent+ " - 状态：" +CodeEnable;
            try
            {
                isUpdate = sc.Update(scModel);
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
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('恭喜！修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！修改失败！');", true);
                }
            }
        }
    }
}