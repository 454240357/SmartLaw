using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.App_Code;
using System.Data;

namespace SmartLaw.Admin.UserBehaviorManage
{
    public partial class UserBehaviorDetail : BasePage
    {
        BLL.EndUser eu = new BLL.EndUser();
        BLL.UserBehavior ub = new BLL.UserBehavior();
        BLL.SysCodeDetail scd = new BLL.SysCodeDetail();
        public string BhID;
        public string uName;
        public string simCardNo;
        public string ipAddr;
        public string sTime;
        public string eTime;
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            BhID = Request.QueryString["BhID"];
            uName = Request.QueryString["uName"];
            simCardNo = Request.QueryString["simCardNo"]; 
            ipAddr = Request.QueryString["ipAddr"]; 
            sTime = Request.QueryString["sTime"];
            eTime = Request.QueryString["eTime"];
            _user = SessionUser.GetSession();
            if (!IsPostBack)
            {
                if (!ReadValue())
                    Panel1.Visible = false;
            }
        }

        public string GetName(object kind)
        {
            List<SmartLaw.Model.EndUser> euModelList = eu.GetModelList(2, kind.ToString(), -1, -1, false);
            if (euModelList.Count > 0)
            {
                return euModelList[0].EndUserName;
            }
            return "";
        }


        private bool ReadValue()
        {  
            DateTime stime = DateTime.MinValue;
            DateTime etime = DateTime.MaxValue;
            if (sTime != null)
            {
                stime = DateTime.Parse(sTime);
            }
            if (eTime != null)
            {
                etime = DateTime.Parse(eTime);
            }
            List<Model.UserBehavior> ubList = ub.GetModelList(simCardNo, ipAddr, null, BhID, stime, etime);
            if (uName != null)
            {
                List<Model.EndUser> euList = eu.GetModelList(1, uName, -1, -1, false);
                ubList.RemoveAll(rt => !euList.Exists(et => et.SimCardNo.Equals(rt.SimCardNO)));
            }
            if (BhID.Equals("WatchPartyAudio"))
            {
                ubList.ForEach(ut => ut.Behavior = scd.GetModel(ut.Behavior).Memo + " | [时长：" + ut.Remarks + "]");
            }
            else
            {
                ubList.ForEach(ut => ut.Behavior = scd.GetModel(ut.Behavior).Memo);
            }
            ViewState["ubList"] = ubList;
            RGrid_UserBehaviorList.DataSource = ubList;
            RGrid_UserBehaviorList.DataBind(); 
            return true;
        } 

         
        protected void RGrid_IntegralList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGrid_UserBehaviorList.DataSource = (List<Model.UserBehavior>)ViewState["ubList"];
        }


    }
}