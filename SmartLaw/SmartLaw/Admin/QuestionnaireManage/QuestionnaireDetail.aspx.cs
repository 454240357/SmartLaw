using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Data;
using System.Text;

namespace SmartLaw.Admin.QuestionnaireManage
{
    public partial class QuestionnaireDetail : BasePage
    {
        public long QnID;
        private SessionUser user;
        SmartLaw.BLL.Questionary qn = new BLL.Questionary();
        SmartLaw.BLL.UserAnswer ua = new BLL.UserAnswer();
        SmartLaw.BLL.Log log = new BLL.Log(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            QnID = long.Parse(Request.QueryString["ID"]);
            if (!IsPostBack)
            {
                if (!ReadValue())
                {
                    Panel1.Visible = false;
                }
            }
        }

        private bool ReadValue()
        {
            string a = "";
            bool s = true;
            long id = 0;
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            dt.Columns.Add(new DataColumn("id", id.GetType()));
            dt.Columns.Add(new DataColumn("q", a.GetType()));
            dt.Columns.Add(new DataColumn("a1", a.GetType()));
            dt.Columns.Add(new DataColumn("a2", a.GetType()));
            dt.Columns.Add(new DataColumn("a3", a.GetType()));
            dt.Columns.Add(new DataColumn("a4", a.GetType()));
            dt.Columns.Add(new DataColumn("s", s.GetType()));
            dt.Columns.Add(new DataColumn("d", s.GetType()));
            SmartLaw.Model.Questionary qnModel = qn.GetQnModel(QnID);
            RTB_Title.Text = qnModel.Title;
            RCB_Enable.SelectedValue = qnModel.IsValid ? "1" : "0";
            List<SmartLaw.Model.Question> qsList = qn.GetQsModelList(1, QnID.ToString(), -1, 4, false);
            bool editable = true;
            if (qsList.Count > 0)
            {
                foreach (SmartLaw.Model.Question qsM in qsList)
                {
                    if (editable && ua.GetOnModelListForQuestion(qsM.ID).Count > 0)
                    {
                        editable = false;
                    }
                    dr = dt.NewRow();
                    dr["id"] = qsM.ID;
                    dr["q"] = qsM.Content;
                    for (int i = 1; i <= qsM.Answer.Length; i++)
                    {
                        dr["a" + i] = qsM.Answer[i - 1];
                    }
                    dr["s"] = qsM.IsSingle;
                    dr["d"] = !qsM.IsSingle;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                dr = dt.NewRow();
                dr["id"] = -1;
                dr["q"] = "";
                dr["a1"] = "";
                dr["a2"] = "";
                dr["a3"] = "";
                dr["a4"] = "";
                dr["s"] = true;
                dr["d"] = false;
                dt.Rows.Add(dr);
            }
            ViewState["IALV"] = dt;
            SetEnable(editable);
            HL_Qcount.Text = qsList.Count.ToString();

            return true;
        }

        private void SetEnable(bool ea)
        {
            RTB_Title.ReadOnly = !ea;
            RadButton2.Enabled = ea; 
            rbtnSubmit.Enabled = ea;
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
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        RadTextBox idbox = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("idbox");
                        RadTextBox box = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("content");
                        RadTextBox box1 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer1");
                        RadTextBox box2 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer2");
                        RadTextBox box3 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer3");
                        RadTextBox box4 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer4");
                        RadButton rbt = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton3");
                        dtCurrentTable.Rows[i - 1]["id"] = long.Parse(idbox.Text);
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
                    drCurrentRow["id"] = -1;
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
                        RadTextBox idbox = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("idbox");
                        RadTextBox box = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("content");
                        RadTextBox box1 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer1");
                        RadTextBox box2 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer2");
                        RadTextBox box3 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer3");
                        RadTextBox box4 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer4");
                        RadButton rbt = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton3");
                        RadButton rbt2 = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton4");
                        idbox.Text = dt.Rows[i]["id"].ToString();
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
                        RadTextBox idbox = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("idbox");
                        RadTextBox box = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("content");
                        RadTextBox box1 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer1");
                        RadTextBox box2 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer2");
                        RadTextBox box3 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer3");
                        RadTextBox box4 = (RadTextBox)RadGrid1.Items[rowIndex].FindControl("answer4");
                        RadButton rbt = (RadButton)RadGrid1.Items[rowIndex].FindControl("RadButton3");
                        dt.Rows[i]["id"] = idbox.Text;
                        dt.Rows[i]["q"] = box.Text;
                        dt.Rows[i]["a1"] = box1.Text;
                        dt.Rows[i]["a2"] = box2.Text;
                        dt.Rows[i]["a3"] = box2.Text;
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
            int rowID = gvRow.ItemIndex + 1;
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
            SmartLaw.Model.Questionary qnModel = qn.GetQnModel(QnID);
            List<SmartLaw.Model.Question> qsModelList = qn.GetQsModelList(1, QnID.ToString());
            List<SmartLaw.Model.Question> qsnewModelList = new List<Model.Question>();
            if (!RadButton2.Checked)
            {
                qnModel.IsValid = true;
                for (int i = 0; i < RadGrid1.Items.Count; i++)
                {
                    RadTextBox idbox = (RadTextBox)RadGrid1.Items[i].FindControl("idbox");
                    SmartLaw.Model.Question qsM = new Model.Question();
                    long iid = long.Parse(idbox.Text);
                    if (iid != -1)
                    {
                        qsM = qn.GetQsModel(iid);
                    }
                    else
                    {
                        qsM.ID = -1;
                    }
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
                    qsM.QuestionaryID = QnID;
                    qsnewModelList.Add(qsM);
                }
            }
            else
            {
                qnModel.IsValid = false;
            }
            SmartLaw.Model.Log logM = new Model.Log();
            logM.OperationItem = "修改调查问卷";
            logM.OperationDetail = "标题：" + title;
            logM.OperationTime = DateTime.Now;
            logM.Operator = user.UserInfo.UserID;
            logM.Memo = "";
            bool isUpdate = false;
            try
            {
                qnModel.Title = title;
                if (qnModel.IsValid)
                {
                    isUpdate = qn.UpdateQuestionary(qnModel);
                    if (isUpdate)
                    {
                        List<long> oldIds = new List<long>();
                        foreach (SmartLaw.Model.Question qsM in qsnewModelList)
                        {
                            if (qsM.ID != -1)
                            {
                                isUpdate = false;
                                oldIds.Add(qsM.ID);
                                isUpdate = qn.UpdateQuestion(qsM);
                                if (!isUpdate)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (qn.AddQuestion(QnID, qsM) == 0)
                                {
                                    isUpdate = false;
                                    break;
                                }
                            }
                        }
                        foreach (SmartLaw.Model.Question qsoldM in qsModelList)
                        {
                            if (!oldIds.Contains(qsoldM.ID))
                            {
                                qn.DeleteQuestion(qsoldM.ID);
                            }
                        }
                    }
                }
                else
                {
                    if (qsModelList.Count > 0)
                    {
                        isUpdate = qn.DeleteAllQuestionsInQn(QnID);
                    }
                    if (isUpdate)
                    {
                        isUpdate = qn.UpdateQuestionary(qnModel);
                    }
                }
                if (isUpdate)
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
                if (isUpdate)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('恭喜！修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！修改失败！');", true);
                }
            }
        }

        protected void btn_ChangeValid_Click(object sender, EventArgs e)
        {
            bool qnEnable = RCB_Enable.SelectedItem.Value == "1" ? true : false;
            Model.Questionary qnMOdel = qn.GetQnModel(QnID);
            qnMOdel.IsValid = qnEnable;
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "修改问卷状态";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "问卷编号：" + QnID + " - 状态：" + qnEnable;
                isUpdate = qn.UpdateQuestionary(qnMOdel);
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
                logModel.Memo += "异常：" + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isUpdate)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('恭喜！状态修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！状态修改失败！');", true);
                }
            }
        }

    }
}