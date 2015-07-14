<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl.ascx.cs" Inherits="SmartLaw.Admin.NewsManage.UserControl"%> 
<script runat="server" type="text/C#">  
  
    protected void Page_Load(object sender, System.EventArgs args)  
    {  
        Telerik.Web.UI.RadFileExplorer rfe = (Telerik.Web.UI.RadFileExplorer)this.FindRadControl(this.Page);  
        if (rfe != null)  
        { 
            rfe.TreeView.OnClientContextMenuShown = "OnTreeContextMenuShown";
            rfe.GridContextMenu.OnClientShown = "OnGridClientShown";
            rfe.AllowPaging = true;
            rfe.PageSize = 10;
            //rfe.OnClientDelete = "OnClientDelete";
            rfe.EnableCreateNewFolder = false; 
        }  
    }  
  
    
  
    private Control FindRadControl(Control parent)  
    {  
        foreach (Control c in parent.Controls)  
        { 
             
            if (c is Telerik.Web.UI.RadFileExplorer) return c;  
            if (c.Controls.Count > 0)  
            {  
                Control sub = FindRadControl(c);  
                if (sub != null) return sub;  
            }  
        }  
        return null;  
    }  
  
</script>  