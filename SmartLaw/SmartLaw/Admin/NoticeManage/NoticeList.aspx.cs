using System;
using System.Collections.Generic; 
using System.Data;
using SmartLaw.BLL;
using Telerik.Web.UI;
using SmartLaw.App_Code;
using System.Text;
using SmartLaw.Admin.NewsManage;
using System.Web.UI.WebControls;
using System.Linq;
namespace SmartLaw.Admin.NoticeManage
{
    public partial class NoticesList : SmartLaw.App_Code.BasePage
    {
        Message msg = new Message();
        MessageToObject mto = new MessageToObject();
        SysCodeDetail scd = new SysCodeDetail(); 
        Log log = new Log(); 
        private SessionUser user; 
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_Notice_Retrieve"); 
            if (!IsPostBack)
            {
                RGrid_NoticeList.Visible = false;
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

        protected void Bt_Search_Click(object sender, EventArgs e)
        {
            int index = 0;
            if (RadDatePicker1.DbSelectedDate != null)
            {
                index++;
            }
            if (RadDatePicker2.DbSelectedDate != null)
            {
                index++;
            }
            if (RadDatePicker3.DbSelectedDate != null)
            {
                index++;
            }
            if (RadDatePicker4.DbSelectedDate != null)
            {
                index++;
            }
            if (RTB_TitleKey.Text != "")
            {
                index++;
            }
            if (RTB_ContentKey.Text != "")
            {
                index++;
            }
            if (!RCB_Enable.SelectedValue.Equals("2"))
            {
                index++;
            }
            int[] keys = new int[index];
            object[] values = new object[index];
            index = 0;
            if (RadDatePicker1.DbSelectedDate != null)
            {
                keys[index] = 0;
                values[index++] = (DateTime)RadDatePicker1.DbSelectedDate;
            }
            if (RadDatePicker2.DbSelectedDate != null)
            {
                keys[index] = 1;
                values[index++] = (DateTime)RadDatePicker2.DbSelectedDate;
            }
            if (RadDatePicker3.DbSelectedDate != null)
            {
                keys[index] = 2;
                values[index++] = (DateTime)RadDatePicker3.DbSelectedDate;
            }
            if (RadDatePicker4.DbSelectedDate != null)
            {
                keys[index] = 3;
                values[index++] = (DateTime)RadDatePicker4.DbSelectedDate;
            }
            if (RTB_TitleKey.Text != "")
            {
                keys[index] = 4;
                values[index++] = RTB_TitleKey.Text;
            }
            if (RTB_ContentKey.Text != "")
            {
                keys[index] = 5;
                values[index++] = RTB_ContentKey.Text;
            }

            if (!RCB_Enable.SelectedValue.Equals("2"))
            {
                keys[index] = 6;
                values[index++] = RCB_Enable.SelectedValue.Equals("1") ? true : false;
            }

            List<Model.Message> msgList = new List<Model.Message>();
            if (index == 0)
            {
                msgList = msg.GetModelList(-1, "", -1, -1, false);
            }
            else
            {
                msgList = msg.GetMessageEx(keys, values, -1, -1, false);
            }
            RadTreeView regionTreeView = RadDropDownTree1.Controls[0] as RadTreeView;
            List<string> identitylist = new List<string>();
            List<SmartLaw.Model.MessageToObject> mtoMList = mto.GetModelList(2, "2", -1, -1, false);
            bool select = false;
            if (!RTB_SIM.Text.Trim().Equals(""))
            {
                select = true;
                string[] SimNoArr = RTB_SIM.Text.Split(',');
                foreach (string sim in SimNoArr)
                {
                    identitylist.Add("|"+sim+"|");
                    mtoMList.AddRange(mto.GetModelList(3, sim, -1, -1, false));
                }
            }
            //if (RadButton3.Checked)
            //{
                if (regionTreeView.SelectedNodes.Count > 0 && !regionTreeView.SelectedValue.Equals("Arear_Jfs"))
                {
                    select = true;
                    identitylist.Add("|" + regionTreeView.SelectedValue + "|");
                }
            //}
            /*else
            {
                foreach (RadTreeNode rtn in regionTreeView.CheckedNodes)
                {
                    if (rtn.CheckState== TreeNodeCheckState.Checked && !rtn.Value.Equals("Arear_Jfs"))
                    {
                        identitylist.Add("|" + rtn.Value + "|");
                        mtoMList.AddRange(mto.GetModelList(3, rtn.Value, -1, -1, false));
                    }
                }
            }*/
                if (RadListBox1.CheckedItems.Count > 0)
                {
                    select = true;
                    foreach (RadListBoxItem rbi in RadListBox1.CheckedItems)
                    {
                        identitylist.Add("|" + rbi.Value + "|");
                        mtoMList.AddRange(mto.GetModelList(3, rbi.Value, -1, -1, false));
                    }
                }
            if (select && mtoMList.Count == 0)
            {
                msgList = new List<Model.Message>();
            }
            else if (select &&　mtoMList.Count != 0)
            { 
                mtoMList = mtoMList.OrderBy(mt => mt.MsgID).ToList();
                List<long> msgIdlist = new List<long>();
                if (RadButton3.Checked)
                {
                    long lastMsgId = mtoMList[0].MsgID;
                    foreach (SmartLaw.Model.MessageToObject mtoM in mtoMList)
                    {
                        if (lastMsgId == mtoM.MsgID)
                        {
                        }
                        else
                        {
                            SmartLaw.Model.Message msgM = msg.GetModel(lastMsgId);
                            if (!msgM.AndOr)
                            {
                                msgIdlist.Add(msgM.AutoID);
                            }
                            else
                            {
                                bool isAdd = true;
                                List<SmartLaw.Model.MessageToObject> mtolist = mto.GetModelList(1, msgM.AutoID.ToString(), -1, -1, false);
                                foreach (SmartLaw.Model.MessageToObject mtom in mtolist)
                                {
                                    if (!identitylist.Contains("|" + mtom.ObjValue + "|"))
                                    {
                                        isAdd = false;
                                        break;
                                    }
                                }
                                if (isAdd)
                                {
                                    msgIdlist.Add(msgM.AutoID);
                                }
                            }
                        }
                        lastMsgId = mtoM.MsgID;
                    }
                    SmartLaw.Model.Message msgModel = msg.GetModel(lastMsgId);
                    if (!msgModel.AndOr)
                    {
                        msgIdlist.Add(msgModel.AutoID);
                    }
                    else
                    {
                        bool isAdd = true;
                        List<SmartLaw.Model.MessageToObject> mtolist = mto.GetModelList(1, msgModel.AutoID.ToString(), -1, -1, false);
                        foreach (SmartLaw.Model.MessageToObject mtom in mtolist)
                        {
                            if (!identitylist.Contains("|" + mtom.ObjValue + "|"))
                            {
                                isAdd = false;
                                break;
                            }
                        }
                        if (isAdd)
                        {
                            msgIdlist.Add(msgModel.AutoID);
                        }
                    }
                }
                else
                {
                    foreach (string id in identitylist)
                    {
                        if (mtoMList.Count == 0)
                        {
                            break;
                        }
                        List<long> mtoIdlist = new List<long>();
                        long lastMsgId = mtoMList[0].MsgID;
                        foreach (SmartLaw.Model.MessageToObject mtoM in mtoMList)
                        {
                            if (lastMsgId == mtoM.MsgID)
                            {

                            }
                            else
                            {
                                SmartLaw.Model.Message msgM = msg.GetModel(lastMsgId);
                                if (!msgM.AndOr)
                                {
                                    msgIdlist.Add(msgM.AutoID);
                                    mtoIdlist.Add(lastMsgId);
                                }
                                else
                                {
                                    List<SmartLaw.Model.MessageToObject> mtolist = mto.GetModelList(1, msgM.AutoID.ToString(), -1, -1, false);
                                    if (mtolist.Count == 1 && ("|"+mtolist[0].ObjValue+"|").Equals(id))
                                    {
                                        msgIdlist.Add(msgM.AutoID);
                                        mtoIdlist.Add(lastMsgId);
                                    }
                                }
                            }
                            lastMsgId = mtoM.MsgID;
                        }
                        SmartLaw.Model.Message msgModel = msg.GetModel(lastMsgId);
                        if (!msgModel.AndOr)
                        {
                            msgIdlist.Add(msgModel.AutoID);
                            mtoIdlist.Add(lastMsgId);
                        }
                        else
                        {
                            List<SmartLaw.Model.MessageToObject> mtolist = mto.GetModelList(1, msgModel.AutoID.ToString(), -1, -1, false);
                            if (mtolist.Count == 1 && mtolist[0].ObjValue.Equals(id))
                            {
                                msgIdlist.Add(msgModel.AutoID);
                                mtoIdlist.Add(lastMsgId);
                            }
                        }
                        mtoMList.RemoveAll(mt => mtoIdlist.Contains(mt.MsgID));
                    }
                }

                msgList.RemoveAll(mt => !msgIdlist.Contains(mt.AutoID));
                /*if (RadListBox1.CheckedItems.Count > 0 || regionTreeView.CheckedNodes.Count > 0 || !RTB_SIM.Text.Trim().Equals(""))
                {
                    List<string> identitylist = new List<string>();
                    foreach (RadTreeNode rtn in regionTreeView.CheckedNodes)
                    {
                        if ((rtn.ParentNode == null && rtn.CheckState == TreeNodeCheckState.Checked) ||
                                   (rtn.ParentNode != null && (!rtn.ParentNode.Checkable || rtn.ParentNode.CheckState != TreeNodeCheckState.Checked) && rtn.CheckState == TreeNodeCheckState.Checked))
                        {
                            identitylist.Add(rtn.Value);
                        }
                    }
                    foreach (RadListBoxItem rbi in RadListBox1.CheckedItems)
                    {
                        identitylist.Add(rbi.Value);
                    }
                    List<string> simList = null;
                    if (!RTB_SIM.Text.Trim().Equals(""))
                    {
                        string[] SimNoArr = RTB_SIM.Text.Split(',');
                        simList = new List<string>(SimNoArr);
                    }
                    List<long> auids = new List<long>();
                    foreach (Model.Message msgM in msgList)
                    {
                        List<Model.MessageToObject> mtoList = mto.GetModelList(1, msgM.AutoID.ToString(), -1, -1, false);
                        if (mtoList.Count == 1 && mtoList[0].ObjType.Equals("2"))
                        {
                            continue;
                        }
                        if (RadButton3.Checked && (identitylist.Count > 1 || (simList != null && identitylist.Count + simList.Count > 1)))
                        {
                            if (msgM.AndOr == false)
                            {
                                auids.Add(msgM.AutoID);
                                continue;
                            }
                            if (simList != null)
                            {
                                foreach (string sim in simList)
                                {
                                    if (!mtoList.Exists(mt => mt.ObjValue.Equals(sim) && mt.ObjType.Equals("1")))
                                    {
                                        auids.Add(msgM.AutoID);
                                        break;
                                    }
                                }
                            }
                            bool isBre = false;
                            foreach (string id in identitylist)
                            {
                                if (!mtoList.Exists(mt => mt.ObjValue.Equals(id) && mt.ObjType.Equals("0")))
                                {
                                    isBre = true;
                                    auids.Add(msgM.AutoID);
                                    break;
                                }
                            }
                            if (!isBre)
                            {
                                mtoList.RemoveAll(rt => rt.ObjType.Equals("1"));
                                if (identitylist.Count != mtoList.Count)
                                {
                                    auids.Add(msgM.AutoID);
                                }
                            }
                        }
                        else
                        {
                            bool NoExist0 = false;
                            bool NoExist1 = false;
                            if (simList != null)
                            {
                                if (!mtoList.Exists(mt => simList.Contains(mt.ObjValue)))
                                {
                                    NoExist0 = true;
                                }
                            }
                            else
                            {
                                NoExist0 = true;
                            }
                            if (!mtoList.Exists(mt => identitylist.Contains(mt.ObjValue)))
                            {
                                NoExist1 = true;
                            }
                            if (NoExist0 && NoExist1)
                            {
                                auids.Add(msgM.AutoID);
                            }
                        }
                    }
                    msgList.RemoveAll(mt => auids.Contains(mt.AutoID));
                }*/
            }
            msgList.ForEach(mt => mt.Contents = StripHTML(mt.Contents));
            RGrid_NoticeList.DataSource = msgList;
            ViewState["NoticeList"] = msgList;
            RGrid_NoticeList.DataBind();
            RGrid_NoticeList.Visible = true;

        }

        public string GetValid(object kind)
        {
            bool b = (bool)kind;
            if (b)
            { return "有效"; }
            return "无效";
        }

        public static string StripHTML(string source)
        {

            try
            {
                string result;
                result = source.Replace("\r", " ");
                result = result.Replace("\n", " ");
                result = result.Replace("'", " ");
                result = result.Replace("\t", string.Empty);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"( )+", " ");
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*head([^>])*>", "<head>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"(<( )*(/)( )*head( )*>)", "</head>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<head>).*(</head>)", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*script([^>])*>", "<script>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"(<( )*(/)( )*script( )*>)", "</script>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"(<script>).*(</script>)", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*style([^>])*>", "<style>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"(<( )*(/)( )*style( )*>)", "</style>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<style>).*(</style>)", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*td([^>])*>", "\t", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*br( )*>", "\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*li( )*>", "\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*div([^>])*>", "\r\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*tr([^>])*>", "\r\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<( )*p([^>])*>", "\r\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&nbsp;", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&bull;", " * ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&lsaquo;", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&rsaquo;", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&trade;", "(tm)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&frasl;", "/", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"<", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @">", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&copy;", "(c)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&reg;", "(r)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, @"&(.{2,6});", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = result.Replace("\n", "\r");
                result = System.Text.RegularExpressions.Regex.Replace(result, "(\r)( )+(\r)", "\r\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(\t)( )+(\t)", "\t\t", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(\t)( )+(\r)", "\t\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(\r)( )+(\t)", "\r\t", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(\r)(\t)+(\r)", "\r\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(\r)(\t)+", "\r\t", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                string breaks = "\r\r\r";
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }
                return result.Trim();
            }
            catch
            {
                //MessageBox.Show("Error");
                return source;
            }

        }

