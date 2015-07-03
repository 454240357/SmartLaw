using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using Telerik.Charting;
using Telerik.Charting.Styles;

namespace SmartLaw.Admin.QuestionnaireManage
{
    public partial class QuestionnaireAccount : BasePage
    {
        SmartLaw.BLL.UserAnswer ua = new SmartLaw.BLL.UserAnswer();
        SmartLaw.BLL.Questionary qn = new SmartLaw.BLL.Questionary();
        private long qnID; 
        private SessionUser user;

        protected override void OnLoad(EventArgs e)
        {
            user = SessionUser.GetSession();
            qnID = long.Parse(Request.QueryString["qnID"]);
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
            SmartLaw.Model.Questionary qnM = qn.GetQnModel(qnID);
            RTB_Title.Text = qnM.Title;
            List<Model.Question> qsList = qn.GetQsModelList(1, qnID.ToString(), -1, 4, false);
            if (qsList.Count == 0)
            {
                return true;
            }
            string str = "";
            DataTable dt = new DataTable(); 
            dt.Columns.Add(new DataColumn("Title", str.GetType()));
            dt.Columns.Add(new DataColumn("QSID", str.GetType()));
            dt.Columns.Add(new DataColumn("xx1", str.GetType()));
            dt.Columns.Add(new DataColumn("xx2", str.GetType()));
            dt.Columns.Add(new DataColumn("xx3", str.GetType()));
            dt.Columns.Add(new DataColumn("xx4", str.GetType()));
            dt.Columns.Add(new DataColumn("yy1", str.GetType()));
            dt.Columns.Add(new DataColumn("yy2", str.GetType()));
            dt.Columns.Add(new DataColumn("yy3", str.GetType()));
            dt.Columns.Add(new DataColumn("yy4", str.GetType()));
            foreach (Model.Question qsM in qsList)
            {
                DataRow dr = dt.NewRow();
                dr["QSID"] = qsM.ID;
                dr["Title"] = qsM.Content;
                for (int x = 1; x <= 4; x++)
                {
                    dr["xx" + x] = "";
                    dr["yy" + x] = "";
                }
                for (int i = 1; i <= qsM.Answer.Length; i++)
                {
                    dr["xx" + i] = qsM.Answer[i - 1];
                }
                int[] qsAcc = ua.CountAnswers(qsM.ID);
                int amount = 0;
                foreach (int qa in qsAcc)
                {
                    amount += qa;
                }
                for (int i = 1; i <= qsM.Answer.Length; i++)
                {
                    string bfbstr = "0%";
                    if (amount != 0)
                    {
                        float bfb =
                            (float)(qsAcc[i - 1]*100 )/ amount;
                        bfbstr = bfb.ToString("0.00")+"%";
                    }
                    dr["yy" + i] = qsAcc[i - 1] + "    [" + bfbstr+"]";
                }
                dt.Rows.Add(dr);
            }
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind(); 
            return true;
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                DataTable dt = new DataTable();
                string str = "";
                int it = 0;
                dt.Columns.Add(new DataColumn("Answer", str.GetType()));
                dt.Columns.Add(new DataColumn("Count", it.GetType())); 
                GridDataItem item = (GridDataItem)e.Item;
                RadChart rc = item.FindControl("RadChart1") as RadChart;
                long qsid = long.Parse(item["QSID"].Text);
                Model.Question qsM = qn.GetQsModel(qsid); 
                int[] qsAcc = ua.CountAnswers(qsM.ID);
                int maxcount = 0; 
                for (int i = 0; i < qsM.Answer.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Answer"] = qsM.Answer[i];
                    dr["Count"] = qsAcc[i];
                    dt.Rows.Add(dr); 
                    maxcount += qsAcc[i]; 
                }
                rc.PlotArea.XAxis.Appearance.MajorGridLines.Visible = false;
                rc.PlotArea.Appearance.FillStyle.MainColor = Color.AliceBlue;
                rc.PlotArea.Appearance.FillStyle.SecondColor = Color.AliceBlue;
                rc.PlotArea.YAxis.MaxValue = maxcount;
                rc.ChartTitle.TextBlock.Text = "";
                rc.DataSource = dt;
                rc.DataBind();

            }

        }
         

    }
}