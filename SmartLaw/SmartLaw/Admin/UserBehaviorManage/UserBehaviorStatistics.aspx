<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="UserBehaviorStatistics.aspx.cs" Inherits="SmartLaw.Admin.UserBehaviorManage.UserBehaviorStatistics" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %>  
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">  
    <div id="position">
        当前位置：用户行为分析 →<h1>用户行为统计</h1>
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
            <div style="width:400px; float:left"> 
				<div>
					<label>类型：</label>
                    <telerik:RadComboBox ID="RCB_SearchType" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="按用户姓名" />
                            <telerik:RadComboBoxItem Value="2" Text="按STB号" />
                            <telerik:RadComboBoxItem Value="3" Text="按IP地址" />
                        </Items>
                    </telerik:RadComboBox>                    
                </div> 
                <div>
					 <label>开始日期：</label>
                     <telerik:RadDatePicker ID="RadDatePicker1" runat="server">
                     </telerik:RadDatePicker>
				</div> 
                <div>
                    <label>行为：</label>
                    <telerik:RadComboBox ID="RCB_Behaviour" runat="server"> 
                    </telerik:RadComboBox>
                </div> 
                <div>
				    <label>&nbsp</label>
                    <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" ></telerik:RadButton>
                </div>	
            </div>
            <div style="width:400px; float:left">
                <div>
					<label>内容：</label>
                    <telerik:RadTextBox ID="TB_Content" runat="server"      Width="100px" />
				</div> 
                <div>
					 <label>结束日期：</label>
                     <telerik:RadDatePicker ID="RadDatePicker2" runat="server">
                     </telerik:RadDatePicker>
				</div> 
            </div> 
            </fieldset> 
        </div>
        <telerik:RadGrid ID="RGrid_List"  AllowSorting="True" OnItemCommand="RGrid_List_ItemCommand"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"   
                        AutoGenerateColumns="False"   onneeddatasource="RGrid_List_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>  
             <telerik:GridTemplateColumn HeaderText="用户行为" SortExpression="Behavior">
                  <ItemTemplate>
                    <asp:Label ID="Label0" runat="server" Text='<%# GetBhName(Eval("Behavior")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="40%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn HeaderText="发生次数" SortExpression="Count">
                <ItemTemplate> 
                    <asp:LinkButton ID="LB_Del" runat="server" CausesValidation="false" 
                        CommandName="Detail" Text='<%# Eval("Count")%>' CommandArgument='<%# Eval("Behavior") %>' ></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="30%"></ItemStyle>
            </telerik:GridTemplateColumn> 

            <telerik:GridTemplateColumn HeaderText="百分比" SortExpression="Percentage">
                  <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetPt(Eval("Percentage")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="30%"></ItemStyle>
            </telerik:GridTemplateColumn>

            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
        <br />
        <br />
        <telerik:RadChart ID="RadChart2" runat="server" DefaultType="Pie" Visible="false" Width="750px" AutoTextWrap="true"    
          OnItemDataBound="RadChart2_ItemDataBound"  >
          <Appearance Dimensions-Width="880px"    >
          </Appearance>
          <Series> 
          <telerik:ChartSeries Name="Series 1" Type="Pie" DataYColumn="Count">
                    <Appearance LegendDisplayMode="ItemLabels"  >
                    </Appearance>
          </telerik:ChartSeries>
          </Series>
      </telerik:RadChart>
    </asp:Panel>
</asp:Content>
