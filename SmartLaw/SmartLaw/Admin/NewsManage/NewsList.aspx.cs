using System;
using System.Collections.Generic; 
using System.Web.UI.WebControls;
using System.Data;
using SmartLaw.BLL;
using Telerik.Web.UI;
using SmartLaw.App_Code;
using System.Text;

namespace SmartLaw.Admin.NewsManage
{
    public partial class NewsList : BasePage
    {
        Category cg = new Category(); 
        News news = new News();
        Log log = new Log(); 
        SysCodeDetail scd = new SysCodeDetail();
        SysUser su = new SysUser();
        private SessionUser user; 
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            //user.ValidateAuthority("Auth_News_Retrieve");
            if (!user.hasAuthority("Auth_News_Add"))
            {
                href1.Visible = false;
            } 
            if (!user.hasAuthority("Auth_News_Examine"))
            {
                A1.Visible = false;
            } 
            if (!IsPostBack)
            {
                RGrid_NewsList.Visible = false;
                //初始化分类选择下拉树
                List<Model.Category> cgList = cg.DataTableToList(cg.GetList(5, "1", -1, 0, false).Tables[0]);
                cgList.RemoveAll(CT => !CT.Memo.Contains("R"));
                DataTable table = new DataTable();
                table.Columns.Add("AutoID");
                table.Columns.Add("ParentCategoryID");
                table.Columns.Add("CategoryName");
                foreach (Model.Category cgm in cgList)
                {
                    if (cgm.ParentCategoryID == -1)
                    {
                        table.Rows.Add(new String[] { cgm.AutoID.ToString(), null, cgm.CategoryName });
                    }
                    else
                    {
                        table.Rows.Add(new String[] { cgm.AutoID.ToString(), cgm.ParentCategoryID.ToString(), cgm.CategoryName });
                    }
                }
                RadDropDownTree2.DataFieldID = "AutoID";
                RadDropDownTree2.DataFieldParentID = "ParentCategoryID";
                RadDropDownTree2.DataValueField = "AutoID";
                RadDropDownTree2.DataTextField = "CategoryName";
                RadDropDownTree2.DataSource = table;
                RadDropDownTree2.DataBind();
                RadTreeView categoryTreeView = RadDropDownTree2.Controls[0] as RadTreeView;
                //categoryTreeView.Nodes[0].Expanded = true;
                categoryTreeView.ShowLineImages = false;  

                List<Model.SysCodeDetail> dataSoucerList = scd.GetModelList(0, "DataSource", -1, -1, false);
                dataSoucerList.RemoveAll(rt => rt.IsValid == false);
                Model.SysCodeDetail dsModel = new Model.SysCodeDetail();
                dsModel.SYSCodeDetialID = "0";
                dsModel.SYSCodeDetialContext = "不限";
                dataSoucerList.Insert(0, dsModel);
                RCB_DataSource.DataValueField = "SYSCodeDetialID";
                RCB_DataSource.DataTextField = "SYSCodeDetialContext";
                RCB_DataSource.DataSource = dataSoucerList;
                RCB_DataSource.DataBind();
 
                List<Model.SysUser> suList = su.GetModelList(-1, "", -1, 4, true);  
                Model.SysUser sm = new Model.SysUser();
                sm.UserID = "0";
                sm.UserName = "不限";
                suList.Insert(0, sm);
                suList.ForEach(st => st.UserName = st.UserName + " [" + st.UserID + "]");
                RCB_Publisher.DataValueField = "UserID";
                RCB_Publisher.DataTextField = "USerName";
                RCB_Publisher.DataSource = suList;
                RCB_Publisher.DataBind();
                RCB_Publisher.SelectedValue = user.UserInfo.UserID;
            }
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            ViewState["sortNo"] = "-1";
            ViewState["desc"] = "0";
            ViewState["sortArg"] = "";
            BindData(0, RGrid_NewsList.PageSize); 
            RGrid_NewsList.CurrentPageIndex = 0;
            RGrid_NewsList.MasterTableView.SortExpressions.Clear();
            RGrid_NewsList.Rebind();
        }

        public string GetValid(object kind)
        {
            bool b = int.Parse(kind.ToString()) == 1;
            if (b)
            { return "有效"; }
            else
            { return "无效"; } 
        }

