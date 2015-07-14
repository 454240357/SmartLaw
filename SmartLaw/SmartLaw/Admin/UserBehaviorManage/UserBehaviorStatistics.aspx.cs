using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using System.Data;
using System.Collections;
using Telerik.Web.UI;
using System.Text;
using Telerik.Charting;
using System.Drawing;

namespace SmartLaw.Admin.UserBehaviorManage
{
    public partial class UserBehaviorStatistics : BasePage
    {
        BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
        BLL.UserBehavior uh = new BLL.UserBehavior();
        BLL.EndUser eu = new BLL.EndUser();
        BLL.Category cg = new BLL.Category();
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                List<Model.SysCodeDetail> scdList = scd.GetModelList(0, "UserBehavior", -1, -1, false);
                SmartLaw.Model.SysCodeDetail scdM = new Model.SysCodeDetail();
                scdM.SYSCodeDetialID = "-1";
                scdM.Memo = "不限";
                scdList.Insert(0, scdM);
                RCB_Behaviour.DataTextField = "Memo";
                RCB_Behaviour.DataValueField = "SYSCodeDetialID";
                RCB_Behaviour.DataSource = scdList;
                RCB_Behaviour.DataBind(); 
                RGrid_List.Visible = false;
            }
        }

        public string GetPt(object obj)
        {
            float bfb = (float)obj;
            return bfb.ToString("0.00") + "%";
        }

        public string GetBhName(object obj)
        {
            string bhId =  obj.ToString();
            Model.SysCodeDetail scdM = scd.GetModel(bhId);
            if (scdM == null)
            {
                return bhId;
            }
            else
            {
                return scdM.Memo;
            }
        }

        protected void RGrid_List_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                string BhID = Convert.ToString(e.CommandArgument);
                string content = TB_Content.Text.Trim();
                string stype = RCB_SearchType.SelectedValue; 
                StringBuilder targetUrl = new StringBuilder("UserBehaviorDetail.aspx?BhID=" + BhID);
                if (!content.Equals(""))
                {
                    if (stype.Equals("1"))
                    {
                        targetUrl.Append("&uName=" + content);
                    }
                    else if(stype.Equals("2"))
                    {
                        targetUrl.Append("&simCardNo=" + content);
                    }
                    else if (stype.Equals("3"))
                    {
                        targetUrl.Append("&ipAddr=" + content);
                    }
                }
                if (RadDatePicker1.DbSelectedDate != null)
                {
                    DateTime stime = (DateTime)RadDatePicker1.DbSelectedDate;
                    targetUrl.Append("&sTime=" + stime.ToString("yyyy-MM-dd"));
                }
                if (RadDatePicker2.DbSelectedDate != null)
                {
                    DateTime etime = (DateTime)RadDatePicker2.DbSelectedDate;
                    targetUrl.Append("&sTime=" + etime.ToString("yyyy-MM-dd"));
                } 
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "openRadWindow('" + targetUrl + "','SubModalWindow')", true);
            }
        }
         

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            string stype = RCB_SearchType.SelectedValue; 
            string content = TB_Content.Text.Trim();
            DataTable dt = new DataTable();
            dt.Columns.Add("Behavior");
            DataColumn countDc= new DataColumn("Count");
            countDc.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(countDc);
            DataColumn percentageDc = new DataColumn("Percentage");
            percentageDc.DataType = System.Type.GetType("System.Single"); 
            dt.Columns.Add(percentageDc);
            List<string> simList = new List<string>();
            string ip = null;
            if (!content.Equals(""))
            {
                if (stype.Equals("1"))
                {
                    List<Model.EndUser> euList = eu.GetModelList(1, content, -1, -1, false);
                    if (euList.Count < 0)
                    {
                        ViewState["dt"] = dt;
                        RGrid_List.DataSource = dt;
                        RGrid_List.DataBind();
                        RGrid_List.Visible = true;
                        return;
                    }
                    else
                    {
                        foreach (Model.EndUser euM in euList)
                        {
                            simList.Add(euM.SimCardNo);
                        }
                    }
                }
                else if (stype.Equals("2"))
                {
                    simList.Add(content);
                }
                else
                {
                    ip = content;
                }
            }
            DateTime stime = RadDatePicker1.DbSelectedDate != null ? (DateTime)RadDatePicker1.DbSelectedDate : DateTime.MinValue;
            DateTime etime = RadDatePicker2.DbSelectedDate != null ? ((DateTime)RadDatePicker2.DbSelectedDate).AddDays(1) : DateTime.MaxValue;
            string behavior = RCB_Behaviour.SelectedValue.Equals("-1") ? "" : RCB_Behaviour.SelectedValue;
            if (simList.Count > 0)
            {
                Hashtable ht = new Hashtable();
                int totalC = 0;
                foreach (string sim in simList)
                {
                    DataSet ds = uh.CountUserBh(sim, ip, null, behavior, stime, etime);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (ht.ContainsKey(dr[0].ToString()))
                        {
                            ht[dr[0].ToString()] = int.Parse(ht[dr[0].ToString()].ToString()) + int.Parse(dr[1].ToString());
                        }
                        else
                        {
                            ht.Add(dr[0].ToString(), int.Parse(dr[1].ToString()));
                        }
                        totalC += int.Parse(dr[1].ToString());
                    }
                }
                foreach (DictionaryEntry de in ht)
                { 
                    float bfb = (float)(int.Parse(de.Value.ToString())* 100) / totalC; 
                    dt.Rows.Add(new Object[] { de.Key.ToString(), de.Value, bfb });
                }
                ViewState["dt"] = dt;
                RGrid_List.DataSource = dt;
                RGrid_List.DataBind();
                RGrid_List.Visible = true; 
            }
            else
            {
                DataSet ds = uh.CountUserBh(null, ip, null, behavior, stime, etime);
                int totalC = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    totalC += int.Parse(dr[1].ToString());
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    float bfb = (float)(int.Parse(dr[1].ToString()) * 100) / totalC;
                    dt.Rows.Add(new Object[] { dr[0].ToString(), dr[1].ToString(), bfb });
                }
                ViewState["dt"] = dt;
                RGrid_List.DataSource = dt;
                RGrid_List.DataBind();
                RGrid_List.Visible = true;
                Color[] seriesColors = new[]
                               {
                                   Color.Red,
                                   Color.Yellow,
                                   Color.Black,
                                   Color.Blue,
                                   Color.White,
                                   Color.Green,
                                   Color.MediumPurple,
                                   Color.Brown,
                                   Color.Orange,
                                   Color.Gray,
                                   Color.DarkRed,
                                   Color.DarkSalmon,
                                   Color.DarkSeaGreen,
                                   Color.DarkSlateBlue,
                                   Color.DarkSlateGray,
                                   Color.DarkTurquoise,
                                   Color.DarkViolet,
                                   Color.DeepPink,
                                   Color.Crimson,
                                   Color.DimGray,
                                   Color.DodgerBlue,
                                   Color.Firebrick,
                                   Color.FloralWhite,
                                   Color.Chocolate,
                                   Color.Coral,
                                   Color.Cornsilk
                               };
                Palette seriesPalette = new Palette("seriesPalette", seriesColors, true);
                RadChart2.CustomPalettes.Add(seriesPalette);
                RadChart2.SeriesPalette = "seriesPalette";
                RadChart2.ChartTitle.TextBlock.Text = "用户行为分析统计结果";
                RadChart2.DataSource = dt;
                RadChart2.DataBind();
                RadChart2.Visible = true;
                RadChart2.PlotArea.Appearance.FillStyle.MainColor = Color.AliceBlue;
                RadChart2.PlotArea.Appearance.FillStyle.SecondColor = Color.AliceBlue;
            }
        }

        protected void RadChart2_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
        {
            string bhId = ((DataRowView)e.DataItem)["Behavior"].ToString();
            Model.SysCodeDetail scdM = scd.GetModel(bhId);
            e.SeriesItem.Name = scdM == null ? bhId : scdM.Memo;
            e.SeriesItem.ActiveRegion.Tooltip = scdM == null ? bhId : scdM.Memo; 
        }

        protected void RGrid_List_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGrid_List.DataSource = (DataTable)ViewState["dt"];
        }


    }
}