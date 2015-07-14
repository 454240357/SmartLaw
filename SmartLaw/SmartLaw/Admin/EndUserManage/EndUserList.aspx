<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="EndUserList.aspx.cs" Inherits="SmartLaw.Admin.EndUserManage.EndUserList" %>
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
        当前位置：终端用户管理 → <h1>查询终端用户</h1>
    </div> 
    <div id="nav">
        <span>检索终端用户</span><a href="AddEndUser.aspx" runat="server" id="href1">添加终端用户</a>
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
                    <Label>身份：</Label>
                    <telerik:RadComboBox ID="RCB_Identity"  runat="Server" CheckBoxes="true" EmptyMessage="请选择"> 
                          <Localization AllItemsCheckedString="全选" />
                    </telerik:RadComboBox>
                </div>
                <div>
                      <Label>区域：</Label> 
                      <telerik:RadDropDownTree ID="RadDropDownTree1"   runat="server" CheckBoxes="CheckChildNodes"  DropDownSettings-Width="100%"   Skin="Default" DefaultMessage="请选择区域"  > 
                      </telerik:RadDropDownTree>
                </div> 
                <div> 
                    <label>姓名：</label> 
					<telerik:RadTextBox ID="RTB_Name" runat="server" Width="100px" /> 
                </div>
				<div> 
                    <label>STB号：</label> 
					<telerik:RadTextBox ID="RTB_SIM" runat="server" Width="100px" /> 
                </div>
                <div>
				    <label>&nbsp</label>
                    <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" ></telerik:RadButton>
               </div>				
		    </fieldset>
        </div>
        <div style=" float:right">
                <label>&nbsp</label> 
                <asp:Button ID="Button1" Visible="false" runat="server" Text=" 批量同步 "  
                    OnClientClick="return blockConfirm('确认同步吗？', event, 330, 100,'','九峰山智慧社区管理平台;')" 
                    onclick="Button1_Click" />      
		</div>
        <div>
        	
       <telerik:RadGrid ID="RGrid_EndUserList"  AllowSorting="True" AllowMultiRowSelection="true"  
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"  onitemcommand="RGrid_EndUserList_ItemCommand" 
                        AutoGenerateColumns="False"   onneeddatasource="RGrid_EndUserList_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>   
            <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" >
                    </telerik:GridClientSelectColumn> 
           <telerik:GridBoundColumn DataField="AutoID" Display="false">
                  <ItemStyle Width="0%" ></ItemStyle>
           </telerik:GridBoundColumn>  

            <telerik:GridTemplateColumn HeaderText="编号" SortExpression="AutoID" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"EndUserDetail.aspx?AutoID=" + Eval("AutoID") %>', 'SubModalWindow'); return false;"><%# Eval("AutoID")%></a> 
                </ItemTemplate>
                <ItemStyle Width="15%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="EndUserName" HeaderText="姓名">
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridBoundColumn DataField="SimCardNo" HeaderText="STB号" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>
            
             <telerik:GridBoundColumn DataField="LastModifyTime" HeaderText="修改时间" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>

            
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LB_Del" runat="server" CausesValidation="false" 
                        CommandName="Del" Text="删除" CommandArgument='<%# Eval("AutoID") %>'  Visible ='<%# deleteAble(Eval("AutoID")) %>'
                        OnClientClick="return blockConfirm('确认删除吗？', event, 330, 100,'','九峰山智慧社区管理平台;"></asp:LinkButton>
                     &nbsp;&nbsp;|&nbsp;&nbsp;
                     <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                        CommandName="Sync" Text="同步" CommandArgument='<%# Eval("AutoID") %>'  Visible ='<%# syncAble(Eval("AutoID")) %>'
                        OnClientClick="return blockConfirm('确认同步吗？', event, 330, 100,'','九峰山智慧社区管理平台;"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="15%"></ItemStyle>
            </telerik:GridTemplateColumn>  

            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True"></Selecting>
            </ClientSettings>
        </telerik:RadGrid> 
	</div>
    </asp:Panel>
</asp:Content>
