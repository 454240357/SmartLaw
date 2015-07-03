using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SmartLaw.BLL;
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace SmartLaw.Admin.NewsManage
{
    public partial class NewsCheck : BasePage
    {
        News news = new News();
        Category cg = new Category();
        Log log = new Log(); 
        SysCodeDetail scd = new SysCodeDetail(); 
        SysUser su = new SysUser();
        private SessionUser user;
        protected void Page_Load(object sender, EventArgs e)
        {

            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_News_Examine"); 
            if (!user.hasAuthority("Auth_News_Retrieve"))
            {
                href1.Visible = false;
            } 
            if (!user.hasAuthority("Auth_News_Add"))
            {
                A1.Visible = false;
            }
            if (!IsPostBack)
            {
                ReadValue();
            }
        }

        public string GetValid(object kind)
        { 
            bool b = bool.Parse(kind.ToString());
            return b ? "有效" :"无效";
        }

        public string GetTime(object time)
        {

            DateTime dt = DateTime.Parse(time.ToString());
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string GetChecked(Object chk)
        {
            int ch = int.Parse(chk.ToString());
            switch (ch)
            {
                case 1:
                    return "已审核";
                case 0:
                    return "待审核";
                default:
                    return "审核未通过";
            }
        }

        public string GetCategoryName(object cgId)
        {
            long cgid = long.Parse(cgId.ToString());
            string cgName = cg.GetModel(cgid).CategoryName;
            return cgName;
        }

        protected void RadButton3_Click(object sender, EventArgs e)
        {
            cmemo.Visible = true;
        }

        protected void RadButton2_Click(object sender, EventArgs e)
        {
            cmemo.Visible = false;
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            int selectCount = RGrid_NewsList.SelectedItems.Count;
            if (selectCount == 0)
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！您未选择任何条目！！');", true);
                return;
            }
            if (!RadButton2.SelectedToggleState.Selected && !RadButton3.SelectedToggleState.Selected)
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！您未选择审核状态（结果）！！');", true);
                return;
            } 
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "审核条目";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now; 
            int totCount = selectCount;
            int sucCount = 0;
            int errCount = 0;
            StringBuilder notExist = new StringBuilder("");
            try
            {
                StringBuilder sb = new StringBuilder("【");
                StringBuilder sb2 = new StringBuilder("【");
                int checkIf = 0;
                if (RadButton2.SelectedToggleState.Selected)
                {
                    checkIf = 1;
                    foreach (GridDataItem item in RGrid_NewsList.MasterTableView.Items)
                    {
                        if ((item["CheckboxSelectColumn"].Controls[0] as CheckBox).Checked)
                        { 
                            String newsid = item["AutoID"].Text; 
                            sb.Append(newsid + " ");
                            if (!news.Exists(long.Parse(newsid)))
                            {
                                errCount++;
                                notExist.Append(newsid+ " ");
                                continue;
                            }
                            Model.News newsModel = news.GetModel(long.Parse(newsid));
                            newsModel.Checked = 1;
                            newsModel.Checker = user.UserInfo.UserID;
                            newsModel.CheckMemo = "";
                            bool isUpdate = false; 
                            isUpdate = news.Update(newsModel);
                            if (isUpdate)
                            {
                                sucCount++;
                                sb2.Append(newsid + " ");
                            } 
                        }
                    }
                } 
                else if (RadButton3.SelectedToggleState.Selected)
                {
                    foreach (GridDataItem item in RGrid_NewsList.MasterTableView.Items)
                    {
                        if ((item["CheckboxSelectColumn"].Controls[0] as CheckBox).Checked)
                        {
                            String newsid = item["AutoID"].Text;
                            sb.Append(newsid + " ");
                            if (!news.Exists(long.Parse(newsid)))
                            {
                                errCount++;
                                notExist.Append(newsid + " ");
                                continue;
                            }
                            Model.News newsModel = news.GetModel(long.Parse(newsid));
                            newsModel.Checked = 2;
                            newsModel.Checker = user.UserInfo.UserID;
                            newsModel.CheckMemo = TB_CheckMemo.Text;
                            bool isUpdate = false;
                            isUpdate = news.Update(newsModel);
                            if (isUpdate)
                            {
                                sucCount++;
                                sb2.Append(newsid + " ");
                            }
                        }
                    }
                }
                sb.Append("】");
                sb2.Append("】");
                logModel.OperationDetail = "条目审核提交列表[" + totCount + "]：" + sb.ToString() + "审核成功列表[" + sucCount + "]:" + sb2.ToString() + " - 审核状态：" + checkIf + " - 审核备注：" + TB_CheckMemo.Text;
            }
            catch (Exception ex)
            {
                logModel.Memo = "异常：" + ex.Message;
            }
            finally
            {
                if (totCount == (sucCount + errCount) && totCount > 0)
                {
                    logModel.Memo = "成功";
                    ReadValue();
                    if (errCount == 0)
                    {
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！审核条目成功！');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！审核条目成功！但是条目【"+notExist.ToString()+"】已不存在,未更新审核信息。');", true);
                    }
                }
                else
                {
                    logModel.Memo = "失败";
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！有条目修改失败！');", true);
                }
                log.Add(logModel);
            }
        }

        private void ReadValue()
        {   
            List<Model.SysUser> suList = new List<Model.SysUser>(); 
            suList = su.GetModelList(-1, "", -1, 4, true); 
            suList.RemoveAll(st => st.UserID.Equals(user.UserInfo.UserID));
            Model.SysUser sm = new Model.SysUser();
            sm.UserID = "0";
            sm.UserName = "不限";
            suList.Insert(0, sm);
            RCB_Publisher.DataValueField = "UserID";
            RCB_Publisher.DataTextField = "USerName";
            RCB_Publisher.DataSource = suList;
            RCB_Publisher.DataBind(); 
        }

        protected void RGrid_NewsList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_NewsList.DataSource = (List<Model.News>)ViewState["NewsList"];
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            List<Model.News> nsList = news.GetModelList(9, "0", -1, 7, false);
            nsList.RemoveAll(nt => nt.Publisher.Equals(user.UserInfo.UserID));
            if (!RCB_Publisher.SelectedValue.Equals("0"))
            {
                nsList.RemoveAll(nt => !nt.Publisher.Equals(RCB_Publisher.SelectedValue));
            }
            RGrid_NewsList.DataSource = nsList;
            ViewState["NewsList"] = nsList;
            RGrid_NewsList.DataBind();
            RGrid_NewsList.Visible = true;
        }


    }
}