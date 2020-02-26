<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowReportExport.aspx.cs"
    Inherits="Web.WeldingManage.ShowReportExport" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<base target="_self" />--%>
   <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript"> 　　    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" style="height: 100%">
        <tr>
            <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 100%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            焊接日报导出&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height:32px">
            <td>
                <table style="width: 100%;">
                    <tr>
                         <td align="right" height="32px" width="8%">
                            <asp:Label ID="Label2" runat="server" Text="单位"></asp:Label> &nbsp;                           
                        </td>
                        <td width="20%" align="left">
                            &nbsp;<asp:DropDownList ID="ddlUnit" 
                                runat="server" CssClass="textboxStyle" Width="90%" Height="22px" AutoPostBack="true"
                                onselectedindexchanged="ddlUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" height="32px" width="8%">
                            <asp:Label ID="Label4" runat="server" Text="工作区"></asp:Label>&nbsp;
                        </td>
                        <td width="12%" align="left">
                            <asp:DropDownList ID="ddlWorkarea" runat="server" CssClass="textboxStyle" Height="22px" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="8%">                           
                            <asp:Label ID="Label3" runat="server" Text="管线"></asp:Label> &nbsp;
                        </td>
                        <td width="22%" align="left">
                            <asp:TextBox ID="txtIsoNo" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px" width="10%">
                            <asp:Label ID="Label1" runat="server" Text="日期"></asp:Label>&nbsp;
                        </td>
                        <td width="25%" align="left" colspan="2">
                            <input id="txtdate1" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                            <asp:Label ID="Label6" runat="server" Text="至"></asp:Label>
                            &nbsp;<input id="txtdate2" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                        <td align="center" colspan="3">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                           <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="导出" OnClick="btnExport_Click" /> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div2" style=" height:500px; overflow: auto; overflow-x: hidden" runat="server">
        <table id="Table2" width="100%" cellpadding="0" cellspacing="0" runat="server">
            <tr>            
            <td style="width: 100%" runat="server">
                    <asp:GridView ID="gvPW_JointInfo" runat="server"  AllowPaging="true"  AllowSorting="True"
                                 PageSize="20" DataSourceID="ObjectDataSource1"
                                AutoGenerateColumns="False" HorizontalAlign="Justify" 
                                Width="100%" OnDataBound="gvPW_JointInfo_DataBound">
                                <AlternatingRowStyle cssclass="GridBgColr" />
                                <columns>                                        
                                       <asp:BoundField DataField="WED_Code" HeaderText="焊工代号">
                                        <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISO_IsoNo" HeaderText="管道编号">
                                        <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JOT_JointNo" HeaderText="焊口编号">
                                        <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                       <asp:BoundField DataField="JOT_JointDesc" HeaderText="规格(mm)">
                                        <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STE_Name" HeaderText="材质">
                                         <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISO_IsoNumber" HeaderText="单线图号">
                                         <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="weldLocal" HeaderText="焊接位置">
                                         <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="WME_Name" HeaderText="焊接方法">
                                         <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="WMT_MatName" HeaderText="焊材牌号">
                                         <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JOT_PrepareTemp" HeaderText="实际预热温度℃">
                                         <ItemStyle Width="7%" />
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="焊接日期" ItemStyle-Width="7%">
                                        <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr >
                                                    <td colspan="3">
                                                        <asp:Label ID="Label10" runat="server" Text="焊接日期"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%">
                                                        <asp:Label ID="Label11" runat="server" Text="月"></asp:Label>
                                                    </td>
                                                    <td  style="width: 50%;">
                                                        <asp:Label ID="Label9" runat="server" Text="日"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%;" >
                                                       <asp:Label ID="Label11" runat="server" Text='<%#Bind("ReportMonth") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 50%" >
                                                       <asp:Label ID="Label5" runat="server" Text='<%#Bind("Reportday") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                       
                                    <asp:TemplateField HeaderText="无损检测报告编号" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("NDTT_CheckCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="热处理报告编号" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                             <asp:Label ID="Label5" runat="server" Text='<%#Bind("JOT_HotRpt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </columns>
                                <HeaderStyle CssClass="GridBgColr" />
                                <RowStyle CssClass="GridRow" />
                                <PagerStyle HorizontalAlign="Left" />
                                <PagerTemplate>
                                        <uc1:GridNavgator ID="GridNavgator1"  runat="server"  />
                                    </PagerTemplate>
                            </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                                TypeName="BLL.WeldReportExportService" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                                SelectCountMethod="GetListCount">
                            <SelectParameters>
                                <asp:Parameter Name="projectId" />
                                <asp:Parameter Name="unitId" />
                                <asp:Parameter Name="workareaId" />
                                <asp:Parameter Name="iso_IsoNo" />
                                <asp:Parameter Name="date1" Type="DateTime"/>
                                <asp:Parameter Name="date2" Type="DateTime" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
         </tr>
         </table>
       </div>
    </form>
</body>
</html>

