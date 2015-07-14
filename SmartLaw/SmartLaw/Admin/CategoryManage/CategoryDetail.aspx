<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryDetail.aspx.cs" Inherits="SmartLaw.Admin.CategoryManage.CategoryDetail" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="width:550px;">
    <form id="form1" runat="server">
            <div id="position">
            当前位置：分类管理 → 分类列表 → 
            <asp:HyperLink ID="HL_CategoryName" runat="server"></asp:HyperLink> → <h1>分类编辑</h1>
            </div>
            
            <div id="nav" runat="server">
            <span>分类信息</span> 
            <a runat="server" id="href1">添加子类</a>
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
<%--                <Windows>
                    <telerik:RadWindow ID="DeviceDetailWindow" runat="server" Title="设备详细信息" AutoSize="false"
                      ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true"/>
                </Windows> --%>
             </telerik:radwindowmanager>

		    <asp:Panel ID="Panel1" runat="server">
            <div class="form">
            <fieldset>      		
				<div>
					<label>编号：</label>
					<telerik:RadTextBox ID="TB_CategoryID" runat="server" Enabled="false"/>
				</div> 
				<div>
					<label>父类：</label>
					<telerik:RadTextBox ID="TB_ParentCategory" runat="server"  Enabled="false"/> 
				</div> 
				<div>
					<label>名称：</label>
					<telerik:RadTextBox ID="TB_CategoryName" runat="server" TextMode="SingleLine"/> 
				</div> 
				<div>
					<label for="CateGory_Enable">状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  AutoPostBack="true" 
                        onselectedindexchanged="RCB_Enable_SelectedIndexChanged" >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
                    <label id="img" runat="server" visible="false" style="color:Red; float:none" ></label> 
				</div>
                <div>
					<label>备注：</label>
					<telerik:RadTextBox ID="TB_Memo" runat="server" TextMode="MultiLine" /> 
				</div>
				<div>
					<label>&nbsp;</label>
					<asp:Button ID="Bt_Modify" runat="server" Text=" 修 改 " 
                        onclick="Bt_Modify_Click" />
				</div>
		    </fieldset>
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