        public string GetTime(object time)
        {

            DateTime dt = DateTime.Parse(time.ToString());
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string GetChecked(Object chk)
        {
            int ch = int.Parse(chk.ToString());
            if (ch==1)
            { 
                return "已审核";
            }
            if (ch == 0)
            {
                return "待审核";
            }
            return "审核未通过";
        }

        public string GetCategoryName(object cgId)
        {
            long cgid = long.Parse(cgId.ToString());
            string cgName = cg.GetModel(cgid).CategoryName;
            return cgName;
        }

        protected void RGrid_NewsList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                Label lb = dataItem.FindControl("Label3") as Label;
                if (lb.Text.Equals("无效"))
                {
                    lb.CssClass = "inValid";
                }
                //(dataItem["Label3"].Controls[0] as Button).CssClass = "MyButton";
            }
        } 

        protected void RGrid_NewsList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            { 
                string AutoID = Convert.ToString(e.CommandArgument);
                if (!news.Exists(long.Parse(AutoID)))
                {
                    int delNo = -1;
                    DataTable newsList = (DataTable)ViewState["NewsList"];
                    for (int i = newsList.Rows.Count - 1; i >= 0; i--)
                    {
                        if (newsList.Rows[i][0].ToString().Equals(AutoID))
                        {
                            delNo = i;
                            break;
                        }
                    }
                    if (delNo != -1)
                    {
                        newsList.Rows.RemoveAt(delNo);
                    }
                    RGrid_NewsList.DataSource = newsList;
                    RGrid_NewsList.DataBind();
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('抱歉！该条目已不存在。');", true);
                }
                else
                {
                    bool isDelete = false;
                    Model.Log logModel = new Model.Log();
                    logModel.OperationItem = "删除条目";
                    logModel.Operator = user.UserInfo.UserID;
                    logModel.OperationTime = DateTime.Now; 
                    Model.News newsModel = news.GetModel(long.Parse(AutoID));
                    try
                    {
                        logModel.OperationDetail = "条目编号：" + newsModel.AutoID + " - 标题：" + newsModel.Title + " - 分类编号：" + newsModel.CategoryID + " - 发布人：" + newsModel.Publisher + " - 审核人：" + newsModel.Checker;
                        isDelete = news.Delete(long.Parse(AutoID)); 
                        if (isDelete)
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
                        if (isDelete)
                        {
                            int delNo = -1;
                            DataTable newsList = (DataTable)ViewState["NewsList"];
                            for (int i = newsList.Rows.Count - 1; i >= 0; i--)
                            {
                                if (newsList.Rows[i][0].ToString().Equals(AutoID))
                                {
                                    delNo = i;
                                    break;
                                }
                            }
                            if (delNo != -1)
                            {
                                newsList.Rows.RemoveAt(delNo);
                            }
                            RGrid_NewsList.DataSource = newsList;
                            RGrid_NewsList.DataBind();
                            RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('恭喜！条目删除成功。');", true);
                        }
                        else
                        {
                            RadScriptManager.RegisterStartupScript(Page, GetType(), "cc2", "OpenAlert('抱歉！条目删除失败。');", true);
                        }
                    }
                }
            }
            if (e.CommandName == "detail")
            {
                long newsid = long.Parse(e.CommandArgument.ToString());
                if (news.Exists(newsid))
                {
                    string targetUrl = "NewsDetail.aspx?AutoID=" + newsid;
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c1", "openRadWindow('" + targetUrl + "','SubModalWindow')", true);
                }
                else
                {
                    int delNo = -1;
                    DataTable newsList = (DataTable)ViewState["NewsList"];
                    for (int i = newsList.Rows.Count - 1; i >= 0; i--)
                    {
                        if (newsList.Rows[i][0].ToString().Equals(newsid.ToString()))
                        {
                            delNo = i;
                            break;
                        }
                    }
                    if (delNo != -1)
                    {
                        newsList.Rows.RemoveAt(delNo);
                    }
                    RGrid_NewsList.DataSource = newsList;
                    RGrid_NewsList.DataBind();
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('抱歉！该条目已不存在。');", true);
                }

            }
            if (e.CommandName == "Page")
            {
                int pageindex = -1;
                int startIndex = 1; 
                if (int.TryParse(e.CommandArgument.ToString(), out pageindex))
                {
                    startIndex = (pageindex - 1) * RGrid_NewsList.PageSize; 
                }
                else
                {
                    if (e.CommandArgument.Equals("Next"))
                    {
                        pageindex = RGrid_NewsList.CurrentPageIndex+1;
                        startIndex = (pageindex)  * RGrid_NewsList.PageSize; 
                    }
                    else if (e.CommandArgument.Equals("Prev"))
                    {
                        pageindex = RGrid_NewsList.CurrentPageIndex-1;
                        startIndex = pageindex * RGrid_NewsList.PageSize; 
                    }
                    else if (e.CommandArgument.Equals("First"))
                    { 
                        startIndex = 0; 
                    }
                    else if (e.CommandArgument.Equals("Last"))
                    {
                        pageindex = RGrid_NewsList.PageCount;
                        startIndex = (pageindex - 1) * RGrid_NewsList.PageSize; 
                    }
                    if (e.CommandArgument.Equals("GoToPage"))
                    {

                    }
                }
                BindData(startIndex, RGrid_NewsList.PageSize);
                RGrid_NewsList.Rebind(); 
            }
        }

        protected void RGrid_NewsList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            int pageindex = RGrid_NewsList.CurrentPageIndex;
            int startIndex = pageindex * RGrid_NewsList.PageSize;
            BindData(startIndex, RGrid_NewsList.PageSize);
        } 

        public bool deleteAble(object auid)
        { 
            Model.News newsM = news.GetModel(long.Parse(auid.ToString()));
            if (newsM.DataSource.Equals("DataSource_Manual"))
            {
                if (canDo("Auth_News_Del",auid.ToString()) )
                {
                    return true;
                } 
            }
            return false;
        }

        private bool canDo(string operationItem,string autoID)  //是否有权限
        {
            DataSet scdDsRole = scd.GetListBySysCode(user.UserInfo.UserID, "Role");
            String user_Role = scdDsRole.Tables[0].Rows[0]["SYSCodeDetialID"].ToString(); ;
            if (user_Role.Equals("Aministrators"))
            {
                return true;
            } 
            if (!user.hasAuthority(operationItem))
            {
                return false;
            }  
            if (news.GetModel(long.Parse(autoID)).Publisher.Equals(user.UserInfo.UserID))
            {
                return true;
            }
            return false;
        }

        protected void RGrid_NewsList_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            String tempSa = ViewState["sortArg"].ToString();
            ViewState["sortArg"] = e.CommandArgument.ToString();
            if (tempSa.Equals(e.CommandArgument.ToString()))
            {
                if (ViewState["desc"].ToString().Equals("0"))
                {
                    ViewState["desc"] = "1";
                }
                else
                {
                    ViewState["desc"] = "0";
                } 
            }
            if (e.CommandArgument.ToString().Equals("Title"))
            {
                ViewState["sortNo"] = "1";
            }
            else if (e.CommandArgument.ToString().Equals("CategoryID"))
            {
                ViewState["sortNo"] = "2";
            }
            else if (e.CommandArgument.ToString().Equals("IsValid"))
            {
                ViewState["sortNo"] = "8";
            }
            else if (e.CommandArgument.ToString().Equals("Checked"))
            {
                ViewState["sortNo"] = "9";
            }
            else if (e.CommandArgument.ToString().Equals("LastModifyTime"))
            {
                ViewState["sortNo"] = "7";
            }
        }


        private bool BindData(int startIndex, int size)
        { 
            RadTreeView categoryTreeView = RadDropDownTree2.Controls[0] as RadTreeView; 
            string TitleKey = TB_TitleKey.Text;
            string ContentKey = TB_ContentKey.Text;
            int index = 0;
            int totalCount = 0;
            DataSet ds = new DataSet();
            if (TitleKey != "")
            {
                index++;
            }
            if (ContentKey != "")
            {
                index++;
            }
            if (!RCB_Enable.SelectedValue.Equals("2"))
            {
                index++;
            }
            if (!RCB_Checked.SelectedValue.Equals("3"))
            {
                index++;
            }
            if (!RCB_DataSource.SelectedValue.Equals("0"))
            {
                index++;
            }
            if (!RCB_Publisher.SelectedValue.Equals("0"))
            {
                index++;
            }
            if (categoryTreeView.CheckedNodes.Count != 0)
            {
                index++;
            }  
            int[] keys = new int[index];
            string[] values = new string[index];
            index = 0;
            if (TitleKey != "")
            {
                keys[index] = 1;
                values[index++] = TitleKey;
            }
            if (ContentKey != "")
            {
                keys[index] = 3;
                values[index++] = ContentKey;
            }
            if (!RCB_Enable.SelectedValue.Equals("2"))
            {
                keys[index] = 8;
                values[index++] = RCB_Enable.SelectedValue;
            }
            if (!RCB_Checked.SelectedValue.Equals("3"))
            {
                keys[index] = 9;
                values[index++] = RCB_Checked.SelectedValue;
            }
            if (!RCB_DataSource.SelectedValue.Equals("0"))
            {
                keys[index] = 5;
                values[index++] = RCB_DataSource.SelectedValue;
            }
            if (!RCB_Publisher.SelectedValue.Equals("0"))
            {
                keys[index] = 4;
                values[index++] = RCB_Publisher.SelectedValue;
            }
            if (categoryTreeView.CheckedNodes.Count != 0)
            {
                StringBuilder cgBuilder = new StringBuilder();
                int count = 0;
                foreach (RadTreeNode rtn in categoryTreeView.CheckedNodes)
                {
                    count++;
                    if (count == categoryTreeView.CheckedNodes.Count)
                    {
                        cgBuilder.Append(rtn.Value);
                    }
                    else
                    {
                        cgBuilder.Append(rtn.Value + ",");
                    }
                } 
                keys[index] = 2;
                values[index++] = cgBuilder.ToString();
            }  
            int sortNo = int.Parse(ViewState["sortNo"].ToString());
            bool desc = ViewState["desc"].ToString().Equals("1") ? true : false;
            if (index != 0)
            {  
                ds = news.GetListEx(keys, values, -1, sortNo, desc, startIndex, size);
                totalCount = news.GetReCordEx(keys, values);
            }
            else
            {
                ds = news.GetListByPage(-1, "", sortNo, desc, startIndex, size);
                totalCount = news.GetRecordCount(-1, "");
            }

            RGrid_NewsList.VirtualItemCount = totalCount;
            RGrid_NewsList.DataSource = ds.Tables[0];
            ViewState["NewsList"] = ds.Tables[0];
            RGrid_NewsList.Visible = true;  
            return true;
        }
         
    }
}