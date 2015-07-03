using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartLaw.DBUtility;
using System.Data;

namespace SmartLaw
{

    public partial class test : System.Web.UI.Page
    {
        public string testSql;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "select * from sysuser limit 1";
            DataSet ds = DbHelperMySQL.Query(sql);
            DataRow dr = ds.Tables[0].Rows[0];
            testSql = dr["UserID"].ToString() + dr["Password"].ToString() + dr["EmployeeID"].ToString() + dr["UserName"].ToString();
        }
    }
}