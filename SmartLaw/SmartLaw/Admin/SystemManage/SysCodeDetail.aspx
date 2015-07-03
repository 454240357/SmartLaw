<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysCodeDetail.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.SysCodeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>大类代码</title>
    <script src="../Js/RadScript.js" type="text/javascript"></script>
</head>
<body style="width: 580px;">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
     <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false"  />
     <telerik:radwindowmanager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1">
        <Localization OK="好的" Cancel="取消" Close="关闭" Maximize="最大化" Minimize="最小化" Reload="刷新" Restore="还原" PinOn="固定" PinOff="解除固定" Yes="是" No="否" />
     </telerik:radwindowmanager>
    <div id="position">
        当前位置：系统管理 → 系统代码 →
        <asp:HyperLink ID="HL_SysCodeName" runat="server"></asp:HyperLink>
        →
        <h1>
            基本信息</h1>
    </div>
    <div id="nav" runat="server">
        <a href='<%="SubSysCodeList.aspx?SysCodeId="+sysCodeId%>'>查询小类代码</a> <a href='<%="AddSubSysCode.aspx?SysCodeId="+sysCodeId%>'>
            添加小类代码</a> <span>大类基本信息</span>
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <div class="form">
            <fieldset>
                <div>
                    <label>
                        代码编号：</label>
                    <asp:Label ID="Lb_CodeName" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label>
                        代码内容：</label>
                    <telerik:RadTextBox ID="TB_CodeContent" runat="server" MaxLength="50" />
                    <span class="tip">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                        SetFocusOnError="true" ControlToValidate="TB_CodeContent" Text="代码内容必须为1-50个字符！"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        是否有效：</label>
                    <telerik:RadComboBox ID="RCB_Enable" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div>
                    <label>
                        代码备注：</label>
                    <telerik:RadTextBox ID="TB_CodeMemo" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                        SkinID="BigTextBox" />
                </div>
                <div>
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="Bt_Modify" runat="server" Text=" 修 改 " OnClick="Bt_Modify_Click"
                        Style="height: 21px" />
                </div>
            </fieldset>
        </div>
    </asp:Panel>
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>
</body>
</html>
