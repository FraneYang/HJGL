<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaComprehensive.aspx.cs"
    Inherits="Web.WeldingReport.MediaComprehensive" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>介质综合分析</title>
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
                        <td align="left" valign="middle" style="width: 45%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;介质综合分析
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
                            <asp:Label ID="Label1" runat="server" Text="单位"></asp:Label>&nbsp;
                        </td>
                        <td align="left" width="15%">
                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textboxStyle" Width="80%" Height="22px" 
                            AutoPostBack="true" onselectedindexchanged="ddlUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="工作区"></asp:Label>&nbsp;
                        </td>
                        <td align="left" width="15%" onkeypress="keypress()">
                            <asp:DropDownList ID="ddlWorkArea" runat="server" CssClass="textboxStyle" Width="80%" Height="22px" >
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label3" runat="server" Text="介质"></asp:Label>&nbsp;
                        </td>
                        <td align="left" width="15%">
                            <asp:DropDownList ID="ddlService" runat="server" CssClass="textboxStyle" Width="80%" Height="22px" >
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="25%">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                              <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="介质综合分析表" OnClick="btnExport_Click" /> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table2"  cellpadding="0" cellspacing="0" runat="server" width="3500px">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvMediaCompre" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" HorizontalAlign="Justify"
                        PageSize="100" Width="100%" OnDataBound="gvMediaCompre_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号"  ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvMediaCompre.PageIndex * gvMediaCompre.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="bsu_unitcode" HeaderText="单位代号" />
                            <asp:BoundField DataField="bsu_unitname" HeaderText="单位名称" />
                            <asp:BoundField DataField="baw_areano" HeaderText="工区" />
                            <asp:BoundField DataField="ser_code" HeaderText="介质代号" />
                            <asp:BoundField DataField="ser_name" HeaderText="介质名称" />
                            <asp:BoundField DataField="total_jot" HeaderText="总焊口数" />
                            <asp:BoundField DataField="total_sjot" HeaderText="预制焊口数" />
                            <asp:BoundField DataField="total_fjot" HeaderText="安装焊口数" />
                            <asp:BoundField DataField="finished_jot" HeaderText="完成焊口数" />
                            <asp:BoundField DataField="finished_sjot" HeaderText="预制完成焊口数" />
                            <asp:BoundField DataField="finished_fjot" HeaderText="安装完成焊口数" />
                            <asp:BoundField DataField="cut_jot" HeaderText="切除焊口数" />
                            <asp:BoundField DataField="finisedrate" HeaderText="完成比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="finisedrate_s" HeaderText="预制完成比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="finisedrate_f" HeaderText="安装完成比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="total_din" HeaderText="总达因" />
                            <asp:BoundField DataField="total_sdin" HeaderText="预制总达因" />
                            <asp:BoundField DataField="total_fdin" HeaderText="安装总达因" />
                            <asp:BoundField DataField="finished_din" HeaderText="完成总达因" />
                            <asp:BoundField DataField="finished_sdin" HeaderText="预制完成总达因" />
                            <asp:BoundField DataField="finished_fdin" HeaderText="安装完成总达因" />
                            <asp:BoundField DataField="finishedrate_din" HeaderText="达因完成比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="finishedrate_sdin" HeaderText="达因预制完成比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="finishedrate_fdin" HeaderText="达因安装完成比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="total_film" HeaderText="总拍片数" />
                            <asp:BoundField DataField="pass_film" HeaderText="合格片数" />
                            <asp:BoundField DataField="passfilm_rate" HeaderText="合格率" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="ext_totalfilm" HeaderText="扩透总数" />
                            <asp:BoundField DataField="ext_passfilm" HeaderText="扩透合格总数" />
                            <asp:BoundField DataField="ext_passrate" HeaderText="扩透合格率" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="point_totalfilm" HeaderText="点口总数" />
                            <asp:BoundField DataField="point_passfilm" HeaderText="点口合格总数" />
                            <asp:BoundField DataField="point_passrate" HeaderText="点口合格率" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="cut_totalfilm" HeaderText="切除总数" />
                            <asp:BoundField DataField="cut_passfilm" HeaderText="切除合格总数" />
                            <asp:BoundField DataField="trust_total_jot" HeaderText="委托总数" />
                            <asp:BoundField DataField="trust_ext_total_jot" HeaderText="委托扩透总数" />
                            <asp:BoundField DataField="trust_point_total_jot" HeaderText="委托点口总数" />
                            <asp:BoundField DataField="check_point_total_jot" HeaderText="已探口数" />
                            <asp:BoundField DataField="repair_jot" HeaderText="返修口数" />
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <PagerStyle HorizontalAlign="Left" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetListCount"
                        SelectMethod="GetListData" TypeName="BLL.MediaComprehensiveService" OnSelecting="ObjectDataSource1_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="unitcode" />
                            <asp:Parameter Name="workareacode" />
                            <asp:Parameter Name="sername" />
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
    if (width > 3500) {
        $("#Table2").width(width);
    }
    var height = parent.document.getElementById("center").offsetHeight;
    var table1Height = $("#Table1").height();
    var hei = height - table1Height - 5;
    $("#div1").height(hei);
</script>
