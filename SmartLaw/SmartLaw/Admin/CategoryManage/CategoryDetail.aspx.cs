using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using SmartLaw.BLL;
using Telerik.Web.UI;
using System.Text;
using System.Data;
using SmartLaw.Admin.NewsManage;

namespace SmartLaw.Admin.CategoryManage
{
    public partial class CategoryDetail : SmartLaw.App_Code.BasePage
    {
        public long autoID;
        private SessionUser user;
        Category cg = new Category(); 
        SysCodeDetail scd = new SysCodeDetail(); 
        Log log = new Log();  
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Category_CRUD"); 
            autoID = long.Parse(Request.QueryString["CategoryID"]);
            if (!IsPostBack)
                if (!ReadValue())
                { 
                    Panel1.Visible = false;
                }
        }
        private bool ReadValue()
        {
            href1.HRef = "AddCategory.aspx?CategoryParentID=" + autoID;
            SmartLaw.Model.Category cgModel = cg.GetModel(autoID);
            if (cgModel == null)
                return false;
            SmartLaw.Model.Category cgParentModel = cg.GetModel(cgModel.ParentCategoryID); 
            TB_CategoryID.Text = cgModel.AutoID.ToString();
            TB_CategoryName.Text = cgModel.CategoryName;
            RCB_Enable.SelectedValue = cgModel.IsValid ? "1" : "0";
            TB_ParentCategory.Text = cgParentModel == null ? "无" : cgParentModel.CategoryName;
            TB_Memo.Text = cgModel.Memo;  
            HL_CategoryName.Text = cgModel.CategoryName;
            HL_CategoryName.NavigateUrl = "CategoryDetail.aspx?CategoryID=" + autoID;
            return true;
        }

        protected void Bt_Modify_Click(object sender, EventArgs e)
        { 
            string categryName = TB_CategoryName.Text;
            if (categryName == "")
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('抱歉！修改失败！分类名称容必须为1-50个字符！');", true);
                return;
            }
            Model.Category cgModel = cg.GetModel(autoID);
            cgModel.CategoryName = categryName;
            cgModel.IsValid = RCB_Enable.SelectedItem.Value == "1" ? true : false;
            cgModel.LastModifyTime = DateTime.Now;
            cgModel.Memo = TB_Memo.Text;
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改分类信息";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            logModel.OperationDetail = "分类编号：" + cgModel.AutoID + " - 分类名称：" + cgModel.CategoryName + " - 父类编号：" + cgModel.ParentCategoryID + "状态：" + cgModel.IsValid; 
            
            try
            {
                isUpdate = cg.Update(cgModel); 
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
                    ReadValue();
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('恭喜，修改分类信息成功！');", true);
                }
                else
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c5", "OpenAlert('抱歉，修改分类信息失败！');", true);
            }
        }

        protected void RCB_Enable_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value.Equals("0"))
            {
                bool validExist = false;
                foreach (SmartLaw.Model.Category cgM in cg.GetModelList(2, autoID.ToString(), -1, -1, false))
                {
                    if (cgM.IsValid)
                    {
                        validExist = true;
                        break;
                    }
                }
                if (validExist)
                {
                    img.InnerText = "该分类的子类中还有有效状态分类，不能置为无效。";
                    img.Visible = true;
                    Bt_Modify.Visible = false;
                }
                else
                {
                    img.Visible = false;
                    Bt_Modify.Visible = true;
                }
            }
            else
            {
                SmartLaw.Model.Category cgModel = cg.GetModel(autoID);
                if (cgModel.ParentCategoryID == -1 || cg.GetModel(cgModel.ParentCategoryID).IsValid)
                {
                    img.Visible = false;
                    Bt_Modify.Visible = true;
                }
                else
                {
                    img.InnerText = "该分类的父类状态为无效，请先修改父类状态。";
                    img.Visible = true;
                    Bt_Modify.Visible = false; ;
                }
            }
        }
    }
}