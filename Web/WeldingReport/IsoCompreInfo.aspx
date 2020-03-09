<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsoCompreInfo.aspx.cs"
    Inherits="Web.WeldingReport.IsoCompreInfo" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管线综合信息</title>
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
                            &nbsp;管线综合信息
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
                        <td align="right" height="32px" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="工作区"></asp:Label>&nbsp;                           
                        </td>
                        <td width="20%" align="left">
                            <asp:DropDownList ID="ddlWorkArea" runat="server" CssClass="textboxStyle" Width="80%" Height="22px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label3" runat="server" Text="管线"></asp:Label>&nbsp;
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtIsoNo" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="管线综合信息表" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table2" width="1800px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvIsoCompreInfo" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="500" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                        DataSourceID="ObjectDataSource1" OnDataBound="gvIsoCompreInfo_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                             <asp:TemplateField HeaderText="序号"  ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvIsoCompreInfo.PageIndex * gvIsoCompreInfo.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UnitName" HeaderText="单位名称" />
                            <asp:BoundField DataField="WorkAreaCode" HeaderText="施工区域" />
                            <asp:BoundField DataField="Iso_isono" HeaderText="管线号" />
                            <asp:BoundField DataField="ISO_TotalDin" HeaderText="总达因" />
                            <asp:BoundField DataField="Jot_count" HeaderText="焊口数" />
                            <asp:BoundField DataField="Ser_name" HeaderText="介质" />
                            <asp:BoundField DataField="Ndtr_rate" HeaderText="探伤比例" />
                            <asp:BoundField DataField="Ndt_name" HeaderText="探伤类型" />
                            <asp:BoundField DataField="ISO_NDTClass" HeaderText="合格等级" />
                            <asp:BoundField DataField="Ste_name" HeaderText="材质" />
                            <asp:BoundField DataField="ISO_DesignPress" HeaderText="设计压力" />
                            <asp:BoundField DataField="ISO_DesignTemperature" HeaderText="设计温度" />
                            <asp:BoundField DataField="ISO_TestPress" HeaderText="实验压力" />
                            <asp:BoundField DataField="ISO_TestTemperature" HeaderText="实验温度" />                          
                            <asp:BoundField DataField="ISO_SysNo" HeaderText="系统号" />
                            <asp:BoundField DataField="ISO_SubSysNo" HeaderText="分系统号" />
                            <asp:BoundField DataField="ISO_CwpNo" HeaderText="工作包号" />
                            <asp:BoundField DataField="ISO_IsoNumber" HeaderText="单线图号" />
                            <asp:TemplateField HeaderText="需要热处理">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# ConvertString(Eval("Is_proess")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PTP_TestPackageNo" HeaderText="试压包号" />
                            <asp:BoundField DataField="PTP_TableDate" HeaderText="试压时间" DataFormatString="{0:d}"/>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                        TypeName="BLL.IsoCompreInfoService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>
                            <asp:Parameter Name="workarea" />
                            <asp:Parameter Name="isono" />
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
    if (width > 1500) {
        $("#Table2").width(width);
    }
    var height = parent.document.getElementById("center").offsetHeight;
    var table1Height = $("#Table1").height();
    var hei = height - table1Height - 5;
    $("#div1").height(hei);
</script>
