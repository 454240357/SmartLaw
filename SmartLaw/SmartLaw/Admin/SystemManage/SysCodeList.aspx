<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="SysCodeList.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.SysCodeList" %>

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
        当前位置：系统管理 → <a href="#">系统代码</a> →
        <h1>查看系统代码</h1>
    </div>
    <div id="nav">
        <span>查看系统代码</span><a href="AddSysCode.aspx">添加系统代码</a>
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
					<label>类型：</label>
                    <telerik:RadComboBox ID="RCB_SearchType" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="按编号" />
                            <telerik:RadComboBoxItem Value="2" Text="按内容" />
                        </Items>
                    </telerik:RadComboBox>                    
                </div>
				<div>
					<label>内容：</label>
                    <telerik:RadTextBox ID="TB_CodeContent" runat="server" MaxLength="20"/>
				</div>
				<div>
				    <label>&nbsp</label>
                    <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" ></telerik:RadButton>
                </div>				
		    </fieldset>
        </div>
        <div>
       <telerik:RadGrid ID="RGrid_SysCode"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None" 
                        AutoGenerateColumns="False" onitemcommand="RGrid_SysCode_ItemCommand" 
                        onneeddatasource="RGrid_SysCode_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>   

            <telerik:GridTemplateColumn HeaderText="代码编号" SortExpression="SysCodeID" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"SysCodeDetail.aspx?SysCodeID=" + Eval("SysCodeID") %>', 'SubModalWindow'); return false;"><%# Eval("SysCodeID")%></a> 
                </ItemTemplate>
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="SYSCodeContext" HeaderText="代码内容" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridTemplateColumn HeaderText="可用性" SortExpression="IsValid" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetValid(Eval("isValid")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>
            
            <telerik:GridBoundColumn DataField="memo" HeaderText="备注" >
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>                                    
                                   
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LB_Del" runat="server" CausesValidation="false" 
                        CommandName="Del" Text="删除代码" CommandArgument='<%# Eval("SYSCodeID") %>' 
                        OnClientClick="return blockConfirm('确认删除吗？', event, 330, 100,'','九峰山智慧社区管理平台');"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn> 
            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
	</div>
    </asp:Panel>
</asp:Content>
