using System;
using System.Collections.Generic; 
using SmartLaw.App_Code;
using SmartLaw.BLL;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using System.Data;
using SmartLaw.Admin.NewsManage;

namespace SmartLaw.Admin.SystemManage
{
    public partial class SysUserDetail : BasePage
    {
        public string loginID; 
        private SessionUser user;
        Log log = new Log();
        SmartLaw.BLL.SysCodeDetail scd = new BLL.SysCodeDetail(); 
        SysCodeRelation scr = new SysCodeRelation(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            loginID = Request.QueryString["LoginID"];
            user = SessionUser.GetSession(); 
            if (!IsPostBack)
                if (!ReadValue())
                    Panel1.Visible = false;
        }

        private bool ReadValue()
        {
            //href1.HRef = "SubSysCodeRelationList.aspx?SubSysCodeId=" + loginID;
            // href2.HRef = "AddSubSysCodeRelation.aspx?SubSysCodeId=" + loginID;
            SysUser su = new SysUser();
            Model.SysUser suModel = su.GetModel(loginID);
            if (suModel == null)
                return false;
            Lb_LoginName.Text = suModel.UserID;
            TB_UserName.Text = suModel.UserName;
            RCB_Enable.SelectedValue = suModel.IsValid ? "1" : "0";
            TB_EmployeeID.Text = suModel.EmployeeID;
            HL_UserName.Text = suModel.UserName.Trim();
            HL_UserName.NavigateUrl = "SysUserDetail.aspx?LoginID=" + suModel.UserID.Trim();
            DataSet dsRole = scd.GetListBySysCode(user.UserInfo.UserID, "Role");
            String role = dsRole.Tables[0].Rows[0]["SYSCodeDetialID"].ToString();  
            DataSet egDsRole = scd.GetListBySysCode(suModel.UserID, "Role");
            string egrole = egDsRole.Tables[0].Rows[0]["SYSCodeDetialID"].ToString(); 
           
            List<Model.SysCodeDetail> scdList = scd.GetModelList(0, "Role", -1, 3, true);
            scdList.RemoveAll(st => st.IsValid == false); 
            if (!role.Equals("Aministrators"))
            {
                if (suModel.UserID.Equals(user.UserInfo.UserID) || (user.hasAuthority("Auth_SysUser_Edit"))
                   )
                {

                    if (suModel.UserID.Equals(user.UserInfo.UserID))
                    {
                        scdList.RemoveAll(st => !st.SYSCodeDetialID.Equals(role));
                        RCB_Enable.Enabled = false; 
                        TB_UserName.ReadOnly = true;
                        TB_EmployeeID.ReadOnly = true;
                    } 
                    Bt_Modify.Visible = true;
                }
                else
                { 
                    Bt_Modify.Visible = false;
                }  
            }
            RCB_Role.DataTextField = "SYSCodeDetialContext";
            RCB_Role.DataValueField = "SYSCodeDetialID";
            RCB_Role.DataSource = scdList;
            RCB_Role.DataBind();
            RCB_Role.SelectedValue = egrole;  
            return true;
        } 

        protected void Bt_Modify_Click(object sender, EventArgs e)
        {
            SysUser su = new SysUser();
            Model.SysUser suModel = new Model.SysUser();
            suModel = su.GetModel(loginID);
            if (suModel == null)
                return;

            string loginName = loginID.Trim();
            string password = TB_Password.Text == "" ? suModel.Password : TB_Password.Text;
            string pwdCheck = TB_PwdCheck.Text == "" ? suModel.Password : TB_PwdCheck.Text;
            string userName = TB_UserName.Text.Trim();
            string employeeID = TB_EmployeeID.Text.Trim();

            string matchPass_Password = @"^[a-z0-9A-Z_-]{7,20}$";

            if (!(Regex.IsMatch(password, matchPass_Password)))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('登录名必须为1-10个字符(可以包含英文、数字和下划线)！');", true);
                return;
            }
            if (password != pwdCheck)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('两次密码输入不一致！');", true);
                return;
            }
            if (userName == "")
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('姓名必须为1-50个字符！');", true);
                return;
            }
            DataSet egDsRole = scd.GetListBySysCode(suModel.UserID, "Role");
            string egrole = egDsRole.Tables[0].Rows[0]["SYSCodeDetialID"].ToString(); //原角色

            string roleId = RCB_Role.SelectedValue;  
            suModel.Password = password;
            suModel.UserName = userName;
            suModel.EmployeeID = employeeID;
            bool enable = RCB_Enable.SelectedItem.Value == "1";
            suModel.IsValid = enable;
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改操作员信息";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            logModel.OperationDetail = "操作员姓名：" + userName + " - 员工号：" + employeeID + " - 状态：" + enable + " - 角色编号：" + roleId;
            try
            {
                bool updateScr = true; 
                List<Model.SysCodeRelation> scrModelList = scr.DataTableToList(scr.GetList(1, suModel.UserID).Tables[0]);
                scrModelList.AddRange(scr.DataTableToList(scr.GetList(2, suModel.UserID).Tables[0]));
                foreach (Model.SysCodeRelation scrM in scrModelList)
                {
                    if (scrM.SysCodeDetialID.Equals(egrole))
                    {
                        updateScr = false;
                        scrM.SysCodeDetialID = roleId;
                        updateScr = scr.Update(scrM);
                    }
                    else if (scrM.SysCodeDetialIDEx.Equals(egrole))
                    {
                        updateScr = false;
                        scrM.SysCodeDetialIDEx = roleId;
                        updateScr = scr.Update(scrM);
                    } 
                    if (updateScr == false)
                    {
                        break;
                    }
                }
                if (updateScr)
                {
                    isUpdate = su.Update(suModel);
                }
                if (isUpdate)
                {
                    logModel.Memo = "成功";
                }
                else
                {
                    if (updateScr)
                    {
                        logModel.Memo = "失败！";
                    }
                    else
                    {
                        logModel.Memo = "失败！角色区域更新失败！";
                    }
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
                    ReadValue();
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('修改操作员成功！');", true);
                }
                else
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c5", "OpenAlert('修改操作员失败！');", true);
            }
        }
    }
}
