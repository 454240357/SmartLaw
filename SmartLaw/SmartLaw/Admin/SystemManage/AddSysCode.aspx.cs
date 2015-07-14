using System; 
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using SmartLaw.BLL;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.SystemManage
{
    public partial class AddSysCode : BasePage
    {
        SysCode sc = new SysCode();
        Log log = new Log();
        private SessionUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Code_CRUD");
        }

        protected void Bt_Add_Click(object sender, EventArgs e)
        {
            
            string CodeName = TB_CodeName.Text.Trim();
            string CodeContent = TB_CodeContent.Text.Trim();
            bool CodeEnable = RCB_Enable.SelectedItem.Value == "1";
            string CodeMemo = TB_CodeMemo.Text.Trim();

            string matchPass_CodeName = @"^[a-z0-9A-Z_-]{1,50}$";
            if (CodeName == "" || !(Regex.IsMatch(CodeName, matchPass_CodeName)))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('代码编号必须为1-50个字符(可以包含英文、数字和下划线)！');", true);
                return;
            }
            if (CodeContent == "")
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('代码内容必须为1-50个字符！');", true);
                return;
            }
            if (sc.Exists(CodeName))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('添加失败！代码编号已存在！');", true);
                return;
            }
            Model.SysCode scModel = new Model.SysCode();
            scModel.SYSCodeID = CodeName;
            scModel.SYSCodeContext = CodeContent;
            scModel.ISValid = CodeEnable;
            scModel.Memo = CodeMemo;
            scModel.LastModifytime = DateTime.Now;
            bool isAdd = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "添加系统代码";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "代码编号：" + CodeName + " - 代码内容：" + CodeContent;
                isAdd = sc.Add(scModel);
                if (isAdd)
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
                logModel.Memo = "异常: " + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isAdd)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('添加成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('添加失败！');", true);
                }
            }
        }
    }
}