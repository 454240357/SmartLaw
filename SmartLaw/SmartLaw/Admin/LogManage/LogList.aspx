<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/MasterPage.master" CodeBehind="LogList.aspx.cs" Inherits="SmartLaw.Admin.LogManage.LogList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %> 
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">  
    <div id="position">
        当前位置：日志管理 →<h1>日志查询</h1>
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
                     <label>日期：</label>
                     <telerik:RadDatePicker ID="RadDatePicker1" runat="server">
                     </telerik:RadDatePicker> 
                </div> 
                <div>
                     <label>操作员：</label>  
                    <telerik:RadComboBox ID="RadComboBox1" runat="server" DefaultMessage="请选择操作员"
                         onselectedindexchanged="RadComboBox1_SelectedIndexChanged" AutoPostBack="true">
                    </telerik:RadComboBox> 
                </div>
				<div>
				    <label>&nbsp;</label>
                    <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" ></telerik:RadButton>
                </div>				
		    </fieldset>
        </div>
        <div>
       <telerik:RadGrid ID="RGrid_LogList"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"  onitemcommand="RGrid_LogList_ItemCommand" 
                        AutoGenerateColumns="False"   onneeddatasource="RGrid_LogList_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>   

            <telerik:GridTemplateColumn HeaderText="日志编号" SortExpression="AutoID" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"LogDetail.aspx?AutoID=" + Eval("AutoID") %>', 'SubModalWindow'); return false;"><%# Eval("AutoID")%></a> 
                </ItemTemplate>
                <ItemStyle Width="15%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="OperationItem" HeaderText="操作项">
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridBoundColumn DataField="OperationTime" HeaderText="操作时间" >
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>
            
             <telerik:GridBoundColumn DataField="Operator" HeaderText="操作员" >
                <ItemStyle Width="15%"></ItemStyle>
            </telerik:GridBoundColumn>

            <telerik:GridTemplateColumn  HeaderText="备注" SortExpression="Memo" >
                 <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetShortMemo(Eval("Memo")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridTemplateColumn>     

            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
	</div>
    </asp:Panel>
</asp:Content>
