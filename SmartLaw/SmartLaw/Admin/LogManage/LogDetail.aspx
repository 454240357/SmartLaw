<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogDetail.aspx.cs" Inherits="SmartLaw.Admin.LogManage.LogDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>日志查看</title>
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
        当前位置：日志管理 →<h1>日志查看</h1>
    </div> 
    <asp:Panel ID="Panel1" runat="server">
        <div class="form">
            <fieldset>
                <div>
                    <label>操作项：</label>
                    <asp:Label ID="Lb_OperationItem" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label>重要参数：</label>
                    <telerik:RadTextBox ID="TB_OperationDetail" runat="server"  ReadOnly = "true" TextMode="MultiLine" SkinID="BigTextBox"  Rows="5" />
                </div>
                <div>
                    <label>操作员：</label>
                    <asp:Label ID="Lb_Operator" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label>操作时间：</label>
                    <asp:Label ID="Lb_OperationTime" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label>备注：</label>
                    <telerik:RadTextBox ID="TB_Memo" runat="server"  ReadOnly = "true" TextMode="MultiLine" SkinID="BigTextBox" Rows="5" />
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
