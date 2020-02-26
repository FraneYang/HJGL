<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelderPerformance.aspx.cs" Inherits="Web.WeldingReport.WelderPerformance" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊工业绩分析</title>
 <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
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
                            &nbsp;焊工业绩分析
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
                            <asp:Label ID="Label1" runat="server" Text="日期"></asp:Label>&nbsp;
                        </td>
                        <td width="26%" align="left">
                            <input id="txtdate1" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                            <asp:Label ID="Label6" runat="server" Text="至"></asp:Label>
                            &nbsp;<input id="txtdate2" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                        <td align="right" width="8%">
                            <asp:Label ID="Label2" runat="server" Text="单位"></asp:Label>&nbsp;
                        </td>
                        <td width="25%" align="left">
                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textboxStyle" AutoPostBack="true"
                               Height="22px"  Width="90%" onselectedindexchanged="ddlUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="8%" align="right">
                            <asp:Label ID="Label4" runat="server" Text="工作区"></asp:Label>&nbsp;
                        </td>
                        <td width="25%" align="left">
                            <asp:DropDownList ID="ddlWorkarea" runat="server" CssClass="textboxStyle" Height="22px" Width="90%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>                       
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="材质"></asp:Label>&nbsp;
                            </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSteel" runat="server" CssClass="textboxStyle"
                               Height="22px" Width="90%">
                            </asp:DropDownList>
                        </td>
                         <td align="right" height="32px">
                            <asp:Label ID="Label5" runat="server" Text="班组"></asp:Label>&nbsp;
                            </td>
                        <td align="left">
                            <asp:DropDownList ID="drpTeamGroup" runat="server" CssClass="textboxStyle" AutoPostBack="true"
                               Height="22px"  Width="90%" onselectedindexchanged="drpTeamGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td width="8%" align="right">
                            <asp:Label ID="Label8" runat="server" Text="焊工"></asp:Label>&nbsp;
                        </td>
                        <td width="25%" align="left">
                            <asp:DropDownList ID="ddlWloName" runat="server" CssClass="textboxStyle" Height="22px" Width="90%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>                       
                        <td align="right">
                            </td>
                        <td align="left">
                           
                        </td>
                         <td align="right" height="32px">
                       
                            </td>
                        <td align="left">

                        </td>
                         <td align="center" colspan="2">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="导出焊工业绩分析表" OnClick="btnExport_Click" /> 
                        </td>
                    </tr>
                     </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
    <table id="Table2" width="3500px" cellpadding="0" cellspacing="0" runat="server">
        <tr>
            <td width="100%">
                <asp:GridView ID="gvWelderPerformance" runat="server" AllowPaging="True" AllowSorting="True"
                    PageSize="100" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                    OnDataBound="gvWelderPerformance_DataBound" DataSourceID="ObjectDataSource1">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                     <asp:TemplateField HeaderText="序号" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Height="25px">
                            <ItemTemplate>
                                <%# gvWelderPerformance.PageIndex * gvWelderPerformance.PageSize + Container.DataItemIndex + 1%>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="wed_code" HeaderText="焊工代码"/>
                        <asp:BoundField DataField="wed_name" HeaderText="焊工名称" />
                        <asp:BoundField DataField="WED_Sex" HeaderText="性别" />
                        <asp:BoundField DataField="nowtotal_din" HeaderText="本期总达因值" />
                        <asp:BoundField DataField="nowtotal_jot" HeaderText="本期总焊口" />
                        <asp:BoundField DataField="nowtotal_repairjot" HeaderText="本期总返口数" />
                        <asp:BoundField DataField="nowrepairrate" HeaderText="本期返修率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="nowfinishedrate" HeaderText="本期成焊率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="current_count_film" HeaderText="本期拍片焊口数" />
                        <asp:BoundField DataField="current_pass_film" HeaderText="本期拍片焊口合格数" />
                        <asp:BoundField DataField="current_passrate" HeaderText="本期焊口合格率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="nowtotalfilm" HeaderText="本期拍片总数" />
                        <asp:BoundField DataField="nowpassfilm" HeaderText="本期拍片合格数" />
                        <asp:BoundField DataField="nownotpassfilm" HeaderText="本期拍片不合格数" />
                        <asp:BoundField DataField="nowpassrate" HeaderText="本期拍片合格率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="nowunpassrate" HeaderText="本期拍片不合格率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="totaldin" HeaderText="总达因值" />
                        <asp:BoundField DataField="total_jot" HeaderText="总焊口" />
                        <asp:BoundField DataField="total_repairjot" HeaderText="总返修口" />
                        <asp:BoundField DataField="repairrate" HeaderText="返修率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="finishedrate" HeaderText="成焊率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="totalfilm" HeaderText="总片数" />
                        <asp:BoundField DataField="passfilm" HeaderText="合格片数" />
                        <asp:BoundField DataField="notpassfilm" HeaderText="不合格数" />
                        <asp:BoundField DataField="passrate" HeaderText="合格率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="unpassrate" HeaderText="不合格率" DataFormatString="{0:N1}%"/>
                        <asp:BoundField DataField="education" HeaderText="所在班组" />
                        <asp:BoundField DataField="WED_IfOnGuard" HeaderText="在岗状态" />
                    </Columns>
                    <HeaderStyle CssClass="GridBgColr" />
                    <RowStyle CssClass="GridRow" />
                    <PagerTemplate>
                        <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                    </PagerTemplate>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                    TypeName="BLL.WelderPerformanceService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                    EnablePaging="True" EnableCaching="false">
                    <SelectParameters>
                        <asp:Parameter Name="unitcode" />
                        <asp:Parameter Name="workareacode" />
                        <asp:Parameter Name="steel"/> 
                        <asp:Parameter Name="wloName"/>
                        <asp:Parameter Name="date1" Type="DateTime"/>
                        <asp:Parameter Name="date2" Type="DateTime" />  
                        <asp:Parameter Name ="projectId" />
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
    if (width > 3500) {
        $("#Table2").width(width);
    }
    var height = parent.document.getElementById("center").offsetHeight;
    var table1Height = $("#Table1").height();
    var hei = height - table1Height - 5;
    $("#div1").height(hei);
</script>