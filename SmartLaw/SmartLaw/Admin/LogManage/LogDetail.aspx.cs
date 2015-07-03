using System; 
using SmartLaw.BLL;
using SmartLaw.App_Code;

namespace SmartLaw.Admin.LogManage
{
    public partial class LogDetail : BasePage
    {
        public string AutoId;
        readonly Log _log = new Log();
        private SessionUser _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            AutoId = Request.QueryString["AutoID"];
            _user = SessionUser.GetSession();
            _user.ValidateAuthority("Auth_Log");
            if (!IsPostBack)
                if (!ReadValue())
                    Panel1.Visible = false;
        }

        private bool ReadValue()
        {
            Model.Log logModel = _log.GetModel(long.Parse(AutoId));
            if (logModel == null)
            {
                return false;
            }
            Lb_OperationItem.Text = logModel.OperationItem;
            TB_OperationDetail.Text = logModel.OperationDetail;
            Lb_Operator.Text = logModel.Operator;
            Lb_OperationTime.Text = logModel.OperationTime.ToString("yyyy-MM-dd HH:mm:ss");
            TB_Memo.Text = logModel.Memo; 
            return true;
        }
    }
}