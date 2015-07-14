<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" Inherits="SmartLaw.Admin.MainMenu" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<script type="text/javascript">
    var clickCalledAfterRadconfirm = false;
    var lastClickedItem = null;
    function OnClientItemClickingHandler(sender, eventArgs) {
        if (!clickCalledAfterRadconfirm) {
            lastClickedItem = eventArgs.get_item();

            if (lastClickedItem.get_text() == "注销(E)") {
                eventArgs.set_cancel(true);
                radconfirm("真的要注销吗？", confirmCallbackFunction, 330, 100, null, '九峰山智慧社区管理平台');
            }
        }
    }


    function confirmCallbackFunction(args) {
        if (args) {
            clickCalledAfterRadconfirm = true;
            if (lastClickedItem.get_navigateUrl() != "" && lastClickedItem.get_navigateUrl() != "#") {
                window.location.href = lastClickedItem.get_navigateUrl();
            }
            else {
                lastClickedItem.click();
            }
        }
        else
            clickCalledAfterRadconfirm = false;
        lastClickedItem = null;
    }  

      function CloseWebPage(){  
          if (navigator.userAgent.indexOf("MSIE") > 0) {  //IE浏览器
             if (navigator.userAgent.indexOf("MSIE 6.0") > 0) {  
                  window.opener = null; window.close();  
              }  
              else {  
                  window.open('', '_top'); window.top.close();  
             }  
         }  
         else if (navigator.userAgent.indexOf("Firefox") > 0) {  //火狐浏览器
             window.location.href = 'about:blank ';  
             //window.history.go(-2);   
         }  
         else {     //其他浏览器
             window.opener = null;   
             window.open('', '_self', '');  
             window.close();  
         }  
     }    
</script>
<telerik:RadMenu ID="RadMenu1" runat="server" style="z-index:1" onitemclick="RadMenu1_ItemClick"  OnClientItemClicking="OnClientItemClickingHandler">
</telerik:RadMenu>
