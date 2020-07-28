<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackagePipelineAnalysis.aspx.cs"
    Inherits="Web.TestPackageManage.TestPackagePipelineAnalysis" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试压包管线综合分析表</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
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
                            &nbsp;试压包管线综合分析表
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr runat="server" id="trProject" visible="false">
                        <td align="right" style="width:10%">
                            <asp:Label ID="Label7" runat="server" Text="项目"></asp:Label>&nbsp;
                        </td>
                        <td align="left"  style="width:30%">
                            <asp:DropDownList ID="drpProject" runat="server" CssClass="textboxStyle" 
                                Height="22px" Width="90%"
                             AutoPostBack="true" onselectedindexchanged="drpProject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td colspan="5">
                        </td>
                    </tr> 
                    <tr>
                         <td align="right" style="width:8%">
                            <asp:Label ID="Label2" runat="server" Text="单位"></asp:Label>&nbsp;
                        </td>
                        <td align="left" style="width:17%">
                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td align="right" style="width:8%">
                            <asp:Label ID="Label4" runat="server" Text="工作区"></asp:Label>&nbsp;
                        </td>
                        <td align="left" style="width:17%">
                            <asp:DropDownList ID="ddlWorkArea" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width:8%">
                            <asp:Label ID="Label13" runat="server" Text="施压包号"></asp:Label>&nbsp;
                        </td>
                        <td align="left" style="width:17%">
                            <asp:TextBox ID="txtTestPackageNo" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%">
                            </asp:TextBox>
                        </td>
                       <td align="right">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="管线综合分析表" OnClick="btnExport_Click" />
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
                    <asp:GridView ID="gvIsoCompre" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="20" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                        DataSourceID="ObjectDataSource1" OnDataBound="gvIsoCompre_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvIsoCompre.PageIndex * gvIsoCompre.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="1%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PTP_TestPackageNo" HeaderText="试压包号" >
                            <HeaderStyle Width="3%" />
                            </asp:BoundField>
                           <asp:BoundField DataField="ISO_IsoNo" HeaderText="管线号" >
                            <HeaderStyle Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="maxdate" HeaderText="最近焊期"  DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="3%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="焊口">
                                <HeaderStyle Width="8%" />
                                        <HeaderTemplate>
                                            <table style="width: 100%; " border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="3" align="center" >
                                                        <asp:Label ID="Label26" runat="server" Text="焊口"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width:30%">
                                                       <asp:Label ID="Label3" runat="server" Text="焊口数"></asp:Label>
                                                    </td>
                                                   <td align="center"  style="width:30%">
                                                        <asp:Label ID="Label1" runat="server" Text="已完成数"></asp:Label>
                                                    </td>
                                                    <td align="center"  style="width:40%">
                                                        <asp:Label ID="Label2" runat="server" Text="完成比例(%)"></asp:Label>
                                                    </td>
                                                </tr>                                              
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td  style="width:30%">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("JotCounts") %>' ></asp:Label>                                                     
                                                    </td>
                                                    <td  style="width:30%">
                                                       <asp:Label ID="Label4" runat="server" Text='<%# Bind("JotCompletedCounts") %>'></asp:Label>                       
                                                    </td>
                                                    <td  style="width:40%">
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("JotCompletedRatio") %>'></asp:Label>  
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="总达因">
                                <HeaderStyle Width="8%" />
                                        <HeaderTemplate>
                                            <table style="width: 100%; " border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="3" align="center" >
                                                        <asp:Label ID="Label26" runat="server" Text="总达因"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center"  style="width:30%">
                                                       <asp:Label ID="Label3" runat="server" Text="达因数量"></asp:Label>
                                                    </td>
                                                   <td align="center"  style="width:30%">
                                                        <asp:Label ID="Label1" runat="server" Text="已完成"></asp:Label>
                                                    </td>
                                                    <td align="center"  style="width:40%">
                                                        <asp:Label ID="Label2" runat="server" Text="完成率(%)"></asp:Label>
                                                    </td>
                                                </tr>                                              
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td   style="width:30%">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DinCounts") %>'></asp:Label>                                                     
                                                    </td>
                                                    <td  style="width:30%">
                                                       <asp:Label ID="Label4" runat="server" Text='<%# Bind("DinCompletedCounts") %>'></asp:Label>                       
                                                    </td>
                                                    <td  style="width:40%">
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("DinCompletedRatio") %>'></asp:Label>  
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="对接接头">
                                <HeaderStyle Width="18%" />
                                        <HeaderTemplate>
                                            <table style="width: 100%; " border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="7" align="center" >
                                                        <asp:Label ID="Label26" runat="server" Text="对接接头"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center"  style="width:15%">
                                                       <asp:Label ID="Label3" runat="server" Text="对接接头总数" Width="90px"></asp:Label>
                                                    </td>
                                                   <td align="center"  style="width:10%">
                                                        <asp:Label ID="Label1" runat="server" Text="已焊接数" ></asp:Label>
                                                    </td>
                                                    <td align="center" style="width:10%">
                                                        <asp:Label ID="Label2" runat="server" Text="已检测数量" ></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label6" runat="server" Text="完成检测比例(%)" ></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label8" runat="server" Text="固定口检测数量"></asp:Label>
                                                    </td>
                                                      <td align="center"  style="width:15%">
                                                        <asp:Label ID="Label9" runat="server" Text="固定口检测比例(%)" ></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:20%">
                                                        <asp:Label ID="Label10" runat="server" Text="焊工" ></asp:Label>
                                                    </td>
                                                </tr>                                              
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                   <td align="center"  style="width:15%">
                                                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("BWCounts") %>'></asp:Label>    
                                                    </td>
                                                   <td align="center"  style="width:10%">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("BWWeldedCounts") %>'></asp:Label>    
                                                    </td>
                                                    <td align="center"  style="width:10%">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BWCheckedCounts") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("BWCompletedCheckedRatio") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("BWFixedCheckedCounts") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("BWFixedCheckedRatio") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:20%" >
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("BWWelders") %>'></asp:Label>    
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="角接接头">
                                <HeaderStyle Width="18%" />
                                        <HeaderTemplate>
                                            <table style="width: 100%; " border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="7" align="center" >
                                                        <asp:Label ID="Label26" runat="server" Text="角接接头"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width:15%">
                                                       <asp:Label ID="Label3" runat="server" Text="角接接头总数"></asp:Label>
                                                    </td>
                                                   <td align="center" style="width:10%">
                                                        <asp:Label ID="Label1" runat="server" Text="已焊接数"></asp:Label>
                                                    </td>
                                                    <td align="center" style="width:10%">
                                                        <asp:Label ID="Label2" runat="server" Text="已检测数量"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label6" runat="server" Text="完成检测比例(%)"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label8" runat="server" Text="固定口检测数量"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label9" runat="server" Text="固定口检测比例(%)"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:20%">
                                                        <asp:Label ID="Label10" runat="server" Text="焊工"></asp:Label>
                                                    </td>
                                                </tr>                                              
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                      <td align="center" style="width:15%">
                                                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("SWCounts") %>'></asp:Label>    
                                                    </td>
                                                   <td align="center" style="width:10%">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SWWeldedCounts") %>'></asp:Label>    
                                                    </td>
                                                    <td align="center" style="width:10%">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("SWCheckedCounts") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("SWCompletedCheckedRatio") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("SWFixedCheckedCounts") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("SWFixedCheckedRatio") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:20%">
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("SWWelders") %>'></asp:Label>    
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="支管接头">
                                <HeaderStyle Width="15%" />
                                        <HeaderTemplate>
                                            <table style="width: 100%; " border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="5" align="center" >
                                                        <asp:Label ID="Label26" runat="server" Text="支管接头"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width:20%">
                                                       <asp:Label ID="Label3" runat="server" Text="支管接头总数"></asp:Label>
                                                    </td>
                                                   <td align="center"  style="width:20%">
                                                        <asp:Label ID="Label1" runat="server" Text="已焊接数"></asp:Label>
                                                    </td>
                                                    <td align="center"  style="width:20%">
                                                        <asp:Label ID="Label2" runat="server" Text="已检测数量"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:20%" >
                                                        <asp:Label ID="Label6" runat="server" Text="完成检测比例(%)"></asp:Label>
                                                    </td>                                                   
                                                      <td align="center"  style="width:20%">
                                                        <asp:Label ID="Label10" runat="server" Text="焊工"></asp:Label>
                                                    </td>
                                                </tr>                                              
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate >
                                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                   <td align="center" style="width:20%">
                                                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("LETCounts") %>'></asp:Label>    
                                                    </td>
                                                   <td align="center" style="width:20%">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("LETWeldedCounts") %>'></asp:Label>    
                                                    </td>
                                                    <td align="center" style="width:20%">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("LETCheckedCounts") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:20%">
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("LETCompletedCheckedRatio") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:20%">
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("LETWelders") %>'></asp:Label>    
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="对接接头">
                                <HeaderStyle Width="18%" />
                                        <HeaderTemplate>
                                            <table style="width: 100%; " border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="8" align="center" >
                                                        <asp:Label ID="Label26" runat="server" Text="对接接头"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width:10%">
                                                       <asp:Label ID="Label3" runat="server" Text="总拍片数"></asp:Label>
                                                    </td>
                                                   <td align="center" style="width:10%">
                                                        <asp:Label ID="Label1" runat="server" Text="合格片数"></asp:Label>
                                                    </td>
                                                    <td align="center" style="width:15%">
                                                        <asp:Label ID="Label2" runat="server" Text="合格率(%)"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:10%">
                                                        <asp:Label ID="Label6" runat="server" Text="委托数"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:10%">
                                                        <asp:Label ID="Label8" runat="server" Text="已探口数"></asp:Label>
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label9" runat="server" Text="检测比例(%)"></asp:Label>
                                                    </td>
                                                     <td align="center" style="width:15%">
                                                        <asp:Label ID="Label10" runat="server" Text="委托比例(%)"></asp:Label>
                                                    </td>
													<td align="center" style="width:15%">
                                                        <asp:Label ID="Label11" runat="server" Text="已探比例(%)"></asp:Label>
                                                    </td>
                                                </tr>                                              
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                   <td align="center" style="width:10%">
                                                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("BWTotalFilm") %>'></asp:Label>    
                                                    </td>
                                                   <td align="center" style="width:10%">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("BWPassFilm") %>'></asp:Label>    
                                                    </td>
                                                    <td align="center"  style="width:15%">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BWPassRatio") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:10%" >
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("BWTrustCounts") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:10%">
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("BWCheckedJotCounts") %>'></asp:Label>    
                                                    </td>
                                                      <td align="center" style="width:15%">
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("BWsource_rate") %>'></asp:Label>    
                                                    </td>
                                                    <td align="center" style="width:15%">
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("BWTrustRatio") %>'></asp:Label>    
                                                    </td>
													<td align="center" style="width:15%">
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("BWCheckedRatio") %>'></asp:Label>    
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                        TypeName="BLL.TestPackagePipelineAnalysisService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>                          
                            <asp:Parameter Name="projectId" />
                            <asp:Parameter Name ="unitId" />
                            <asp:Parameter Name ="workAreaId" />
                            <asp:Parameter Name ="testPackageNo" />
                            <asp:Parameter Name ="flag" />
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
