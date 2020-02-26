<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysSet.aspx.cs" Inherits="Web.SysManage.SysSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
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
                            系统环境设置&nbsp;
                        </td>
                        <td align="right" valign="middle" style="width: 45%; height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 35px">
            <td width="15%">焊接日报编号</td>
            <td align="left" style="width: 35%">
                <asp:CheckBox ID="ckbDayReport" runat="server"  Text="是否自动生成"/>
            </td>
        </tr>
        <tr style="height: 35px">
            <td width="15%">是否允许焊接日报导入</td>
            <td align="left" style="width: 35%">
                <asp:CheckBox ID="ckbDayReportDataIn" runat="server"  Text="是"/>
            </td>
        </tr>
         <tr style="height: 35px">
            <td width="15%">点口编号</td>
            <td align="left" style="width: 35%">
                <asp:CheckBox ID="ckbPoint" runat="server"  Text="是否自动生成"/>
            </td>
        </tr>
        <tr style="height: 35px">
            <td width="15%">无损检测委托单</td>
            <td align="left" style="width: 35%">
                <asp:RadioButtonList runat="server" ID="robStandard" 
                    RepeatDirection="Horizontal">
                   <asp:ListItem Value="1">3543-G401(1)</asp:ListItem>
                   <asp:ListItem Value="2">3543-G401(2)</asp:ListItem>
                   <asp:ListItem Value="3">第三方委托单</asp:ListItem>
                   <asp:ListItem Value="4">第三方委托单(神化)</asp:ListItem>
                </asp:RadioButtonList> 
            </td>   
        </tr>
         <tr style="height: 35px">
            <td width="15%">无损检测委托单对应管线</td>
            <td align="left" style="width: 35%">
                <asp:CheckBox ID="ckbTrust" runat="server"  Text="一个无损检测委托单是否只对应一条管线"/>
            </td>
        </tr>
        <tr style="height: 35px">
             <td></td>
             <td align="left">
               说明:(1)需监理和总包签字；(2)需总包、监理、施工、检测单位签字
            </td>
        </tr>
         <tr style="height: 35px">
            <td width="15%">是否监理版本</td>
            <td align="left" style="width: 35%">
                <asp:CheckBox ID="ckbSupervisor" runat="server"  Text="是"/>
            </td>
        </tr>
         <tr style="height: 35px">
            <td width="15%">检测单拍片数是否需自动生成</td>
            <td align="left" style="width: 35%">
                <asp:CheckBox ID="ckbFilmNum" runat="server"  Text="是" />
            </td>
        </tr>
          <tr style="height: 35px">
             <td></td>
             <td align="left">
               直径90以下 2张片（法兰3张）;90~477 6张片;477以上按照直径*3.14/250来算，如果有小数，保留整数部分+1取整。如果取整后是奇数，再+1。
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
