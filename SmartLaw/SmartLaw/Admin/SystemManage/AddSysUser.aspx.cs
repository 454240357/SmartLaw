using System;
using System.Collections.Generic; 
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using SmartLaw.BLL;
using System.Data;

namespace SmartLaw.Admin.SystemManage
{
    public partial class AddSysUser : BasePage
    {
        private SessionUser user;
        Log log = new Log();
        BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
        SysCodeRelation scr = new SysCodeRelation(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession(); 
            user.ValidateAuthority("Auth_SysUser_Add");
            if (!IsPostBack)
            {  
                DataSet dsRole = scd.GetListBySysCode(user.UserInfo.UserID, "Role");
                String role = dsRole.Tables[0].Rows[0]["SYSCodeDetialID"].ToString(); 
                List<Model.SysCodeDetail> scdList = scd.GetModelList(0, "Role", -1, 3, true);
                scdList.RemoveAll(st => st.IsValid==false);  
                RCB_Role.DataTextField = "SYSCodeDetialContext";
                RCB_Role.DataValueField = "SYSCodeDetialID";
                RCB_Role.DataSource = scdList;
                RCB_Role.DataBind(); 
            }
        }

        protected void Bt_Add_Click(object sender, EventArgs e)
        {
            string loginName = TB_LoginName.Text.Trim();
            string password = TB_Password.Text.Trim();
            string pwdCheck = TB_PwdCheck.Text.Trim();
            string userName = TB_UserName.Text.Trim();
            string employeeID = TB_EmployeeID.Text.Trim(); 
            string matchPass_LoginName = @"^[a-z0-9A-Z_-]{1,20}$";
            string matchPass_Password = @"^[a-z0-9A-Z_-]{7,20}$";

            if (loginName == "" || !(Regex.IsMatch(loginName, matchPass_LoginName)))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('登录名必须为1-10个字符(可以包含英文、数字和下划线)！');", true);
                return;
            }
            if (password == "" || !(Regex.IsMatch(password, matchPass_Password)))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('密码必须为7-20个字符(可以包含英文、数字和下划线)！');", true);
                return;
            }
            if (password != pwdCheck)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('两次密码输入不一致！');", true);
                return;
            }
            if (userName == "")
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('姓名必须为1-50个字符！');", true);
                return;
            }
            string roleId = RCB_Role.SelectedValue;   
            bool enable = RCB_Enable.SelectedItem.Value == "1" ? true : false;

            Model.SysUser suModel = new Model.SysUser();
            suModel.UserID = loginName;
            suModel.Password = password;
            suModel.UserName = userName;
            suModel.EmployeeID = employeeID; 
            suModel.IsValid = enable;
            SysUser su = new SysUser();
            bool isAdd = false;
            long scrAdd = 0;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "添加用户";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "登录名：" + loginName + " - 用户名：" + userName + " - 角色:" + RCB_Role.SelectedItem.Text;
                isAdd = su.Add(suModel);
                Model.SysCodeRelation scrModel = new Model.SysCodeRelation();
                if (isAdd)
                {
                    scrModel.SysCodeDetialID = loginName;
                    scrModel.SysCodeDetialIDEx = roleId;
                    scrAdd = scr.Add(scrModel); 
                }

                if (isAdd && scrAdd!=0)
                {
                    logModel.Memo = "成功";
                }
                else if (!isAdd)
                {
                    logModel.Memo = "失败！";
                }
                else
                {
                    logModel.Memo = "角色设置失败！";
                }
            }
            catch (Exception ex)
            {
                logModel.Memo = "异常：" + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isAdd && scrAdd!=0)
                { 
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c5", "OpenAlert('恭喜！操作员\"" + loginName + "\"添加成功！');", true);
                }
                else if (!isAdd)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c6", "OpenAlert('抱歉！操作员\"" + loginName + "\"添加失败！');", true);

                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c6", "OpenAlert('操作员\"" + loginName + "\"添加成功！但是角色设置失败,请到系统代码中重新关联该用户角色！！');", true);

                }
            }
        } 
    }
}