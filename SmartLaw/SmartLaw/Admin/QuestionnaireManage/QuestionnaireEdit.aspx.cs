using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Collections;
using System.Data;
using System.Text;

namespace SmartLaw.Admin.QuestionnaireManage
{
    public partial class QuestionnaireEdit : BasePage
    {
        private SessionUser user;
        SmartLaw.BLL.Questionary qn = new BLL.Questionary(); 
        SmartLaw.BLL.Log log = new BLL.Log(); 
        protected void Page_Load(object sender, EventArgs e)
        { 
            user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                SetInitialRowgrid();
            }
            
        }

        private void SetInitialRowgrid()
        {
            string a = "";
            bool s = true;
            if (ViewState["IALV"] == null)
            {
                DataTable dt = new DataTable();
                DataRow dr = dt.NewRow();
                dt.Columns.Add(new DataColumn("q", a.GetType()));
                dt.Columns.Add(new DataColumn("a1", a.GetType()));
                dt.Columns.Add(new DataColumn("a2", a.GetType()));
                dt.Columns.Add(new DataColumn("a3", a.GetType()));
                dt.Columns.Add(new DataColumn("a4", a.GetType()));
                dt.Columns.Add(new DataColumn("s", s.GetType()));
                dt.Columns.Add(new DataColumn("d", s.GetType()));
                dr = dt.NewRow();
                dr["q"] = "";
                dr["a1"] = "";
                dr["a2"] = "";
                dr["a3"] = "";
                dr["a4"] = "";
                dr["s"] = true;
                dr["d"] = false;
                dt.Rows.Add(dr);
                ViewState["IALV"] = dt;
            }
            HL_Qcount.Text = "1";
            RadGrid1.DataSource = ViewState["IALV"];
        }

