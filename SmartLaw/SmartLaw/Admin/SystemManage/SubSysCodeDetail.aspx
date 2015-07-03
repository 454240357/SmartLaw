<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubSysCodeDetail.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.SubSysCodeDetail" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小类代码</title>
</head>
<body style="width:560px;">
    <form id="form1" runat="server">
		<div id="position">当前位置：系统管理 → 系统代码
		→ <asp:HyperLink ID="HL_SysCodeName" runat="server"></asp:HyperLink>
		→ <asp:HyperLink ID="HL_SubSysCodeName" runat="server"></asp:HyperLink>
		→ <h1>基本信息</h1>
		</div>

            <div id="nav" runat="server">
			<span>小类基本信息</span>
            <a href='<%="SubSysCodeRelationList.aspx?SubSysCodeId="+subSysCodeId%>'>查询关联</a> 
            <a href='<%="AddSubSysCodeRelation.aspx?SubSysCodeId="+subSysCodeId%>'>添加关联</a> 
		    </div>
        <telerik:RadFormDecorator ID="FormDecorator1" runat="server" >
        </telerik:RadFormDecorator>			    
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
                </Windows>  --%>   
             </telerik:radwindowmanager>
            <asp:Panel ID="Panel1" runat="server">
            <div class="form">
            <fieldset>      		
				<div>
					<label>编号：</label>
				    <asp:Label ID="Lb_SubCodeName" runat="server" Text=""></asp:Label>
				</div>				
				  
				<div>
					<label>内容：</label>
                    <telerik:RadTextBox ID="TB_SubCodeContent" runat="server" MaxLength="50"/>
					<span class="tip">*</span>
					<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" Text="代码内容必须为1-50个字符！" SetFocusOnError="true"
                        ControlToValidate="TB_SubCodeContent"></asp:RequiredFieldValidator>					
				</div>
				
				<div>
					<label>状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
				</div>  
				<div>
				<label>备注：</label>
                    <telerik:RadTextBox ID="TB_SubCodeMemo" runat="server"  Columns="50" Rows="5" SkinID="BigTextBox"
                        TextMode="MultiLine"/>															
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