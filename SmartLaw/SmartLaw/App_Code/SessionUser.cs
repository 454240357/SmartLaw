using System.Collections.Generic; 
using System.Web;
using SmartLaw.BLL;
using System.Web.SessionState;
using System.Web.Configuration;
using System.Data;
namespace SmartLaw.App_Code
{
    public class SessionUser
    {
        private Model.SysUser _UserInfo = new Model.SysUser();
        private KeyValuePair<string, string> _Role;
        private Dictionary<string, string> _Authority;
        private static string LoginSessionName = "loginuser";
        SysCodeDetail scd = new SysCodeDetail();
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public Model.SysUser UserInfo
        {
            get { return _UserInfo; }
        }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        public KeyValuePair<string, string> Role
        {
            get { return _Role; }
        }

        /// <summary>
        /// 用户拥有的权限
        /// </summary>
        public Dictionary<string, string> Authority
        {
            get { return _Authority; }
        }

        public SessionUser()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            _UserInfo = null;
        }

        /// <summary>
        /// 设置登录Session
        /// </summary>
        /// <param name="LoginID">登录ID</param>
        /// <param name="Password">登录密码</param>
        /// <param name="Session">Session</param>
        public void SetSession(string LoginID, string Password, HttpSessionState Session)//支付卡方式设置Session
        {
            SysUser su = new SysUser();
            Model.SysUser user = su.GetModel(LoginID);
            if (user == null || !user.IsValid)
                return;
            if (user.Password.Equals(Password))
            {   //Role
                _UserInfo = user; 
                DataSet roleDs = scd.GetListBySysCode(UserInfo.UserID, "Role");
                List<Model.SysCodeDetail> roleList = scd.DataTableToList(roleDs.Tables[0]);
                _Role = new KeyValuePair<string, string>(roleList[0].SYSCodeDetialID, roleList[0].SYSCodeDetialContext);
                DataSet authDs = scd.GetListBySysCode(_Role.Key, "Auth");
                List<Model.SysCodeDetail> authList = scd.DataTableToList(authDs.Tables[0]);
                _Authority = new Dictionary<string, string>();
                foreach (Model.SysCodeDetail scdM in authList)
                {
                    _Authority.Add(scdM.SYSCodeDetialID, scdM.SYSCodeDetialContext);
                }
                Session[LoginSessionName] = this;
            }
            else//得到用户信息失败
            {
                _UserInfo = null;
                _Role = new KeyValuePair<string, string>(null, null);
                Session[LoginSessionName] = null;
            }
        }

        /// <summary>
        /// 获得当前Session
        /// </summary>
        /// <returns></returns>
        public static SessionUser GetSession()
        {
            if (HttpContext.Current.Session[LoginSessionName] == null)
            {
                HttpContext.Current.Session.Abandon();//会在进入登录页面前调用 HttpContext.Current.Session.Abandon(); 清除Session，注销用户登录信息。
                string loginUrl = WebConfigurationManager.AppSettings["LoginPageUrl"].Trim();
                HttpContext.Current.Response.Redirect(loginUrl + "?returnUrl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()), true);
            }
            return (SessionUser)HttpContext.Current.Session[LoginSessionName];
        }


        /// <summary>
        /// 用户是否已经登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session[LoginSessionName] == null)
                return false;
            return true;
        }

        /// <summary>
        /// 是否有操作权限
        /// </summary>
        /// <param name="OperateItemId">操作项</param>
        /// <returns></returns>
        public bool hasAuthority(string OperateItemId)
        {
            if (_Authority.ContainsKey(OperateItemId))
                return true;
            return false;
        }


        /// <summary>
        /// 验证权限，没有权限时返回登录页面
        /// </summary>
        /// <param name="OperateItemId"></param>
        public void ValidateAuthority(string OperateItemId)
        {
            if (!hasAuthority(OperateItemId))
            {
                string loginUrl = WebConfigurationManager.AppSettings["LoginPageUrl"].Trim();
                HttpContext.Current.Response.Redirect(loginUrl, true);
            }
        }
         
    }
}