using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Data;

namespace SmartLaw.Admin.QuestionnaireManage
{
    public partial class QuestionnaireList : BasePage
    {
        private SessionUser user;
        SmartLaw.BLL.Questionary qn = new BLL.Questionary();
        SmartLaw.BLL.UserAnswer ua = new BLL.UserAnswer();
        SmartLaw.BLL.Log log = new BLL.Log();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                RadGrid1.Visible = false;
            }
        }




        public string GetValid(object kind)
        {
            bool b = (bool)kind;
            if (b)
            { return "有效"; }
            else
            { return "无效"; }
        }

        public bool deleteAble(object auid)
        { 
            return true;
        }

        public bool StsAble(object auid)
        {
            return true;
        }


        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            List<SmartLaw.Model.Questionary> qnList = new List<Model.Questionary>();
            string titlekey = TB_TitleKey.Text.Trim();
            if (titlekey != "")
            {
                qnList = qn.GetQnModelList(1, titlekey);
            }
            else if (RCB_Enable.SelectedValue != "2")
            {
                qnList = qn.GetQnModelList(2, RCB_Enable.SelectedValue);
            }
            else
            {
                qnList = qn.DataTableToListForQn(qn.GetAllQuestionaryList().Tables[0]);
            }
            RadGrid1.DataSource = qnList;
            ViewState["qnList"] = qnList;
            RadGrid1.DataBind();
            RadGrid1.Visible = true;
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                Label lb = dataItem.FindControl("Label3") as Label;
                if (lb.Text.Equals("无效"))
                {
                    lb.CssClass = "inValid";
                } 
            }
        }


        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            { 
                string ID = Convert.ToString(e.CommandArgument);
                if (!qn.ExistQuestionary(long.Parse(ID)))
                {
                    List<SmartLaw.Model.Questionary> qnList = (List<SmartLaw.Model.Questionary>)ViewState["qnList"];
                    qnList.RemoveAll(qt => qt.ID == long.Parse(ID));
                    RadGrid1.DataSource = qnList;
                    RadGrid1.Rebind();
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('抱歉！该问卷已不存在。');", true);
                }
                else
                {
                    bool isDelete = true;
                    Model.Log logModel = new Model.Log();
                    logModel.OperationItem = "删除问卷";
                    logModel.Operator = user.UserInfo.UserID;
                    logModel.OperationTime = DateTime.Now;
                    Model.Questionary qnModel = qn.GetQnModel(long.Parse(ID));
                    try
                    {
                        logModel.OperationDetail = "问卷编号：" + qnModel.ID + " - 标题：" + qnModel.Title;
                        List<Model.Question> qsList = qn.GetQsModelList(1, ID, -1, -1, false);
                        if (qsList.Count > 0)
                        { 
                            string[] questionIDlist = new string[qsList.Count];
                            int i = 0;
                            foreach (Model.Question qsM in qsList)
                            {
                                questionIDlist[i++] = qsM.ID.ToString();
                            } 
                            ua.DeleteList(questionIDlist);
                        }
                        if (isDelete &&  qn.GetQsModelList(1, qnModel.ID.ToString()).Count > 0)
                        {
                            isDelete = false;
                            isDelete = qn.DeleteAllQuestionsInQn(qnModel.ID);
                        }
                        if (isDelete)
                        {
                            isDelete = false;
                            isDelete = qn.DeleteQuestionary(qnModel.ID);
                        }
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
                            List<SmartLaw.Model.Questionary> qnList = (List<SmartLaw.Model.Questionary>)ViewState["qnList"];
                            qnList.RemoveAll(qt => qt.ID == long.Parse(ID));
                            RadGrid1.DataSource = qnList;
                            RadGrid1.Rebind();
                            RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('恭喜！问卷删除成功。');", true);
                        }
                        else
                        {
                            RadScriptManager.RegisterStartupScript(Page, GetType(), "cc2", "OpenAlert('抱歉！问卷删除失败。');", true);
                        }
                    }
                }
            }
            if (e.CommandName == "Sts")
            {
                string ID = Convert.ToString(e.CommandArgument);
                if (!qn.ExistQuestionary(long.Parse(ID)))
                {
                    List<SmartLaw.Model.Questionary> qnList = (List<SmartLaw.Model.Questionary>)ViewState["qnList"];
                    qnList.RemoveAll(qt => qt.ID == long.Parse(ID));
                    RadGrid1.DataSource = qnList;
                    RadGrid1.Rebind();
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('抱歉！该问卷已不存在。');", true);
                }
                else
                {
                    string targetUrl = "QuestionnaireAccount.aspx?qnID=" + ID;
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "openRadWindow('" + targetUrl + "','SubModalWindow')", true);
                }
            }
        } 

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = ViewState["qnList"];
        } 

    }
}