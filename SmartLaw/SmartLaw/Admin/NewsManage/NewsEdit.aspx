<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeBehind="NewsEdit.aspx.cs" Inherits="SmartLaw.Admin.NewsManage.NewsEdit" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %> 
 <%@ Register Src="LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <script src="../js/RadScript.js" type="text/javascript"></script>  
<script type="text/javascript">
    Telerik.Web.UI.Editor.ClipboardImagesProvider.prototype.supportsClipboardData = function (event) { 
        return false;
    }  
    function OnClientPasteHtml(editor, args) {
        var commandName = args.get_commandName(); 
        if (commandName == "Paste" || commandName == "PasteFromWord") {
            var value = args.get_value(); 
            if (value.match(/\<img .+?\>/ig)) {
                //var strippedContent = value.replace(/\<img .+?\>/ig, "");
                alert("图片不能直接复制进编辑器，请打开菜单栏图片选择器插入图片！！");
                args.set_cancel(true);
            }
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
<uc1:LeftMenu ID="LeftMenu2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
                   <div id="position">
            当前位置：条目管理 → <h1>添加条目</h1>
            </div>  
            <div id="nav">
                <a href="NewsList.aspx" runat="server" id="href1">检索条目</a><span>添加条目</span> <a href="NewsCheck.aspx" runat="server" id="A1">条目审核</a>
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
              <div style="padding-left:60px">
              <Label>分类：</Label>
            <telerik:RadDropDownTree ID="RadDropDownTree2" runat="server" Skin="Default" DropDownSettings-Width="100%"   DefaultMessage="请选择分类"> 
              </telerik:RadDropDownTree>
              </div> 
             <telerik:RadMultiPage ID="RadMultiPag1" runat="server" SelectedIndex="0" > 
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                    <br />
                      <div style="padding-left:60px;">
                      <label>标题：</label> 
					  <telerik:RadTextBox ID="Title2" runat="server" SkinID="NewsTextBox" />
                      </div>
                      <div style="padding-left:60px">
                      <label>内容：</label>  
                      <div> 
                      <telerik:RadEditor ID="contentEditor" runat="server"  Height="440px"   ExternalDialogsPath="EditorDialogs"  OnClientPasteHtml="OnClientPasteHtml" 
                       ContentAreaMode="Iframe" NewLineMode="P" AutoResizeHeight="false" 
                              BorderStyle="NotSet" ToolsFile="tools.xml" ContentFilters="None" 
                              onfiledelete="contentEditor_FileDelete"> 
                          <ImageManager   ViewPaths="~/Images"  UploadPaths="~/Images" DeletePaths="~/Images" MaxUploadFileSize="204800" EnableImageEditor="false"  >
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
                      <div style="padding-left:40px; ">
                     <telerik:RadButton ID="RadButton1" runat="server" Text="适应机顶盒格式" 
                              ButtonType="ToggleButton" ToggleType="CheckBox"   AutoPostBack="true" 
                              OnCheckedChanged="RadButton1_CheckedChanged"></telerik:RadButton>
                      
                     <telerik:RadButton ID="saveEditBtn" runat="server" Text="应用编辑"  onclick="saveEditBtn_Click" Visible="false" />
                     </div>
                     </div>
                     </div> 
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="RadPageView1" runat="server"> 
                    <br />
                    <div style="padding-left:60px;">
					<label>标题：</label>
					 <telerik:RadTextBox ID="Title1" runat="server" SkinID="NewsTextBox"  />
				    </div>
                    <div style="padding-left:60px">
                    <label>内容：</label>
                    </div>
                    <div style="padding-left:100px"> 
                    <telerik:RadTextBox ID="contentText" runat="server"  TextMode="MultiLine" Height="450px" SkinID="NewsTextBox" />
                     </div>
                     </telerik:RadPageView>
             </telerik:RadMultiPage>   
             <div style="padding-left:100px"> 
             <br />
             <telerik:RadButton ID="rbtnSubmit" runat="server" Text="提交"   UseSubmitBehavior="false"
                     OnClick="rbtnSubmit_Click" Height="22px" />
			 </div>   
             </asp:Panel> 
             
</asp:Content>

