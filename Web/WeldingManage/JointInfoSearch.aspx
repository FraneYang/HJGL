<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointInfoSearch.aspx.cs"
    Inherits="Web.WeldingManage.JointInfoSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置焊口查询条件</title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript"> 
　　        function ShowWorkStageClose(result) {
            window.returnValue = result;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" runat="server" width="100%" cellpadding="1" cellspacing="1">
            <tr>
                <td style="width: 100%; background: url('../Images/bg-1.gif')">
                    <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 45%; font-size: 11pt; font-weight: bold">
                                <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image1" runat="server" />
                                &nbsp;设置查询条件
                            </td>
                            <td align="right" valign="middle" style="width: 55%; height: 30px;">
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Search.gif" EnableTheming="True"
                                    OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" height="32px" width="30%">
                                &nbsp;
                                <asp:Label ID="Label1" runat="server" Text="焊口代号"></asp:Label>
                            </td>
                            <td align="left" width="70%">
                                &nbsp;<asp:TextBox ID="txtJOTNO" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="32px">
                                &nbsp;
                                <asp:Label ID="Label2" runat="server" Text="管线号"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtISOID" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="32px">
                                &nbsp;
                                <asp:Label ID="Label3" runat="server" Text="焊接区域"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlWLOCODE" runat="server" CssClass="textboxStyle" Width="80%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="32px">
                                <asp:Label ID="Label4" runat="server" Text="焊口规格"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtJointDesc" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="32px">
                                <asp:Label ID="Label5" runat="server" Text="焊缝类型"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlJOTYID" runat="server" CssClass="textboxStyle" Width="80%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="32px">
                                <asp:Label ID="Label6" runat="server" Text="焊接方法"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlWMEID" runat="server" CssClass="textboxStyle" Width="80%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                             <tr>
                            <td align="right" height="32px">
                                <asp:Label ID="Label7" runat="server" Text="是否焊接"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpDReportID" runat="server" CssClass="textboxStyle" Width="80%">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="2">否</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                          <tr>
                            <td align="right" height="32px">
                                <asp:Label ID="Label8" runat="server" Text="点口扩透否"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpPW_PointID" runat="server" CssClass="textboxStyle" Width="80%">
                                      <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="2">否</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
