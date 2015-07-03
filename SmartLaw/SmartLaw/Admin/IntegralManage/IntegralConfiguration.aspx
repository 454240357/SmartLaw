<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="IntegralConfiguration.aspx.cs" Inherits="SmartLaw.Admin.IntegralManage.IntegralConfiguration" %>
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
        当前位置：积分管理 →<h1>积分配置</h1>
    </div> 
    <div id="nav">
                <a href="IntegralList.aspx" >积分历史</a><span>积分配置</span>
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
         <telerik:RadListBox runat="server" ID="RadListBox1" AllowTransfer="true" AllowTransferOnDoubleClick="true"  EnableDragAndDrop="true"
         TransferToID="RadListBoxDestination" TransferMode="Move" DataSortField="Memo" SelectionMode="Multiple"
         AutoPostBackOnTransfer="True"    Height="450px" Width="300px">
            <HeaderTemplate>
                <h3>已选择积分兑换项</h3>
            </HeaderTemplate>
             <EmptyMessageTemplate>无已选择项</EmptyMessageTemplate> 
            <itemtemplate>  
                   <telerik:RadTextBox ID="IgID" runat="server" Visible="false"   Text='<%# DataBinder.Eval(Container, "Value")%>' /> 
                   <Label><%# DataBinder.Eval(Container, "Text")%>：</Label>
                   <telerik:RadTextBox ID="IgPoint" runat="server"     Text='<%# DataBinder.Eval(Container.DataItem, "SYSCodeDetialContext")%>' Width="50px" /> 
            </itemtemplate> 
        </telerik:RadListBox>
        <telerik:RadListBox runat="server" ID="RadListBoxDestination"  AllowTransferOnDoubleClick="true" SelectionMode="Multiple" TransferMode="Move" EnableDragAndDrop="true"   Height="450px" Width="200px">
            <HeaderTemplate>
                <h3>待选择积分兑换项</h3> 
            </HeaderTemplate>
            <EmptyMessageTemplate>无待选择项</EmptyMessageTemplate> 
            <itemtemplate>
                  <telerik:RadTextBox ID="IgID" runat="server"  Visible="false"  Text='<%# DataBinder.Eval(Container, "Value")%>' />   
                  <Label><%# DataBinder.Eval(Container, "Text")%></Label> 
            </itemtemplate>
         </telerik:RadListBox>
         <br />
         <br />
         <div>
                <Label>&nbsp;</Label>
                <telerik:RadButton ID="RbtnSubmit" runat="server" Text="提交"   UseSubmitBehavior="false"
                     OnClick="RbtnSubmit_Click" Height="22px" />
		</div>  
    </div>
    </asp:Panel>
</asp:Content>
