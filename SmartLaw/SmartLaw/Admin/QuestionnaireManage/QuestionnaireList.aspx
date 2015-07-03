<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"  CodeBehind="QuestionnaireList.aspx.cs" Inherits="SmartLaw.Admin.QuestionnaireManage.QuestionnaireList" %>
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
        当前位置：问卷管理 →<h1>检索问卷</h1>
    </div> 
    <div id="nav">
        <span>检索问卷</span><a href="QuestionnaireEdit.aspx" runat="server" id="href1">添加问卷</a>
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
                <div>
                     <label>标题关键字：</label>  
                     <telerik:RadTextBox ID="TB_TitleKey" runat="server"  Width="100px" />
                </div>
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
				    <label>&nbsp;</label>
                    <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" ></telerik:RadButton>
                </div>				
		    </fieldset>
        </div>
        <div>
       <telerik:RadGrid ID="RadGrid1"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"  onitemcommand="RadGrid1_ItemCommand" 
                        AutoGenerateColumns="False"   
                onneeddatasource="RadGrid1_NeedDataSource" 
                OnitemDataBound="RadGrid1_ItemDataBound" CellSpacing="0" Culture="zh-CN" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>   

            <telerik:GridTemplateColumn HeaderText="问卷编号" SortExpression="ID" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"QuestionnaireDetail.aspx?ID=" + Eval("ID") %>', 'SubModalWindow'); return false;"><%# Eval("ID")%></a> 
                </ItemTemplate>
                <ItemStyle Width="15%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn DataField="Title" HeaderText="标题">
                <ItemStyle Width="50%"></ItemStyle>
            </telerik:GridBoundColumn>
            
            <telerik:GridTemplateColumn HeaderText="状态" SortExpression="IsValid" >
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# GetValid(Eval("isValid")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn> 
             
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LB_Del" runat="server" CausesValidation="false" 
                        CommandName="Del" Text="删除" CommandArgument='<%# Eval("ID") %>'  Visible ='<%# deleteAble(Eval("ID")) %>'
                        OnClientClick="return blockConfirm('确认删除吗？删除后关于该问卷的用户的回答记录也将被删除！！', event, 330, 100,'','九峰山智慧社区管理平台');"></asp:LinkButton>
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                        CommandName="Sts" Text="统计" CommandArgument='<%# Eval("ID") %>'  Visible ='<%# StsAble(Eval("ID")) %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="15%"></ItemStyle>
            </telerik:GridTemplateColumn>     

            </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
            </ClientSettings>
        </telerik:RadGrid> 
	</div>
    </asp:Panel>
</asp:Content>
