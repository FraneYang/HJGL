<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointInfoBatchEdit.aspx.cs"
    Inherits="Web.WeldingManage.JointInfoBatchEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批量增加焊口信息</title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function keypress() {
            var keyASCII = event.keyCode;
            if ((keyASCII >= 48 && keyASCII <= 57) || keyASCII == 46) {

            }
            else {
                event.keyCode = 0;
            }
        }

        function WindowClose(result) {
            window.returnValue = result;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;<asp:Label runat="server" ID="lblTitle">批量增加焊口</asp:Label>
                        </td>
                        
                        <td align="right" valign="middle" style="height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td align="right" height="32px" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="焊口号"></asp:Label>&nbsp;
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtJointNo" runat="server" CssClass="textboxStyle" Width="8%" ToolTip="焊口前固定字符"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtJointNo1" runat="server" CssClass="textboxStyle" 
                                Width="10%" onkeypress="keypress()"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJointNo1"
                                Display="Dynamic" ErrorMessage="&quot;请输入焊口号1！&quot;" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            &nbsp;<asp:Label ID="Label3" runat="server" Text="到"></asp:Label>
                            &nbsp;<asp:TextBox ID="txtJointNo2" runat="server" CssClass="textboxStyle" 
                                Width="10%" onkeypress="keypress()"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtJointNo2"
                                Display="Dynamic" ErrorMessage="&quot;请输入焊口号2!&quot;" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label1" runat="server" Text="施工区域"></asp:Label>&nbsp;
                        </td>
                        <td align="left" width="40%">
                            <asp:TextBox ID="txtWorkAreaId" runat="server" CssClass="textboxStyle" ReadOnly="True"
                                Width="80%"></asp:TextBox>
                        </td>
                        <td width="10%" align="right">
                            <asp:Label ID="Label4" runat="server" Text="焊接区域"></asp:Label>  &nbsp;
                        </td>
                        <td width="40%" align="left">
                            <asp:DropDownList ID="ddlWLO" runat="server" CssClass="textboxStyle" Width="80%"  Height="22px">
                               <asp:ListItem Value="F">安装</asp:ListItem>
                               <asp:ListItem Value="S">预制</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label5" runat="server" Text="材质1"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSTE1" runat="server" CssClass="textboxStyle" Width="80%"  Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="CheckDropDownList"
                                ControlToValidate="ddlSTE1" Display="Dynamic" ErrorMessage="&quot;请选择材质1！&quot;"
                                ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="材质2"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSTE2" runat="server" CssClass="textboxStyle" Width="80%"  Height="22px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label7" runat="server" Text="焊口规格"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJointDesc" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtJointDesc"
                                Display="Dynamic" ErrorMessage="&quot;请输入焊口规格！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="焊缝类型"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlJOTY_ID" runat="server" CssClass="textboxStyle" Width="80%"  Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="CheckDropDownList"
                                ControlToValidate="ddlJOTY_ID" Display="Dynamic" ErrorMessage="&quot;请选择焊缝类型！&quot;"
                                ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label9" runat="server" Text="寸径"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtSize" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSize"
                                Display="Dynamic" ErrorMessage="&quot;请输入尺寸！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label10" runat="server" Text="外径"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtDia" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDia"
                                Display="Dynamic" ErrorMessage="&quot;请输入外径！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label11" runat="server" Text="焊口属性"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlJointAttribute" runat="server" CssClass="textboxStyle" Width="80%"  Height="22px">
                                <asp:ListItem>活动</asp:ListItem>
                                <asp:ListItem>固定</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label23" runat="server" Text="焊接方法"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlWME_ID" runat="server" CssClass="textboxStyle" Width="80%"  Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="CheckDropDownList"
                                ControlToValidate="ddlWME_ID" Display="Dynamic" ErrorMessage="&quot;请选择焊接方法！&quot;"
                                ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                       <td align="right">
                            <asp:Label ID="Label24" runat="server" Text="焊丝"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlWeldSilk" runat="server" CssClass="textboxStyle" Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" class="style1" height="32px">
                            <asp:Label ID="Label25" runat="server" Text="焊条"></asp:Label>&nbsp;
                        </td>
                        <td align="left" class="style1">
                            <asp:DropDownList ID="ddlWeldMat" runat="server" CssClass="textboxStyle" Width="80%">
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Text="壁厚"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtSch" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSch"
                                Display="Dynamic" ErrorMessage="&quot;请输入壁厚！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>

                         <td align="right">
                            是否需热处理&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpIS_Proess" runat="server" CssClass="textboxStyle" Width="80%"  Height="22px">
                                <asp:ListItem Value="0">否</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px; right: 750px;" runat="server" HeaderText="请注意！" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Save" />
    </form>
</body>
</html>
