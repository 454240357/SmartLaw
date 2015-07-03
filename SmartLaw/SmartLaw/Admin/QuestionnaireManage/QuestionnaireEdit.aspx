<%@ Page Language="C#"  MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeBehind="QuestionnaireEdit.aspx.cs" Inherits="SmartLaw.Admin.QuestionnaireManage.QuestionnaireEdit" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %> 
 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>  
    <script type="text/javascript">
            function rowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            } 
                
    </script> 
    <style type="text/css">
    .form label
    {
       float:none !important;
    } 
    
    #lbr
    {
       float:left !important;
    } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
<uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content> 
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
                   <div id="position">
            当前位置：问卷管理 → <h1>添加问卷</h1>
            </div>  
            <div id="nav">
                <a href="QuestionnaireList.aspx" runat="server" id="href1">检索问卷</a><span>添加问卷</span>
            </div> 
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"> </telerik:RadAjaxLoadingPanel>
           <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Panel1">
                    <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" /> 
                    </UpdatedControls>
                </telerik:AjaxSetting>                                                     
            </AjaxSettings>
            </telerik:RadAjaxManager>  
            <asp:Panel runat="server" ID="Panel1" > 
            <div class="form">
            <fieldset>
              <div>
                    <Label id="lbr">问卷标题：</Label>
                    <telerik:RadTextBox ID="RTB_Title" runat="server" SkinID="QaBox" />
              </div> 
              
              <br />
                    <Label id="lbr">问卷内容：</Label>  
                    <telerik:RadButton ID="RadButton2" runat="server" Text="暂不设置"  ButtonType="ToggleButton" ToggleType="CheckBox" onclick="RadButton2_Click" ></telerik:RadButton>
             <br />  
             </fieldset>
               <telerik:RadGrid ID="RadGrid1"    AllowSorting="True" runat="server" GridLines="None"  OnNeedDataSource="RadGrid1_NeedDataSource" 
                AutoGenerateColumns="False"  ShowFooter="true"  >
                    <MasterTableView NoMasterRecordsText="没有任何记录。">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="问题">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="content" Text='<%# Bind("q") %>' SkinID="BigTextBox" runat="server" TextMode="MultiLine" Height="100px"></telerik:RadTextBox> 
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="ImageButton1" runat="server"   OnClick="ImageButton1_Click" >添加</asp:LinkButton>
                                </FooterTemplate> 
                                <ItemStyle Width="30%"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="选项">
                                <ItemTemplate>
                                    <div>
                                        <Label>选项1：</Label>
                                        <telerik:RadTextBox ID="answer1"  Text='<%# Bind("a1") %>' runat="server" SkinID="BigTextBox2"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="answer1" Text="不能出现“|”字符！"  ForeColor="Red"
                                                ValidationExpression="^[^|]*$" Display="Dynamic"  SetFocusOnError="true"></asp:RegularExpressionValidator>	
                                     </div>
                                     <div>
                                         <Label>选项2：</Label>
                                         <telerik:RadTextBox ID="answer2" Text='<%# Bind("a2") %>' runat="server" SkinID="BigTextBox2"></telerik:RadTextBox>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="answer2" Text="不能出现“|”字符！"  ForeColor="Red"
                                                ValidationExpression="^[^|]*$" Display="Dynamic"  SetFocusOnError="true"></asp:RegularExpressionValidator>	
                                    </div>
                                    <div>
                                        <Label>选项3：</Label>
                                        <telerik:RadTextBox ID="answer3" Text='<%# Bind("a3") %>' runat="server" SkinID="BigTextBox2" ></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="answer3" Text="不能出现“|”字符！"  ForeColor="Red"
                                                ValidationExpression="^[^|]*$" Display="Dynamic"  SetFocusOnError="true"></asp:RegularExpressionValidator>	
                                    </div>
                                    <div>
                                        <Label>选项4：</Label>
                                        <telerik:RadTextBox ID="answer4"  Text='<%# Bind("a4") %>' runat="server" SkinID="BigTextBox2"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="answer4" Text="不能出现“|”字符！"  ForeColor="Red"
                                                ValidationExpression="^[^|]*$" Display="Dynamic"  SetFocusOnError="true"></asp:RegularExpressionValidator>	
                                    </div>
                                </ItemTemplate> 
                                <ItemStyle Width="45%"></ItemStyle>
                            </telerik:GridTemplateColumn> 
                            <telerik:GridTemplateColumn HeaderText="选型">
                                <ItemTemplate>
                                <div>
                                 <telerik:RadButton ID="RadButton3"  ToggleType="Radio"  runat="server" 
                                    ButtonType="StandardButton" GroupName="CheckType" AutoPostBack="false" Checked='<%# Bind("s") %>' width="50px">
                                         <ToggleStates>
                                           <telerik:RadButtonToggleState Text="单选" PrimaryIconCssClass="rbToggleRadioChecked"/>
                                           <telerik:RadButtonToggleState Text="单选" PrimaryIconCssClass="rbToggleRadio" /> 
                                         </ToggleStates>
                                </telerik:RadButton>
                                </div>
                                <br />
                                <div>
                                 <telerik:RadButton ID="RadButton4"  ToggleType="Radio" runat="server" Checked='<%# Bind("d") %>'  ButtonType="StandardButton" GroupName="CheckType" AutoPostBack="false"  width="50px">
                                         <ToggleStates>
                                           <telerik:RadButtonToggleState Text="多选" PrimaryIconCssClass="rbToggleRadioChecked"  />
                                           <telerik:RadButtonToggleState Text="多选" PrimaryIconCssClass="rbToggleRadio" /> 
                                         </ToggleStates>
                                </telerik:RadButton> 
                                </div>
                                </ItemTemplate>
                                <ItemStyle Width="15%"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="rowrem_IT" runat="server"
                                     OnClientClick="return blockConfirm('确认移除吗？', event, 330, 100,'','九峰山智慧社区管理平台');"  OnClick="rowrem_IT_Click">移除</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="10%"></ItemStyle>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
                    </ClientSettings>
                </telerik:RadGrid> 
                <div>
                    <Label>问题总数：</Label>
                    <asp:HyperLink ID="HL_Qcount" runat="server"></asp:HyperLink>
                </div> 
              <div>
                <Label>&nbsp;</Label>
                <telerik:RadButton ID="rbtnSubmit" runat="server" Text="提交"   UseSubmitBehavior="false"
                     OnClick="rbtnSubmit_Click" Height="22px" />
			 </div>  
             </div> 
             </asp:Panel> 
             
</asp:Content>

