<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="IntegralList.aspx.cs" Inherits="SmartLaw.Admin.IntegralManage.IntegralList" %>
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
        当前位置：积分管理 →<h1>积分历史</h1>
    </div> 
    <div id="nav">
                <span>积分历史</span><a href="IntegralConfiguration.aspx" >积分配置</a>
                <a href="GiftList.aspx">礼品查询</a><a href="GiftEdit.aspx" >礼品定义</a>
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
            <fieldset>  
                <div>
					<label>类型：</label>
                    <telerik:RadComboBox ID="RCB_SearchType" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="按STB号" />
                            <telerik:RadComboBoxItem Value="2" Text="按姓名" />
                        </Items>
                    </telerik:RadComboBox>                    
                </div>
				<div>
					<label>内容：</label>
                    <telerik:RadTextBox ID="TB_CodeContent" runat="server" MaxLength="20"/>
				</div>
				<div>
				    <label>&nbsp;</label>
                    <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" ></telerik:RadButton>
                </div>				
		    </fieldset>
        </div> 
        <div>
       <telerik:RadGrid ID="RGrid_IntegralList"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"  AutoGenerateColumns="False"  onneeddatasource="RGrid_IntegralList_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>  
            <telerik:GridTemplateColumn HeaderText="姓名">
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"IntegralDetail.aspx?SimID=" + Eval("SimCardNo") %>', 'SubModalWindow'); return false;"><%# GetName(Eval("SimCardNo"))%></a> 
                </ItemTemplate>
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="SimCardNo" HeaderText="STB号">
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>

             <telerik:GridBoundColumn DataField="TotalIntegral" HeaderText="积分总值" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>

            <telerik:GridBoundColumn DataField="LastModifyTime" HeaderText="最后修改时间" >
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridBoundColumn>  
            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
	</div>
    </asp:Panel>
</asp:Content>
