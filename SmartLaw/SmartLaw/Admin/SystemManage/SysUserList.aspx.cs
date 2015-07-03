using System;
using System.Collections.Generic; 
using SmartLaw.App_Code;
using SmartLaw.BLL;
using System.Data;

namespace SmartLaw.Admin.SystemManage
{
    public partial class SysUserList : BasePage
    {
        private SessionUser user;
        SysUser su = new SysUser(); 
        BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            if (!user.hasAuthority("Auth_SysUser_Add"))
            {
                href1.Visible = false;
            }
            if (!IsPostBack)
                RGrid_SysUserList.Visible = false;
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            int searchType = int.Parse(RCB_SearchType.SelectedItem.Value);
            string keyWords = TB_KeyWords.Text.Trim(); 
            List<Model.SysUser> suList = new List<Model.SysUser>();
            if (keyWords != "")
            {
                if (searchType == 1)
                {
                    suList = su.GetModelList(-1, keyWords, -1, 4, true);
                    suList.RemoveAll(st => !st.UserID.Contains(keyWords));
                }
                else
                {
                    suList = su.GetModelList(3, keyWords, -1, 4, true);
                }
            }
            else
            {
                suList = su.GetModelList(-1, "", -1, 4, true);
            } 
            RGrid_SysUserList.DataSource = suList;
            ViewState["SysUserList"] = suList;
            RGrid_SysUserList.DataBind();
            RGrid_SysUserList.Visible = true;
        }

        public string GetValid(object kind)
        {
            Boolean b =(Boolean) kind;
            return b ? "有效" : "无效";
        }

        protected void RGrid_SysUserList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGrid_SysUserList.DataSource = (List<SmartLaw.Model.SysUser>)ViewState["SysUserList"];
        }


    }
}