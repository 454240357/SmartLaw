<%@ Page Language="C#"  MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeBehind="CategoryView.aspx.cs" Inherits="SmartLaw.Admin.CategoryManage.CategoryView" %>
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
        当前位置：分类管理 →<h1>分类列表</h1>
    </div>   
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel> 
     <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings> 
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
     <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">

            //indicates whether the user is currently dragging a tree node
            var treeViewDragInProgress = false;

            //indicate that the user started dragging a tree node
            function onTreeViewDragStart(sender, args) {
                treeViewDragInProgress = true;
            }

            //handle the drop of the tree node
            function onTreeViewDropping(sender, args) {
                //indicate that the user stopped dragging
                treeViewDragInProgress = false;

                //restore the cursor to the default state
                document.body.style.cursor = "";

                //get the html element on which the node is dropped
                var target = args.get_htmlElement();

                //if dropped on the treeview itself return.
                if (isOverElement(target, "<%= RadTreeView1.ClientID %>")) {
                    return;
                }
                //if not cancel the dropping event so it does not postback
                if (!target) {
                    args.set_cancel(true);
                    return;
                }

                //the node was dropped on the listbox - set the htmlElement.
                //it can be later accessed via the HtmlElementID property of the RadTreeNodeDragDropEventArgs
                args.set_htmlElement(target);
            }

            //chech if a given html element is a child of an element with the specified id
            function isOverElement(target, id) {
                while (target) {
                    if (target.id == id)
                        break;

                    target = target.parentNode;
                }
                return target;
            }

            function checkDropTargets(target) {
                if (isOverElement(target, "<%= RadTreeView1.ClientID %>")) {
                    //if the mouse is over the treeview or listbox - set the cursor to default
                    document.body.style.cursor = "";
                } else {
                    //else set the cursor to "no-drop" to indicate that dropping over this html element is not supported
                    document.body.style.cursor = "no-drop";
                }
            }


            //update the cursor if the user is dragging the node over supported drop target - listbox or treeview
            function onTreeViewDragging(sender, args) {
                checkDropTargets(args.get_htmlElement());
            }

            var lastClickedItem = null;
            var clickCalledAfterRadconfirm = false;
            function onClientContextMenuItemClicking(sender, args) {
               
                var menuItem = args.get_menuItem();
                var treeNode = args.get_node();
                menuItem.get_menu().hide(); 
                switch (menuItem.get_value()) {
                    case "new":
                        break;
                    case "edit": 
                        break; 
                    case "delete":
                        /*var result = confirm("确认删除分类: " + treeNode.get_text());
                        args.set_cancel(!result);*/
                        if (!clickCalledAfterRadconfirm) {
                            args.set_cancel(true);
                            lastClickedItem = args.get_menuItem();
                            radconfirm("确认删除？", confirmCallbackFunction, 330, 100, null, "九峰山智慧社区管理平台");
                        }  
                        break;
                }
            }
            function confirmCallbackFunction(args) {
                if (args) {
                    clickCalledAfterRadconfirm = true;
                    lastClickedItem.click();
                    clickCalledAfterRadconfirm = false; 
                }
                else
                    clickCalledAfterRadconfirm = false; 
                lastClickedItem = null;
            }  
        </script>
    </telerik:RadCodeBlock> 
     <asp:Panel ID="Panel1" runat="server">   
      <telerik:RadAjaxPanel ID="RadAjaxPanel1"  runat="server" LoadingPanelID="RadAjaxLoadingPanel1"> 
          <div style="padding-top:10px">  
           <telerik:RadButton ID="btreload" runat="server" onclick="btreload_Click" Text=" 刷  新 "></telerik:RadButton>
            &nbsp;
           <telerik:RadButton ID="RadButton1" runat="server" Text=" 全部折叠 " onclick="RadButton1_Click"></telerik:RadButton>
            &nbsp;
           <telerik:RadButton ID="RadButton2" runat="server"  Text=" 全部展开 " onclick="RadButton2_Click"></telerik:RadButton>
            &nbsp; 
           <telerik:RadButton ID="btn_Submit" runat="server" Text=" 更新排序 " onclick="btn_Submit_Click"></telerik:RadButton>
         </div>  
         <div style="padding-top:10px">
         <telerik:RadTreeView runat="server" ID="RadTreeView1"   EnableDragAndDrop="True" OnContextMenuItemClick="RadTreeView1_ContextMenuItemClick"
              OnClientContextMenuItemClicking="onClientContextMenuItemClicking"  
                 OnClientNodeDragStart="onTreeViewDragStart" 
                 OnClientNodeDropping="onTreeViewDropping" OnNodeDrop="RadTreeView1_NodeDrop"
            OnClientNodeDragging="onTreeViewDragging"  EnableDragAndDropBetweenNodes="true" 
                 AllowNodeEditing="False" > 
            <ContextMenus>
            <telerik:RadTreeViewContextMenu>
            <Items>
            <telerik:RadMenuItem value="new" Text="添加子类"></telerik:RadMenuItem>
            <telerik:RadMenuItem Value="edit" Text="编辑分类"></telerik:RadMenuItem>
            <telerik:RadMenuItem Value="delete" Text="删除分类"></telerik:RadMenuItem>
            </Items> 
            </telerik:RadTreeViewContextMenu> 
            </ContextMenus> 
        </telerik:RadTreeView>  
         </div>
     </telerik:RadAjaxPanel> 
     </asp:Panel>
</asp:Content>
