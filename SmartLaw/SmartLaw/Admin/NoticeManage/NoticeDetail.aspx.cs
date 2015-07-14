using System;
using System.Collections.Generic; 
using SmartLaw.App_Code;
using SmartLaw.BLL;
using System.Data;
using System.Text;
using Telerik.Web.UI; 
using SmartLaw.Admin.NewsManage;
using System.Text.RegularExpressions;

namespace SmartLaw.Admin.NoticeManage
{
    public partial class NoticeDetail : BasePage
    {
        public long autoID;
        private SessionUser user;
        Message msg = new Message();
        SysCodeDetail scd = new SysCodeDetail();
        MessageToObject mto = new MessageToObject();
        StringBuilder rgSb = new StringBuilder();
        Log log = new Log();
        List<string> sbIlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            autoID = long.Parse(Request.QueryString["AutoID"]);
            if (!IsPostBack)
            { 
                if (!ReadValue())
                {
                    Panel1.Visible = false;
                } 
            }
        }

        void RadDropDownTree1_NodeDataBound(object sender, Telerik.Web.UI.DropDownTreeNodeDataBoundEventArguments e)
        {
            if (sbIlist!=null && sbIlist.Contains(e.DropDownTreeNode.Value ))
            {
                e.DropDownTreeNode.CreateEntry();
            }
        }

