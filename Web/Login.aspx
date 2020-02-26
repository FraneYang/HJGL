<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>诺必达管道焊接管理系统登录窗口</title>
    <script type="text/javascript" language="javascript">
        function CheckDropDownList(source, args) {
            var user = document.getElementById("UserName").value;
            if (user != "gly") {
                if (args.Value == "0")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            //            if (args.Value == "0")
            //                args.IsValid = false;
            //            else
            //                args.IsValid = true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="ImageButton1" defaultfocus="UserName">
    <table style="width: 100%; height: 100%; position: fixed" id="LogOnTable" runat="server"
        border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="middle" align="center" style="width: 100%; height: 100%;">
                <table id="table1" runat="server" style="background-image: url('Images/LoginHSSE.jpg');
                    height: 540px; width: 798px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right" style="height: 30px; width: 90%; font-weight: bold;">
                            <asp:LinkButton ID="lbtnRegedit" runat="server" Text="软件注册" Font-Underline="False"
                                OnClick="lbtnRegedit_Click" ForeColor="White"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" valign="middle" style="height: 140px;">
                            <asp:Label ID="Label1" Font-Size="36pt" runat="server" Text="诺必达管道焊接管理系统V3.0" Font-Bold="True" Font-Names="微软雅黑"
                                ForeColor="#000040"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 120px">
                        <td style="height: 120px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                        </td>
                        <td class="LoginFontSize" style="width: 60px">
                            用户:
                        </td>
                        <td align="left" style="width: 250px">
                            <input type="text" id="UserName" runat="server" style="width: 120px" />
                        </td>
                        <td style="width: 250px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                        </td>
                        <td style="width: 60px">
                            密码:
                        </td>
                        <td align="left" style="width: 250px">
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Style="width: 120px" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 10px;">
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 75px">
                        </td>
                        <td style="width: 60px">
                            项目:
                        </td>
                        <td align="left" colspan="2" style="width: 250px">
                            <asp:DropDownList ID="drpProject" runat="server" Height="22" Width="400px" CssClass="textboxStyle">
                            </asp:DropDownList>
                            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择项目&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpProject" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                        </td>
                        <td style="width: 60px">
                        </td>
                        <td style="font-size: 9pt; height:25px" align="left">
                            <input type="checkbox" id="savemessgae" runat="server" />保存用户名
                        </td>
                        <td style="width: 350px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style=" height:20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                        </td>
                        <td style="width: 60px">
                        </td>
                        <td colspan="2" align="left">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/dlu1.gif" runat="server" OnClick="ImageButton1_Click"
                                ValidationGroup="Save" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/dlu.gif" runat="server" OnClick="ImageButton2_Click" />
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px">
                        </td>
                        <td style="font-size: 10pt; height: 30px" colspan="2">
                            推荐分辨率：1440 * 900
                        </td>
                        <td align="left" style="width: 350px; font-size: 10pt;">
                            请使用IE6.0以上版本浏览器
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; position: absolute;
        top: 8px; right: 824px;" runat="server" ValidationGroup="Save" HeaderText="请注意！"
        ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
    </form>
</body>
</html>
