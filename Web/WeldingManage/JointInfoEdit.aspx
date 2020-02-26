<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false"  CodeBehind="JointInfoEdit.aspx.cs" 
    Inherits="Web.WeldingManage.JointInfoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                        <td align="left" valign="middle" style="font-size: 11pt; font-weight: bold" colspan="2">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;<asp:Label runat="server" ID="lblTitle"></asp:Label>
                        </td>
                        <td align="left" valign="middle" style="font-size: 11pt; font-weight: bold">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle" style="font-size: 11pt; font-weight: bold">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle" style="font-size: 11pt; font-weight: bold">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle" style="font-size: 11pt; font-weight: bold">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle" style="font-size: 11pt; font-weight: bold">
                            &nbsp;
                        </td>
                        <td align="right" valign="middle" style="height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="font-size: 10pt; font-weight: bold" width="10%">
                            <asp:Label ID="Label43" runat="server" Text="焊接情况"></asp:Label>&nbsp;
                        </td>
                        <td align="left" valign="middle" width="15%">
                            <asp:TextBox ID="txtReport" runat="server" CssClass="textboxStyle" Enabled="False"
                                Width="90%"></asp:TextBox>
                        </td>
                        <td align="right" valign="middle" style="font-size: 10pt; font-weight: bold" width="10%">
                            <asp:Label ID="Label44" runat="server" Text="点口情况"></asp:Label>&nbsp;
                        </td>
                        <td align="left" valign="middle" width="15%">
                            <asp:TextBox ID="txtPoint" runat="server" CssClass="textboxStyle" Enabled="False"
                                Width="90%"></asp:TextBox>
                        </td>
                        <td align="right" valign="middle" style="font-size: 10pt; font-weight: bold" width="10%">
                            <asp:Label ID="Label45" runat="server" Text="委托情况"></asp:Label>&nbsp;
                        </td>
                        <td align="left" valign="middle" width="15%">
                            <asp:DropDownList ID="ddlTrustFlag" runat="server" CssClass="textboxStyle" Enabled="False"
                                Width="90%" Height="22px">
                                <asp:ListItem Value="00">未下委托</asp:ListItem>
                                <asp:ListItem Value="01">一次委托,未审核</asp:ListItem>
                                <asp:ListItem Value="02">一次委托,已审核</asp:ListItem>
                                <asp:ListItem Value="11">二次委托,未审核</asp:ListItem>
                                <asp:ListItem Value="12">二次委托,已审核</asp:ListItem>
                                <asp:ListItem Value="21">三次委托,未审核</asp:ListItem>
                                <asp:ListItem Value="22">三次委托,已审核</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right" valign="middle" style="font-size: 10pt; font-weight: bold" width="10%">
                            <asp:Label ID="Label46" runat="server" Text="探伤标志"></asp:Label>&nbsp;
                        </td>
                        <td align="left" valign="middle" style="height: 30px;" width="15%">
                            <asp:DropDownList ID="ddlCheckFlag" runat="server" CssClass="textboxStyle" Enabled="False"
                                Width="90%" Height="22px">
                                <asp:ListItem Value="00">未检测</asp:ListItem>
                                <asp:ListItem Value="01">一次检测,未审核</asp:ListItem>
                                <asp:ListItem Value="02">一次检测,已审核</asp:ListItem>
                                <asp:ListItem Value="11">二次检测,未审核</asp:ListItem>
                                <asp:ListItem Value="12">二次检测,已审核</asp:ListItem>
                                <asp:ListItem Value="21">三次检测,未审核</asp:ListItem>
                                <asp:ListItem Value="22">三次检测,已审核</asp:ListItem>
                            </asp:DropDownList>
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
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Text="施工区域"></asp:Label>&nbsp;
                        </td>
                        <td align="left" width="25%">
                            <asp:TextBox ID="txtWorkAreaId" runat="server" CssClass="textboxStyle" ReadOnly="True"
                                Width="90%"></asp:TextBox>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="管线号"></asp:Label>&nbsp;
                        </td>
                        <td align="left" width="25%">
                            <asp:TextBox ID="txtISONO" runat="server" CssClass="textboxStyle" Width="90%" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label3" runat="server" Text="焊口代号" Font-Bold="true"></asp:Label>
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJointNo" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJointNo"
                                Display="Dynamic" ErrorMessage="&quot;请输入焊口代号！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            &nbsp;
                            <asp:Label ID="Label4" runat="server" Text="焊接区域"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlWLO_CODE" runat="server" CssClass="textboxnoneborder" Width="90%" Height="22px">
                               <asp:ListItem Value="F">安装</asp:ListItem>
                               <asp:ListItem Value="S">预制</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="材质1" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSTE1" runat="server" CssClass="textboxStyle" ValidationGroup="Save"
                                Width="90%"  Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddlSTE1"
                                Display="Dynamic" ErrorMessage="&quot;请选择材质1！&quot;" ForeColor="Red" ValidationGroup="Save"
                                ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                        <td align="right">
                             <asp:Label ID="Label6" runat="server" Text="材质2"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSTE2" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            &nbsp;
                            <asp:Label ID="Label7" runat="server" Text="所属管段"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBelongPipe" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="组件1号"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlComponent1" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="组件2号"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlComponent2" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label10" runat="server" Text="焊口规格" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJointDesc" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtJointDesc"
                                Display="Dynamic" ErrorMessage="&quot;请输入焊口规格！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="炉批号1"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtHeartNo1" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Text="炉批号2"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtHeartNo2" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label13" runat="server" Text="焊缝类型" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlJOTY_ID" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="CheckDropDownList"
                                ControlToValidate="ddlJOTY_ID" Display="Dynamic" ErrorMessage="&quot;请选择焊缝类型！&quot;"
                                ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="寸径" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtSize" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSize"
                                Display="Dynamic" ErrorMessage="&quot;请输入寸径！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label15" runat="server" Text="外径" ></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtDia" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label16" runat="server" Text="坡口类型" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlJST_ID" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="CheckDropDownList"
                                ControlToValidate="ddlJST_ID" Display="Dynamic" ErrorMessage="&quot;请选择坡口类型！&quot;"
                                ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label17" runat="server" Text="壁厚" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtSch" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSch"
                                Display="Dynamic" ErrorMessage="&quot;请输入壁厚！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label18" runat="server" Text="实际壁厚"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtFactSch" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label19" runat="server" Text="后热温度"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeydown="keypress()">
                            <asp:TextBox ID="txtLastTemp" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label20" runat="server" Text="层间温度"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtCellTemp" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label21" runat="server" Text="预热温度"></asp:Label>&nbsp;
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtPrepareTemp" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label22" runat="server" Text="焊口属性" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlJointAttribute" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                                <asp:ListItem>活动</asp:ListItem>
                                <asp:ListItem>固定</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="CheckDropDownList"
                                ControlToValidate="ddlJointAttribute" Display="Dynamic" ErrorMessage="&quot;请选择焊口属性！&quot;"
                                ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label23" runat="server" Text="焊接方法" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlWME_ID" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="CheckDropDownList"
                                ControlToValidate="ddlWME_ID" Display="Dynamic" ErrorMessage="&quot;请选择焊接方法！&quot;"
                                ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label24" runat="server" Text="焊丝"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlWeldSilk" runat="server" CssClass="textboxStyle" Width="90%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style1" height="32px">
                            <asp:Label ID="Label25" runat="server" Text="焊条"></asp:Label>&nbsp;
                        </td>
                        <td align="left" class="style1">
                            <asp:DropDownList ID="ddlWeldMat" runat="server" CssClass="textboxStyle" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" class="style1">
                            <asp:Label ID="Label26" runat="server" Text="焊接电流"></asp:Label>&nbsp;
                        </td>
                        <td align="left" class="style1">
                            <asp:TextBox ID="txtElectricity" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                        <td align="right" class="style1">
                            <asp:Label ID="Label27" runat="server" Text="焊接电压"></asp:Label>&nbsp;
                        </td>
                        <td align="left" class="style1">
                            <asp:TextBox ID="txtVoltage" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label36" runat="server" Text="日报编号"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlReportCode" runat="server" CssClass="textboxStyle" Enabled="False"
                                Width="90%"  Height="22px">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label37" runat="server" Text="日报日期"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <input id="txtReportDate" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                readonly="readonly" style="width: 90%; cursor: hand" disabled="disabled" />
                        </td>
                        <td align="right">
                            <asp:Label ID="Label38" runat="server" Text="完成达因"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDoneDia" runat="server" CssClass="textboxStyle" Enabled="False"
                                Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label39" runat="server" Text="打底焊工"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtFloorWelder" ReadOnly="true" CssClass="textboxStyle"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label40" runat="server" Text="焊工名称"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                           <asp:TextBox runat="server" ID="txtFloorWelderName" ReadOnly="true" CssClass="textboxStyle"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;
                            <asp:Label ID="Label47" runat="server" Text="焊口状态"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlJointStatus" runat="server" CssClass="textboxStyle" Enabled="False"
                                Width="90%"  Height="22px">
                                <asp:ListItem Value="100">正常</asp:ListItem>
                                <asp:ListItem Value="102">扩透</asp:ListItem>
                                <asp:ListItem Value="101">点口</asp:ListItem>
                                <asp:ListItem Value="104">已切除</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label41" runat="server" Text="盖面焊工"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                           <asp:TextBox runat="server" ID="txtCellWelder" ReadOnly="true" CssClass="textboxStyle"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label42" runat="server" Text="焊工名称"></asp:Label>&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtCellWelderName" ReadOnly="true" CssClass="textboxStyle"></asp:TextBox>
                        </td>
                        <td align="right">
                            是否需热处理&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpIS_Proess" runat="server" CssClass="textboxStyle" Width="90%"  Height="22px">
                                <asp:ListItem Value="0">否</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label35" runat="server" Text="备注"></asp:Label>&nbsp;
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px; " runat="server" HeaderText="请注意！" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Save" />
    </form>
</body>
</html>