        protected void RGrid_NewsList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                Label lb = dataItem.FindControl("Label3") as Label;
                if (lb.Text.Equals("无效"))
                {
                    lb.CssClass = "inValid";
                } 
            }
        } 

        public string GetMessageType(object mt)
        {
            Model.SysCodeDetail scdModel = scd.GetModel(mt.ToString()); 
            if (scdModel==null)
            { 
                return "";
            }
            return scdModel.SYSCodeDetialContext;

        }

        public string GetTime(object time)
        {

            DateTime dt = DateTime.Parse(time.ToString());
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string GetChecked(Object chk)
        {
            int ch = int.Parse(chk.ToString());
            if (ch==1)
            { 
                return "已审核";
            }
            if (ch == 0)
            {
                return "待审核";
            }
            return "审核未通过";
        }

        public string GetContent(Object id)
        {
            int msgId = int.Parse(id.ToString());
            Model.Message msgM = msg.GetModel(msgId);
            if (msgM != null)
            {
                return msgM.Contents.Length > 25 ? msgM.Contents.Substring(0, 25) : msgM.Contents;
            }
            else
            {
                return "已失效";
            }
        }

        public bool DeleteAble(object auid)
        {
            if (CanDo("Auth_Notice_Del", auid.ToString()))
            {
                return true;
            }
            return false;
        }

        private bool CanDo(string operationItem, string autoID)  //是否有权限
        {
            DataSet scdDsRole = scd.GetListBySysCode(user.UserInfo.UserID, "Role");
            String user_Role = scdDsRole.Tables[0].Rows[0]["SYSCodeDetialID"].ToString(); 
            if (user_Role.Equals("Aministrators"))
            {
                return true;
            } 
            if (!user.hasAuthority(operationItem))
            {
                return false;
            } 
            if (msg.GetModel(long.Parse(autoID)).Publisher.Equals(user.UserInfo.UserID))
            {
                return true;
            }
            return false;
        }

        protected void RGrid_NoticeList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_NoticeList.DataSource = (DataTable)ViewState["NoticeList"];
        }

        protected void RGrid_NoticeList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string AutoID = Convert.ToString(e.CommandArgument);
                bool isDelete = false;
                Model.Log logModel = new Model.Log();
                logModel.OperationItem = "删除公告";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now; 
                try
                {
                    Model.Message msgModel = msg.GetModel(long.Parse(AutoID));
                    logModel.OperationDetail = "公告编号：" + msgModel.AutoID + " - 标题：" + msgModel.Title + " - 发布人：" + msgModel.Publisher;
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
                        isDelete = msg.Delete(long.Parse(AutoID));
                    }
                    if (isDelete)
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
                    if (isDelete)
                    { 
                        List<Model.Message> msgList = (List<Model.Message>)ViewState["NoticeList"];
                        msgList.RemoveAll(rt => rt.AutoID == long.Parse(AutoID));
                        RGrid_NoticeList.DataSource = msgList;
                        RGrid_NoticeList.DataBind();
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc1", "OpenAlert('恭喜公告删除成功。');", true);
                    }
                    else
                    {
                        RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cc2", "OpenAlert('抱歉！公告删除失败。');", true);
                    }
                }
            }
        }

        protected void RGrid_NewsList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGrid_NoticeList.DataSource = (DataTable)ViewState["NoticeList"];
        }
         
    }
}