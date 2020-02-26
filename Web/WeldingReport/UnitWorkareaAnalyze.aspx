<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWorkareaAnalyze.aspx.cs"
    Inherits="Web.WeldingReport.UnitWorkareaAnalyze" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位工区进度分析</title>
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
                            &nbsp;单位工区进度分析
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
                                Height="22px" Width="80%" AutoPostBack="true" 
                                onselectedindexchanged="drpProject_SelectedIndexChanged">
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
                            <asp:Label ID="Label1" runat="server" Text="日期"></asp:Label>&nbsp;
                        </td>
                        <td width="25%" align="left">
                            <input id="txtdate1" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                            <asp:Label ID="Label6" runat="server" Text="至"></asp:Label>
                            &nbsp;<input id="txtdate2" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="单位"></asp:Label>&nbsp;
                        </td>
                        <td width="20%" align="left">
                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textboxStyle" 
                                Height="22px" Width="80%" AutoPostBack="true" 
                                onselectedindexchanged="ddlUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" height="32px" width="10%">
                            <asp:Label ID="Label3" runat="server" Text="装置"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                              <asp:DropDownList ID="ddlInstalcode" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="80%">
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="工作区"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlWorkarea" runat="server" CssClass="textboxStyle" Height="22px" Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" height="32px">
                            <asp:Label ID="Label5" runat="server" Text="钢材类型"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSteType" runat="server" CssClass="textboxStyle" Height="22px" Width="80%">
                                <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                <asp:ListItem Value="1">碳钢</asp:ListItem>
                                <asp:ListItem Value="2">不锈钢</asp:ListItem>
                                <asp:ListItem Value="3">鉻钼钢</asp:ListItem>
                                <asp:ListItem Value="4">其他</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td align="center" colspan="2">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                             <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="单位工区进度分析表" OnClick="btnExport_Click" /> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table8" width="2800px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvUnitAreaAnalyze" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="100" AutoGenerateColumns="False" HorizontalAlign="Justify" ShowFooter="True" Width="100%"
                        OnDataBound="gvUnitAreaAnalyze_DataBound" DataSourceID="ObjectDataSource1">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvUnitAreaAnalyze.PageIndex * gvUnitAreaAnalyze.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                <FooterTemplate>
                                    合计:
                                </FooterTemplate>
                                <FooterStyle Width="50px" Height="25px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="bsu_unitname" HeaderText="单位名称" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="InstallationName" HeaderText="装置" ItemStyle-Width="120px" />
                            <asp:BoundField DataField="baw_areano" HeaderText="工区" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="total_jot" HeaderText="总焊口" ItemStyle-Width="80px" />
                            <asp:BoundField DataField="total_sjot" HeaderText="预制总焊口" ItemStyle-Width="80px"/>
                            <asp:BoundField DataField="total_fjot" HeaderText="安装总焊口" ItemStyle-Width="80px"/>
                            <asp:BoundField DataField="cut_total_jot" HeaderText="切除焊口" ItemStyle-Width="80px"/>
                            <asp:BoundField DataField="total_din" HeaderText="总达因" ItemStyle-Width="60px"/>
                            <asp:BoundField DataField="total_Sdin" HeaderText="预制总达因" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="total_Fdin" HeaderText="安装总达因" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finished_total_jot_bq" HeaderText="本期完成焊口数" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finished_total_sjot_bq" HeaderText="本期完成预制焊口数" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finished_total_fjot_bq" HeaderText="本期完成安装焊口数" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finisedrate_bq" HeaderText="本期完成比例" DataFormatString="{0:N1}%" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finisedrate_s_bq" HeaderText="本期预制完成比例" DataFormatString="{0:N1}%" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finisedrate_f_bq" HeaderText="本期安装完成比例"  DataFormatString="{0:N1}%" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finished_total_din_bq" HeaderText="本期完成达因" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finished_total_Sdin_bq" HeaderText="本期完成预制达因" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finished_total_Fdin_bq" HeaderText="本期完成安装达因" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finisedrate_din_bq" HeaderText="本期完成达因比例"  DataFormatString="{0:N1}%" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finisedrate_din_s_bq" HeaderText="本期完成预制达因比例"  DataFormatString="{0:N1}%" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finisedrate_din_f_bq" HeaderText="本期完成安装达因比例"  DataFormatString="{0:N1}%" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finished_total_jot" HeaderText="完成焊口" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finished_total_sjot" HeaderText="完成预制焊口" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finished_total_fjot" HeaderText="完成安装焊口" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finisedrate" HeaderText="完成比例"  DataFormatString="{0:N1}%" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finisedrate_f" HeaderText="安装完成比例"  DataFormatString="{0:N1}%" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finisedrate_s" HeaderText="预制完成比例"  DataFormatString="{0:N1}%" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finished_total_din" HeaderText="完成达因" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finished_total_sdin" HeaderText="完成预制达因" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finished_total_Fdin" HeaderText="完成安装达因" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="finisedrate_din" HeaderText="完成达因比例"  DataFormatString="{0:N1}%" ItemStyle-Width="80px"/>
                            <asp:BoundField DataField="finisedrate_din_s" HeaderText="完成预制达因比例"  DataFormatString="{0:N1}%" ItemStyle-Width="90px"/>
                            <asp:BoundField DataField="finisedrate_din_f" HeaderText="完成安装达因比例"  DataFormatString="{0:N1}%" ItemStyle-Width="90px"/>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                        TypeName="BLL.UnitWorkareaAnalyzeService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>
                            <asp:Parameter Name="unitNo" />
                            <asp:Parameter Name="areaNo" />
                            <asp:Parameter Name="installationId" />
                            <asp:Parameter Name="ste_steeltype" />
                            <asp:Parameter Name="startTime" Type="DateTime" />
                            <asp:Parameter Name="endTime" Type="DateTime" />
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
        $("#Table8").width(width);
    }
    var height = parent.document.getElementById("center").offsetHeight;
    var table1Height = $("#Table1").height();
    var hei = height - table1Height - 5;
    $("#div1").height(hei);
</script>
