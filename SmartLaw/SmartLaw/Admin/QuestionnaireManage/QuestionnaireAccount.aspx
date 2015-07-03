<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireAccount.aspx.cs" Inherits="SmartLaw.Admin.QuestionnaireManage.QuestionnaireAccount" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="width: 850px;">
    <form id="form1" runat="server">
            <div id="position">
                当前位置： 问卷管理→
               <h1>问卷统计</h1>
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
          <asp:Panel runat="server" ID="Panel1" > 
            <div class="form">
                <fieldset>
                  <div>
                        <Label>问卷标题：</Label>
                        <telerik:RadTextBox ID="RTB_Title" runat="server" SkinID="QaBox2"  ReadOnly="true"/>
                  </div> 
                 </fieldset> 
                 <telerik:RadGrid ID="RadGrid1"   runat="server" GridLines="Vertical" AutoGenerateColumns="False"  OnItemDataBound="RadGrid1_ItemDataBound"  ShowFooter="true">
                                 <MasterTableView NoMasterRecordsText="没有任何记录。">
                                 <Columns>
                                      <telerik:GridBoundColumn DataField="QSID"  Display="false">
                                            <ItemStyle Width="0%" ></ItemStyle>
                                      </telerik:GridBoundColumn> 
                                      <telerik:GridBoundColumn DataField="Title" HeaderText="问题列表">
                                            <ItemStyle Width="30%" ></ItemStyle>
                                      </telerik:GridBoundColumn>  
                                     <telerik:GridTemplateColumn HeaderText="统计结果">
                                            <ItemTemplate> 
                                                  <telerik:RadChart ID="RadChart1" runat="server"  AutoLayout="true" Height="150px" Width="500px"  SeriesOrientation="Horizontal">
                                                        <Series>
                                                             <telerik:ChartSeries DataYColumn="Count"    Appearance-FillStyle-MainColor="Chocolate" Appearance-FillStyle-SecondColor="Chocolate" Name="计数" >
                                                             </telerik:ChartSeries> 
                                                        </Series>
                                                        <PlotArea> 
                                                             <XAxis  DataLabelsColumn="Answer" ></XAxis> 
                                                             <YAxis AutoScale="false" MinValue="0" Step="1" LabelStep="1"  AxisMode="Extended"></YAxis>
                                                        </PlotArea> 
                                                 </telerik:RadChart>     
                                           </ItemTemplate> 
                                           <ItemStyle Width="70%"></ItemStyle>
                                   </telerik:GridTemplateColumn>   
                            </Columns> 
                     </MasterTableView>
                     <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
                     </ClientSettings>
              </telerik:RadGrid> 
            </div>
        </asp:Panel> 
    </form>
    <script type="text/javascript">
        changeWindowSize();
    </script> 
</body>
</html>
