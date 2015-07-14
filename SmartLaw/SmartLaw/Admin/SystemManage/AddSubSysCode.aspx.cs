using System; 
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using SmartLaw.BLL;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.SystemManage
{
    public partial class AddSubSysCode : BasePage
    {
        public string sysCodeId;
        private SessionUser user;
        SysCode sc = new SysCode();
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
            Lb_CodeName.Text = scModel.SYSCodeID.Trim();
            HL_SysCodeName.Text = scModel.SYSCodeContext.Trim();
            HL_SysCodeName.NavigateUrl = "SubSysCodeList.aspx?SysCodeId=" + scModel.SYSCodeID.Trim();
            return true;
        }

        protected void Bt_Add_Click(object sender, EventArgs e)
        {
            string sysCodeDetailID = TB_SubCodeName.Text.Trim();
            string sysCodeDetailContext = TB_SubCodeContent.Text.Trim();
            bool isValid = RCB_Enable.SelectedValue.Trim() == "1" ? true : false;
            string memo = TB_SubCodeMemo.Text.Trim();
            string matchPass_SubCodeName = @"^[a-z0-9A-Z_-]{1,50}$";

            if (sysCodeDetailID == "" || !(Regex.IsMatch(sysCodeDetailID, matchPass_SubCodeName)))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉！添加失败！代码编号必须为1-50个字符(可以包含英文、数字和下划线)！');", true);
                return;
            }
            if (sysCodeDetailContext == "")
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('抱歉！添加失败！代码内容必须为1-50个字符！');", true);
                return;
            }
            BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
            if (scd.Exists(sysCodeDetailID))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('抱歉！添加失败！代码编号已存在！');", true);
                return;
            }
            Model.SysCodeDetail scdModel = new Model.SysCodeDetail();
            scdModel.SYSCodeID = sysCodeId;
            scdModel.SYSCodeDetialID = sysCodeDetailID;
            scdModel.SYSCodeDetialContext = sysCodeDetailContext;
            scdModel.Memo = memo;
            scdModel.LastModifyTime = DateTime.Now;
            scdModel.IsValid = isValid;
            bool isSubAdd = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "添加子类";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "大类编号：" + sysCodeId + " - 子类编号：" + sysCodeDetailID + " - 子类内容：" + sysCodeDetailContext;
                
                isSubAdd = scd.Add(scdModel);
                if (isSubAdd)
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
                if (isSubAdd)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('恭喜！添加成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('抱歉！添加失败！');", true);
                }
            }
        }
    }
}