<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsoCmprehensive.aspx.cs"
    Inherits="Web.WeldingReport.IsoCmprehensive" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管线综合分析</title>
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
                            &nbsp;管线综合分析
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
                            <asp:DropDownList ID="drpProject" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%" AutoPostBack="true" OnSelectedIndexChanged="drpProject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" height="32px">
                        </td>
                        <td align="left">
                        </td>
                        <td align="center" colspan="4">
                        </td>
                    </tr>
                    <tr style="height:32px">
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Text="单位"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="管线"></asp:Label>&nbsp;
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtIsoNo" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                    <tr style="height:32px">
                        <td align="right" style="width:10%">
                            <asp:Label ID="Label4" runat="server" Text="工作区"></asp:Label>&nbsp;
                        </td>
                        <td align="left" style="width:20%">
                            <asp:DropDownList ID="ddlWorkArea" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width:10%">
                            <asp:Label ID="Label5" runat="server" Text="材质"></asp:Label>&nbsp;
                        </td>
                        <td align="left" style="width:20%">
                            <asp:DropDownList ID="ddlSteel" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                         <td align="right" style="width:10%">
                            <asp:Label ID="Label1" runat="server" Text="焊缝类型"></asp:Label>&nbsp;
                        </td>
                        <td align="left" style="width:20%">
                            <asp:DropDownList ID="drpJotType" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width:10%">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif" ToolTip="管线综合分析表"
                                OnClick="btnExport_Click" />
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
                            <asp:BoundField DataField="bsu_unitcode" HeaderText="单位代码">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bsu_unitname" HeaderText="单位名称">
                                <HeaderStyle Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="baw_areano" HeaderText="工区代号">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iso_isono" HeaderText="管线号">
                                <HeaderStyle Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="maxdate" HeaderText="最近焊期">
                                <HeaderStyle Width="3%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_jot" HeaderText="总焊口">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_sjot" HeaderText="预制总焊口">
                                <HeaderStyle Width="3%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_fjot" HeaderText="安装总焊口">
                                <HeaderStyle Width="3%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finished_total_jot" HeaderText="完成焊口">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finished_total_sjot" HeaderText="预制完成焊口">
                                <HeaderStyle Width="3%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finished_total_fjot" HeaderText="安装完成焊口">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cut_total_jot" HeaderText="切除焊口">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finisedrate" HeaderText="完成比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finisedrate_s" HeaderText="预制完成比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finisedrate_f" HeaderText="安装完成比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_din" HeaderText="总达因">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_Sdin" HeaderText="预制达因">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_Fdin" HeaderText="安装达因">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finished_total_din" HeaderText="完成总达因">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finished_total_Sdin" HeaderText="预制完成总达因">
                                <HeaderStyle Width="3%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finished_total_Fdin" HeaderText="安装完成总达因">
                                <HeaderStyle Width="3%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finisedrate_din" HeaderText="完成比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finisedrate_din_s" HeaderText="预制完成比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finisedrate_din_f" HeaderText="安装完成比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_film" HeaderText="总拍片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pass_film" HeaderText="合格片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="passreate" HeaderText="合格率" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ext_total_film" HeaderText="扩透总片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ext_pass_film" HeaderText="扩透合格片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ext_passreate" HeaderText="扩透合格率" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="point_total_film" HeaderText="点口总片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="point_pass_film" HeaderText="点口合格片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="point_passreate" HeaderText="点口合格率" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cut_total_film" HeaderText="切除总片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cut_pass_film" HeaderText="切除合格片数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ext_jot" HeaderText="扩透数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="point_jot" HeaderText="点口数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="trust_total_jot" HeaderText="委托数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="check_total_jot" HeaderText="已探口数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_repairjot" HeaderText="返口数">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="source_rate" HeaderText="检测比例">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="trustrate" HeaderText="委托比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="checkrate" HeaderText="已探比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FixedCheckRate" HeaderText="固定口检测比例" DataFormatString="{0:N1}%">
                                <HeaderStyle Width="2%" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                        TypeName="BLL.IsoCmprehensiveService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>
                            <asp:Parameter Name="unitNo" />
                            <asp:Parameter Name="isoNo" />
                            <asp:Parameter Name="areaNo" />
                            <asp:Parameter Name="steel" />
                            <asp:Parameter Name="projectId" />
                            <asp:Parameter Name="flag" />
                            <asp:Parameter Name="supervisorUnitId" />
                            <asp:Parameter Name="jotTypeId" />
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
