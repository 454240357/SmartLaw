<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserDetail.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.SysUserDetail" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>操作员详情</title>
</head>
<body style="width:500px;">
    <form id="form1" runat="server">
            <div id="position">
            当前位置：系统管理 → 管理操作员 → 
            <asp:HyperLink ID="HL_UserName" runat="server"></asp:HyperLink> → <h1>基本信息</h1>
            </div>
            
            <div id="nav" runat="server">
            <span>基本信息</span>
            <!--<a runat="server" id="href1">查询关联</a>
            <a runat="server" id="href2">添加关联</a>-->
		    </div>
	    
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
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"/>

            <script src="../Js/RadScript.js" type="text/javascript"></script>
             <telerik:radwindowmanager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1" >
<%--                <Windows>
                    <telerik:RadWindow ID="DeviceDetailWindow" runat="server" Title="设备详细信息" AutoSize="false"
                      ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true"/>
                </Windows> --%>
             </telerik:radwindowmanager>

		    <asp:Panel ID="Panel1" runat="server">
            <div class="form">
            <fieldset>      		
				<div>
					<label>登录名：</label>
					<telerik:RadTextBox ID="Lb_LoginName" runat="server" Enabled="false"/>
				</div> 
				<div>
					<label>操作员姓名：</label>
					<telerik:RadTextBox ID="TB_UserName" runat="server" MaxLength="50"/>
					<span class="tip">*</span>
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  Display="Dynamic" SetFocusOnError="true"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="TB_UserName" Text="姓名必须为1-50个字符！"></asp:RequiredFieldValidator>											
				</div>
				<div>
					<label>员工号：</label>
					<telerik:RadTextBox ID="TB_EmployeeID" runat="server" MaxLength="20"/>
					<span class="tip"></span>
				</div>
                <div>
					<label>角色：</label>
					<telerik:RadComboBox ID="RCB_Role" runat="server">
                    </telerik:RadComboBox>
					<span class="tip"></span>
				</div> 
                <div>
					<label>密码：</label>
					<telerik:RadTextBox ID="TB_Password" runat="server" MaxLength="20" TextMode="Password"/>
					<span class="tip">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TB_Password" Text="密码必须为7-20个字符(可以包含英文、数字和下划线)！" 
                         ValidationExpression="^[a-z0-9A-Z_-]{7,20}$" Display="Dynamic"  SetFocusOnError="true"></asp:RegularExpressionValidator>				
				</div>
				<div>
					<label>重新输入密码：</label>
					<telerik:RadTextBox ID="TB_PwdCheck" runat="server" MaxLength="20" TextMode="Password"/>
					<span class="tip">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="TB_Password" ControlToValidate="TB_PwdCheck" Text="二次密码输入不一致！"  SetFocusOnError="true"></asp:CompareValidator>			
				</div>
				<div>
					<label for="SubCode_Enable">状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
				</div>
				<div>
					<label>&nbsp;</label>
					<asp:Button ID="Bt_Modify" runat="server" Text=" 修 改 " 
                        onclick="Bt_Modify_Click" />
				</div>
		    </fieldset>
		    </div>
		    </asp:Panel>
       <telerik:RadFormDecorator ID="FormDecorator1" runat="server" >
        </telerik:RadFormDecorator>
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>      
</body>
</html>
