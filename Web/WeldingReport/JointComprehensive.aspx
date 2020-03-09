<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointComprehensive.aspx.cs"
    Inherits="Web.WeldingReport.JointComprehensive" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊口综合信息</title>
      <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ValidateGroupControl.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowSearch(values) {
            window.showModalDialog("JointComprehensiveOut.aspx?rnd=" + (new Date()).getTime() + "&values=" + values , "", "status=no;dialogWidth=1200px;dialogHeight=600px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");      
          }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 100%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;焊口综合信息
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr runat="server" id="trProject" visible="false">
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="项目"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpProject" runat="server" CssClass="textboxStyle" 
                                Height="22px" Width="80%"
                             AutoPostBack="true" onselectedindexchanged="drpProject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" height="32px">  
                        </td>
                        <td align="left">
                        </td>
                         <td align="center" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px" width="8%">
                            <asp:Label ID="Label2" runat="server" Text="工作区"></asp:Label>&nbsp;                            
                        </td>
                        <td width="15%" align="left">
                            <asp:DropDownList ID="ddlWorkArea" runat="server" CssClass="textboxStyle" Width="95%" Height="22px">
                            </asp:DropDownList>
                        </td>
                         <td align="right" width="10%">                           
                            <asp:Label ID="Label1" runat="server" Text="焊口规格"></asp:Label>&nbsp;
                        </td>
                        <td width="15%" align="left">
                            <asp:TextBox ID="txtJointDesc" runat="server" CssClass="textboxStyle" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" width="8%">                           
                            <asp:Label ID="Label3" runat="server" Text="管线"></asp:Label>&nbsp;
                        </td>
                        <td width="20%" align="left">
                            <asp:TextBox ID="txtIsoNo" runat="server" CssClass="textboxStyle" Width="95%"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="焊口综合信息表" OnClick="btnExport_Click" /> 
                            <asp:ImageButton ID="btnExportJots" runat="server" ImageUrl="~/Images/ExportJots.gif"
                                ToolTip="按导入模板导出焊口信息" OnClick="btnExportJots_Click" /> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table2" width="2000px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvJointCompre" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="20" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                        DataSourceID="ObjectDataSource1" OnDataBound="gvJointCompre_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvJointCompre.PageIndex * gvJointCompre.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WorkAreaCode" HeaderText="工区" />
                            <asp:BoundField DataField="ISO_ISONO" HeaderText="管线号" />
                            <asp:BoundField DataField="JOT_BelongPipe" HeaderText="所属管段" />
                            <asp:BoundField DataField="JOT_JointNo" HeaderText="焊口号" />
                            <asp:BoundField DataField="JOT_Dia" HeaderText="外径" />
                            <asp:BoundField DataField="JOT_Sch" HeaderText="壁厚" />
                            <asp:BoundField DataField="JOT_FactSch" HeaderText="实际壁厚" />
                            <asp:BoundField DataField="STE_NAME" HeaderText="材质" />
                            <asp:BoundField DataField="JOT_CellWelder" HeaderText="盖面焊工" />
                            <asp:BoundField DataField="JOT_FloorWelder" HeaderText="打底焊工" />
                            <asp:BoundField DataField="WME_Name" HeaderText="焊接方法" />
                            <asp:BoundField DataField="NDTR_Rate" HeaderText="探伤比例" />
                            <asp:BoundField DataField="SER_NAME" HeaderText="介质" />
                            <asp:BoundField DataField="JOT_WeldDate" HeaderText="焊接日期" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="JOT_DailyReportNo" HeaderText="日报告号" />
                            <asp:BoundField DataField="CH_TRUSTCODE1" HeaderText="委托单号" />
                             <asp:BoundField DataField="ProessName" HeaderText="是否需要热处理" />                            
                            <asp:BoundField DataField="CHT_CHECKDATE" HeaderText="检测日期" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="JOT_Size" HeaderText="尺寸" />
                            <asp:BoundField DataField="WMT_MatCode" HeaderText="焊条牌号" />
                            <asp:BoundField DataField="WMT_Matname" HeaderText="焊条名称" />
                            <asp:BoundField DataField="HsCode" HeaderText="焊丝牌号" />
                            <asp:BoundField DataField="Hsname" HeaderText="焊丝名称" />
                            <asp:BoundField DataField="JOT_JointDesc" HeaderText="规格" />
                            <asp:BoundField DataField="If_dkName" HeaderText="是否点口" />
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                        TypeName="BLL.JointComprehensiveService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>
                            <asp:Parameter Name="workarea" />
                            <asp:Parameter Name="isono" />
                            <asp:Parameter Name="jointDesc" />
                            <asp:Parameter Name="projectId" />
                             <asp:Parameter Name ="flag" />
                             <asp:Parameter Name ="supervisorUnitId" /> 
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var width = parent.document.getElementById("center").offsetWidth;
    if (width > 2000) {
        $("#Table2").width(width);
    }
    var height = parent.document.getElementById("center").offsetHeight;
    var table1Height = $("#Table1").height();
    var hei = height - table1Height - 5;
    $("#div1").height(hei);
</script>
