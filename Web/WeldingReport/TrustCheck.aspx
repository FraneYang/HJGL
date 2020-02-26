<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrustCheck.aspx.cs" Inherits="Web.WeldingReport.TrustCheck" %>


<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>委托检测数据一览表</title>
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
                            &nbsp;委托检测数据一览表
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
                            &nbsp;<asp:DropDownList ID="drpProject" 
                                runat="server" CssClass="textboxStyle" Height="22px" Width="90%"
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
                       
                        <td align="left">
                            <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/Images/Statistics.gif" ValidationGroup="Save"
                                OnClick="btnFind_Click" />
                           <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="导出委托检测数据一览表" OnClick="btnExport_Click" /> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table2" width="100%" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvTrustCheck" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="100" AutoGenerateColumns="False" ShowFooter="true" HorizontalAlign="Justify" Width="100%"
                        DataSourceID="ObjectDataSource1" 
                        ondatabound="gvTrustCheck_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvTrustCheck.PageIndex * gvTrustCheck.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                               <FooterTemplate>
                                  合计:
                               </FooterTemplate>
                               <FooterStyle HorizontalAlign="Right" Height="25px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="bsu_unitcode" HeaderText="单位代号" >
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bsu_unitname" HeaderText="单位名称" >
                            <HeaderStyle Width="18%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="devicename" HeaderText="装置名称" >
                            <HeaderStyle Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WorkAreaCode" HeaderText="工 区" >
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="trust_Audit_total" HeaderText="已审核委托单数" >
                            <HeaderStyle Width="11%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="trust_NoAudit_total" HeaderText="未审核委托单数" >
                            <HeaderStyle Width="11%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="check_Audit_total" HeaderText="已审核检测单数" >
                            <HeaderStyle Width="11%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="check_NoAudit_total" HeaderText="未审核检测单数" >
                            <HeaderStyle Width="11%" />
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
                        TypeName="BLL.TrustCheckService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>
                            <asp:Parameter Name="unitId" />
                            <asp:Parameter Name="workAreaId" />
                            <asp:Parameter Name="projectId" />
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
