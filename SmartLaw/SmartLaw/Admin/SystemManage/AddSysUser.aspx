<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="AddSysUser.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.AddSysUser" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register src="LeftMenu.ascx" tagname="LeftMenu" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Js/RadScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:LeftMenu ID="LeftMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div id="position">
            当前位置：系统管理 → <a href="AddSysUser.aspx">管理操作员</a> → <h1>添加操作员</h1>
            </div>
            
            <div id="nav">
            <a href="SysUserList.aspx">查询操作员</a>
            <span>添加操作员</span>
		    </div>		    
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

            <asp:Panel runat="server" ID="Panel1">
		    <div class="form">
            <fieldset>      		
				<div>
					<label>登录名：</label>
					<telerik:RadTextBox ID="TB_LoginName" runat="server" MaxLength="20"/> 
				</div> 
				<div>
					<label>操作员姓名：</label>
					<telerik:RadTextBox ID="TB_UserName" runat="server" MaxLength="50" /> 
				</div>
				<div>
					<label>员工号：</label>
					<telerik:RadTextBox ID="TB_EmployeeID" runat="server" MaxLength="20"/>
					<span class="tip"></span>
				</div>
				<div>
					<label>角色：</label>
					<telerik:RadComboBox ID="RCB_Role" runat="server" AutoPostBack="true"  >
                    </telerik:RadComboBox>
					<span class="tip"></span>
				</div> 
               <div>
					<label>密码：</label>
					<telerik:RadTextBox ID="TB_Password" runat="server" MaxLength="20" TextMode="Password"/> 
				</div>
				<div>
					<label>重新输入密码：</label>
					<telerik:RadTextBox ID="TB_PwdCheck" runat="server" MaxLength="20" TextMode="Password"/> 
				</div>
				<div>
					<label for="RCB_Enable">状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>                      
				</div>
				<div>
					<label>&nbsp;</label>
					<asp:Button ID="Bt_Add" runat="server" Text=" 添 加 " onclick="Bt_Add_Click" 
                        style="height: 21px" />
				</div>
		    </fieldset>
		    </div>
            </asp:Panel>	                 
</asp:Content>

