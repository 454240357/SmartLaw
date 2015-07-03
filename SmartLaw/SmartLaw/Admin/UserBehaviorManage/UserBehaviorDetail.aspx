<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBehaviorDetail.aspx.cs" Inherits="SmartLaw.Admin.UserBehaviorManage.UserBehaviorDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>用户行为详情</title>
    <script src="../Js/RadScript.js" type="text/javascript"></script>
</head>
<body style="width: 900px;">
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
        当前位置：用户行为分析 →<h1>用户行为详情</h1>
    </div> 
    <asp:Panel ID="Panel1" runat="server"> 
       <div>
       <telerik:RadGrid ID="RGrid_UserBehaviorList"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"  AutoGenerateColumns="False"  onneeddatasource="RGrid_IntegralList_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>  
            <telerik:GridTemplateColumn HeaderText="姓名">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%#  GetName(Eval("SimCardNo")) %>'></asp:Label> 
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="SimCardNo" HeaderText="STB号">
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>

            <telerik:GridBoundColumn DataField="Behavior" HeaderText="用户行为">
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn> 

            <telerik:GridBoundColumn DataField="IpAddr" HeaderText="IP" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>

            <telerik:GridBoundColumn DataField="ScanTime" HeaderText="时间" >
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>  
           
            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
	</div>
    </asp:Panel>
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>
</body>
</html>
