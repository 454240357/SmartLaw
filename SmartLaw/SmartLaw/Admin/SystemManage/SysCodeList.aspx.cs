using System; 
using SmartLaw.BLL;
using System.Data;
using Telerik.Web.UI;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.SystemManage
{
    public partial class SysCodeList : BasePage
    {
        SysCode sc = new SysCode();
        Log log = new Log();
        private SessionUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Code_CRUD");
            if (!IsPostBack)
                RGrid_SysCode.Visible = false;
        }

        public string GetValid(object kind)
        {
            bool b = int.Parse(kind.ToString())==1;
            return b ? "有效" : "无效";
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            int searchType = int.Parse(RCB_SearchType.SelectedItem.Value);
            string codeContent = TB_CodeContent.Text.Trim();
            DataSet scDataSet = new DataSet();
            try
            {
                if (codeContent == "")
                {
                    scDataSet = sc.GetAllList();
                }
                else if (searchType == 1)
                {
                    scDataSet = sc.GetList(0, codeContent);
                }
                else if (searchType == 2)
                {
                    scDataSet = sc.GetList(1, codeContent);
                } 
                RGrid_SysCode.DataSource = scDataSet.Tables[0]; ;
                ViewState["SysCodeList"] = scDataSet.Tables[0];
                RGrid_SysCode.DataBind();
                RGrid_SysCode.Visible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取大类列表失败："+ex.Message);
            }
        }

        protected void RGrid_SysCode_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGrid_SysCode.DataSource = (DataTable)ViewState["SysCodeList"];
        }

        protected void RGrid_SysCode_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string sysCodeId = Convert.ToString(e.CommandArgument); 
                bool result = false;
                Model.Log logModel = new Model.Log();
                logModel.OperationItem = "删除系统代码";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                try
                {
                    Model.SysCode scModel = sc.GetModel(sysCodeId); 
                    logModel.OperationDetail = "代码编号：" + scModel.SYSCodeID + " - 代码内容：" + scModel.SYSCodeContext;
                    result = sc.Delete(sysCodeId);
                    if (result)
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
                    if (result)
                    { 
                        int delNo = -1;
                        DataTable sysCodeList = (DataTable)ViewState["SysCodeList"];
                        for (int i = sysCodeList.Rows.Count - 1; i >= 0; i--)
                        {
                            if (sysCodeList.Rows[i][0].ToString().Equals(sysCodeId))
                            {
                                delNo = i;
                                break;
                            }
                        }
                        if (delNo != -1)
                        { 
                            sysCodeList.Rows.RemoveAt(delNo);
                        }
                        RGrid_SysCode.DataSource = sysCodeList;
                        RGrid_SysCode.DataBind();
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('恭喜！代码删除成功。');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('抱歉！代码删除失败。<br />请检查该大类是否存在小类未删除!');", true);
                    }
                }
            }
        }
    } 
    
}