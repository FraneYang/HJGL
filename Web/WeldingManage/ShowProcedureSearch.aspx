<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowProcedureSearch.aspx.cs"
    Inherits="Web.WeldingManage.ShowProcedureSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工艺编号查询</title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
     <script language="JavaScript" type="text/javascript"> 
　　         function CloseWindows(result) {
             window.returnValue = result;
             window.close();
         }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%">
        <tr>
            <td align="left" height="120px" valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td height="40px" width="50%">
                            <asp:CheckBox ID="cbMaterailType" runat="server" Text="母材类别" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbGroupType" runat="server" Text="组别" />
                        </td>
                    </tr>
                    <tr>
                        <td height="40px">
                            <asp:CheckBox ID="cbWeldingMethod" runat="server" Text="焊接方法" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbJointMaterial" runat="server" Text="焊口材料" />
                        </td>
                    </tr>
                    <tr>
                        <td height="40px">
                            <asp:CheckBox ID="cbIsHot" runat="server" Text="热处理" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbSch" runat="server" Text="壁厚" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" height="40px">
                <asp:ImageButton ID="btnConfirm" runat="server" Height="20px" ImageUrl="~/Images/confirm.gif"
                    OnClick="btnConfirm_Click" />
            </td>
        </tr>
        <tr>
            <td align="left" height="40px">
                <asp:Label ID="Label1" runat="server" Text="工艺编号："></asp:Label>
                <asp:DropDownList ID="drpProcedure" runat="server" CssClass="textboxStyle" Height="22px"
                    Width="60%">
                </asp:DropDownList>
                <asp:ImageButton ID="btnSave" runat="server" Height="20px" ImageUrl="~/Images/confirm.gif"
                    OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
