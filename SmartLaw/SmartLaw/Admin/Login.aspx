<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmartLaw.Admin.Login" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title></title>
     <link rel="stylesheet" type="text/css" href="login.css" />
     <script src="js/RadScript.js" type="text/javascript"></script>
</head>
<body>
<script type="text/javascript">
    function f_clear() {
        document.getElementById("<%= TB_LoginId.ClientID %>").value = ""; 
        document.getElementById("<%= TB_Password.ClientID %>").value = "";
    }
</script>
<form id="form1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>                                                     
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"/>
    <telerik:radwindowmanager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1">    
    </telerik:radwindowmanager>
    <asp:Panel ID="Panel1" runat="server">
    <div id="main">
    <div class="control"> 
            <span class="title"><%=Titlestr %></span>
            <input type="button" class="close" title="关闭窗口" onclick="window.close();"/>		
		    <asp:TextBox ID="TB_LoginId" runat="server" CssClass="loginName" MaxLength="20" ToolTip="请输入用户名" EnableTheming="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RangeValidator1" runat="server" ErrorMessage="用户名不能为空。" 
                ControlToValidate="TB_LoginId" CssClass="error1"></asp:RequiredFieldValidator>
            <asp:TextBox ID="TB_Password" runat="server" CssClass="password" TextMode="Password" MaxLength="20" ToolTip="请输入密码" EnableTheming="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RangeValidator2" runat="server" ErrorMessage="密码不能为空。" 
                ControlToValidate="TB_Password" CssClass="error2"></asp:RequiredFieldValidator>
            <asp:CheckBox ID="CB_Storage" runat="server" CssClass="remember" ToolTip="记住以便下次登录"/>
            <asp:Button ID="Bt_Login" runat="server" CssClass="login" ToolTip="登录系统" onclick="Bt_Login_Click"/>
            <input type="button" class="cancel" title="清除信息" onclick="f_clear();"/>
	        </div>
	</div>
	</asp:Panel>
</form>
</body>
</html>
