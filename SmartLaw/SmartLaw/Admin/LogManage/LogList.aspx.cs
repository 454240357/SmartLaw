using System; 
using SmartLaw.App_Code;
using SmartLaw.BLL;
using System.Data;
using Telerik.Web.UI;

namespace SmartLaw.Admin.LogManage
{
    public partial class LogList : BasePage
    {
        readonly Log _log = new Log();
        readonly SysUser _su = new SysUser();
        private SessionUser _user;
        static string _selectedV = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = SessionUser.GetSession();
            _user.ValidateAuthority("Auth_Log");
           
            if (!IsPostBack)
            {
                DataSet ds = _su.GetList(4, "1");
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = "不限";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "不限";
                dr[4] = 1;
                ds.Tables[0].Rows.InsertAt(dr, 0);
                RadComboBox1.DataTextField = "UserName";
                RadComboBox1.DataValueField = "UserId";
                RadComboBox1.DataSource = ds.Tables[0];
                RadComboBox1.DataBind(); 
                RadDatePicker1.DbSelectedDate = DateTime.Today;
                RadComboBox1.SelectedValue = _user.UserInfo.UserID;
                _selectedV = _user.UserInfo.UserID;
                RGrid_LogList.Visible = false;
            }
        }

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            if (RadDatePicker1.DbSelectedDate == null)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('日期不能为空！');", true);
                return;
            }
            DateTime selectTime = (DateTime)RadDatePicker1.DbSelectedDate; 
            string operatorStr = RadComboBox1.SelectedValue;
            DataSet ds = new DataSet();
            ds = _log.GetList(2, selectTime.ToString("yyyy-MM-dd")); 
            DataTable dt = ds.Tables[0]; 
            dt.AcceptChanges();
            if (operatorStr != "不限")
            {
                foreach (DataRow dr in dt.Rows)
                { 
                    if (!operatorStr.Contains(dr["Operator"].ToString()))
                    {
                        dr.Delete();
                    } 
                }
            }
            RGrid_LogList.DataSource = dt;
            ViewState["RGrid_LogList"] = dt;
            RGrid_LogList.DataBind();
            RGrid_LogList.Visible = true;
            RadComboBox1.SelectedValue = operatorStr;
        }

        public string GetShortMemo(Object obj)
        {
            string longMemo = obj.ToString();
            if (longMemo.Length > 50)
            { 
                return longMemo.Substring(0, 50) + "...";
            }
            return longMemo;
        }

        protected void RGrid_LogList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_LogList.DataSource = (DataTable)ViewState["RGrid_LogList"];
        }

        protected void RGrid_LogList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            RadComboBox1.SelectedValue = _selectedV;
        }

        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        { 
            _selectedV = e.Value; 
        }
    }
}