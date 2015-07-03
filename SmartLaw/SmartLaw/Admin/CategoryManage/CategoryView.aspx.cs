using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using SmartLaw.BLL;
using System.Collections;
using SmartLaw.App_Code;
using SmartLaw.Admin.NewsManage;

namespace SmartLaw.Admin.CategoryManage
{
    public partial class CategoryView : SmartLaw.App_Code.BasePage
    {
        Category cg = new Category();
        SysCodeDetail scd = new SysCodeDetail(); 
        Log log = new Log(); 
        private SessionUser user; 
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession(); 
            if (!IsPostBack)
            {
                BindData();
            } 
        }

        private void BindData()
        {
            List<SmartLaw.Model.Category> cgList = cg.DataTableToList(cg.GetList(5,"1",-1,0,false).Tables[0]);
            DataTable table = new DataTable();
            table.Columns.Add("AutoID");
            table.Columns.Add("ParentCategoryID");
            table.Columns.Add("CategoryName");
            foreach (SmartLaw.Model.Category cgM in cgList)
            {
                if (cgM.ParentCategoryID==-1)
                {
                    table.Rows.Add(new String[] { cgM.AutoID.ToString(), null, cgM.CategoryName });
                }
                else
                {
                    table.Rows.Add(new String[] { cgM.AutoID.ToString(), cgM.ParentCategoryID.ToString(), cgM.CategoryName });
                }
            }
            RadTreeView1.DataFieldID = "AutoID";
            RadTreeView1.DataFieldParentID = "ParentCategoryID";
            RadTreeView1.DataTextField = "CategoryName";
            RadTreeView1.DataValueField = "AutoID";  
            RadTreeView1.DataSource = table;
            RadTreeView1.DataBind();
            RadTreeView1.Nodes[0].Expanded = true; 
            RadTreeView1.ShowLineImages = false; 
            //无增删改权限
            if (!user.hasAuthority("Auth_Category_CRUD"))
            {
                foreach (RadTreeNode rtn in RadTreeView1.GetAllNodes())
                {
                    rtn.EnableContextMenu = false;
                    if (!cg.GetModel(long.Parse(rtn.Value)).IsValid)
                    {
                        rtn.CssClass = "inValid";
                    }

                }
            }
            //有增删改权限
            else
            {
                foreach (RadTreeNode rtn in RadTreeView1.GetAllNodes())
                {
                    if (!cg.GetModel(long.Parse(rtn.Value)).IsValid)
                    {
                        rtn.CssClass = "inValid";
                    } 
                }
            }
            //无排序权限
            if (!user.hasAuthority("Auth_Category_Order"))
            {
                RadTreeView1.EnableDragAndDrop = false;
                RadTreeView1.EnableDragAndDropBetweenNodes = false;
            }
            //有排序权限
            else
            { 
            }
            btn_Submit.Visible = false;
        }
        protected void RadTreeView1_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {  
            e.SourceDragNode.Remove();
            if (e.DestDragNode.Level == 0)
            {
                e.DestDragNode.Nodes.Add(e.SourceDragNode);
            }
            else
            {
                e.DestDragNode.InsertAfter(e.SourceDragNode);
            }
            btn_Submit.Visible = true;
        }

