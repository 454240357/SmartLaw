<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="GiftEdit.aspx.cs" Inherits="SmartLaw.Admin.IntegralManage.GiftEdit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>
     <script type="text/jscript">
         function KeyPress(sender, args) {
             if (args.get_keyCharacter() == sender.get_numberFormat().DecimalSeparator) {
                 args.set_cancel(true);
             }
         } 
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">  
    <div id="position">
        当前位置：积分管理 →<h1>礼品定义</h1>
    </div> 
    <div id="nav">
                <a href="IntegralList.aspx" >积分历史</a><a href="IntegralConfiguration.aspx" >积分配置</a>
                <a href="GiftList.aspx">礼品查询</a><span>礼品定义</span>
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
                 <label>礼品名称：</label> 
				 <telerik:RadTextBox ID="GName" runat="server" />
            </div>
            <div>
                 <label>描述：</label> 
				 <telerik:RadTextBox ID="GMemo" runat="server"  Columns="80" Rows="5" TextMode="MultiLine" SkinID="BigTextBox"/>
            </div>
            <div>
                 <label>单位：</label> 
				 <telerik:RadTextBox ID="GUnit" runat="server" />
            </div>
            <div>
                 <label>库存：</label> 
				  <telerik:RadNumericTextBox ID="GStockn" runat="server"  MinValue="1"  Width="135px"  ShowSpinButtons="true" ToolTip="滚轮调整数值" >
                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />   
                        <ClientEvents OnKeyPress="KeyPress" /> 
                  </telerik:RadNumericTextBox>
            </div>
            <div>
                 <label>积分兑换值：</label> 
			     <telerik:RadNumericTextBox ID="GPointsn" runat="server"  MinValue="1"  Width="135px"  ShowSpinButtons="true" ToolTip="滚轮调整数值">
                     <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />   
                     <ClientEvents OnKeyPress="KeyPress" /> 
                 </telerik:RadNumericTextBox>
            </div> 
            <div>
                <Label>&nbsp;</Label>
                <telerik:RadButton ID="RbtnSubmit" runat="server" Text="提交"   UseSubmitBehavior="false"
                     OnClick="RbtnSubmit_Click" Height="22px" />
		    </div> 
        </div>
         
    </asp:Panel>
</asp:Content>
