<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndUserDetail.aspx.cs" Inherits="SmartLaw.Admin.EndUserManage.EndUserDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>终端用户信息</title>
    <script src="../Js/RadScript.js" type="text/javascript"></script>
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
        当前位置：终端用户管理 → <h1>终端用户信息</h1>
    </div> 
    <asp:Panel ID="Panel1" runat="server">
        <div class="form">
            <fieldset>
                <div>
                    <label>编号：</label>
                    <asp:Label ID="Lb_AutoID" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label>姓名：</label>
                    <telerik:RadTextBox ID="RTB_Name" runat="server" Width="100px" /> 
                </div>
                <div>
                    <label>STB号：</label>
                    <telerik:RadTextBox ID="RTB_SIM" runat="server" Width="100px" ReadOnly="true"/> 
                </div>
                <div>
                     <Label>身份：</Label>
                     <telerik:RadComboBox ID="RCB_Identity"  runat="Server" CheckBoxes="true" EmptyMessage="请选择"> 
                          <Localization AllItemsCheckedString="全选" />
                     </telerik:RadComboBox>
                </div> 
                <div>
                      <Label>区域：</Label> 
                      <telerik:RadDropDownTree ID="RadDropDownTree1"   runat="server"  DropDownSettings-Width="100%"   Skin="Default" DefaultMessage="请选择区域"  > 
                      </telerik:RadDropDownTree>
                </div>
                <div>
                    <label for="CateGory_Enable">状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div>
					<label>&nbsp;</label>
					<asp:Button ID="Bt_Modify" runat="server" Text=" 修 改 " 
                        onclick="Bt_Modify_Click" />
				</div>
            </fieldset>
        </div>
    </asp:Panel>
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>
</body>
</html>
