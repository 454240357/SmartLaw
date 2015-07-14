<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/MasterPage.master" CodeBehind="IntegralExchangeRecords.aspx.cs" Inherits="SmartLaw.Admin.IntegralManage.IntegralExchangeRecords" %>
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
        当前位置：积分管理 →<h1>积分兑换记录</h1>
    </div> 
     <div id="nav">
                <a href="IntegralList.aspx" >积分历史</a><a href="IntegralConfiguration.aspx" >积分配置</a>
                <a href="GiftList.aspx">礼品查询</a><a href="GiftEdit.aspx" >礼品定义</a>
                <span>积分兑换记录</span>
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
                        </Items>
                    </telerik:RadComboBox>                    
                </div>
				
                <div>
					 <label>开始日期：</label>
                     <telerik:RadDatePicker ID="RadDatePicker1" runat="server">
                     </telerik:RadDatePicker>
				</div> 
                <div>
                    <label>礼品：</label>
                    <telerik:RadComboBox ID="RCB_Prize" runat="server"> 
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
                    <telerik:RadTextBox ID="TB_Content" runat="server"  Width="100px" />
				</div> 
                <div>
					 <label>结束日期：</label>
                     <telerik:RadDatePicker ID="RadDatePicker2" runat="server">
                     </telerik:RadDatePicker>
				</div>  
                <div>
                    <label>状态：</label>
                   <telerik:RadComboBox ID="RCB_State" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="2" Text="不限" />
                            <telerik:RadComboBoxItem Value="1" Text="已领取" />
                            <telerik:RadComboBoxItem Value="0" Text="未领取" /> 
                        </Items>
                    </telerik:RadComboBox>  
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

            <telerik:GridTemplateColumn HeaderText="用户名称" SortExpression="AutoID" >
                <ItemTemplate>
                    <asp:Label ID="Label0" runat="server" Text='<%# GetName(Eval("SimCardNo")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="12%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="SimCardNo" HeaderText="STB号">
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridTemplateColumn HeaderText="兑换的礼品" SortExpression="PrizeID" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetPrize(Eval("PrizeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="12%"></ItemStyle>
            </telerik:GridTemplateColumn>
            
             <telerik:GridBoundColumn DataField="Amount" HeaderText="数量" >
                <ItemStyle Width="8%"></ItemStyle>
            </telerik:GridBoundColumn>

             <telerik:GridTemplateColumn HeaderText="状态" SortExpression="IsTaken" >
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# GetState(Eval("IsTaken")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="8%"></ItemStyle>
            </telerik:GridTemplateColumn>   

            <telerik:GridTemplateColumn HeaderText="兑换时间(领取时间)" SortExpression="TakenTime">
                  <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# GetTime(Eval("TakenTime")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>

             <telerik:GridBoundColumn DataField="Remarks" HeaderText="备注" >
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridBoundColumn>

            <telerik:GridTemplateColumn>
                <ItemTemplate> 
                    <asp:LinkButton ID="LB_Taken" runat="server" CausesValidation="false" visible='<%# GetTaken(Eval("IsTaken")) %>'
                       OnClientClick="return blockConfirm('确认已领取吗？', event, 330, 100,'','九峰山智慧社区管理平台');" CommandName="Taken" Text='已领取' CommandArgument='<%#Eval("AutoID")%>' ></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn> 

            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
    </asp:Panel>
</asp:Content>
