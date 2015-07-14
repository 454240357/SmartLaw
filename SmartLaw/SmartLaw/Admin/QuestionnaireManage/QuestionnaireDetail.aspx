<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireDetail.aspx.cs" Inherits="SmartLaw.Admin.QuestionnaireManage.QuestionnaireDetail" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="width: 950px;">
    <form id="form1" runat="server">
            <div id="position">
                当前位置： 问卷管理→
               <h1>问卷详情</h1>
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
        <div>
            <asp:Panel runat="server" ID="Panel1" > 
            <div class="form">
            <fieldset>
              <div>
                    <Label>问卷标题：</Label>
                    <telerik:RadTextBox ID="RTB_Title" runat="server" SkinID="QaBox2" />
              </div>
              <br /> 
               <div> 
                     <label>状态：</label>
                     <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                          <Items>
                              <telerik:RadComboBoxItem Value="1" Text="有效" />
                              <telerik:RadComboBoxItem Value="0" Text="无效" />
                           </Items>
                     </telerik:RadComboBox>
                     <telerik:RadButton ID="btn_ChangeValid" runat="server" Text="点击修改状态" onclick="btn_ChangeValid_Click" />
               </div> 
              <br />
                    <Label id="Label2">问卷内容：</Label>  
                    <telerik:RadButton ID="RadButton2" runat="server" Text="暂不设置"  ButtonType="ToggleButton" ToggleType="CheckBox" onclick="RadButton2_Click" ></telerik:RadButton>
             <br />  
             </fieldset>
               <telerik:RadGrid ID="RadGrid1"  AllowSorting="True" runat="server" GridLines="None"  OnNeedDataSource="RadGrid1_NeedDataSource" 
                AutoGenerateColumns="False"   ShowFooter="true"  >
                    <MasterTableView NoMasterRecordsText="没有任何记录。">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="id" Visible="false">
                                    <ItemTemplate>
                                            <telerik:RadTextBox ID="idbox" Text='<%# Bind("id") %>' runat="server" ></telerik:RadTextBox> 
                                    </ItemTemplate>
                                    <ItemStyle Width="0%" > 
                                    </ItemStyle>
                            </telerik:GridTemplateColumn>
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
                                    <asp:LinkButton ID="rowrem_IT" runat="server" OnClientClick="return blockConfirm('确认移除吗？', event, 330, 100,'','九峰山智慧社区管理平台');"   OnClick="rowrem_IT_Click">移除</asp:LinkButton>
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
        </div>
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script> 
</body>
</html>
