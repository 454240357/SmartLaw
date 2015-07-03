<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeBehind="NoticeEdit.aspx.cs" Inherits="SmartLaw.Admin.NoticeManage.NoticeEdit" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>  
<%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
<uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
            <div id="position">
                 当前位置：公告发布管理 → <h1>公告添加</h1>
            </div>  
            <div id="nav">
                <span>公告添加</span><a href="NoticeList.aspx">公告列表</a> 
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
               <telerik:RadTabStrip ID="TabStrip1" runat="server" EnableDragToReorder="false"  MultiPageID="RadMultiPag1" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab  Text="高级模式" Value="2"></telerik:RadTab>
                        <telerik:RadTab  Text="基础模式" Value="1"></telerik:RadTab>
                    </Tabs>
               </telerik:RadTabStrip> 
               <br />
               <div class="form"> 
                        <div>
                           <div style="width:400px; float:left"> 
                                  <Label ID="lbr">类型：</Label>
                                  <telerik:RadComboBox ID="RCB_Type" runat="server"  OnSelectedIndexChanged="RCB_Type_SelectedIndexChanged" AutoPostBack="true"> 
                                  </telerik:RadComboBox>
                                  <asp:Label ID="msgLb" ForeColor="Red" Text="请切换至基础模式编辑" Visible="false" runat="server"></asp:Label>
                           </div>
                           <div id="Div1" runat="server" visible = "false" style="width:400px; float:left">
                                  <Label ID="lbr">弹走方式：</Label>
                                  <telerik:RadComboBox ID="RCB_DisType" runat="server" > 
                                  </telerik:RadComboBox>
                           </div>
                           <div id="Div2" runat="server" visible = "false" style="width:400px; float:left">
                                  <Label ID="lbr">滚动次数：</Label>
                                  <telerik:RadTextBox ID="RTB_RollTimes" runat="server" Width="135px" />
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="RTB_RollTimes" Text="必须为正整数！"  ForeColor="Red"
                                                ValidationExpression="^\+?[1-9][0-9]*$" Display="Dynamic"  SetFocusOnError="true"></asp:RegularExpressionValidator>	
                                  <telerik:RadButton ID="RadButton2" runat="server" Text="不限"   Checked="true"
                                            ButtonType="ToggleButton" ToggleType="CheckBox" onclick="RadButton2_Click" ></telerik:RadButton>
                           </div>
                       </div>
                       <br />
                       <br />
                       <div>   
                           <div style="width:400px; float:left"> 
                                 <label ID="lbr">有效期开始：</label>
                                 <telerik:RadDatePicker ID="RadDatePicker1" runat="server">
                                 </telerik:RadDatePicker> 
                           </div>
                           <div style="width:400px; float:left">
                                 <label ID="lbr">有效期截止：</label>
                                 <telerik:RadDatePicker ID="RadDatePicker2" runat="server">
                                 </telerik:RadDatePicker>  
                            </div> 
                       </div> 
                       <div>
                            <label ID="lbr">发送对象筛选：</label> 
                            <br />
                            <br /> 
                            <telerik:RadButton ID="RadButton5" runat="server" Text="不限" 
                                ButtonType="ToggleButton" ToggleType="CheckBox" onclick="RadButton5_Click" ></telerik:RadButton> 
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
                                <telerik:RadButton ID="RadButton3"  ToggleType="Radio"  runat="server" ButtonType="StandardButton" GroupName="CheckType" AutoPostBack="False">
                                         <ToggleStates>
                                           <telerik:RadButtonToggleState Text="且" PrimaryIconCssClass="rbToggleRadioChecked"/>
                                           <telerik:RadButtonToggleState Text="且" PrimaryIconCssClass="rbToggleRadio" /> 
                                         </ToggleStates>
                                </telerik:RadButton>
                                 <telerik:RadButton ID="RadButton4"  ToggleType="Radio" runat="server" ButtonType="StandardButton" GroupName="CheckType" AutoPostBack="False" Checked="true">
                                         <ToggleStates>
                                           <telerik:RadButtonToggleState Text="或" PrimaryIconCssClass="rbToggleRadioChecked"  />
                                           <telerik:RadButtonToggleState Text="或" PrimaryIconCssClass="rbToggleRadio" /> 
                                         </ToggleStates>
                                </telerik:RadButton> 
                            </div>
                        </div> 
                       <div>
                            <label ID="lbr">内容：</label> 
                       </div> 
                </div> 
               <telerik:RadMultiPage ID="RadMultiPag1" runat="server" SelectedIndex="0" > 
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <br />  
                            <div style="padding-left:60px;">
                                    <telerik:RadEditor ID="ContentEditor" runat="server"  Height="400px"  MaxTextLength="200" ExternalDialogsPath="EditorDialogs" 
                                        ContentAreaMode="Iframe" NewLineMode="P" AutoResizeHeight="false" BorderStyle="NotSet" ToolsFile="tools.xml" ContentFilters="None"> 
                                            <ImageManager  ViewPaths="~/Images" UploadPaths="~/Images" DeletePaths="~/Images" MaxUploadFileSize="204800" EnableImageEditor="false"  >
                                            </ImageManager>
                                            <ContextMenus>
                                                <telerik:EditorContextMenu TagName="IMG" Enabled="false"></telerik:EditorContextMenu>
                                                <telerik:EditorContextMenu TagName="TABLE"> 
                                                      <telerik:EditorTool Name="DeleteTable" />
                                                 </telerik:EditorContextMenu>
                                                 <telerik:EditorContextMenu TagName="TD">
                                                      <telerik:EditorTool Name="InsertRowAbove" />
                                                      <telerik:EditorTool Name="InsertRowBelow" />
                                                      <telerik:EditorTool Name="DeleteRow" />
                                                      <telerik:EditorTool Name="InsertColumnLeft" />
                                                      <telerik:EditorTool Name="InsertColumnRight" />
                                                      <telerik:EditorTool Name="MergeColumns" />
                                                      <telerik:EditorTool Name="MergeRows" />
                                                      <telerik:EditorTool Name="SplitCell" />
                                                      <telerik:EditorTool Name="DeleteCell" /> 
                                                </telerik:EditorContextMenu>
                                                <telerik:EditorContextMenu TagName="A"> 
                                                      <telerik:EditorTool Name="Unlink" />
                                                </telerik:EditorContextMenu>
                                            </ContextMenus>
                                    </telerik:RadEditor> 
                            </div>
                            <div style="padding-left:100px;">
                                     <telerik:RadButton ID="RadButton1" runat="server" Text="适应机顶盒格式" 
                                         ButtonType="ToggleButton" ToggleType="CheckBox"   AutoPostBack="true" 
                                         onclick="RadButton1_Click" style="height: 15px" >
                                     </telerik:RadButton> 
                                     <telerik:RadButton ID="SaveEditBtn" runat="server" Text="应用编辑"  Visible="false" 
                                         onclick="SaveEditBtn_Click" />
                             </div>   
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView1" runat="server"> 
                            <br />  
                            <div style="padding-left:100px;">
                                   <telerik:RadTextBox ID="ContentText" runat="server"  MaxLength="100"  TextMode="MultiLine" Height="400px" SkinID="NewsTextBox" />
                            </div> 
                        </telerik:RadPageView>
                </telerik:RadMultiPage> 
                <div style="padding-left:100px;">
                     <telerik:RadButton ID="RbtnSubmit" runat="server" Text="提交"  UseSubmitBehavior="false" onclick="RbtnSubmit_Click" />
			    </div>   
            </asp:Panel> 
</asp:Content>
