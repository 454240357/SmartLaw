<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/MasterPage.master" CodeBehind="AddEndUser.aspx.cs" Inherits="SmartLaw.Admin.EndUserManage.AddEndUser" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>  
 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:LeftMenu ID="LeftMenu2" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
            <div id="position">
            当前位置：终端用户管理 → <h1>添加终端用户</h1>
            </div>  
            <div id="nav">
                <a href="EndUserList.aspx" runat="server" id="href1">检索终端用户</a><span>添加终端用户</span>
            </div>
            
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"> </telerik:RadAjaxLoadingPanel>
           <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Panel1">
                    <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>                                                     
            </AjaxSettings>
            </telerik:RadAjaxManager>  
            <asp:Panel runat="server" ID="Panel1" >  
             <div class="form">
                <fieldset>   
                    <br />
                    <div>
                          <Label>身份：</Label>
                          <telerik:RadComboBox ID="RCB_Identity"  runat="Server" CheckBoxes="true" EmptyMessage="请选择"> 
                                <Localization AllItemsCheckedString="全选" />
                          </telerik:RadComboBox>
                    </div> 
                    <div>
                         <Label>区域：</Label> 
                         <telerik:RadDropDownTree ID="RadDropDownTree1"   runat="server"  DropDownSettings-Width="100%"  Skin="Default" DefaultMessage="请选择区域"  > 
                          </telerik:RadDropDownTree>
                     </div>   
                                  <div >
                                      <label>姓名：</label> 
					                  <telerik:RadTextBox ID="RTB_Name" runat="server" Width="100px" />
                                  </div> 
                                  <div >
                                      <label>STB号：</label> 
					                  <telerik:RadTextBox ID="RTB_SIM" runat="server" Width="100px" />
                                  </div>  
                     <div style="padding-left:100px"> 
                         <br />
                         <telerik:RadButton ID="SubmitBtn" runat="server" Text="提交"   
                                 UseSubmitBehavior="false"  Height="22px" onclick="SubmitBtn_Click" />
			         </div> 
                   </fieldset>
                </div>  
             </asp:Panel> 
             
</asp:Content>

