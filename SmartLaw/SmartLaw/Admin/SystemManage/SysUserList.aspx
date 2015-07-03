<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeBehind="SysUserList.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.SysUserList" %>
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
            当前位置：系统管理 → <a href="AddSysUser.aspx">管理操作员</a> → <h1>查询操作员</h1>
            </div>
            
            <div id="nav">
            <span>查询操作员</span>
            <a href="AddSysUser.aspx" runat="server" id="href1">添加操作员</a>
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
					<label>类型：</label>
                    <telerik:RadComboBox ID="RCB_SearchType" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="按登录名" />
                            <telerik:RadComboBoxItem Value="2" Text="按姓名" />
                        </Items>
                    </telerik:RadComboBox>
                </div>			
				  
				<div>
					<label>内容：</label>
					<telerik:RadTextBox ID="TB_KeyWords" runat="server" MaxLength="20"/>
				</div>
				<div>
				    <label>&nbsp</label>
				    <asp:Button ID="Bt_Search" runat="server" Text=" 查 询 " onclick="Bt_Search_Click"/>
                </div>
		    </fieldset>
		    </div>		    
		    	<div>
           <telerik:RadGrid ID="RGrid_SysUserList" runat="server" GridLines="None" 
                        AutoGenerateColumns="False" AllowSorting="true"
                        onneeddatasource="RGrid_SysUserList_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>           
            <telerik:GridTemplateColumn HeaderText="登录名" SortExpression="UserID" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"SysUserDetail.aspx?LoginID=" + Eval("UserID") %>', 'SubModalWindow'); return false;"><%# Eval("UserID")%></a>
<%--                    <a href='SysUserDetail.aspx?LoginID=<%# Eval("LoginID")%>'><%# Eval("LoginID")%></a>--%>
                </ItemTemplate>
                <ItemStyle Width="30%"></ItemStyle>
            </telerik:GridTemplateColumn>
            
            <telerik:GridBoundColumn DataField="UserName" HeaderText="姓名" >
                <ItemStyle Width="30%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridBoundColumn DataField="EmployeeID" HeaderText="员工号" >
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridTemplateColumn HeaderText="状态" SortExpression="ISValid" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetValid(Eval("ISValid")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>                                   
            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
            </telerik:RadGrid>
				</div>
				</asp:Panel>	
</asp:Content>