        private void AddNewRowToGrid()
        { 
            int rowIndex = 0; 
            if (ViewState["IALV"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["IALV"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <=dtCurrentTable.Rows.Count; i++)
                    {
                        RadTextBox box = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("content");
                        RadTextBox box1 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer1");
                        RadTextBox box2 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer2");
                        RadTextBox box3 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer3");
                        RadTextBox box4 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer4");
                        RadButton rbt = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton3"); 
                        dtCurrentTable.Rows[i - 1]["q"] = box.Text;
                        dtCurrentTable.Rows[i - 1]["a1"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["a2"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["a3"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["a4"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["s"] = rbt.Checked;
                        dtCurrentTable.Rows[i - 1]["d"] = !rbt.Checked;
                        rowIndex += 1;
                    }
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["q"] = "";
                    drCurrentRow["a1"] = "";
                    drCurrentRow["a2"] = "";
                    drCurrentRow["a3"] = "";
                    drCurrentRow["a4"] = "";
                    drCurrentRow["s"] = true;
                    drCurrentRow["d"] = false;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    HL_Qcount.Text = (int.Parse(HL_Qcount.Text) + 1).ToString();
                    ViewState["IALV"] = dtCurrentTable; 
                    RadGrid1.DataSource = dtCurrentTable;
                    RadGrid1.Rebind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            } 
        }

        private void SetPreviousDatagrid()
        { 
            int rowIndex = 0;
            if (ViewState["IALV"] != null)
            {
                DataTable dt = (DataTable)ViewState["IALV"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RadTextBox box = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("content");
                        RadTextBox box1 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer1");
                        RadTextBox box2 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer2");
                        RadTextBox box3 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer3");
                        RadTextBox box4 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer4");
                        RadButton rbt = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton3");
                        RadButton rbt2 = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton4"); 
                        box.Text = dt.Rows[i]["q"].ToString();
                        box1.Text = dt.Rows[i]["a1"].ToString();
                        box2.Text = dt.Rows[i]["a2"].ToString();
                        box2.Text = dt.Rows[i]["a3"].ToString();
                        box4.Text = dt.Rows[i]["a4"].ToString();
                        rbt.Checked = (bool)dt.Rows[i]["s"];
                        rbt2.Checked = !rbt.Checked;
                        rowIndex += 1;  
                    }
                }
                ViewState["IALV"] = dt; 
            } 
        }

        private void SetPreviousDatagrid2()
        { 
            int rowIndex = 0;
            if (ViewState["IALV"] != null)
            {
                DataTable dt = (DataTable)ViewState["IALV"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RadTextBox box = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("content");
                        RadTextBox box1 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer1");
                        RadTextBox box2 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer2");
                        RadTextBox box3 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer3");
                        RadTextBox box4 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer4");
                        RadButton rbt = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton3"); 
                        dt.Rows[i]["q"] = box.Text;
                        dt.Rows[i]["a1"] = box1.Text;
                        dt.Rows[i]["a2"] = box2.Text;
                        dt.Rows[i]["a3"]= box2.Text;
                        dt.Rows[i]["a4"] = box4.Text;
                        dt.Rows[i]["s"] = rbt.Checked;
                        dt.Rows[i]["d"] = !rbt.Checked;
                        rowIndex += 1;
                    }
                }
                ViewState["IALV"] = dt; 
            } 
        }

        protected void rowrem_IT_Click(object sender, EventArgs e)
        {

            SetPreviousDatagrid2();
            LinkButton lb = sender as LinkButton;
            GridDataItem gvRow = lb.NamingContainer as GridDataItem;
            int rowID = gvRow.ItemIndex+1;
            if (ViewState["IALV"] != null)
            {
                DataTable dt = (DataTable)ViewState["IALV"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.ItemIndex <= dt.Rows.Count - 1)
                    {
                        dt.Rows.RemoveAt(rowID - 1);
                    }
                } 
                HL_Qcount.Text = (int.Parse(HL_Qcount.Text) - 1).ToString();
                ViewState["IALV"] = dt;
                RadGrid1.DataSource = dt;
                RadGrid1.Rebind();
            }
            SetPreviousDatagrid();
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = ViewState["IALV"];
        } 

        protected void rbtnSubmit_Click(object sender, EventArgs e)
        {
            string title = RTB_Title.Text;
            if (title.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，标题不能为空！');", true);
                return;
            }
            SmartLaw.Model.Questionary qnModel = new Model.Questionary();
            List<SmartLaw.Model.Question> qsModelList = new List<Model.Question>();
            if (!RadButton2.Checked)
            {
                qnModel.IsValid = true;
                for (int i = 0; i < RadGrid1.Items.Count; i++)
                {
                    SmartLaw.Model.Question qsM = new Model.Question();
                    qsM.Orders = i;
                    RadTextBox box = (RadTextBox)RadGrid1.Items[i].FindControl("content");
                    string Content = box.Text;
                    if (Content.Trim() == "")
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，问题不能为空！');", true);
                        return;
                    }
                    qsM.Content = Content;
                    RadTextBox box1 = (RadTextBox)RadGrid1.Items[i].FindControl("answer1");
                    string Answer1 = box1.Text;
                    RadTextBox box2 = (RadTextBox)RadGrid1.Items[i].FindControl("answer2");
                    string Answer2 = box2.Text;
                    RadTextBox box3 = (RadTextBox)RadGrid1.Items[i].FindControl("answer3");
                    string Answer3 = box3.Text;
                    RadTextBox box4 = (RadTextBox)RadGrid1.Items[i].FindControl("answer4");
                    string Answer4 = box4.Text;
                    StringBuilder ansSb = new StringBuilder();
                    if (Answer1.Trim() != "")
                    {
                        ansSb.Append(Answer1.Trim() + "|");
                    }
                    if (Answer2.Trim() != "")
                    {
                        ansSb.Append(Answer2.Trim() + "|");
                    }
                    if (Answer3.Trim() != "")
                    {
                        ansSb.Append(Answer3.Trim() + "|");
                    }
                    if (Answer4.Trim() != "")
                    {
                        ansSb.Append(Answer4.Trim() + "|");
                    }
                    if (ansSb.ToString().Equals(""))
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，一个问题至少有一个选项！');", true);
                        return;
                    }
                    string[] ans = ansSb.ToString().Substring(0, ansSb.ToString().Length - 1).Split('|');
                    qsM.Answer = ans;
                    RadButton rbt = RadGrid1.Items[i].FindControl("RadButton3") as RadButton;
                    qsM.IsSingle = rbt.Checked;
                    qsModelList.Add(qsM);
                }
            }
            else
            {
                qnModel.IsValid = false;
            }
            SmartLaw.Model.Log logM = new Model.Log();
            logM.OperationItem = "新增调查问卷";
            logM.OperationDetail = "标题："+title;
            logM.OperationTime = DateTime.Now;
            logM.Operator = user.UserInfo.UserID;
            logM.Memo = "";
            long idAdd = 0; 
            try
            {
                qnModel.Title = title;

                if (qnModel.IsValid)
                {
                    idAdd = qn.AddQuestionary(qnModel, qsModelList.ToArray());
                }
                else
                {
                    idAdd = qn.AddEmptyQuestionary(qnModel);
                }
                if (idAdd != 0)
                {
                    logM.Memo += "成功";
                }
                else
                {
                    logM.Memo += "失败";
                }
            }
            catch (Exception ex)
            {
                logM.Memo += ex.Message;
            }
            finally
            {
                log.Add(logM);
                if (idAdd!=0)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('恭喜！添加成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！添加失败！');", true);
                } 
            }
        }

        protected void RadButton2_Click(object sender, EventArgs e)
        {
            if (RadButton2.Checked)
            {
                RadGrid1.Enabled = false;
            }
            else
            {
                RadGrid1.Enabled = true;
            }
        } 
 

    }
}