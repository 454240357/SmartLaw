using System; 
using System.Web; 
using Telerik.Web.UI;
using System.Web.Configuration;
using SmartLaw.App_Code;

namespace SmartLaw.Admin
{
    public partial class MainMenu : System.Web.UI.UserControl
    {
        private SessionUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            if (!IsPostBack)
            {

                RadMenuItem radMenuItem1 = new RadMenuItem("功能(F)");
                radMenuItem1.AccessKey = "F";
                radMenuItem1.Items.Add(new RadMenuItem("注销(E)"));
                radMenuItem1.Items.Add(new RadMenuItem("关闭(X)"));
                radMenuItem1.PostBack = false;

                RadMenuItem radMenuItem4 = new RadMenuItem("系统代码管理(M)");
                radMenuItem4.AccessKey = "M";
                if (user.hasAuthority("Auth_Code_CRUD"))
                {
                    radMenuItem4.Items.Add(new RadMenuItem("查看系统代码(V)", "SystemManage/SysCodeList.aspx") { AccessKey = "V" });
                    radMenuItem4.Items.Add(new RadMenuItem("新建系统代码(N)", "SystemManage/AddSysCode.aspx") { AccessKey = "N" }); 
                    radMenuItem4.Items.Add(new RadMenuItem() { IsSeparator = true });
                }
                radMenuItem4.Items.Add(new RadMenuItem("查看操作员(V)", "SystemManage/SysUserList.aspx") { AccessKey = "V" });
                if (user.hasAuthority("Auth_SysUser_Add"))
                {
                    radMenuItem4.Items.Add(new RadMenuItem("新建操作员(N)", "SystemManage/AddSysUser.aspx") { AccessKey = "N" });
                }
                radMenuItem4.PostBack = false;

                RadMenuItem radMenuItem3 = new RadMenuItem("条目管理(P)");
                radMenuItem3.AccessKey = "P";
                bool newsTag = false;
               
                if (user.hasAuthority("Auth_News_Retrieve"))
                {
                    newsTag = true;
                    radMenuItem3.Items.Add(new RadMenuItem("检索条目(B)", "NewsManage/NewsList.aspx") { AccessKey = "B" });
                }
                if (user.hasAuthority("Auth_News_Add"))
                {
                    newsTag = true;
                    radMenuItem3.Items.Add(new RadMenuItem("添加条目(J)", "NewsManage/NewsEdit.aspx") { AccessKey = "J" });
                }

                if (user.hasAuthority("Auth_News_Examine"))
                {
                    newsTag = true;
                    radMenuItem3.Items.Add(new RadMenuItem("条目审核(E)", "NewsManage/NewsCheck.aspx") { AccessKey = "E" });

                }  
                RadMenuItem radMenuItem8 = new RadMenuItem("公告管理(P)");
                radMenuItem8.AccessKey = "N";
                radMenuItem8.Items.Add(new RadMenuItem("公告添加(K)", "NoticeManage/NoticeEdit.aspx") { AccessKey = "K" });
                radMenuItem8.Items.Add(new RadMenuItem("公告列表(H)", "NoticeManage/NoticeList.aspx") { AccessKey = "H" });

                RadMenuItem radMenuItem9 = new RadMenuItem("积分管理(I)");
                radMenuItem9.AccessKey = "I";
                radMenuItem9.Items.Add(new RadMenuItem("积分历史(K)", "IntegralManage/IntegralList.aspx") { AccessKey = "K" });
                radMenuItem9.Items.Add(new RadMenuItem("积分配置(C)", "IntegralManage/IntegralConfiguration.aspx") { AccessKey = "C" });
                radMenuItem9.Items.Add(new RadMenuItem("礼品查询(L)", "IntegralManage/GiftList.aspx") { AccessKey = "L" });
                radMenuItem9.Items.Add(new RadMenuItem("礼品定义(D)", "IntegralManage/GiftEdit.aspx") { AccessKey = "D" });
                radMenuItem9.Items.Add(new RadMenuItem("积分兑换记录(H)", "IntegralManage/IntegralExchangeRecords.aspx") { AccessKey = "H" });
                
                RadMenuItem radMenuItem10 = new RadMenuItem("问卷管理(Q)");
                radMenuItem10.AccessKey = "Q"; 
                radMenuItem10.Items.Add(new RadMenuItem("检索问卷(C)", "QuestionnaireManage/QuestionnaireList.aspx") { AccessKey = "C" });
                radMenuItem10.Items.Add(new RadMenuItem("添加问卷(A)", "QuestionnaireManage/QuestionnaireEdit.aspx") { AccessKey = "A" });
                
                RadMenuItem radMenuItem6 = new RadMenuItem("分类管理(C)");
                radMenuItem6.AccessKey = "C"; 
                radMenuItem6.Items.Add(new RadMenuItem("分类查询(S)", "CategoryManage/CategoryView.aspx") { AccessKey = "S" });

                RadMenuItem radMenuItem7 = new RadMenuItem("终端用户管理(C)");
                radMenuItem7.AccessKey = "U"; 
               
                bool userTag = false;

                if (user.hasAuthority("Auth_EndUser_Retrieve"))
                {
                    userTag = true;
                    radMenuItem7.Items.Add(new RadMenuItem("查询终端用户(R)", "EndUserManage/EndUserList.aspx") { AccessKey = "R" });
                }
                if (user.hasAuthority("Auth_News_Add"))
                {
                    userTag = true;
                    radMenuItem7.Items.Add(new RadMenuItem("添加终端用户(A)", "EndUserManage/AddEndUser.aspx") { AccessKey = "A" });

                } 
                
                RadMenuItem radMenuItem11 = new RadMenuItem("用户行为分析(C)");
                radMenuItem11.AccessKey = "C";
                radMenuItem11.Items.Add(new RadMenuItem("用户行为统计(A)", "UserBehaviorManage/UserBehaviorStatistics.aspx") { AccessKey = "A" });

                
                

                RadMenuItem radMenuItem5 = new RadMenuItem("日志管理(L)");
                radMenuItem5.AccessKey = "L";
                radMenuItem5.Items.Add(new RadMenuItem("日志查询(R)", "LogManage/LogList.aspx") { AccessKey = "R" });

                RadMenuItem skinMenu = new RadMenuItem("皮肤(T)");
                skinMenu.AccessKey = "T";
                skinMenu.Items.Add(new RadMenuItem("Windows7") { AccessKey = "W" });
                skinMenu.Items.Add(new RadMenuItem("Black") { AccessKey = "B" });
                skinMenu.Items.Add(new RadMenuItem("Metro") { AccessKey = "M" }); 
                skinMenu.PostBack = false;

                AddRadMenuItem(radMenuItem1);
               
                
                if (newsTag)
                {
                    AddRadMenuItem(radMenuItem3);
                }
                AddRadMenuItem(radMenuItem8);
                if (userTag)
                {
                    AddRadMenuItem(radMenuItem7);
                }
                AddRadMenuItem(radMenuItem10); 
                AddRadMenuItem(radMenuItem9);
                AddRadMenuItem(radMenuItem11);
                AddRadMenuItem(radMenuItem6);
                AddRadMenuItem(radMenuItem4); 
                if (user.hasAuthority("Auth_Log"))
                {
                    AddRadMenuItem(radMenuItem5);
                }

                AddRadMenuItem(skinMenu);
            }
        }

