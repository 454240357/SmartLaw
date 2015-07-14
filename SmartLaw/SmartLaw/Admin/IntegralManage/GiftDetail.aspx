<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftDetail.aspx.cs" Inherits="SmartLaw.Admin.IntegralManage.GiftDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>礼品编辑</title>
    <script src="../Js/RadScript.js" type="text/javascript"></script>
    <script type="text/jscript">
        function KeyPress(sender, args) {
            if (args.get_keyCharacter() == sender.get_numberFormat().DecimalSeparator) {
                args.set_cancel(true);
            }
        } 
    </script> 
</head>
<body style="width: 580px;">
    <form id="form1" runat="server">
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
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
     <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false"  />
     <telerik:radwindowmanager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1">
        <Localization OK="好的" Cancel="取消" Close="关闭" Maximize="最大化" Minimize="最小化" Reload="刷新" Restore="还原" PinOn="固定" PinOff="解除固定" Yes="是" No="否" />
     </telerik:radwindowmanager>
    <div id="position">
        当前位置：积分管理 →<h1>礼品编辑</h1>
    </div> 
    <asp:Panel ID="Panel1" runat="server">
         <div class="form">
            <fieldset>
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
                         <telerik:RadNumericTextBox ID="GStockn" runat="server"  MinValue="1"  Width="135px"   ShowSpinButtons="true" ToolTip="滚轮调整数值">
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
                        <label>状态：</label>
                        <telerik:RadComboBox ID="RCB_Enable" runat="server"  AutoPostBack="true"
                            onselectedindexchanged="RCB_Enable_SelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="有效" />
                                <telerik:RadComboBoxItem Value="0" Text="无效" />
                            </Items>
                        </telerik:RadComboBox>
                    </div> 
                    <div>
					        <label>&nbsp;</label>
					        <asp:Button ID="Bt_Modify" runat="server" Text=" 修 改 " onclick="Bt_Modify_Click" />
		           </div>
            </fieldset>
        </div>
       <div> 
	</div>
    </asp:Panel>
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>
</body>
</html>
