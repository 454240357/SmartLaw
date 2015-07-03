<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubSysCodeList.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.SubSysCodeList" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="width:900px">
    <form id="form1" runat="server">
    <div id="position">
            当前位置：系统管理 → 系统代码
		→ <asp:HyperLink ID="HL_SysCodeName" runat="server"></asp:HyperLink>
		→ <h1>查询小类</h1>
    </div>

            <div id="nav" runat="server">
            <span>查询小类代码</span>
            <a href='<%="AddSubSysCode.aspx?SysCodeId="+sysCodeId%>'>添加小类代码</a>
            <a href='<%="SysCodeDetail.aspx?SysCodeId="+sysCodeId%>'>大类基本信息</a>  
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
                <Windows>
                    <telerik:RadWindow ID="SubSysCodeDetailWindow" runat="server" Title="小类详细信息" AutoSize="false"
                      ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" KeepInScreenBounds="true"  
                        OnClientActivate="maximizeWindow"  OnClientClose="restoreWindow"
                      Width="580px" Height="360px"/>
                </Windows>     
             </telerik:radwindowmanager>
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
					<label>关键字：</label>
                    <telerik:RadTextBox ID="TB_KeyWords" runat="server" MaxLength="20"/>															

                </div>
                <div>
					<label>&nbsp</label>
                    <asp:Button ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" />
                </div>                			
				</fieldset>
	        </div>

		    
				<div>
            <telerik:RadGrid ID="RGrid_SubSysCode"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None" 
                        AutoGenerateColumns="False" onitemcommand="RGrid_SubSysCode_ItemCommand" 
                        onneeddatasource="RGrid_SubSysCode_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>           
            <telerik:GridTemplateColumn HeaderText="小类编号" SortExpression="SYSCodeDetialID" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"SubSysCodeDetail.aspx?SubSysCodeId=" + Eval("SYSCodeDetialID") %>', 'SubSysCodeDetailWindow'); return false;"><%# Eval("SYSCodeDetialID")%></a>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>
            
           <telerik:GridBoundColumn DataField="SYSCodeDetialContext" HeaderText="小类内容" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridTemplateColumn HeaderText="可用性" SortExpression="IsValid" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetValid(Eval("IsValid")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn> 
            
            <telerik:GridBoundColumn DataField="memo" HeaderText="备注" >
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>                                    
                                   
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LB_Del" runat="server" CausesValidation="false" 
                        CommandName="Del" Text="删除" CommandArgument='<%# Eval("SYSCodeDetialID") %>' 
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
        <telerik:RadFormDecorator ID="FormDecorator1" runat="server" >
        </telerik:RadFormDecorator>					
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>     
</body>
</html>
