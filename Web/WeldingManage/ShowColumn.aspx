<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowColumn.aspx.cs" Inherits="Web.WeldingManage.ShowColumn" %>

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
                            &nbsp;选择显示列
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                        <asp:CheckBox runat="server" ID="ckAll" Text="全选" AutoPostBack="true" 
                                oncheckedchanged="ckAll_CheckedChanged" />&nbsp;
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/Select.jpg" OnClick="btnSave_Click"
                                EnableTheming="True" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%" align="left">
                <div id="div1" style="width: 100%; height:250px; overflow: auto;" runat="server">
                <asp:CheckBoxList ID="chblColumn" runat="server" Width="95%" 
                CellSpacing="3" DataTextField="Text" 
                DataValueField="Value">
               </asp:CheckBoxList>
               </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
