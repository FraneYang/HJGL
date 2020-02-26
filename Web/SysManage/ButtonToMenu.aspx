<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ButtonToMenu.aspx.cs" Inherits="Web.SysManage.ButtonToMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>按钮权限设置</title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" style="border-bottom-style: solid;
        border-bottom-color: Silver">
        <tr>
            <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 55%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            按钮权限设置&nbsp;
                        </td>
                        <td align="right" valign="middle" style="width: 45%; height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                        <td height="32" align="right" width="10%">
                            <asp:Label ID="Label1" runat="server" Text="模块名称:"></asp:Label>
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="drpMainMenu" runat="server" CssClass="textboxStyle" 
                                 Width="150px" Height="24px" onselectedindexchanged="drpMainMenu_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="drpMainMenu"
                                Display="Dynamic" ErrorMessage="&quot;请选择模块名称！&quot;" ForeColor="Red" ValidationGroup="Save"
                                ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>

                         <td height="32" align="right" width="10%">
                            <asp:Label ID="Label3" runat="server" Text="菜单名称:"></asp:Label>
                        </td>
                        <td align="left" width="40%">
                            <asp:DropDownList ID="ddlMenuName" runat="server" CssClass="textboxStyle" 
                                 Width="200px" Height="24px" onselectedindexchanged="ddlMenuName_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddlMenuName"
                                Display="Dynamic" ErrorMessage="&quot;请选择菜单名称！&quot;" ForeColor="Red" ValidationGroup="Save"
                                ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr><td style=" height:30px"></td></tr>
                    <tr>
                        <td height="32" align="right" width="10%" valign="top">
                            <asp:Label ID="Label2" runat="server" Text="按钮名称"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:CheckBoxList ID="cblButtonName" runat="server" Width="539px" 
                                RepeatColumns="5" RepeatDirection="Horizontal">
                                <asp:ListItem>增加</asp:ListItem>
                                <asp:ListItem>修改</asp:ListItem>
                                <asp:ListItem>删除</asp:ListItem>
                                <asp:ListItem>保存</asp:ListItem>
                                <asp:ListItem>导入</asp:ListItem>
                                <asp:ListItem>导出</asp:ListItem>
                                <asp:ListItem>打印</asp:ListItem>
                                
                                <asp:ListItem>审核</asp:ListItem>
                                <asp:ListItem>取消审核</asp:ListItem>
                                <asp:ListItem>数据库备份</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 7px; position: absolute;
        top: -5px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Save" />
    </form>
</body>
</html>