        private void AddRadMenuItem(RadMenuItem menuitem)
        {
            if (menuitem.Items == null || menuitem.Items.Count == 0)
                return;
            RadMenu1.Items.Add(menuitem);
        }

        protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
        {
            if (e.Item.Owner is RadMenuItem)
            {
                if (((RadMenuItem)(e.Item.Owner)).Text.Contains("皮肤"))
                {
                    string skinValue = e.Item == null ? "Windows7" : e.Item.Text;
                    SetSkin(skinValue);
                    Session["SkinId"] = skinValue;
                    Response.Write("<script>window.location=window.location;</script>");
                }

                if (((RadMenuItem)(e.Item.Owner)).Text.Contains("功能"))
                {
                    string value = e.Item.Text;
                    if (value.Contains("注销"))
                    {
                        Session.Abandon();
                        ClearPwdCookie();
                        string loginUrl = WebConfigurationManager.AppSettings["LoginPageUrl"].Trim();
                        Response.Redirect(loginUrl);
                    }
                    if (value.Contains("关闭"))
                    {
                        Session.Abandon();
                        Response.Write("<script>if (navigator.userAgent.indexOf(\"MSIE\") > 0) {  " +
                                            "if (navigator.userAgent.indexOf(\"MSIE 6.0\") > 0) {  window.opener = null; window.close(); }" +
                                            "else { window.open('', '_top'); window.top.close();}" +
                                            "}" +
                                            "else if (navigator.userAgent.indexOf(\"Firefox\") > 0) { window.location.href = 'about:blank ';}" +
                                            "else { window.opener = null; window.open('', '_self', '');window.close();}"+
                                            " </script>");
                    }
                }
            }
        }

        private void SetSkin(string value)
        {
            HttpCookie cookie = new HttpCookie("CustomSkin");
            cookie.Values["Skin"] = value;
            //设置过期时间为一个月
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
        }

        private void ClearPwdCookie()
        {
            HttpCookie cookie = Request.Cookies["Login"];
            if (cookie == null)
                return;
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
        }
    }
}