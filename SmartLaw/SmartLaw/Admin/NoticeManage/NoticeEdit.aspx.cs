using System;
using System.Collections.Generic; 
using SmartLaw.BLL;
using System.Data;
using Telerik.Web.UI;
using SmartLaw.App_Code; 
using SmartLaw.Admin.NewsManage;
using System.Text.RegularExpressions; 

namespace SmartLaw.Admin.NoticeManage
{
    public partial class NoticeEdit : BasePage
    {
        Message msg = new Message();
        MessageToObject mto = new MessageToObject();
        SysCodeDetail scd = new SysCodeDetail(); 
        Log log = new Log(); 
        private SessionUser user; 
        protected void Page_Load(object sender, EventArgs e)
        { 
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Notice_Add");
            if (!IsPostBack)
            {   
                RadDatePicker1.DbSelectedDate = DateTime.Today;
                List<Model.SysCodeDetail> msgTypeList = scd.GetModelList(0, "MessageType", -1, 1, false);
                msgTypeList.RemoveAll(rt => rt.IsValid == false);
                RCB_Type.DataTextField = "SYSCodeDetialContext";
                RCB_Type.DataValueField = "SYSCodeDetialID";
                RCB_Type.DataSource = msgTypeList;
                RCB_Type.DataBind();
                RCB_Type.SelectedValue = "Popup";
                List<Model.SysCodeDetail> popmsgDisTypeList = scd.GetModelList(0, "PopmsgCloseType", -1, 1, false);
                popmsgDisTypeList.RemoveAll(rt => rt.IsValid == false);
                RCB_DisType.DataTextField = "Memo";
                RCB_DisType.DataValueField = "SYSCodeDetialID";
                RCB_DisType.DataSource = popmsgDisTypeList;
                RCB_DisType.DataBind();
                Div1.Visible = true;
                List<Model.SysCodeDetail> identityList = scd.GetModelList(0, "Identity", -1, 1, false);
                identityList.RemoveAll(rt => rt.IsValid == false); 
                RadListBox1.DataTextField = "SYSCodeDetialContext";
                RadListBox1.DataValueField = "SYSCodeDetialID";
                RadListBox1.DataSource = identityList;
                RadListBox1.DataBind();
                BindRegionTree();
            } 
        } 

        protected void BindRegionTree()
        {
            //初始化区域选择下拉树
            List<Model.SysCodeDetail> rgList = scd.GetModelList(0, "Region", -1, 1, false);
            rgList.RemoveAll(rt => rt.IsValid == false); 
            rgList.Find(rt => rt.SYSCodeDetialID.Equals("Area_Jfs")).Memo = null;
            RadDropDownTree1.DataFieldID = "SYSCodeDetialID";
            RadDropDownTree1.DataFieldParentID = "Memo";
            RadDropDownTree1.DataValueField = "SYSCodeDetialID";
            RadDropDownTree1.DataTextField = "SYSCodeDetialContext";
            RadDropDownTree1.DataSource = rgList;
            RadDropDownTree1.DataBind();
            RadTreeView treeView = RadDropDownTree1.Controls[0] as RadTreeView;
            treeView.Nodes[0].Expanded = true;
            treeView.ShowLineImages = false;
        }

