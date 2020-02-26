<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSearch.aspx.cs" Inherits="Web.WeldingManage.ShowSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Search.gif" OnClick="btnSave_Click"
                                EnableTheming="True" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       <tr>
                <td style="width:100%">
                    <table width="100%">
                    <tr id="tr0" runat="server" style="height: 32px">
                            <td align="right" style="width: 25%">
                                <asp:Label ID="Label9" runat="server" Text="管线代号"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 75%">
                        <asp:TextBox ID="txtISO_IsoNo" runat="server" Width="85%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr1" runat="server" style="height: 32px">
                            <td align="right" style="width: 20%">
                                <asp:Label ID="Label3" runat="server" Text="介质"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 80%">
                                   <asp:DropDownList ID="drpSER" runat="server" Height="22" Width="85%"
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="tr2" runat="server" style="height: 32px">
                            <td align="right" style="width: 25%">
                                <asp:Label ID="Label4" runat="server" Text="探伤类型"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 75%">
                             <asp:DropDownList ID="drpNDT" runat="server" Height="22" Width="85%"
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="tr3" runat="server" style="height: 32px">
                            <td align="right" style="width: 25%">
                                <asp:Label ID="Label1" runat="server" Text="单线图号"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 75%">
                              <asp:TextBox ID="txtISO_IsoNumber" runat="server" Width="85%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                            <tr id="tr9" runat="server" style="height: 32px">
                            <td align="right" style="width: 25%">
                                <asp:Label ID="Label10" runat="server" Text="材质"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 75%">
                                                           <asp:DropDownList ID="drpSTE" runat="server" Height="22" Width="85%"
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                            </td>
                        </tr>
                            <tr id="tr10" runat="server" style="height: 32px">
                            <td align="right" style="width: 25%">
                                <asp:Label ID="Label11" runat="server" Text="规格"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 75%">
                              <asp:TextBox ID="txtISO_Specification" runat="server" Width="85%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
    </form>
</body>
</html>
