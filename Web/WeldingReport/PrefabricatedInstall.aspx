<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrefabricatedInstall.aspx.cs"
    Inherits="Web.WeldingReport.PrefabricatedInstall" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>预制安装进度</title>
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
                            &nbsp;预制安装进度
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
                            <asp:Label ID="Label7" runat="server" Text="项目"></asp:Label>&nbsp;&nbsp;
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
                        <td align="right" height="32px" width="7%">
                            <asp:Label ID="Label2" runat="server" Text="单位"></asp:Label>&nbsp;
                            &nbsp;
                        </td>
                        <td width="20%" align="left">
                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textboxStyle" Height="22px" Width="80%"
                            AutoPostBack="true" 
                                onselectedindexchanged="ddlUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="7%">
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="工作区"></asp:Label>&nbsp;
                        </td>
                        <td width="20%" align="left">
                            <asp:DropDownList ID="ddlWorkArea" runat="server" CssClass="textboxStyle" Height="22px" Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td width="7%" align="right">
                            <asp:Label ID="Label4" runat="server" Text="材质"></asp:Label>&nbsp;
                        </td>
                        <td width="20%" align="left">
                            <asp:DropDownList ID="ddlSteel" runat="server" CssClass="textboxStyle" Height="22px" Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="预制安装进度表" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table2" width="1500px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvPreInstall" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="100" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                        DataSourceID="ObjectDataSource1" OnDataBound="gvPreInstall_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                             <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvPreInstall.PageIndex * gvPreInstall.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="bsu_unitcode" HeaderText="单位代码" />
                            <asp:BoundField DataField="bsu_unitname" HeaderText="单位名称" />
                            <asp:BoundField DataField="baw_areano" HeaderText="施工区域" />
                            <asp:BoundField DataField="ste_stecode" HeaderText="材质代号" />
                            <asp:BoundField DataField="ste_stename" HeaderText="材质名称" />
                            <asp:BoundField DataField="iso_isono" HeaderText="管线号" />
                            <asp:BoundField DataField="max_din" HeaderText="最大尺寸" />
                            <asp:BoundField DataField="total_din" HeaderText="总达因量" />
                            <asp:BoundField DataField="finished_total_din" HeaderText="完成总量" />
                            <asp:BoundField DataField="finisedrate_din" HeaderText="完成进度比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="total_Sdin" HeaderText="预制总量" />
                            <asp:BoundField DataField="finished_total_Sdin" HeaderText="预制完成" />
                            <asp:BoundField DataField="finisedrate_din_s" HeaderText="预制进度比例" DataFormatString="{0:N1}%"/>
                            <asp:BoundField DataField="total_Fdin" HeaderText="安装总量" />
                            <asp:BoundField DataField="finished_total_Fdin" HeaderText="安装完成" />
                            <asp:BoundField DataField="finisedrate_din_f" HeaderText="安装进度比例" DataFormatString="{0:N1}%"/>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                        TypeName="BLL.PrefabricatedInstallService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>
                            <asp:Parameter Name="unitcode" />
                            <asp:Parameter Name="areaNo" />
                            <asp:Parameter Name="steel" />
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