        protected void RadTreeView1_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            string commondName = e.MenuItem.Value; 
            RadTreeNode clickedNode = e.Node;
            string cgid = clickedNode.Value;
            if (commondName.Equals("edit"))
            {
                if (cg.Exists(long.Parse(cgid)))
                {
                    string targetUrl = "CategoryDetail.aspx?CategoryID=" + cgid;
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "openRadWindow('" + targetUrl + "','SubModalWindow')", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉，该分类已不存在！（请刷新！）');", true);
                }
            }
            else if (commondName.Equals("new"))
            {
                if (cg.Exists(long.Parse(cgid)))
                {
                    string targetUrl = "AddCategory.aspx?CategoryParentID=" + cgid;
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "openRadWindow('" + targetUrl + "','SubModalWindow')", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉，该分类已不存在！（请刷新！）');", true);
                }
            }
            else if (commondName.Equals("delete"))
            {
                if (!cg.Exists(long.Parse(cgid)))
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉，该分类已不存在！（请刷新！）');", true);
                }
                else
                {
                    if (cg.GetList(2, cgid).Tables[0].Rows.Count != 0)
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('删除失败，仍含有子类的分类不能删除！');", true);
                    }
                    else
                    {
                        bool isDelete = true;
                        Model.Log logModel = new Model.Log();
                        logModel.OperationItem = "删除分类";
                        logModel.Operator = user.UserInfo.UserID;
                        logModel.OperationTime = DateTime.Now; 
                        try
                        {
                            Model.Category cgModel = cg.GetModel(long.Parse(cgid)); 
                            logModel.OperationDetail = "分类编号：" + cgModel.AutoID + " - 分类名称：" + cgModel.CategoryName + " - 父类编号：" + cgModel.ParentCategoryID + " - 备注：" + cgModel.Memo;
                             
                            isDelete = cg.Delete(long.Parse(cgid)); 
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
                                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c4", "OpenAlert('删除成功！');", true);
                                e.Node.Remove();
                            }
                            else
                            {
                                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c5", "OpenAlert('删除失败！');", true);
                            }
                        }
                    }
                }
            }

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            RadTreeNode rootNode = RadTreeView1.Nodes[0];
            int c = RadTreeView1.GetAllNodes().Count;
            string rootAutoId = rootNode.Value;
            Hashtable ht = new Hashtable();
            ht.Add(0, 1);
            int currentLevel = 0;
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改分类排序";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            logModel.OperationDetail = "";
            logModel.Memo = "";
            try
            {
                foreach (RadTreeNode rtn in RadTreeView1.GetAllNodes())
                {
                    if (ht.Contains(rtn.Level))
                    {
                        if (rtn.Level != currentLevel && rtn.ParentNode.Level == currentLevel)
                        {
                            ht[rtn.Level] = 1;
                        }
                        SmartLaw.Model.Category caModel = cg.GetModel(long.Parse(rtn.Value));
                        if (caModel != null)
                        {
                            if (caModel.ParentCategoryID != -1 &&
                                (caModel.ParentCategoryID != long.Parse(rtn.ParentNode.Value) ||
                                 caModel.Orders != Int32.Parse(ht[rtn.Level].ToString())))
                            {
                                isUpdate = false;
                                caModel.Orders = Int32.Parse(ht[rtn.Level].ToString());
                                caModel.ParentCategoryID = long.Parse(rtn.ParentNode.Value);
                                isUpdate = cg.Update(caModel);
                                if (!isUpdate)
                                {
                                    logModel.Memo = "失败！";
                                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('更新分类信息失败！');", true);
                                    break;
                                }
                            }
                            ht[rtn.Level] = caModel.Orders + 1; 
                            currentLevel = rtn.Level;
                        }
                    }
                    else
                    {
                        ht.Add(rtn.Level, 1);
                        SmartLaw.Model.Category caModel = cg.GetModel(long.Parse(rtn.Value));
                        if (caModel != null)
                        {
                            if (caModel.ParentCategoryID != -1 &&
                                (caModel.ParentCategoryID != long.Parse(rtn.ParentNode.Value) ||
                                 caModel.Orders != Int32.Parse(ht[rtn.Level].ToString())))
                            {
                                isUpdate = false;
                                caModel.Orders = Int32.Parse(ht[rtn.Level].ToString());
                                caModel.ParentCategoryID = long.Parse(rtn.ParentNode.Value);
                                isUpdate = cg.Update(caModel);
                                if (!isUpdate)
                                {
                                    logModel.Memo = "失败！";
                                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('更新分类信息失败！');", true);
                                    break;
                                }
                            }
                            ht[rtn.Level] = caModel.Orders + 1;
                            currentLevel = rtn.Level;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logModel.Memo = "异常：" + ex.Message;
            }
            finally
            {
                if (isUpdate)
                {
                    logModel.Memo = "成功";
                    log.Add(logModel);
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('恭喜！更新分类信息成功！');", true);
                }
                else
                {
                    log.Add(logModel);
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('抱歉！更新分类信息失败！');", true);
                }
            }

        }

        protected void btreload_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            RadTreeView1.CollapseAllNodes();
        }

        protected void RadButton2_Click(object sender, EventArgs e)
        {
            RadTreeView1.ExpandAllNodes();
        } 
  
    }
}