using System.Collections.Generic; 
using System.Web;
using SmartLaw.BLL;
using System.Web.SessionState;
using System.Web.Configuration;
using System.Data;
namespace SmartLaw.App_Code
{
    public class SessionEnduser
    {
        private static string LoginSessionName = "loginenduser";
        private string[] _status;
        private long _userid;
        private string _name;
        private string _simcard;
        private string _address;
        private string _theme;
        private string _cityCode ;
        /// <summary>
        /// 用户拥有的身份
        /// </summary>
        public string[] Status
        {
            get { return _status; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID
        {
            get { return _userid; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// 用户卡号或机顶盒号
        /// </summary>
        public string SimCard
        {
            get { return _simcard; }
        }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string Address
        {
            get { return _address; }
        }
        /// <summary>
        /// 用户默认首页样式
        /// </summary>
        public string Theme
        {
            get { return _theme; }
        }
        /// <summary>
        /// 用户默认城市
        /// </summary>
        public string CityCode
        {
            get { return _cityCode; }
        }

        public SessionEnduser()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 设置登录Session
        /// </summary>
        public void SetSession(string _SimCard,string _Theme,string _CityCode, HttpSessionState Session)
        {
            using (MongoDBService.MongoDBServiceSoapClient mongo = new MongoDBService.MongoDBServiceSoapClient())
            {
                try
                {
                    string msg;
                    MongoDBService.EndUser enduser = mongo.SelectEnduserBySimCardNo(_SimCard, out msg);
                    if (enduser == null)
                        return;
                    this._userid = long.Parse(enduser.AutoID);
                    this._simcard = _SimCard;
                    this._name = enduser.EnduserName;
                    this._address = "";
                    this._theme = _Theme;
                    this._cityCode = _CityCode;
                    this._status = enduser.Identities.ToArray();
                    Session[LoginSessionName] = this;
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie("stbid", _SimCard));
                }
                catch
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 获得当前Session
        /// </summary>
        /// <returns></returns>
        public static SessionEnduser GetSession()
        {
            if (HttpContext.Current.Session[LoginSessionName] == null)
            {
                HttpContext.Current.Session.Abandon();//会在进入登录页面前调用 HttpContext.Current.Session.Abandon(); 清除Session，注销用户登录信息。
                string loginUrl = WebConfigurationManager.AppSettings["LoadUserPageUrl"].Trim();
                HttpContext.Current.Response.Redirect(loginUrl + "?returnUrl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()), true);
            }
            return (SessionEnduser)HttpContext.Current.Session[LoginSessionName];
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
    }
}