        protected void RbtnSubmit_Click(object sender, EventArgs e)
        {
            string msgType = RCB_Type.SelectedValue;
            string closeBy = "";
            if (msgType.Equals("Popup"))
            {
                closeBy = RCB_DisType.SelectedValue;
            }
            else if (msgType.Equals("Roll"))
            {
                if (!RadButton2.Checked && RTB_RollTimes.Text.Trim().Equals(""))
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('滚动次数不能为空！');", true);
                    return;
                }
                closeBy = RadButton2.Checked ? "-1" : RTB_RollTimes.Text; 
            }
            if (RadDatePicker1.DbSelectedDate == null)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('抱歉，有效期限不能为空！');", true);
                return;
            }
            DateTime availableTime = (DateTime)RadDatePicker1.DbSelectedDate;
            DateTime expiredTime = RadDatePicker2.DbSelectedDate==null? new DateTime(2050,12,31) : (DateTime)RadDatePicker2.DbSelectedDate;  
            string content = TabStrip1.SelectedTab.Value.Equals("2") ? (RadButton1.Checked ? CheckStr(ContentEditor.Content) : ContentEditor.Content) : ContentText.Text;
            if (content.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，公告内容不能为空！');", true);
                return;
            }
            RadTreeView regionTreeView = RadDropDownTree1.Controls[0] as RadTreeView;
            if (!RadButton5.Checked && RadListBox1.CheckedItems.Count == 0 && regionTreeView.SelectedNodes.Count == 0 && RTB_SIM.Text.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，请设置对象发送条件！！');", true);
                return;
            }
            if (!RadButton5.Checked && !RadButton3.Checked && !RadButton4.Checked)
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，请选择对象条件关系！！');", true);
                return;
            } 
            Model.Message msgModel = new Model.Message(); 
            msgModel.Contents = content; 
            msgModel.MessageType = msgType;
            msgModel.DataType = TabStrip1.SelectedTab.Value.Equals("2") ? "DataType_Html" : "DataType_Text";
            msgModel.AndOr = RadButton3.Checked;
            msgModel.AvailableTime = availableTime;
            msgModel.DisappearType = closeBy;
            msgModel.ExpiredTime = expiredTime;
            msgModel.IsValid = true;
            msgModel.LastModifyTime = DateTime.Now;
            msgModel.Memo = "";
            msgModel.Orders = 0;
            msgModel.Publisher = user.UserInfo.UserID; 
            long msgId = 0;
            bool isAdd = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "添加公告";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now; 
                logModel.OperationDetail =  "类型:"+msgType ; 
                msgId =msg.Add(msgModel);
                if (msgId > 0)
                { 
                    if (RadButton5.Checked)
                    {
                        Model.MessageToObject mtoModel = new Model.MessageToObject();
                        mtoModel.MsgID = msgId;
                        mtoModel.ObjType = "2";
                        mtoModel.ObjValue = ""; 
                        isAdd = mto.Add(mtoModel);
                    }
                    else
                    {
                        //身份
                        isAdd = true;
                        foreach (RadListBoxItem rbi in RadListBox1.CheckedItems)
                        {
                            isAdd = false;
                            Model.MessageToObject mtoModel = new Model.MessageToObject();
                            mtoModel.MsgID = msgId;
                            mtoModel.ObjType = "0";
                            mtoModel.ObjValue = rbi.Value;
                            isAdd = mto.Add(mtoModel);
                            if (!isAdd)
                            {
                                break;
                            }
                        }
                        //区域
                        if (isAdd)
                        {
                            isAdd = true;
                            if (!regionTreeView.SelectedValue.Equals("Area_Jfs"))
                            {
                                isAdd = false;
                                Model.MessageToObject mtoModel = new Model.MessageToObject();
                                mtoModel.MsgID = msgId;
                                mtoModel.ObjType = "0";
                                mtoModel.ObjValue = regionTreeView.SelectedValue;
                                isAdd = mto.Add(mtoModel);
                            }
                        }
                        //用户
                        if (isAdd)
                        {
                            isAdd = true;
                            if (RTB_SIM.Text.Trim() != "")
                            {
                                string[] SimNoArr = RTB_SIM.Text.Split(',');
                                foreach (string simNo in SimNoArr)
                                {
                                    if (!simNo.Trim().Equals(""))
                                    {
                                        isAdd = false;
                                        Model.MessageToObject mtoModel = new Model.MessageToObject();
                                        mtoModel.MsgID = msgId;
                                        mtoModel.ObjType = "1";
                                        mtoModel.ObjValue = simNo.Trim();
                                        isAdd = mto.Add(mtoModel);
                                        if (!isAdd)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        } 

                    }
                }
                if (msgId != 0 && isAdd)
                {
                    logModel.Memo = "成功";
                }
                else
                {
                    logModel.Memo = "失败！";
                } 
            }
            catch (Exception ex)
            {
                logModel.Memo = "异常：" + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (msgId != 0 && isAdd)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('恭喜！编辑成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！编辑失败！');", true);
                } 
            }
        } 

        protected void RadButton5_Click(object sender, EventArgs e)
        {
            if (RadButton5.Checked)
            {
                RadTabStrip1.Enabled = false;
                RadListBox1.Enabled = false;
                RadDropDownTree1.Enabled = false;
                RTB_SIM.Enabled = false;
                RadButton3.Enabled = false;
                RadButton4.Enabled = false;
            }
            else
            {
                RadTabStrip1.Enabled = true;
                RadListBox1.Enabled = true;
                RadDropDownTree1.Enabled = true;
                RTB_SIM.Enabled = true;
                RadButton3.Enabled = true;
                RadButton4.Enabled = true;
            }
        }

        protected void RCB_Type_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RCB_Type.SelectedValue.Equals("Roll"))
            {
                Div1.Visible = false;
                // Div2.Visible = true; 
                ContentEditor.EditModes = EditModes.Preview;
                RadButton1.Enabled = false;
                SaveEditBtn.Enabled = false;
                msgLb.Visible =  true;

            }
            else if (RCB_Type.SelectedValue.Equals("Popup"))
            {
                Div2.Visible = false;
                Div1.Visible = true;
                ContentEditor.EditModes = EditModes.All;
                ContentEditor.EditModes = ContentEditor.EditModes ^ EditModes.Preview;
                RadButton1.Enabled = true;
                SaveEditBtn.Enabled = true;
                msgLb.Visible = false;
            }
        }

        protected void RadButton2_Click(object sender, EventArgs e)
        {
            if (RadButton2.Checked)
            {
                RTB_RollTimes.Enabled = false;
            }
            else
            {
                RTB_RollTimes.Enabled = true;
            }
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            if (RadButton1.Checked)
            {

                ViewState["tempContent"] = ContentEditor.Content;
                string tempStr = CheckStr(ContentEditor.Content);
                ContentEditor.Content = tempStr;
                ContentEditor.EditModes = EditModes.Preview;
                SaveEditBtn.Visible = true;
            }
            else
            {
                ContentEditor.Content = ViewState["tempContent"] == null ? "" : ViewState["tempContent"].ToString();
                ContentEditor.EditModes = EditModes.All;
                ContentEditor.EditModes = ContentEditor.EditModes ^ EditModes.Preview;
                RadButton1.Checked = false;
                SaveEditBtn.Visible = false;
            }
        }

        public static string CheckStr(string html)
        {
            try
            {
                //过滤style
                // Regex reg = new Regex(@"\sstyle=""(?<style>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //过滤class
                Regex reg2 = new Regex(@"\sclass=""(?<class>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //过滤color
                Regex reg3 = new Regex(@"\scolor=""(?<color>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //过滤width
                Regex reg4 = new Regex(@"\swidth=""(?<width>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex reg4x = new Regex(@"width=\d*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //过滤height
                Regex reg5 = new Regex(@"\sheight=""(?<height>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex reg5x = new Regex(@"height=\d*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex regfont = new Regex(@"<\/?(?:font)[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex rega = new Regex(@"<a.*?>|</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //html = reg.Replace(html, "");
                html = reg2.Replace(html, "");
                html = reg3.Replace(html, "");
                html = reg4.Replace(html, "");
                html = reg5.Replace(html, "");
                html = reg4x.Replace(html, "");
                html = reg5x.Replace(html, "");
                html = regfont.Replace(html, "");
                html = rega.Replace(html, "");
                //Regex reg0 = new Regex(@"\sstyle=(""')(?<style>([^""';]+;?)+)""", RegexOptions.IgnoreCase);
                Regex reg0 = new Regex(@"style\s*=("")[^""]*("")", RegexOptions.IgnoreCase);  //双引号的style
                Regex reg1 = new Regex(@"style\s*=(')[^']*(')", RegexOptions.IgnoreCase);     //单引号的 style
                MatchCollection matches = reg0.Matches(html);
                if (matches.Count > 0)
                {
                    foreach (Match mc in matches)
                    {
                        if (mc.Value.ToLower().Contains("text-align"))
                        {
                            Regex rgx = new Regex(@"[^""]*(text-align:[^;]*)[^""]*", RegexOptions.IgnoreCase);
                            string str = rgx.Replace(mc.Value, "$1");
                            html = html.Replace(mc.Value, str);
                        }
                        else
                        {
                            html = html.Replace(mc.Value, "");
                        }
                    }
                }
                MatchCollection matches2 = reg1.Matches(html);
                if (matches2.Count > 0)
                {
                    foreach (Match mc in matches2)
                    {
                        if (mc.Value.ToLower().Contains("text-align"))
                        {
                            Regex rgx = new Regex(@"[^']*(text-align:[^;]*)[^']*", RegexOptions.IgnoreCase);
                            string str = rgx.Replace(mc.Value, "$1");
                            html = html.Replace(mc.Value, str);
                        }
                        else
                        {
                            html = html.Replace(mc.Value, "");
                        }
                    }
                }

            }
            catch
            { }
            return html;
        }

        protected void SaveEditBtn_Click(object sender, EventArgs e)
        {
            ContentEditor.EditModes = EditModes.All;
            ContentEditor.EditModes = ContentEditor.EditModes ^ EditModes.Preview;
            SaveEditBtn.Visible = false;
            RadButton1.Checked = false;
        }
         
         
    }


}