<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="NewsList.aspx.cs" Inherits="SmartLaw.Admin.NewsManage.NewsList"%>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>
    <style type="text/css">
    .inValid
    { 
        color:red;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">  
    <div id="position">
        当前位置：条目管理 → <h1>检索条目</h1> 
    </div>
    <div id="nav">
        <span>检索条目</span><a href="NewsEdit.aspx" runat="server" id="href1">添加条目</a><a href="NewsCheck.aspx" runat="server" id="A1">条目审核</a>
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
                <Label>分类：</Label>
                <telerik:RadDropDownTree ID="RadDropDownTree2" runat="server" CheckBoxes="CheckChildNodes" Skin="Default"  DropDownSettings-Width="100%"
                             DefaultMessage="请选择分类" > 
                </telerik:RadDropDownTree>
                </div> 
				<div>
					<label>标题关键字：</label>
                    <telerik:RadTextBox ID="TB_TitleKey" runat="server"  Width="100px" />
				</div>
                <div>
                    <label>数据来源：</label>
                    <telerik:RadComboBox ID="RCB_DataSource" runat="server"> 
                    </telerik:RadComboBox>
                </div>
                
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
            <div style="width:400px; float:left">
                 <div>
                    <label>状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="2" Text="不限" /> 
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div>
                    <label>审核状态：</label>
                   <telerik:RadComboBox ID="RCB_Checked" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="3" Text="不限" />
                            <telerik:RadComboBoxItem Value="1" Text="已审核" />
                            <telerik:RadComboBoxItem Value="0" Text="待审核" />
                            <telerik:RadComboBoxItem Value="2" Text="审核未通过" />
                        </Items>
                    </telerik:RadComboBox>  
                </div>
				<div>
					<label>内容关键字：</label>
                    <telerik:RadTextBox ID="TB_ContentKey" runat="server"  Width="100px" />
				</div> 
            </div>  
			  			
		    </fieldset>
        </div>
        <div>
       <telerik:RadGrid ID="RGrid_NewsList"  AllowSorting="True" AllowCustomPaging = "true"   
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"  
                        AutoGenerateColumns="False"  onitemcommand="RGrid_NewsList_ItemCommand" 
                onneeddatasource="RGrid_NewsList_NeedDataSource"  OnItemDataBound="RGrid_NewsList_ItemDataBound"
                onsortcommand="RGrid_NewsList_SortCommand">
            <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。" AllowNaturalSort="false" AllowCustomSorting="true">
            <Columns>  
            <telerik:GridBoundColumn DataField="AutoID" Visible="false">
                <ItemStyle Width="0%" ></ItemStyle>
            </telerik:GridBoundColumn>   

            <telerik:GridTemplateColumn HeaderText="标题" SortExpression="Title" >
                <ItemTemplate> 
                    <asp:LinkButton ID="LB_Detail" runat="server" CausesValidation="false"   CommandName="detail" Text='<%# Eval("Title")%>' CommandArgument='<%# Eval("AutoID") %>' ></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="30%"></ItemStyle>
            </telerik:GridTemplateColumn> 

            <telerik:GridTemplateColumn  HeaderText="分类" SortExpression="CategoryID" >
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# GetCategoryName(Eval("CategoryID")) %>'></asp:Label>
                </ItemTemplate> 
                <ItemStyle Width="20%" ></ItemStyle>
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

            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LB_Del" runat="server" CausesValidation="false" 
                        CommandName="Del" Text="删除" CommandArgument='<%# Eval("AutoID") %>'  Visible ='<%# deleteAble(Eval("AutoID")) %>'
                        OnClientClick="return blockConfirm('确认删除吗？', event, 330, 100,'','九峰山智慧社区管理平台');"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn> 
            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
	</div>
    </asp:Panel>
</asp:Content>
