using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using Telerik.Web.UI;

namespace SmartLaw.Admin.IntegralManage
{
    public partial class IntegralExchangeRecords : BasePage
    {
        SmartLaw.BLL.Prize pr = new BLL.Prize();
        SmartLaw.BLL.UserPrize upr = new BLL.UserPrize();
        SmartLaw.BLL.EndUser eu = new BLL.EndUser();
        SmartLaw.BLL.Log log = new BLL.Log();
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                List<Model.Prize> prList = pr.GetModelList(-1, "", -1, 7, true);
                Model.Prize prM = new Model.Prize();
                prM.PrizeName = "不限";
                prM.AutoID = -1;
                prList.Insert(0, prM);
                RCB_Prize.DataTextField = "PrizeName";
                RCB_Prize.DataValueField = "AutoID"; 
                RCB_Prize.DataSource = prList;
                RCB_Prize.DataBind();
                RGrid_List.Visible = false;
            }
        }

        public string GetTime(object time)
        { 
            DateTime dt = DateTime.Parse(time.ToString());
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string GetName(object obj)
        {
            string sim = obj.ToString();
            List<Model.EndUser> euMList = eu.GetModelList(2, sim, -1, -1, false);
            if (euMList.Count > 0)
            {
                return euMList[0].EndUserName;
            }
            else
            {
                return "不存在";
            } 
        }

        public string GetPrize(object obj)
        {
            string pId = obj.ToString();
            Model.Prize prM = pr.GetModel(long.Parse(pId));
            if (prM != null)
            {
                return prM.PrizeName;
            }
            else
            {
                return "已失效";
            }
        }

        public string GetState(object obj)
        {
            bool isTaken = (bool)obj;
            if (isTaken)
            {
                return "已领取";
            }
            else
            {
                return "未领取";
            }

        }

        public bool GetTaken(object obj)
        {
            bool isTaken = (bool)obj;
            if (isTaken)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        protected void RGrid_List_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGrid_List.DataSource = (List<Model.UserPrize>)ViewState["uprList"];
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            string stype = RCB_SearchType.SelectedValue;
            List<Model.UserPrize> uprList = new List<Model.UserPrize>();
            string content = TB_Content.Text.Trim();
            if (!content.Equals(""))
            {
                if (stype.Equals("1"))
                {
                    List<Model.EndUser> euList = eu.GetModelList(1, content, -1, -1, false);
                    foreach (Model.EndUser euM in euList)
                    {
                        uprList = upr.GetModelList(6, euM.SimCardNo, -1, 4, true);
                    }
                }
                else if (stype.Equals("2"))
                { 
                    uprList = upr.GetModelList(6, content, -1, 4, true);
                }
            } 
            else
            {
                uprList = upr.GetModelList(-1, "", -1, 4, true);
            }
            if (RadDatePicker1.DbSelectedDate != null)
            {
                DateTime sTime = (DateTime)RadDatePicker1.DbSelectedDate; 
                uprList.RemoveAll(ut => ut.TakenTime.CompareTo(sTime) < 0);
            }
            if (RadDatePicker2.DbSelectedDate != null)
            {
                DateTime eTime = (DateTime)RadDatePicker2.DbSelectedDate;
                eTime = eTime.AddDays(1); 
                uprList.RemoveAll(ut => ut.TakenTime.CompareTo(eTime) > 0);
            }
            if (!RCB_Prize.SelectedValue.Equals("-1"))
            {
                uprList.RemoveAll(ut => ut.PrizeID != long.Parse(RCB_Prize.SelectedValue));
            }
            if (!RCB_State.SelectedValue.Equals("2"))
            {
                bool isTaken = RCB_State.SelectedValue.Equals("1");
                uprList.RemoveAll(ut => ut.IsTaken != isTaken);
            }
            ViewState["uprList"] = uprList;
            RGrid_List.DataSource = uprList;
            RGrid_List.DataBind();
            RGrid_List.Visible = true;
        }


        protected void RGrid_List_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Taken")
            {
                string AutoID = Convert.ToString(e.CommandArgument);
                Model.UserPrize upM = upr.GetModel(long.Parse(AutoID));
                Model.Log logModel = new Model.Log();
                logModel.OperationItem = "设置礼品[已领取]";
                logModel.OperationTime = DateTime.Now;
                logModel.Operator = _user.UserInfo.UserID;
                logModel.Memo = "";
                logModel.OperationDetail = "ID:" + AutoID + "-礼品:" + (pr.GetModel(upM.PrizeID) == null ? upM.PrizeID.ToString() : pr.GetModel(upM.PrizeID).PrizeName)+"兑换时间："+upM.TakenTime;
                if (upM == null)
                { 
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('抱歉！该记录不存在。');", true);
                    return;
                }
                if (upM.IsTaken == true)
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('抱歉！该用户已领取。');", true);
                    return;
                }
                bool isTaken = false;
                try
                {
                    string outmsg;
                    upM.TakenTime = DateTime.Now;
                    isTaken = upr.PrizeTaken(upM, out outmsg); 
                    logModel.Memo += outmsg;
                }
                catch (Exception ex)
                {
                    logModel.Memo += ex.Message;
                }
                finally
                {
                    log.Add(logModel);
                    if (isTaken)
                    {

                        List<Model.UserPrize> uprList = (List<Model.UserPrize>)ViewState["uprList"];
                        foreach (Model.UserPrize up in uprList)
                        {
                            if (up.AutoID == long.Parse(AutoID))
                            {
                                up.IsTaken = true;
                                up.TakenTime = upM.TakenTime;
                                break;
                            }
                        }
                        RGrid_List.DataSource = uprList;
                        RGrid_List.DataBind();
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('恭喜！设置成功');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(Page, GetType(), "cc1", "OpenAlert('抱歉！设置失败');", true);
                    }
                }
                
            }
        }

    }
}