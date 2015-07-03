<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSubSysCode.aspx.cs" Inherits="SmartLaw.Admin.SystemManage.AddSubSysCode" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="width:550px;">
    <form id="form1" runat="server">
<div id="position">
        当前位置：系统管理 → 系统代码
		→ <asp:HyperLink ID="HL_SysCodeName" runat="server"></asp:HyperLink>
		→ <h1>添加小类</h1>
</div> 

            <div id="nav" runat="server">
            <a href='<%="SubSysCodeList.aspx?SysCodeId="+sysCodeId%>'>查询小类代码</a> 
            <span>添加小类代码</span>
			<a href='<%="SysCodeDetail.aspx?SysCodeId="+sysCodeId%>'>大类基本信息</a> 
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
             </telerik:radwindowmanager>
        <div class="form">             
		    <asp:Panel ID="Panel1" runat="server">
		    <fieldset>      		
				<div>
					<label>所属大类编号：</label>
				    <asp:Label ID="Lb_CodeName" runat="server" Text=""></asp:Label>
				</div>
                <div>
					<label>小类编号：</label>
                    <telerik:RadTextBox ID="TB_SubCodeName" runat="server" MaxLength="50"/>															
				    <span class="tip">*</span>
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" SetFocusOnError="true"
                        ControlToValidate="TB_SubCodeName" Text="代码编号必须为1-50个字符(可以包含英文、数字和下划线)！">
                        </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TB_SubCodeName" Text="代码编号必须为1-20个字符(可以包含英文、数字和下划线)！" 
                         ValidationExpression="^[a-z0-9A-Z_-]{1,50}$" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                </div> 
				<div>
					<label>代码内容：</label>
                    <telerik:RadTextBox ID="TB_SubCodeContent" runat="server" MaxLength="50"/>															
					<span class="tip">*</span>
					<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" Text="代码内容必须为1-50个字符！" Display="Dynamic" SetFocusOnError="true"
                        ControlToValidate="TB_SubCodeContent"></asp:RequiredFieldValidator>		
				</div>
				
				<div>
					<label>是否有效：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                    <Items>
                    <telerik:RadComboBoxItem Text="有效" Value="1" />
                    <telerik:RadComboBoxItem Text="无效" Value="0" />
                    </Items>
                    </telerik:RadComboBox>
				</div>
				
				<div>
				<label>代码备注：</label>
                    <telerik:RadTextBox ID="TB_SubCodeMemo" runat="server"  Columns="50" Rows="5" SkinID="BigTextBox" TextMode="MultiLine"/>															
				</div>
				
				<div>
					<label>&nbsp;</label>
					<asp:Button ID="Bt_Add" runat="server" Text=" 添 加 " onclick="Bt_Add_Click"  />
				</div>
		    </fieldset>	
		    </asp:Panel>
		    </div>
        <telerik:RadFormDecorator ID="FormDecorator1" runat="server" >
        </telerik:RadFormDecorator>				    
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>      
</body>
</html>
