<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="SmartLaw.Admin.NewsManage.NewsDetail" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"> 
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
    <title></title>  
</head> 
<body  style="width: 850px;"> 
    <form id="form1" runat="server">
            <div id="position">
            当前位置：条目管理 → <a href="NewsList.aspx">检索条目</a> →
           <h1>条目详细</h1>
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

		    <asp:Panel ID="Panel1" runat="server"> 
            <telerik:RadButton runat="server" ID="btn_ReEdit" Text="重新编辑" 
                    onclick="btn_ReEdit_Click" />
           <telerik:RadButton runat="server" ID="btn_CancelEdit" Text="取消编辑"  Visible="false"
                    onclick="btn_ReEdit_Click" /> 
            <div class="form">
            <fieldset> 
            
                <div> 
					<label>分类：</label>
					<telerik:RadTextBox ID="TB_Category" runat="server"  ReadOnly="true" /> 
                    <telerik:RadDropDownTree ID="RadDropDownTree2" runat="server" Skin="Default"   DropDownSettings-Width="100%"
                        DefaultMessage="不选则维持原分类" Visible="false" > 
                    </telerik:RadDropDownTree>
				</div> 
				<div>
					<label>发布人：</label>
					<telerik:RadTextBox ID="TB_Publisher" runat="server"  ReadOnly="true" /> 
                </div>
                <div>
					<label>数据来源：</label>
					<telerik:RadTextBox ID="TB_DataSource" runat="server"  ReadOnly="true" /> 
                </div>
                <div>
                    <label for="CateGory_Enable">状态：</label>
                    <telerik:RadComboBox ID="RCB_Enable" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="有效" />
                            <telerik:RadComboBoxItem Value="0" Text="无效" />
                        </Items>
                    </telerik:RadComboBox>
                    <telerik:RadButton ID="btn_ChangeValid" runat="server" Text="点击修改状态" 
                        onclick="btn_ChangeValid_Click" />
                </div> 
                <div>
                    <label>审核状态：</label>
                   <telerik:RadComboBox ID="RCB_Checked" Runat="server"  >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="已审核" />
                            <telerik:RadComboBoxItem Value="0" Text="待审核" />
                            <telerik:RadComboBoxItem Value="2" Text="审核未通过" />
                        </Items>
                    </telerik:RadComboBox> 
                    <telerik:RadButton ID="btn_Checked" runat="server" Text="修改审核信息" onclick="btn_Checked_Click" />
                </div>
                <div>
                    <label>审核备注：</label>
                    <telerik:RadTextBox ID="TB_CheckMemo" runat="server" Columns="50" Rows="5" TextMode="MultiLine" SkinID="BigTextBox" /> 
                </div>
                <div>  
                    <label>审核人：</label>
					<telerik:RadTextBox ID="TB_Checker" runat="server" ReadOnly="true" /> 
                </div> 
                <div>
					<label>标题：</label>
					<telerik:RadTextBox ID="Title1" ReadOnly="true"  runat="server" SkinID="NewsTextBox"  />
				</div> 

                <telerik:RadTabStrip ID="TabStrip1" runat="server" EnableDragToReorder="false"  MultiPageID="RadMultiPag1" style="padding-left:100px; width:700px">
                    <Tabs>
                        <telerik:RadTab  Text="高级模式" Value="1"></telerik:RadTab>
                        <telerik:RadTab  Text="基础模式" Value="2"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip> 
				<div>
                <label>内容：</label> 
                </div>
                <telerik:RadMultiPage ID="RadMultiPag1" runat="server"> 
                
                <telerik:RadPageView ID="RadPageView2" runat="server"> 
                <telerik:RadEditor ID="contentEditor"  runat="server"  Height="440px"  ExternalDialogsPath="EditorDialogs"
                ContentAreaMode="Iframe" NewLineMode="P" AutoResizeHeight="false" 
                        SkinID="RadEditor2" BorderStyle="NotSet" ToolsFile="tools.xml" 
                        ContentFilters="None" onfiledelete="contentEditor_FileDelete"> 
                    <ImageManager ViewPaths="~/Images" UploadPaths="~/Images" DeletePaths="~/Images" MaxUploadFileSize="204800"  ></ImageManager>
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
                <div style="padding-left:100px;">
                     <telerik:RadButton ID="RadButton1" runat="server" Text="适应机顶盒格式预览"  ButtonType="ToggleButton" ToggleType="CheckBox" Visible="false"  AutoPostBack="true" OnCheckedChanged="RadButton1_CheckedChanged"></telerik:RadButton>
                      
                     <telerik:RadButton ID="saveEditBtn" runat="server" Text="应用编辑"  onclick="saveEditBtn_Click" Visible="false" />
                 </div>
                 </telerik:RadPageView>

                <telerik:RadPageView ID="RadPageView1" runat="server">
                    <telerik:RadTextBox ID="contentText" runat="server"   TextMode="MultiLine" Height="450px" SkinID="NewsTextBox" ReadOnly="true"  />
		        </telerik:RadPageView> 


                 </telerik:RadMultiPage>
                 <div>
					<label>&nbsp;</label> 
					<telerik:RadButton ID="Btn_Modify" runat="server" Text=" 修 改 " UseSubmitBehavior="false"  onclick="Btn_Modify_Click" Visible="false" />  
				</div> 
                 </fieldset>
		    </div>
		    </asp:Panel>
       <telerik:RadFormDecorator ID="FormDecorator1" runat="server" >
        </telerik:RadFormDecorator>
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script>      
</body>
</html>
