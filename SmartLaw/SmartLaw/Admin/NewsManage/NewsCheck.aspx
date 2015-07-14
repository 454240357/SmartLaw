<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="NewsCheck.aspx.cs" Inherits="SmartLaw.Admin.NewsManage.NewsCheck" %>
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
            当前位置：条目管理 → <h1>条目审核</h1>
            </div>  
            <div id="nav">
                <a href="NewsList.aspx" runat="server" id="href1">检索条目</a><a href="NewsEdit.aspx" runat="server" id="A1">添加条目</a> <span>条目审核</span>
            </div>
            
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"> </telerik:RadAjaxLoadingPanel>
           <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings> 
                 <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
                </telerik:AjaxSetting>                         
            </AjaxSettings>
            </telerik:RadAjaxManager>  
            <asp:Panel ID="Panel1" runat="server">
                <div class="form">
                    <fieldset> 
                        <div style="width:400px; float:left"> 
                            <div>
					            <label>发布人：</label>
                                <telerik:RadComboBox ID="RCB_Publisher"  runat="Server"> 
                                </telerik:RadComboBox>
				            </div>  
                            <div>
				                <label>&nbsp</label>
                                <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                                    onclick="Bt_Search_Click" ></telerik:RadButton>
                           </div>	
                        </div> 
		            </fieldset>
                </div> 
               <telerik:RadGrid ID="RGrid_NewsList"  AllowSorting="True"   AllowMultiRowSelection="true"  
                    AllowPaging="True" PageSize="15" runat="server" GridLines="None" onneeddatasource="RGrid_NewsList_NeedDataSource"  
                                AutoGenerateColumns="False"    >
                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                    <MasterTableView NoMasterRecordsText="没有任何记录。">
                    <Columns>  
                     <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" >
                    </telerik:GridClientSelectColumn> 
                    <telerik:GridTemplateColumn HeaderText="标题" SortExpression="Title" >
                        <ItemTemplate>
                            <a href="#" onclick="openRadWindow( '<%#"NewsDetail.aspx?AutoId=" + Eval("AutoID") %>', 'SubModalWindow')"><%# Eval("Title")%></a>
                        </ItemTemplate>
                        <ItemStyle Width="30%"></ItemStyle>
                    </telerik:GridTemplateColumn> 

                    <telerik:GridTemplateColumn  HeaderText="分类" SortExpression="CategoryID" >
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# GetCategoryName(Eval("CategoryID")) %>'></asp:Label>
                        </ItemTemplate> 
                        <ItemStyle Width="30%" ></ItemStyle>
                    </telerik:GridTemplateColumn> 

                    <telerik:GridTemplateColumn HeaderText="可用性" SortExpression="IsValid" >
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# GetValid(Eval("isValid")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </telerik:GridTemplateColumn> 

                    <telerik:GridTemplateColumn HeaderText="审核状态" SortExpression="Checked" >
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# GetChecked(Eval("Checked")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </telerik:GridTemplateColumn>     
            
                     <telerik:GridTemplateColumn HeaderText="编辑时间" SortExpression="LastModifyTime">
                          <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# GetTime(Eval("LastModifyTime")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20%"></ItemStyle>
                    </telerik:GridTemplateColumn> 
                    <telerik:GridBoundColumn DataField="AutoID" Display="false">
                        <ItemStyle Width="0%" ></ItemStyle>
                    </telerik:GridBoundColumn>   
                    </Columns>
                    </MasterTableView>
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True"></Selecting>
                    </ClientSettings>
                </telerik:RadGrid>  
            <br />
            <div>
            <telerik:RadButton ID="RadButton2"  ToggleType="Radio"  runat="server"
                    ButtonType="StandardButton" GroupName="CheckType" onclick="RadButton2_Click">
                     <ToggleStates>
                       <telerik:RadButtonToggleState Text="已审核" PrimaryIconCssClass="rbToggleRadioChecked" Width="100px" />
                       <telerik:RadButtonToggleState Text="已审核" PrimaryIconCssClass="rbToggleRadio" Width="100px" /> 
                     </ToggleStates>
            </telerik:RadButton>
             <telerik:RadButton ID="RadButton3"  ToggleType="Radio"  runat="server"
                    ButtonType="StandardButton" GroupName="CheckType" 
                    onclick="RadButton3_Click" >
                     <ToggleStates>
                       <telerik:RadButtonToggleState Text="审核未通过" PrimaryIconCssClass="rbToggleRadioChecked" Width="100px" />
                       <telerik:RadButtonToggleState Text="审核未通过" PrimaryIconCssClass="rbToggleRadio" Width="100px" /> 
                     </ToggleStates>
            </telerik:RadButton> 
            </div>
            <br /> 
            <div runat="server" id ="cmemo" visible="false">
                    <label>审核备注：</label>
                    <br />
                    <telerik:RadTextBox ID="TB_CheckMemo" runat="server" Columns="80" Rows="5" TextMode="MultiLine" SkinID="BigTextBox" /> 
            </div> 
            <br />
            
            <telerik:RadButton runat="server"  ID="RadButton1"  Text="提交" 
                    UseSubmitBehavior="false" onclick="RadButton1_Click" ></telerik:RadButton>
             
            </asp:Panel>
</asp:Content>
