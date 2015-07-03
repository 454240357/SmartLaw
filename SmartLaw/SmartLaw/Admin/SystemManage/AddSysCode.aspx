<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="AddSysCode.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.AddSysCode" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/RadScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
            <div id="position">
            当前位置：系统管理 → <a href="SysCodeList.aspx">系统代码</a> → <h1>添加系统代码</h1>
            </div>            
            <div id="nav">
            <a href="SysCodeList.aspx">查询系统代码</a>
			<span>添加系统代码</span>
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

		    <asp:Panel ID="Panel1" runat="server">             
            <div class="form">
            <fieldset>      		
				<div>
					<label>代码编号：</label>
                    <telerik:RadTextBox ID="TB_CodeName" runat="server" MaxLength="50"/> 
				</div>				
				  
				<div>
					<label>代码内容：</label>
                    <telerik:RadTextBox ID="TB_CodeContent" runat="server" MaxLength="50"/>	 
				</div>
				
				<div>
					<label>是否有效：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Text="有效" Value="1" />
                            <telerik:RadComboBoxItem Text="无效" Value="0" />
                        </Items>
                    </telerik:RadComboBox>
				</div>
				
				<div>
				<label>代码备注：</label>
                    <telerik:RadTextBox ID="TB_CodeMemo" runat="server" Columns="50" Rows="5" 
                        TextMode="MultiLine" SkinID="BigTextBox"/>															
				</div>
				
				<div>
					<label>&nbsp;</label>
					<asp:Button ID="Bt_Add" runat="server" Text=" 添 加 " onclick="Bt_Add_Click" />
				</div>
				</fieldset>
				
				</div>
				</asp:Panel>
</asp:Content>

