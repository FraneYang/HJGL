<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcedureImageEdit.aspx.cs"
    Inherits="Web.WeldingManage.ProcedureImageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ValidateGroupControl.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function keypress() {
            var keyASCII = event.keyCode;
            if ((keyASCII >= 48 && keyASCII <= 57) || keyASCII == 46) {

            }
            else {
                event.keyCode = 0;
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="85%" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 50%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;编辑图片信息
                        </td>
                        <td align="right" valign="middle" style="width: 50%; height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btnReturn" runat="server" ImageUrl="~/Images/Return.gif" OnClick="btnReturn_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr style="height: 35px">
                        <td align="right" style="width: 15%">
                            <asp:Label ID="Label2" runat="server" Text="图片名称"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtImageContent" runat="server" Width="80%" Rows="1" CssClass="textboxStyle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtImageContent"
                                Display="Dynamic" ErrorMessage="&quot;内容说明不能为空！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="width: 15%">
                            &nbsp;<asp:Label ID="Label11" runat="server" Text="焊接方法"></asp:Label>
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpWeldingMethod" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td align="right" onkeypress="keypress()" style="width: 15%">
                            <asp:Label ID="Label12" runat="server" Text="厚度"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtThikness" runat="server" Width="80%" Rows="1" CssClass="textboxStyle"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 15%">
                            <asp:Label ID="Label13" runat="server" Text="焊缝类型"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpJOTYID" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td align="right" style="width: 15%">
                            <asp:Label ID="Label14" runat="server" Text="坡口类型"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpJSTID" runat="server" CssClass="textboxStyle" Height="22px"
                                Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 15%">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td align="right" style="width: 15%">
                            <asp:Label ID="Label7" runat="server" Text="附件上传"></asp:Label>&nbsp;
                        </td>
                        <td align="left" style="width: 35%">
                            <asp:FileUpload ID="fuAttachUrl" runat="server" Width="90%" />
                        </td>
                        <td align="right" style="width: 15%">
                            <asp:Label ID="Label10" runat="server" Text="原附件查看："></asp:Label>
                        </td>
                        <td align="left" style="width: 35%">
                            <asp:Label ID="lblAttachUrl" runat="server"></asp:Label>
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
