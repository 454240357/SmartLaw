<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="GiftList.aspx.cs" Inherits="SmartLaw.Admin.IntegralManage.GiftList" %>
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
        当前位置：积分管理 →<h1>礼品查询</h1>
    </div> 
    <div id="nav">
                <a href="IntegralList.aspx" >积分历史</a><a href="IntegralConfiguration.aspx" >积分配置</a>
                <span>礼品查询</span><a href="GiftEdit.aspx" >礼品定义</a>
                <a href="IntegralExchangeRecords.aspx">积分兑换记录</a>
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
                <div>
					<label>类型：</label>
                    <telerik:RadComboBox ID="RCB_SearchType" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="按礼品名称" />
                            <telerik:RadComboBoxItem Value="2" Text="按积分兑换值" />
                        </Items>
                    </telerik:RadComboBox>                    
                </div>
				<div>
					<label>内容：</label>
                    <telerik:RadTextBox ID="TB_Content" runat="server" MaxLength="20"/>
				</div>
                <div>
                    <Label>&nbsp;</Label>
                    <telerik:RadButton ID="RbtnSubmit" runat="server" Text="查询"   UseSubmitBehavior="false"
                         OnClick="RbtnSubmit_Click" Height="22px" />
		        </div> 
        </div>
        <br />
         <telerik:RadGrid ID="RGrid_GiftList"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"  
                        AutoGenerateColumns="False"   onneeddatasource="RGrid_GiftList_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>   

            <telerik:GridTemplateColumn HeaderText="礼品名称" SortExpression="PrizeName" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"GiftDetail.aspx?AutoID=" + Eval("AutoID") %>', 'SubModalWindow'); return false;"><%# Eval("PrizeName")%></a> 
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="PrizeUnit" HeaderText="单位">
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridBoundColumn DataField="Stock" HeaderText="库存" >
                <ItemStyle Width="15%"></ItemStyle>
            </telerik:GridBoundColumn>
            
             <telerik:GridBoundColumn DataField="Registrant" HeaderText="操作员" >
                <ItemStyle Width="12%"></ItemStyle>
            </telerik:GridBoundColumn>

            <telerik:GridBoundColumn DataField="Points" HeaderText="积分兑换值"  >
                  <ItemStyle Width="12%"></ItemStyle>
            </telerik:GridBoundColumn>   

            <telerik:GridTemplateColumn HeaderText="编辑时间" SortExpression="RegTime">
                  <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetTime(Eval("RegTime")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn HeaderText="状态" SortExpression="Stock" >
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# GetValid(Eval("Stock")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="9%"></ItemStyle>
            </telerik:GridTemplateColumn>

            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
    </asp:Panel>
</asp:Content>