        private bool ReadValue()
        {
            Model.Message msgModel = msg.GetModel(autoID);
            if (msgModel == null)
            {
                return false;
            }
            if (!user.hasAuthority("Auth_Notice_Edit") || !msgModel.Publisher.Equals(user.UserInfo.UserID))
            {
                btn_ReEdit.Enabled = false;
            }
            if (!canDo("Auth_Notice_Edit"))
            { 
                btn_ChangeValid.Enabled = false;
            } 
            //公告类型绑定
            List<Model.SysCodeDetail> msgTypeList = scd.GetModelList(0, "MessageType", -1, 1, false);
            msgTypeList.RemoveAll(rt => rt.IsValid == false);
            RCB_Type.DataTextField = "SYSCodeDetialContext";
            RCB_Type.DataValueField = "SYSCodeDetialID";
            RCB_Type.DataSource = msgTypeList;
            RCB_Type.DataBind();
            RCB_Type.SelectedValue = msgModel.MessageType;
            //弹走类型绑定
            List<Model.SysCodeDetail> popmsgDisTypeList = scd.GetModelList(0, "PopmsgCloseType", -1, 1, false);
            popmsgDisTypeList.RemoveAll(rt => rt.IsValid == false);
            RCB_DisType.DataTextField = "Memo";
            RCB_DisType.DataValueField = "SYSCodeDetialID";
            RCB_DisType.DataSource = popmsgDisTypeList;
            RCB_DisType.DataBind();
            if (msgModel.MessageType.Equals("Popup"))
            {
                Div1.Visible = true;
                RCB_DisType.SelectedValue = msgModel.DisappearType;
            }
            else if (msgModel.MessageType.Equals("Roll"))
            {
                Div2.Visible = true;
                if (msgModel.DisappearType.Equals("-1"))
                {
                    RadButton2.Checked = true;
                    RTB_RollTimes.Enabled = false;
                }
                else
                {
                    RTB_RollTimes.Text = msgModel.DisappearType;
                }
            }
            RadDatePicker1.DbSelectedDate = msgModel.AvailableTime;
            if (msgModel.ExpiredTime.CompareTo(new DateTime(2050, 12, 31)) != 0)
            {
                RadDatePicker2.DbSelectedDate = msgModel.ExpiredTime; 
            }
            //身份列表绑定
            List<Model.SysCodeDetail> identityList = scd.GetModelList(0, "Identity", -1, 1, false);
            identityList.RemoveAll(rt => rt.IsValid == false);
            RadListBox1.DataTextField = "SYSCodeDetialContext";
            RadListBox1.DataValueField = "SYSCodeDetialID";
            RadListBox1.DataSource = identityList;
            RadListBox1.DataBind();  
            List<Model.MessageToObject> mtoList = mto.GetModelList(1, msgModel.AutoID.ToString(), -1, -1, false); 
            if (mtoList.Count == 1 && mtoList[0].ObjType.Equals("2"))
            {
                RadButton5.Checked = true;
                RadTabStrip1.Enabled = false;
                RadListBox1.Enabled = false;
                RadDropDownTree1.Enabled = false;
                RTB_SIM.Enabled = false;
                RadButton3.Enabled = false;
                RadButton4.Enabled = false;
            }
            else
            {
                sbIlist = new List<string>();
                StringBuilder simSb = new StringBuilder();
                foreach (Model.MessageToObject mtoM in mtoList)
                {
                    if (mtoM.ObjType.Equals("0"))
                    {
                        sbIlist.Add(mtoM.ObjValue);
                    } 
                    else if (mtoM.ObjType.Equals("1"))
                    {
                        simSb.Append(mtoM.ObjValue);
                    }

                }
                if (sbIlist.Count>0)
                { 
                    foreach (RadListBoxItem rbi in RadListBox1.Items)
                    {
                        if (sbIlist.Contains(rbi.Value))
                        {
                            rbi.Checked = true;
                        }
                    }
                }
                if (simSb.ToString().Length > 0)
                {
                    RTB_SIM.Text = simSb.ToString().Substring(0, simSb.ToString().Length - 1);
                }
            }
            RadDropDownTree1.NodeDataBound += RadDropDownTree1_NodeDataBound;
            BindRegionTree();
            if (msgModel.AndOr)
            {
                RadButton3.Checked = true;
            }
            else
            {
                RadButton4.Checked = true; 
            }
            TB_Publisher.Text = msgModel.Publisher;
            RCB_Order.SelectedValue = msgModel.Orders.ToString();
            RCB_Enable.SelectedValue = msgModel.IsValid ? "1" : "0"; 
            ContentEditor.Content = msgModel.Contents;
            if (msgModel.DataType.Equals("DataType_Text"))
            {
                ContentText.Text = msgModel.Contents;
            }
            SetEnable(false);

            return true;
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

        protected void RCB_Type_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RCB_Type.SelectedValue.Equals("Roll"))
            {
                Div1.Visible = false;
                Div2.Visible = true;
                ContentEditor.EditModes = EditModes.Preview;
                RadButton1.Enabled = false;
                SaveEditBtn.Enabled = false;
                msgLb.Visible = true;
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

        protected void SaveEditBtn_Click(object sender, EventArgs e)
        {
            ContentEditor.EditModes = EditModes.All;
            ContentEditor.EditModes = ContentEditor.EditModes ^ EditModes.Preview;
            SaveEditBtn.Visible = false;
            RadButton1.Checked = false;
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


        private bool canDo(string operationItem)  //是否有权限
        {
            if (!user.hasAuthority(operationItem))
            {
                return false;
            } 
            return true; ;
        }
         

        protected void btn_ChangeValid_Click(object sender, EventArgs e)
        {
            bool msgEnable = RCB_Enable.SelectedItem.Value == "1" ? true : false;
            Model.Message msgMOdel = msg.GetModel(autoID);
            msgMOdel.IsValid = msgEnable;
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "修改公告状态";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now; 
                logModel.OperationDetail = "公告编号：" + autoID + " - 状态：" + msgEnable;
                isUpdate = msg.Update(msgMOdel);
                if (isUpdate)
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
                logModel.Memo += "异常：" + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isUpdate)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c2", "OpenAlert('恭喜！状态修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！状态修改失败！');", true);
                }
            }
        }

        protected void btn_ReEdit_Click(object sender, EventArgs e)
        {
            if (btn_ReEdit.Visible == true)
            {
                btn_ReEdit.Visible = false;
                btn_CancelEdit.Visible = true;
                SetEnable(true);
            }
            else
            {
                btn_ReEdit.Visible = true;
                btn_CancelEdit.Visible = false;
                SetEnable(false);
            }
        }

        private bool CheckContentlength(string str)
        {
            if (str.Length > 100)
                return false;
            return true;
        }

        protected void Btn_Modify_Click(object sender, EventArgs e)
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
            if (RadDatePicker1.DbSelectedDate == null )
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('抱歉，有效期限不能为空！');", true);
                return;
            }
            DateTime availableTime = (DateTime)RadDatePicker1.DbSelectedDate;
            DateTime expiredTime = RadDatePicker2.DbSelectedDate == null ? new DateTime(2050, 12, 31) : (DateTime)RadDatePicker2.DbSelectedDate;  
            string content = TabStrip1.SelectedTab.Value.Equals("2") ? (RadButton1.Checked ? CheckStr(ContentEditor.Content) : ContentEditor.Content) : ContentText.Text;
            if (content.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c1", "OpenAlert('抱歉，内容不能为空！');", true);
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
            Model.Message msgModel = msg.GetModel(autoID); 
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
            bool isUpdate = true;
            bool isAdd = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "修改公告";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "类型:" + msgType;
                List<Model.MessageToObject> mtoList = mto.GetModelList(1, msgModel.AutoID.ToString(), -1, -1, false);
                StringBuilder sbai = new StringBuilder();
                foreach (Model.MessageToObject mtoM in mtoList)
                {
                    sbai.Append(mtoM.AutoID.ToString() + ",");
                }
                int x = 1;
                if (sbai.ToString() != "")
                {
                    x = 0;
                    x = mto.DeleteList(sbai.ToString().Substring(0, sbai.ToString().Length - 1).Split(','));
                }
                if (x > 0)
                { 
                    isUpdate = msg.Update(msgModel);
                    if (isUpdate)
                    {
                        if (RadButton5.Checked)
                        {
                            Model.MessageToObject mtoModel = new Model.MessageToObject();
                            mtoModel.MsgID = msgModel.AutoID;
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
                                mtoModel.MsgID = msgModel.AutoID;
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
                                    mtoModel.MsgID = msgModel.AutoID;
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
                                            mtoModel.MsgID = msgModel.AutoID;
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
                }
                if (isUpdate && isAdd)
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
                if (isUpdate && isAdd)
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c", "OpenAlert('恭喜！编辑成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "c3", "OpenAlert('抱歉！编辑失败！');", true);
                }
            }
        }

        private void SetEnable(bool vb)
        {
            RCB_Type.Enabled = vb; 
            if (!vb)
            {
                ContentEditor.EditModes = EditModes.Preview;
            }
            else
            {
                ContentEditor.EditModes = EditModes.All; 
                ContentEditor.EditModes = ContentEditor.EditModes ^ EditModes.Preview;
            }
            ContentText.ReadOnly = !vb;
            Btn_Modify.Visible = vb;
        }
         
    }
}