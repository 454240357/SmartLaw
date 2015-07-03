<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubSysCodeRelationList.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.SubSysCodeRelationList" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="width:900px;">
    <form id="form1" runat="server">
		<div id="position">当前位置：系统管理 → 管理类别
		→ <asp:HyperLink ID="HL_SysCodeName" runat="server"></asp:HyperLink>
		→ <asp:HyperLink ID="HL_SubSysCodeName" runat="server"></asp:HyperLink>
		→ <h1>查询关联</h1>
		</div>
            <div id="nav" runat="server">
			<a href='<%="SubSysCodeDetail.aspx?SubSysCodeId="+subSysCodeId%>'>小类基本信息</a> 
            <span>查询关联</span>
            <a href='<%="AddSubSysCodeRelation.aspx?SubSysCodeId="+subSysCodeId%>'>添加关联</a> 
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
			    <ul class="twoColumn">
			    <li>
				    <label>代码名称：</label>
					<telerik:RadTextBox ID="TB_SubCode" runat="server" MaxLength="20"/>
		        </li>
                <li>
                    <label>所属大类：</label>
                    <telerik:RadComboBox ID="RCB_SysCodeList" Runat="server" Filter="Contains" 
                        AutoPostBack="true"  ></telerik:RadComboBox> 
                </li>
			    </ul>
		    </div>
            <div style="width:400px; float:left">
					<label>&nbsp;</label>
                    <asp:Button ID="Bt_Search" runat="server" Text=" 查 找 " 
                        onclick="Bt_Search_Click"/>              
            </div>
            <div style=" float:right">
                <label>&nbsp</label>
				<asp:Button ID="Button1" runat="server" Text=" 删除关联 " onclick="Button1_Click" OnClientClick="return blockConfirm('确认删除关联吗？', event, 330, 100,'','九峰山智慧社区管理平台');" />      
		    </div>	
	        </fieldset>
                <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                <fieldset>
                <div class="tabsub">
                    <asp:Label ID="BigCode" runat="server" Text='<%# Eval("SYSCodeID")%>'></asp:Label>
				</div>
				</fieldset>
				<div>
           <telerik:RadGrid ID="RGrid_RelationSubSysCode"   AllowMultiRowSelection="true"  runat="server" GridLines="None" 
                        AutoGenerateColumns="False" OnItemCommand="RGrid_RelationSubSysCode_ItemCommand" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>    
            <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" >
            </telerik:GridClientSelectColumn>         
            <telerik:GridTemplateColumn HeaderText="代码编号" SortExpression="SYSCodeDetialID" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"SubSysCodeDetail.aspx?SubSysCodeId=" + Eval("SYSCodeDetialID") %>', 'SubSysCodeDetailWindow'); return false;"><%# Eval("SYSCodeDetialID")%></a>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>
            
            <telerik:GridBoundColumn DataField="SYSCodeDetialContext" HeaderText="代码内容" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridTemplateColumn HeaderText="可用性" SortExpression="IsValid" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetValid(Eval("IsValid")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn>
            
            <telerik:GridBoundColumn DataField="SYSCodeID" HeaderText="所属大类" >
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridBoundColumn DataField="memo" HeaderText="备注" >
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>                                    
              <telerik:GridBoundColumn DataField="SYSCodeDetialID" Display="false">
                        <ItemStyle Width="0%" ></ItemStyle>
             </telerik:GridBoundColumn>                     
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LB_DelRelation" runat="server" CausesValidation="false" 
                        CommandName="DelRelation" Text="删除关联" CommandArgument='<%# Eval("SYSCodeDetialID") %>' 
                        OnClientClick="return blockConfirm('确认删除关联吗？', event, 330, 100,'','九峰山智慧社区管理平台');"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn>
            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True"></Selecting>
            </ClientSettings>
        </telerik:RadGrid>				
                </div>

                </ItemTemplate>
                </asp:Repeater>
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
