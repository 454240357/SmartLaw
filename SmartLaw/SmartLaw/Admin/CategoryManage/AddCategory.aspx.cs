using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using SmartLaw.BLL;
using Telerik.Web.UI;
using System.Data;
using SmartLaw.Admin.NewsManage;

namespace SmartLaw.Admin.CategoryManage
{
    public partial class AddCategory : SmartLaw.App_Code.BasePage
    {
        public long cpautoID;
        private SessionUser user;
        Category cg = new Category(); 
        SysCodeDetail scd = new SysCodeDetail();
        Log log = new Log(); 
        List<SmartLaw.Model.Regional> rgList;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Category_CRUD"); 
            cpautoID = long.Parse(Request.QueryString["CategoryParentID"]);
            if (!IsPostBack)
                if (!ReadValue())
                    Panel1.Visible = false;
        }

        private bool ReadValue()
        { 
            href1.HRef = "CategoryDetail.aspx?CategoryID=" + cpautoID;
            SmartLaw.Model.Category cpModel = cg.GetModel(cpautoID); 
            if (cpModel == null)
                return false;   
            TB_ParentCategory.Text = cpModel.CategoryName; 
            HL_CategoryName.Text = cpModel.CategoryName;
            HL_CategoryName.NavigateUrl = "CategoryDetail.aspx?CategoryID=" + cpautoID;
            return true;
        }

        protected void Bt_Submit_Click(object sender, EventArgs e)
        {
            string categryName = TB_CategoryName.Text;
            if (categryName == "")
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('抱歉！添加失败！分类名称必须为1-50个字符！');", true);
                return;
            }
            SmartLaw.Model.Category cgModel = new Model.Category();
            cgModel.ParentCategoryID = cpautoID;
            cgModel.CategoryName = categryName;
            cgModel.IsValid = RCB_Enable.SelectedItem.Value == "1" ? true : false;
            cgModel.LastModifyTime = DateTime.Now;
            cgModel.Memo = TB_Memo.Text;
            long newAutoID = 0;  
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "添加分类";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            logModel.OperationDetail = "分类名称：" + categryName + " - 父类编号：" + cpautoID + " - 分类备注：" + TB_Memo.Text; 
            try
            {
                cgModel.Orders = cg.GetList(2, cpautoID.ToString()).Tables[0].Rows.Count + 1;
                newAutoID = cg.Add(cgModel);  
                if (newAutoID != 0)
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
                if (newAutoID != 0)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('添加子类成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c5", "OpenAlert('添加子类失败！');", true);
                } 
            }
        }
    }
}