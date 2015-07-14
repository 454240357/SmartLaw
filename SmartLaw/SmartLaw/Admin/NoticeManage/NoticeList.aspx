<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="NoticeList.aspx.cs" Inherits="SmartLaw.Admin.NoticeManage.NoticesList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>
    <style type="text/css">
        .list-panel {
        position:relative;
        }
        .RadListBox
        {
            margin: 0 auto !important;
        }
        .getCheckedItems {
            left: 300px;
            position: absolute;
            top: 0px;
        }
        .form label
        {
        	float:none !important;
        }
        #lbr
        {
        	float:left !important;
        } 
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
        当前位置：公告发布管理 → <h1>公告列表</h1> 
    </div>
    <div id="nav">
        <a href="NoticeEdit.aspx">公告添加</a><span>公告列表</span> 
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
                       <div style="width:400px; float:left"> 
                             <label ID="lbr">有效期>=：</label>
                             <telerik:RadDatePicker ID="RadDatePicker1" runat="server">
                             </telerik:RadDatePicker> 
                       </div>
                       <div style="width:400px; float:left">
                             <label ID="lbr"><=：</label>
                             <telerik:RadDatePicker ID="RadDatePicker2" runat="server">
                             </telerik:RadDatePicker>  
                       </div> 
                </div> 
                <div>   
                       <div style="width:400px; float:left"> 
                             <label ID="lbr">失效期>=：</label>
                             <telerik:RadDatePicker ID="RadDatePicker3" runat="server">
                             </telerik:RadDatePicker> 
                       </div>
                       <div style="width:400px; float:left">
                             <label ID="lbr"><=：</label>
                             <telerik:RadDatePicker ID="RadDatePicker4" runat="server">
                             </telerik:RadDatePicker>  
                       </div> 
                </div>
                <div> 
				    <div style="width:400px; float:left"> 
					    <label ID="lbr">标题关键字：</label>
                        <telerik:RadTextBox ID="RTB_TitleKey" runat="server" MaxLength="20" Width="100px" />
				    </div>
                    <div style="width:400px; float:left"> 
					    <label ID="lbr">内容关键字：</label>
                        <telerik:RadTextBox ID="RTB_ContentKey" runat="server" MaxLength="20" Width="100px" />
				    </div>
                </div>  
                <label ID="lbr">发送对象筛选：</label> 
                            <br />
                            <br />  
                            <div style="width:450px;padding-left:100px">
                            <telerik:RadTabStrip runat="server" ID="RadTabStrip1" Orientation="HorizontalTop"  SelectedIndex="0" MultiPageID="RadMultiPage2"  Width="400px">
                                <Tabs>
                                    <telerik:RadTab Text="身份">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="区域">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="用户">
                                    </telerik:RadTab>
                                </Tabs> 
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage2" SelectedIndex="0" Width="400px">
                                <telerik:RadPageView runat="server" ID="RadPageView3" >
                                <div class="list-panel">
                                    <telerik:RadListBox ID="RadListBox1" runat="server" CheckBoxes="true" ShowCheckAll="true" Width="400px" Height="100px">
                                        
                                    </telerik:RadListBox>
                                </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView runat="server" ID="RadPageView4" >
                                    <telerik:RadDropDownTree ID="RadDropDownTree1"   runat="server"  DropDownSettings-Width="100%"    
                                                   Skin="Default" DefaultMessage="请选择区域"  > 
                                     </telerik:RadDropDownTree>
                                </telerik:RadPageView>
                                <telerik:RadPageView runat="server" ID="RadPageView5" >
                                     <telerik:RadTextBox ID="RTB_SIM" runat="server"  TextMode="MultiLine" Height="100px" SkinID="SimTextBox" EmptyMessage="输入用户STB号（以逗号分隔）..."/>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage> 
                            </div> 
                            <div>
                                <label ID="lbr">筛选关系：</label> 
                                <telerik:RadButton ID="RadButton3"  ToggleType="Radio"  runat="server" 
                                    ButtonType="StandardButton" GroupName="CheckType" AutoPostBack="false"  
                                    Width="63px" >
                                         <ToggleStates>
                                           <telerik:RadButtonToggleState Text="且" PrimaryIconCssClass="rbToggleRadioChecked"/>
                                           <telerik:RadButtonToggleState Text="且" PrimaryIconCssClass="rbToggleRadio" /> 
                                         </ToggleStates>
                                </telerik:RadButton>
                                 <telerik:RadButton ID="RadButton4"  ToggleType="Radio" runat="server" 
                                    ButtonType="StandardButton" GroupName="CheckType" AutoPostBack="false" 
                                    Checked="true" Width="63px" >
                                         <ToggleStates>
                                           <telerik:RadButtonToggleState Text="或" PrimaryIconCssClass="rbToggleRadioChecked"  />
                                           <telerik:RadButtonToggleState Text="或" PrimaryIconCssClass="rbToggleRadio" /> 
                                         </ToggleStates>
                                </telerik:RadButton> 
                            </div>
                <div>
                    <label ID="lbr">状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="2" Text="不限" /> 
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
				<div>
				    <label ID="lbr">&nbsp</label>
                    <telerik:RadButton ID="Bt_Search" runat="server" Text=" 查 询 " 
                        onclick="Bt_Search_Click" ></telerik:RadButton>
                </div>				
		    </fieldset> 
       <telerik:RadGrid ID="RGrid_NoticeList"  AllowSorting="True"
            AllowPaging="True" PageSize="15" runat="server" GridLines="None"   OnItemDataBound="RGrid_NewsList_ItemDataBound"
                        AutoGenerateColumns="False"  onitemcommand="RGrid_NoticeList_ItemCommand" 
                onneeddatasource="RGrid_NoticeList_NeedDataSource">
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <MasterTableView NoMasterRecordsText="没有任何记录。">
            <Columns>  
            <telerik:GridBoundColumn DataField="AutoID"   Display="false">
                <ItemStyle Width="0%" ></ItemStyle>
            </telerik:GridBoundColumn>   

            <telerik:GridTemplateColumn HeaderText="内容" SortExpression="Title" >
                <ItemTemplate>
                    <a href="#" onclick="openRadWindow( '<%#"NoticeDetail.aspx?AutoId=" + Eval("AutoID") %>', 'SubModalWindow')"><%# GetContent(Eval("AutoID"))%></a>
                </ItemTemplate>
                <ItemStyle Width="25%"></ItemStyle>
            </telerik:GridTemplateColumn>  
            <telerik:GridTemplateColumn HeaderText="类型" SortExpression="MessageType" >
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# GetMessageType(Eval("MessageType")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn>   
            <telerik:GridTemplateColumn HeaderText="可用性" SortExpression="IsValid" >
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# GetValid(Eval("isValid")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%"></ItemStyle>
            </telerik:GridTemplateColumn>     
            
             <telerik:GridTemplateColumn HeaderText="最后编辑时间" SortExpression="LastModifyTime">
                  <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetTime(Eval("LastModifyTime")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="20%"></ItemStyle>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LB_Del" runat="server" CausesValidation="false" 
                        CommandName="Del" Text="删除" CommandArgument='<%# Eval("AutoID") %>'  Visible ='<%# DeleteAble(Eval("AutoID")) %>'
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
